//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DungeonSeeker
//{
//    public static class ActionCommandAttribute
//    {
//        public static bool IsOwnTarget(string commandName)
//        {
//            if ((commandName == Database.GLORY) ||
//                (commandName == Database.BLACK_CONTRACT) ||
//                (commandName == Database.IMMORTAL_RAVE) ||
//                (commandName == Database.GALE_WIND) ||
//                (commandName == Database.AETHER_DRIVE) ||
//                (commandName == Database.GENESIS) ||
//                (commandName == Database.ONE_IMMUNITY) ||
//                // スキル
//                (commandName == Database.PURE_PURIFICATION) ||
//                (commandName == Database.ANTI_STUN) ||
//                (commandName == Database.STANCE_OF_DEATH) ||
//                (commandName == Database.STANCE_OF_FLOW) ||
//                (commandName == Database.INNER_INSPIRATION) ||
//                (commandName == Database.TRUTH_VISION) ||
//                (commandName == Database.HIGH_EMOTIONALITY) ||
//                (commandName == Database.PAINFUL_INSANITY) || // [警告] 自分対象だと敵全員ってことになる？
//                (commandName == Database.VOID_EXTRACTION) ||
//                (commandName == Database.NOTHING_OF_NOTHINGNESS) ||
//                // 武器能力
//                (commandName == Database.RARE_LIFE_SWORD) ||
//                (commandName == Database.RARE_AUTUMN_ROD) ||
//                (commandName == Database.COMMON_HAYATE_ORB) ||
//                (commandName == Database.EPIC_RING_OF_OSCURETE) || 
//                // 防御
//                (commandName == Database.DEFENSE) ||
//                (commandName == Database.DEFENSE_EN) ||
//                // ためる
//                (commandName == Database.TAMERU) ||
//                (commandName == Database.TAMERU_EN)
//                )
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public static bool IsAllyTarget(string commandName)
//        {
//            if (// 単一対象でBUFF系
//                (commandName == Database.PROTECTION) ||
//                (commandName == Database.SAINT_POWER) ||
//                (commandName == Database.SHADOW_PACT) ||
//                (commandName == Database.BLOODY_VENGEANCE) ||
//                (commandName == Database.FLAME_AURA) ||
//                (commandName == Database.HEAT_BOOST) ||
//                (commandName == Database.ABSORB_WATER) ||
//                (commandName == Database.MIRROR_IMAGE) ||
//                (commandName == Database.PROMISED_KNOWLEDGE) ||
//                (commandName == Database.WORD_OF_LIFE) ||
//                (commandName == Database.WORD_OF_FORTUNE) ||
//                (commandName == Database.ETERNAL_PRESENCE) ||
//                (commandName == Database.RISE_OF_IMAGE) ||
//                (commandName == Database.DEFLECTION) ||
//                // 単一対象でダメージ・ヒール・効果系
//                (commandName == Database.FRESH_HEAL) ||
//                (commandName == Database.RESURRECTION) ||
//                (commandName == Database.CELESTIAL_NOVA) ||
//                (commandName == Database.LIFE_TAP) ||
//                (commandName == Database.CLEANSING)
//                )
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public static bool IsEnemyTarget(string commandName)
//        {
//            if (//単一対象でダメージ・効果系
//                (commandName == Database.FIRE_BALL) ||
//                (commandName == Database.FLAME_STRIKE) ||
//                (commandName == Database.VOLCANIC_WAVE) ||
//                (commandName == Database.ICE_NEEDLE) ||
//                (commandName == Database.FROZEN_LANCE) ||
//                (commandName == Database.HOLY_SHOCK) ||
//                (commandName == Database.CELESTIAL_NOVA) ||
//                (commandName == Database.DARK_BLAST) ||
//                (commandName == Database.DEVOURING_PLAGUE) ||
//                (commandName == Database.DAMNATION) ||
//                (commandName == Database.ABSOLUTE_ZERO) ||
//                (commandName == Database.DISPEL_MAGIC) ||
//                (commandName == Database.TRANQUILITY) ||
//                (commandName == Database.WHITE_OUT) ||
//                (commandName == Database.TIME_STOP) ||
//                (commandName == Database.WORD_OF_POWER) ||
//                (commandName == Database.FLASH_BLAZE) ||
//                // スキル
//                (commandName == Database.STRAIGHT_SMASH) ||
//                (commandName == Database.DOUBLE_SLASH) ||
//                (commandName == Database.CRUSHING_BLOW) ||
//                (commandName == Database.SOUL_INFINITY) ||
//                (commandName == Database.COUNTER_ATTACK) ||
//                (commandName == Database.ENIGMA_SENSE) ||
//                (commandName == Database.SILENT_RUSH) ||
//                (commandName == Database.OBORO_IMPACT) ||
//                (commandName == Database.STANCE_OF_STANDING) ||
//                (commandName == Database.KINETIC_SMASH) ||
//                (commandName == Database.CATASTROPHE) ||
//                (commandName == Database.STANCE_OF_EYES) ||
//                (commandName == Database.NEGATE) ||
//                (commandName == Database.CARNAGE_RUSH) ||
//                // 武器能力
//                (commandName == Database.RARE_AERO_BLADE) ||
//                (commandName == Database.RARE_ICE_SWORD) ||
//                (commandName == Database.EPIC_MERGIZD_SOL_BLADE)
//                )
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public static bool IsAll(string commandName)
//        {
//            if ((commandName == Database.LAVA_ANNIHILATION)
//                )
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }
//    }
//}
