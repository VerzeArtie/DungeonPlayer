using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public static class TruthItemAttribute
    {
        // 自由
        // 捨てられない
        // 他者に渡せない
        // 
        public enum Transfer
        {
            Any,
            Cannot_HandOver,
            Cannot_Trash,
        }

        public static Transfer CheckImportantItem(string itemName)
        {
            if (itemName == Database.RARE_TOOMI_BLUE_SUISYOU)
            {
                return Transfer.Cannot_Trash;
            }
            if ((itemName == Database.POOR_PRACTICE_SWORD_ZERO) ||
                (itemName == Database.POOR_PRACTICE_SWORD_1) ||
                (itemName == Database.POOR_PRACTICE_SWORD_2) ||
                (itemName == Database.COMMON_PRACTICE_SWORD_3) ||
                (itemName == Database.COMMON_PRACTICE_SWORD_4) ||
                (itemName == Database.RARE_PRACTICE_SWORD_5) ||
                (itemName == Database.RARE_PRACTICE_SWORD_6) ||
                (itemName == Database.EPIC_PRACTICE_SWORD_7) ||
                (itemName == Database.LEGENDARY_FELTUS))
            {
                return Transfer.Cannot_Trash;
            }

            if (itemName == "天空の翼（レプリカ）" ||
                itemName == "黒真空の鎧（レプリカ）" ||
                itemName == "白銀の剣（レプリカ）")
            {
                return Transfer.Cannot_HandOver;
            }
            if (itemName == "ラナのイヤリング")
            {
                return Transfer.Cannot_HandOver;
            }
            return Transfer.Any;
        }
    }
}
