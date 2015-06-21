using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public static class TruthLayout
    {
        // ボスのレイアウト
        public static int BOSS_LINE_LOC_X = 700;
        public static int BOSS_STATUS_LOC_X = 800;

        public static int BOSS_INSTANT_LABEL_LOC_Y = 400;
        public static int BOSS_INSTANT_LABEL_WIDTH = 300;
        public static int BOSS_INSTANT_LABEL_HEIGHT = 40;

        public static int BOSS_MANA_LABEL_LOC_Y = 500;
        public static int BOSS_MANA_LABEL_WIDTH = BOSS_INSTANT_LABEL_WIDTH;
        public static int BOSS_MANA_LABEL_HEIGHT = BOSS_INSTANT_LABEL_HEIGHT;

        public static int BOSS_NAME_LABEL_LOC_Y = 200;
        public static int BOSS_MAIN_OBJ_LOC_Y = 300;
        public static int BOSS_LIFE_LABEL_LOC_X = BOSS_LINE_LOC_X + 100;
        public static int BOSS_LIFE_LABEL_LOC_Y = 300;
        public static int BOSS_ACTION_LABEL_LOC_X = BOSS_LINE_LOC_X + 100;
        public static int BOSS_ACTION_LABEL_LOC_Y = 350;
        public static int BOSS_CRITICAL_LABEL_LOC_Y = 305;
        public static int BOSS_DAMAGE_LABEL_LOC_Y = 325;
        public static int BOSS_BUFF_LOC_Y = 450;

        public static int LAST_BOSS_NAME_LABEL_LOC_Y = 130;
    }
}
