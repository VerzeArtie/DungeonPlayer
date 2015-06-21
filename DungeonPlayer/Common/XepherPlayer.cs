using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//using SlimDX;
//using SlimDX.XAudio2;
//using SlimDX.Multimedia;
using System.Threading;
using System.IO;
using SharpDX;
using SharpDX.Multimedia;
using SharpDX.XAudio2;
using SharpDX.MediaFoundation;


namespace DungeonPlayer
{
    public partial class XepherPlayer
    {
        //[System.Runtime.InteropServices.DllImport("winmm.dll",
        //    CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //private static extern int mciSendString(string command,
        //   System.Text.StringBuilder buffer, int bufferSize, IntPtr hwndCallback);

        //private string aliasName = "MediaFile";
        //string[] fileName;

        // Declare the nofify constant
        //public const int MM_MCINOTIFY = 953;

        // XAudioデバイス
        XAudio2 device = null;
        MasteringVoice masteringVoice = null;

        // 音楽再生用
        System.Threading.Thread th2;
        bool endFlag2 = false;
        string fileName2 = string.Empty;
        string fileName2_2 = string.Empty; // 初回ループが終わった後、次のミュージックファイルに移るためのファイル名
        bool changeToSecondMusic = false; // 初回ループが終わったのを検知して、次のミュージックに移るためのフラグ
        bool stopSoundFlag2 = false;
        int loopBeginTime = 0;
        bool MuteFlag = false; // 一時消音

        // 効果音再生用
        System.Threading.Thread th;
        bool endFlag = false;
        string fileName = string.Empty;
        bool stopSoundFlag = false;

        public XepherPlayer()
        {
            this.device = new XAudio2();


            //fileName = System.IO.Directory.GetFiles(Database.BaseSoundFolder);
            this.masteringVoice = new MasteringVoice(device);

            th = new Thread(new System.Threading.ThreadStart(UpdateSound));
            th.IsBackground = true;
            th.Start();

            th2 = new Thread(new System.Threading.ThreadStart(UpdateMusic));
            th2.Priority = ThreadPriority.Highest;
            th2.IsBackground = true;
            th2.Start();
        }


        /// <summary>
        /// Play a sound file. Supported format are Wav(pcm+adpcm) and XWMA
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="text">Text to display</param>
        /// <param name="fileName">Name of the file.</param>
        static void PLaySoundFile(XAudio2 device, string text, string fileName)
        {
            Console.WriteLine("{0} => {1} (Press esc to skip)", text, fileName);
            var stream = new SoundStream(File.OpenRead(fileName));
            var waveFormat = stream.Format;
            var buffer = new AudioBuffer
            {
                Stream = stream.ToDataStream(),
                AudioBytes = (int)stream.Length,
                Flags = BufferFlags.EndOfStream
            };
            stream.Close();

            var sourceVoice = new SourceVoice(device, waveFormat, true);
            // Adds a sample callback to check that they are working on source voices
            sourceVoice.BufferEnd += (context) => Console.WriteLine(" => event received: end of buffer");
            sourceVoice.SubmitSourceBuffer(buffer, stream.DecodedPacketsInfo);
            sourceVoice.Start();

            int count = 0;
            while (sourceVoice.State.BuffersQueued > 0 && !IsKeyPressed(ConsoleKey.Escape))
            {
                if (count == 50)
                {
                    Console.Write(".");
                    Console.Out.Flush();
                    count = 0;
                }
                Thread.Sleep(10);
                count++;
            }
            Console.WriteLine();

            sourceVoice.DestroyVoice();
            sourceVoice.Dispose();
            buffer.Stream.Dispose();
        }
        /// <summary>
        /// Determines whether the specified key is pressed.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        ///   <c>true</c> if the specified key is pressed; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsKeyPressed(ConsoleKey key)
        {
            return Console.KeyAvailable && Console.ReadKey(true).Key == key;
        }

