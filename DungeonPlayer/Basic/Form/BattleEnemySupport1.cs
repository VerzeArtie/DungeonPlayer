using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace DungeonPlayer
{
    public partial class BattleEnemy : MotherForm
    {
        // [�x���ő�]�F�퓬�t�F�[�Y�͕����̖����A�����̓G��z�肵���X�P���g�����K�v�ł��B���Ȃ炸���Ȃ��Ă��������B
        // �������v3�l�܂ŁA�G���v4�l�܂łƂ͌���܂���B�g�������ŏ�����g��ł��������B
        private void BattleStart()
        {
            // �퓬�O�A�A�b�v�L�[�v
            UpkeepStep();

            // �퓬�O�A��������
            if (RunAwayStep(this.mc.PA))
            {
                this.DialogResult = DialogResult.Abort;
                if (this.mc != null)
                {
                    this.mc.CleanUpBattleEnd();
                }
                if (this.sc != null)
                {
                    this.sc.CleanUpBattleEnd();
                }
                if (this.tc != null)
                {
                    this.tc.CleanUpBattleEnd();
                }
                return; // �������ꍇ�A�������f
            }

            // [�x��]�F���̎��_�œG�̍s�������܂��ĂȂ��ƁAStanceOfStainding��CounterAttack�Ƃ������X�L�����G���ł͌��ʂ𔭊����܂���B
            int enemyActionNum = SetupEnemyAction();

            // [�x��]�F���񃂁[�h�Ŏw�肪����ꍇ�A�����v���C���[�͏������Œ�ɂȂ�悤�ɂ��Ă��������B
            // �퓬�J�n�O�A�����̌���
            SortedList<int, MainCharacter> ActiveList = new SortedList<int, MainCharacter>();
            AddActivePlayer(ActiveList, this.mc, this.ec1.Agility, this.ec1.Mind);
            AddActivePlayer(ActiveList, this.sc, this.ec1.Agility, this.ec1.Mind);
            AddActivePlayer(ActiveList, this.tc, this.ec1.Agility, this.ec1.Mind);
            AddActivePlayer(ActiveList, this.ec1);
            // �G�̐���������ꍇ�A�����ǉ����Ă��������B
            // �����̐���������ꍇ�A�����ǉ����Ă��������B

            //this.textBox1.Text += (ActiveList.Keys[0].ToString() +"   " + ActiveList.Keys[1].ToString() + "   " + ActiveList.Keys[2].ToString() + "   " + ActiveList.Keys[3].ToString()) + "\r\n";


            // �퓬�J�n
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList.Values[ii].GetType() == typeof(MainCharacter))
                {
                    if (!ActiveList.Values[ii].Dead)
                    {
                        PlayerAttackPhase(ActiveList.Values[ii]); if (ActiveList.Values[ii].CurrentGaleWind == 1) PlayerAttackPhase(ActiveList.Values[ii], true);
                    }
                }
                else if (ActiveList.Values[ii].GetType() == typeof(EnemyCharacter1))
                {
                    if (!ActiveList.Values[ii].Dead)
                    {
                        if (enemyActionNum != 0)
                        {
                            EnemyAttackPhase(ActiveList.Values[ii].Target, enemyActionNum); if (ActiveList.Values[ii].CurrentGaleWind == 1) EnemyAttackPhase(ActiveList.Values[ii].Target, this.bossAlreadyActionNum);
                        }
                        else
                        {
                            EnemyAttackPhase(); if (ActiveList.Values[ii].CurrentGaleWind == 1) EnemyAttackPhase(ActiveList.Values[ii].Target, this.bossAlreadyActionNum);
                        }
                    }
                }

                if (PlayerDeathCheck()) // ���ł������������ꍇ�A�v���C���[����������̂Ƃ���B
                {
                    for (int jj = 0; jj < ActiveList.Count; jj++)
                    {
                        ActiveList.Values[jj].CleanUpBattleEnd();
                    }
                    return; // ���炩�e���Ŏ������g�����񂾏ꍇ�A�������f
                }
                if (EnemyDeathCheck())
                {
                    for (int jj = 0; jj < ActiveList.Count; jj++)
                    {
                        ActiveList.Values[jj].CleanUpBattleEnd();
                    }
                    return; // �G��|�����ꍇ�A�������f
                }

            }

            // �퓬��̒ǉ����ʃt�F�[�Y
            AfterBattleEffect();
            if (PlayerDeathCheck())
            {
                for (int jj = 0; jj < ActiveList.Count; jj++)
                {
                    ActiveList.Values[jj].CleanUpBattleEnd();
                }
                return; // ���炩�e���Ŏ������g�����񂾏ꍇ�A�������f
            }
            if (EnemyDeathCheck())
            {
                for (int jj = 0; jj < ActiveList.Count; jj++)
                {
                    ActiveList.Values[jj].CleanUpBattleEnd();
                }
                return; // ���炩�̉e���œG���g�����񂾏ꍇ�A�������f
            }

            // �퓬�I����A�N���[���i�b�v
            CleanUpStep();
        }


    }
}
