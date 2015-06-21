using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class BattleEnemy : MotherForm
    {
        private void SelectNone(MainCharacter player)
        {
            player.PA = PlayerAction.None;
        }

        private void SelectNormalAttack(MainCharacter player)
        {
            player.PA = PlayerAction.NormalAttack;
        }

        private void SelectDefense(MainCharacter player)
        {
            player.PA = PlayerAction.Defense; // [警告]本来Defenseを選択するだけのフラグのはずだが、このフラグを立てた時点で効果が発揮されてしまっている。
        }

        // 魔法 //
        // [聖]
        private void SelectFreshHeal(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.FRESH_HEAL;
        }

        private void SelectProtection(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.PROTECTION;
        }

        private void SelectHolyShock(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.HOLY_SHOCK;
        }

        private void SelectSaintPower(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.SAINT_POWER;
        }

        private void SelectGlory(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.GLORY;
        }

        private void SelectResurrection(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.RESURRECTION;
        }

        private void SelectCelestialNova(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.CELESTIAL_NOVA;
        }

        // [闇]
        private void SelectDarkBlast(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.DARK_BLAST;
        }

        private void SelectShadowPact(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.SHADOW_PACT;
        }

        private void SelectLifeTap(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.LIFE_TAP;
        }

        private void SelectBlackContract(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.BLACK_CONTRACT;
        }

        private void SelectDevouringPlague(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.DEVOURING_PLAGUE;
        }

        private void SelectBloodyVengeance(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.BLOODY_VENGEANCE;
        }

        private void SelectDamnation(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.DAMNATION;
        }

        // [火]
        private void SelectFireBall(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.FIRE_BALL;
        }

        private void SelectFlameAura(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.FLAME_AURA;
        }

        private void SelectHeatBoost(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.HEAT_BOOST;
        }

        private void SelectFlameStrike(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.FLAME_STRIKE;
        }

        private void SelectVolcanicWave(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.VOLCANIC_WAVE;
        }

        private void SelectImmortalRave(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.IMMORTAL_RAVE;
        }

        private void SelectLavaAnnihilation(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.LAVA_ANNIHILATION;
        }

        // [水]
        private void SelectIceNeedle(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.ICE_NEEDLE;
        }

        private void SelectAbsorbWater(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.ABSORB_WATER;
        }

        private void SelectCleansing(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.CLEANSING;
        }

        private void SelectFrozenLance(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.FROZEN_LANCE;
        }

        private void SelectMirrorImage(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.MIRROR_IMAGE;
        }

        private void SelectPromisedKnowledge(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.PROMISED_KNOWLEDGE;
        }

        private void SelectAbsoluteZero(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.ABSOLUTE_ZERO;
        }

        // [理]
        private void SelectWordOfPower(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.WORD_OF_POWER;
        }

        private void SelectGaleWind(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.GALE_WIND;
        }

        private void SelectWordOfLife(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.WORD_OF_LIFE;
        }

        private void SelectWordOfFortune(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.WORD_OF_FORTUNE;
        }

        private void SelectAetherDrive(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.AETHER_DRIVE;
        }

        private void SelectGenesis(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.GENESIS;
        }

        private void SelectEternalPresence(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.ETERNAL_PRESENCE;
        }


        // [空]
        private void SelectDispelMagic(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.DISPEL_MAGIC;
        }

        private void SelectRiseOfImage(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.RISE_OF_IMAGE;
        }

        private void SelectDeflection(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.DEFLECTION;
        }

        private void SelectTranquility(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.TRANQUILITY;
        }

        private void SelectOneImmunity(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.ONE_IMMUNITY;
        }

        private void SelectWhiteOut(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.WHITE_OUT;
        }

        private void SelectTimeStop(MainCharacter player)
        {
            player.PA = PlayerAction.UseSpell;
            player.CurrentSpellName = Database.TIME_STOP;
        }



        // スキル //
        // [動]
        private void SelectStraightSmash(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.STRAIGHT_SMASH;
        }

        private void SelectDoubleSlash(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.DOUBLE_SLASH;
        }

        private void SelectCrushingBlow(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.CRUSHING_BLOW;
        }

        private void SelectSoulInfinity(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.SOUL_INFINITY;
        }

        // [静]
        private void SelectCounterAttack(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.COUNTER_ATTACK;
            //player.CurrentCounterAttack = true;
            player.CurrentCounterAttack = Database.INFINITY; // 後編編集
        }

        private void SelectPurePurification(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.PURE_PURIFICATION;
        }

        private void SelectAntiStun(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.ANTI_STUN;
        }

        private void SelectStanceOfDeath(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.STANCE_OF_DEATH;
        }

        // [柔]
        private void SelectStanceOfFlow(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.STANCE_OF_FLOW;
        }

        private void SelectEnigmaSense(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.ENIGMA_SENSE;
        }

        private void SelectSilentRush(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.SILENT_RUSH;
        }

        private void SelectOboroImpact(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.OBORO_IMPACT;
        }

        // [剛]
        private void SelectStanceOfStanding(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.STANCE_OF_STANDING;
            //player.CurrentStanceOfStanding = true;
            player.CurrentStanceOfStanding = 1; // 後編編集
        }

        private void SelectInnerInspiration(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.INNER_INSPIRATION;
        }

        private void SelectKineticSmash(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.KINETIC_SMASH;
        }

        private void SelectCatastrophe(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.CATASTROPHE;
        }

        // [心眼]
        private void SelectTruthVision(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.TRUTH_VISION;
        }

        private void SelectHighEmotionality(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.HIGH_EMOTIONALITY;
        }

        private void SelectStanceOfEyes(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.STANCE_OF_EYES;
        }

        private void SelectPainfulInsanity(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.PAINFUL_INSANITY;
        }

        // [無心]
        private void SelectNegate(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.NEGATE;
        }

        private void SelectVoidExtraction(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.VOID_EXTRACTION;
        }

        private void SelectCarnageRush(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.CARNAGE_RUSH;
        }

        private void SelectNothingOfNothingness(MainCharacter player)
        {
            player.PA = PlayerAction.UseSkill;
            player.CurrentSkillName = Database.NOTHING_OF_NOTHINGNESS;
        }



    }
}