        private void GetDecorded(string path, ref FileStream baseStream, ref WaveFormat waveFormat, ref int totalSize, ref byte[] newData)
        {
            if (baseStream != null) { return; }

            IEnumerator<DataPointer> basePointer = null;
            List<int> pSize = new List<int>();
            List<IntPtr> dst = new List<IntPtr>();

            
            // 音楽データをオープンしてサンプリングデータを採取
            baseStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            var audioDecorder = new AudioDecoder(baseStream);
            waveFormat = audioDecorder.WaveFormat;
            basePointer = audioDecorder.GetSamples().GetEnumerator();

            // ファイルに書き出す場合はコレ
            //var outputWavStream = new FileStream(Database.BaseMusicFolder + fileName2 + ".wav", FileMode.Create, FileAccess.Write);
            //var wavWriter = new WavWriter(outputWavStream);
            //wavWriter.Begin(audioDecorder.WaveFormat);
            //wavWriter.AppendData(audioDecorder.GetSamples());
            //wavWriter.End();
            //outputWavStream.Close();

            // enumeratorに格納されているサンプリングデータをバッファ配列に記憶させる。
            while (true)
            {
                if (basePointer.MoveNext())
                {
                    pSize.Add(basePointer.Current.Size);
                    dst.Add(Utilities.AllocateMemory(basePointer.Current.Size));
                    Utilities.CopyMemory(dst[dst.Count - 1], basePointer.Current.Pointer, basePointer.Current.Size);
                    totalSize += basePointer.Current.Size;
                }
                else
                {
                    break;
                }
            }

            // バッファ配列を一つのデータとして結合する。
            unsafe
            {
                newData = new byte[totalSize];
                for (int ii = 0; ii < dst.Count; ii++)
                {
                    fixed (byte* _bp = &newData[pSize[ii] * ii])
                    {
                        Utilities.CopyMemory((IntPtr)_bp, dst[ii], pSize[ii]);
                    }
                }
            }

            // バッファ配列はもう使わないので、解放する。
            for (int ii = 0; ii < dst.Count; ii++)
            {
                Utilities.FreeMemory(dst[ii]);
            }
        }

