using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DungeonPlayer
{
    public static class GroundOne
    {
        //public static TruthDungeonMapAll map = null;
        public static TruthWorldEnvironment WE2 = null; // ゲームストーリー全体のワールド環境フラグ
        public static TruthInformation information = null; // ヘルプ情報
        public static TruthPlaybackMessage playback = null; // メッセージプレイバック
        public static List<String> playbackMessage = new List<string>(); // プレイバックメッセージテキスト
        public static List<TruthPlaybackMessage.infoStyle> playbackInfoStyle = new List<TruthPlaybackMessage.infoStyle>(); // プレイバックメッセージスタイル
        public static XepherPlayer sound = null; // サウンド音源

        public static int Difficulty = 1; // ゲーム難易度 デフォルトは１：普通

        public static bool EnableBGM = true; // ミュージック、デフォルトはオン
        public static bool EnableSoundEffect = true; // 効果音、デフォルトはオン




        #region "BGM再生と効果音関連"

        public static void InitializeSoundData()
        {
            try
            {
                if (GroundOne.sound == null)
                {
                    GroundOne.sound = new XepherPlayer();
                }
                GroundOne.sound.InitializeSoundList();
            }
            catch
            {
                GroundOne.EnableSoundEffect = false;
                GroundOne.EnableBGM = false;
                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
            }
        }
        public static void PlaySoundEffect(string soundName)
        {
            try
            {
                if (GroundOne.EnableSoundEffect)
                {
                    if (GroundOne.sound == null)
                    {
                        GroundOne.sound = new XepherPlayer();
                    }
                    GroundOne.sound.PlayMP3(soundName);
                }
            }
            catch
            {
                GroundOne.EnableSoundEffect = false;
                GroundOne.EnableBGM = false;
                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
            }
        }

        public static void PlayDungeonMusic(string targetMusicName, int loopBegin)
        {
            PlayDungeonMusic(targetMusicName, string.Empty, loopBegin);
        }
        public static void PlayDungeonMusic(string targetMusicName, string targetMusicName2, int loopBegin)
        {
            try
            {
                if (GroundOne.EnableBGM)
                {
                    if (GroundOne.sound == null)
                    {
                        GroundOne.sound = new XepherPlayer();
                    }
                    else
                    {
                        GroundOne.sound.StopMusic();
                    }
                    GroundOne.sound.PlayMusic(targetMusicName, targetMusicName2, loopBegin);
                }
            }
            catch
            {
                GroundOne.EnableSoundEffect = false;
                GroundOne.EnableBGM = false;
                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
            }
        }

        public static void StopDungeonMusic()
        {
            try
            {
                if (GroundOne.EnableBGM)
                {
                    if (GroundOne.sound != null)
                    {
                        GroundOne.sound.StopMusic();
                    }
                }
            }
            catch
            {
                GroundOne.EnableSoundEffect = false;
                GroundOne.EnableBGM = false;
                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
            }
        }

        public static void TempStopDungeonMusic()
        {
            if (GroundOne.sound != null)
            {
                GroundOne.sound.UpdateMuteFlag(true);
            }
        }

        public static void ResumeDungeonMusic()
        {
            if (GroundOne.sound != null)
            {
                GroundOne.sound.UpdateMuteFlag(false);
            }
        }
        #endregion
    }
}