        private void DisposeData(ref FileStream fs, ref int totalSize)
        {
            if (fs != null) { fs.Close(); fs = null; }
            totalSize = 0;
        }
        private void UpdateMusic()
        {
            FileStream baseStream = null;
            WaveFormat waveFormat = null;
            int totalSize = 0;
            byte[] newData = null;
            try
            {
                while (endFlag2 == false)
                {
                    int loop = 999;
                    bool exec = false;


                    // BGMファイル名が登録されたらスタート
                    if (fileName2 != string.Empty && this.changeToSecondMusic == false)
                    {
                        if (this.fileName2_2 != string.Empty) { loop = 0; } // 次の曲が登録されている場合、ループカウントを０にする。

                        // ループ再生に入る前で、音楽データをセットアップする。
                        GetDecorded(Database.BaseMusicFolder + fileName2, ref baseStream, ref waveFormat, ref totalSize, ref newData);
                        this.fileName2 = String.Empty;
                        exec = true;
                    }
                    else if (fileName2_2 != string.Empty && this.changeToSecondMusic)
                    {
                        // ループ再生に入る前で、音楽データをセットアップする。
                        GetDecorded(Database.BaseMusicFolder + fileName2_2, ref baseStream, ref waveFormat, ref totalSize, ref newData);
                        this.fileName2_2 = String.Empty;
                        exec = true;
                    }
                    else
                    {
                        exec = false;
                    }

                    if (exec)
                    {
                        AudioBuffer buffer = new AudioBuffer();
                        unsafe
                        {
                            fixed (byte* _bp = newData)
                            {
                                buffer.AudioDataPointer = (IntPtr)_bp;
                                buffer.AudioBytes = totalSize;
                                buffer.Flags = BufferFlags.EndOfStream;
                                buffer.LoopBegin = this.loopBeginTime;
                                buffer.LoopCount = loop;
                            }
                        }

                        // Adds a sample callback to check that they are working on source voices
                        //sourceVoice.BufferEnd += (context) => Console.WriteLine(" => event received: end of buffer");
                        SourceVoice source1 = new SourceVoice(device, waveFormat, true);
                        source1.BufferEnd += source1_BufferEnd;
                        source1.SubmitSourceBuffer(buffer, null);//stream.DecodedPacketsInfo);
                        source1.Start();

                        // loop until the sound is done playing.
                        while (source1.State.BuffersQueued > 0)
                        {
                            System.Threading.Thread.Sleep(0);
                            if (this.stopSoundFlag2)
                            {
                                source1.Stop();
                                break;
                            }
                            if (this.MuteFlag)
                            {
                                source1.SetVolume(0);
                            }
                            else
                            {
                                if (source1.Volume != 1.0f)
                                {
                                    source1.SetVolume(1.0f);
                                }
                            }
                        }
                    }

                    // 何も指示が無い場合は、ココで待ちうけ
                    System.Threading.Thread.Sleep(0);
                    DisposeData(ref baseStream, ref totalSize);
                    this.stopSoundFlag2 = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        // ループ回数の分だけ曲が回り終えたら本イベントが発行される。
        void source1_BufferEnd(IntPtr obj)
        {
//            MessageBox.Show("music end");
            this.changeToSecondMusic = true;
        }

        // ただしサウンドは現状大量に存在しないため、対象が少ないのであれば、バッファに事前に読み込んでおく。
        Dictionary<string, byte[]> soundList = new Dictionary<string, byte[]>();
        Dictionary<string, WaveFormat> soundFormatList = new Dictionary<string, WaveFormat>();
        Dictionary<string, int> soundTotalList = new Dictionary<string, int>();
        public void InitializeSoundList()
        {
            List<string> soundNameList = new List<string>();
            soundNameList.Add(Database.SOUND_1);
            soundNameList.Add(Database.SOUND_2);
            soundNameList.Add(Database.SOUND_3);
            soundNameList.Add(Database.SOUND_4);
            soundNameList.Add(Database.SOUND_5);
            soundNameList.Add(Database.SOUND_6);
            soundNameList.Add(Database.SOUND_7);
            soundNameList.Add(Database.SOUND_8);
            soundNameList.Add(Database.SOUND_9);
            soundNameList.Add(Database.SOUND_10);
            soundNameList.Add(Database.SOUND_11);
            soundNameList.Add(Database.SOUND_12);
            soundNameList.Add(Database.SOUND_13);
            soundNameList.Add(Database.SOUND_14);
            soundNameList.Add(Database.SOUND_15);
            soundNameList.Add(Database.SOUND_16);
            soundNameList.Add(Database.SOUND_17);
            soundNameList.Add(Database.SOUND_18);
            soundNameList.Add(Database.SOUND_19);
            soundNameList.Add(Database.SOUND_20);
            soundNameList.Add(Database.SOUND_21);
            soundNameList.Add(Database.SOUND_22);
            soundNameList.Add(Database.SOUND_23);
            soundNameList.Add(Database.SOUND_24);
            soundNameList.Add(Database.SOUND_25);
            soundNameList.Add(Database.SOUND_26);
            soundNameList.Add(Database.SOUND_27);
            soundNameList.Add(Database.SOUND_28);
            soundNameList.Add(Database.SOUND_29);
            soundNameList.Add(Database.SOUND_30);
            soundNameList.Add(Database.SOUND_31);
            soundNameList.Add(Database.SOUND_32);
            soundNameList.Add(Database.SOUND_33);
            soundNameList.Add(Database.SOUND_34);
            soundNameList.Add(Database.SOUND_35);
            soundNameList.Add(Database.SOUND_36);
            soundNameList.Add(Database.SOUND_37);
            soundNameList.Add(Database.SOUND_38);
            soundNameList.Add(Database.SOUND_39);
            soundNameList.Add(Database.SOUND_40);
            soundNameList.Add(Database.SOUND_41);
            soundNameList.Add(Database.SOUND_42);
            soundNameList.Add(Database.SOUND_43);
            soundNameList.Add(Database.SOUND_44);
            soundNameList.Add(Database.SOUND_45);
            soundNameList.Add(Database.SOUND_46);
            soundNameList.Add(Database.SOUND_47);
            soundNameList.Add(Database.SOUND_48);
            soundNameList.Add(Database.SOUND_49);
            soundNameList.Add(Database.SOUND_50);
            soundNameList.Add(Database.SOUND_51);
            soundNameList.Add(Database.SOUND_52);
            soundNameList.Add(Database.SOUND_53);
            soundNameList.Add(Database.SOUND_54);
            soundNameList.Add(Database.SOUND_55);
            soundNameList.Add(Database.SOUND_56);
            soundNameList.Add(Database.SOUND_57);
            soundNameList.Add(Database.SOUND_58);
            soundNameList.Add(Database.SOUND_59);
            soundNameList.Add(Database.SOUND_60);
            soundNameList.Add(Database.SOUND_61);
            soundNameList.Add(Database.SOUND_62);
            soundNameList.Add(Database.SOUND_63);
            soundNameList.Add(Database.SOUND_64);
            soundNameList.Add(Database.SOUND_65);
            soundNameList.Add(Database.SOUND_66);
            soundNameList.Add(Database.SOUND_67);
            soundNameList.Add(Database.SOUND_68);
            soundNameList.Add(Database.SOUND_69);
            soundNameList.Add(Database.SOUND_70);
            soundNameList.Add(Database.SOUND_71);

            for (int ii = 0; ii < soundNameList.Count; ii++)
            {
                FileStream baseStream = null;
                WaveFormat waveFormat = null;
                int totalSize = 0;
                byte[] newData = null;
                GetDecorded(Database.BaseSoundFolder + soundNameList[ii], ref baseStream, ref waveFormat, ref totalSize, ref newData);
                this.soundList.Add(soundNameList[ii], newData);
                this.soundFormatList.Add(soundNameList[ii], waveFormat);
                this.soundTotalList.Add(soundNameList[ii], totalSize);
                DisposeData(ref baseStream, ref totalSize);
            }
        }
        private void UpdateSound()
        {
            try
            {
                while (endFlag == false)
                {
                    // BGMファイル名が登録されたらスタート
                    if (fileName != string.Empty)
                    {
                        // ループ再生に入る前で、音楽データをセットアップする。
                        byte[] newData = null;
                        WaveFormat waveFormat = null;
                        int totalSize = 0;
                        if (this.soundList.TryGetValue(fileName, out newData) == false) { this.fileName = String.Empty; }
                        else if (this.soundFormatList.TryGetValue(fileName, out waveFormat) == false) { this.fileName = String.Empty; }
                        else if (this.soundTotalList.TryGetValue(fileName, out totalSize) == false) { this.fileName = String.Empty; }
                        else
                        {
                            AudioBuffer buffer = new AudioBuffer();
                            unsafe
                            {
                                fixed (byte* _bp = newData)
                                {
                                    buffer.AudioDataPointer = (IntPtr)_bp;
                                    buffer.AudioBytes = totalSize;
                                    buffer.Flags = BufferFlags.EndOfStream;
                                }
                            }

                            SourceVoice sourceVoice = new SourceVoice(device, waveFormat, true);
                            sourceVoice.SubmitSourceBuffer(buffer, null);//stream.DecodedPacketsInfo);
                            sourceVoice.Start();

                            // loop until the sound is done playing.
                            while (sourceVoice.State.BuffersQueued > 0)
                            {
                                System.Threading.Thread.Sleep(0);
                                if (this.stopSoundFlag)
                                {
                                    sourceVoice.Stop();
                                    break;
                                }
                            }
                        }

                        //  buffer.Dispose();
                        //sourceVoice.Dispose();
                        //stream.Dispose();
                        this.fileName = String.Empty;
                    }

                    // 何も指示が無い場合は、ココで待ちうけ
                    System.Threading.Thread.Sleep(0);
                    this.stopSoundFlag = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void StopMusic()
        {
            this.stopSoundFlag2 = true;
            while (true)
            {
                if (this.stopSoundFlag2 == false)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(0);
                }
            }
        }

        public void PlayMusic(string fileName, int loopBegin)
        {
            PlayMusic(fileName, string.Empty, loopBegin);
        }
        public void PlayMusic(string fileName, string secondMusicName, int loopBegin)
        {
            this.stopSoundFlag2 = true;
            while (true)
            {
                if (this.stopSoundFlag2 == false)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(0);
                }
            }

            this.loopBeginTime = loopBegin;
            this.changeToSecondMusic = false;
            this.fileName2 = fileName;
            if (secondMusicName != string.Empty)
            {
                this.fileName2_2 = secondMusicName;
            }
        }

        public void UpdateMuteFlag(bool mute)
        {
            this.MuteFlag = mute;
        }

        public void PlayMP3(string fileName)
        {
            this.stopSoundFlag = true;
            while (true)
            {
                if (this.stopSoundFlag == false)
                {
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(1);
                }
            }

            this.fileName = fileName;
            //if (System.IO.File.Exists(Database.BaseSoundFolder + fileName))
            //{
            //    StopMP3();
            //    string cmd;
            //    //ファイルを開く
            //    cmd = "open \"" + Database.BaseSoundFolder + fileName + "\" alias " + aliasName;
            //    if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0)
            //        return;
            //    //再生する
            //    cmd = "play " + aliasName;
            //    mciSendString(cmd, null, 0, IntPtr.Zero); // this.Handle);
            //}
        }


        public void StopMP3()
        {
            //string cmd;
            ////再生しているWAVEを停止する
            //cmd = "stop " + aliasName;
            //mciSendString(cmd, null, 0, IntPtr.Zero);
            ////閉じる
            //cmd = "close " + aliasName;
            //mciSendString(cmd, null, 0, IntPtr.Zero);

        }

        private void InitializeComponent()
        {
        }

        public void Disactive()
        {
            endFlag = true;
            th.Join();
            endFlag2 = true;
            th2.Join();
            if (this.masteringVoice != null)
            {
                this.masteringVoice.Dispose();
                this.masteringVoice = null;
            }
            if (this.device != null)
            {
                this.device.Dispose();
                this.device = null;
            }
        }
    }
}
