using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    /// <summary>
    /// 後編から新規ゲーム、ロード、リロードに関わらず常に記憶されるクラス
    /// </summary>
    public class TruthWorldEnvironment
    {
        // エンディング
        public bool SeekerEnd { get; set; }
        public bool SeekerEndingRoll { get; set; }

        // 真実世界にて
        public bool SeekerEvent1104 { get; set; }
        public bool SeekerEvent1103 { get; set; }
        public bool SeekerEvent1102_fail { get; set; }
        public bool SeekerEvent1102 { get; set; }
        public bool SeekerEvent1101_fail { get; set; }
        public bool SeekerEvent1101 { get; set; }

        // 現実世界４階にて
        public bool SeekerEvent1014 { get; set; }
        public bool SeekerEvent1013 { get; set; }
        public bool SeekerEvent1012 { get; set; }
        public bool SeekerEvent1011 { get; set; }
        public bool SeekerEvent1015 { get; set; }
        public bool SeekerEvent1010 { get; set; }
        public bool SeekerEvent1009 { get; set; }
        public bool SeekerEvent1008 { get; set; }
        public bool SeekerEvent1007 { get; set; }
        public bool SeekerEvent1006 { get; set; }
        public bool SeekerEvent1005 { get; set; }
        public bool SeekerEvent1004 { get; set; }
        public bool SeekerEvent1003 { get; set; }
        public bool SeekerEvent1002 { get; set; }
        public bool SeekerEvent1001 { get; set; }

        // 現実世界３階にて
        public bool SeekerEvent925 { get; set; }
        public bool SeekerEvent924_fail { get; set; }
        public bool SeekerEvent924 { get; set; }
        public bool SeekerEvent923 { get; set; }
        public bool SeekerEvent922 { get; set; }
        public bool SeekerEvent921 { get; set; }
        public bool SeekerEvent920 { get; set; }
        public bool SeekerEvent919 { get; set; }
        public bool SeekerEvent918 { get; set; }
        public bool SeekerEvent917 { get; set; }
        public bool SeekerEvent916 { get; set; }
        public bool SeekerEvent915 { get; set; }
        public bool SeekerEvent914 { get; set; }
        public bool SeekerEvent913 { get; set; }
        public bool SeekerEvent912 { get; set; }
        public bool SeekerEvent911 { get; set; }
        public bool SeekerEvent910 { get; set; }
        public bool SeekerEvent909 { get; set; }
        public bool SeekerEvent908 { get; set; }
        public bool SeekerEvent907 { get; set; }
        public bool SeekerEvent906 { get; set; }
        public bool SeekerEvent905 { get; set; }
        public bool SeekerEvent904 { get; set; }
        public bool SeekerEvent903 { get; set; }
        public bool SeekerEvent902 { get; set; }
        public bool SeekerEvent901 { get; set; }

        // 現実世界２階にて
        public bool SeekerEvent822 { get; set; }
        public bool SeekerEvent821_fail { get; set; }
        public bool SeekerEvent821 { get; set; }
        public bool SeekerEvent820 { get; set; }
        public bool SeekerEvent819 { get; set; }
        public bool SeekerEvent818 { get; set; }
        public bool SeekerEvent817 { get; set; }
        public bool SeekerEvent816 { get; set; }
        public bool SeekerEvent815 { get; set; }
        public bool SeekerEvent814 { get; set; }
        public bool SeekerEvent813 { get; set; }
        public bool SeekerEvent812 { get; set; }
        public bool SeekerEvent811 { get; set; }
        public bool SeekerEvent810 { get; set; }
        public bool SeekerEvent809 { get; set; }
        public bool SeekerEvent808 { get; set; }
        public bool SeekerEvent807 { get; set; }
        public bool SeekerEvent806 { get; set; }
        public bool SeekerEvent805 { get; set; }
        public bool SeekerEvent804 { get; set; }
        public bool SeekerEvent803 { get; set; }
        public bool SeekerEvent802 { get; set; }
        public bool SeekerEvent801 { get; set; }

        // 現実世界１階にて
        public bool SeekerEvent706 { get; set; }
        public bool SeekerEvent705 { get; set; }
        public bool SeekerEvent704 { get; set; }
        public bool SeekerEvent703 { get; set; }
        public bool SeekerEvent702 { get; set; }
        public bool SeekerEvent701 { get; set; }

        // ダンジョン侵入階層の値
        public int RealDungeonArea { get; set; }

        // ホームタウンにて
        public bool SeekerEvent605 { get; set; }
        public bool SeekerEvent604 { get; set; }
        public bool SeekerEvent603 { get; set; }
        public bool SeekerEvent602 { get; set; }
        public bool SeekerEvent601 { get; set; }

        // エンド時、オートセーブが効いている事を表示するため
        public bool AutoSaveInfo { get; set; }

        // 最下層にて
        public bool SeekerEvent511 { get; set; }
        public bool SeekerEvent510 { get; set; }
        public bool SeekerEvent509 { get; set; }
        public bool SeekerEvent508_fail { get; set; }
        public bool SeekerEvent508 { get; set; }
        public bool SeekerEvent507 { get; set; }
        public bool SeekerEvent506 { get; set; }
        public bool SeekerEvent505 { get; set; }
        public bool SeekerEvent504 { get; set; }
        public bool SeekerEvent503 { get; set; }
        public bool SeekerEvent502 { get; set; }
        public bool SeekerEvent501 { get; set; }

        public bool SeekerEvent3 { get; set; }
        public bool SeekerEvent2 { get; set; }
        public bool SeekerEvent1 { get; set; }
        public bool StartSeeker { get; set; } // Seekerの始まり
        public bool SelectFalseStatue { get; set; } // 究極の二択、失敗を選択した後、タイトルメニュー項目「Seeker」を追加
        public bool DungeonEvent1 { get; set; } // 究極の二択
        public bool RealWorld { get; set; } // ４階、現実世界の始まり

        public bool AvailableArcheTypeCommand { get; set; } // 元核情報を見せるかどうか

        public bool AvailableMixSpellSkill { get; set; } // 複合魔法・スキル情報を見せるかどうか

        public int PracticeSwordCount { get; set; } // 練習用の剣『封』で戦闘勝利した回数

        public bool TruthKey5 { get; set; } // ☆☆☆　真実解キールート５の踏破に成功
        public bool TruthKey4 { get; set; } // ☆☆☆　真実解キールート４の踏破に成功
        public bool TruthKey3 { get; set; } // ☆☆☆　真実解キールート３の踏破に成功
        public bool TruthKey2 { get; set; } // ☆☆☆　真実解キールート２の踏破に成功
        public bool TruthKey1 { get; set; } // ☆☆☆　真実解キールート１の踏破に成功

        public bool TruthRecollection5 { get; set; } // ５階真実回想
        public bool TruthRecollection4 { get; set; } // ４階真実回想
        public bool TruthRecollection3_4 { get; set; } // ３階真実回想４
        public bool TruthRecollection3_3 { get; set; } // ３階真実回想３
        public bool TruthRecollection3_2 { get; set; } // ３階真実回想２
        public bool TruthRecollection3_1 { get; set; } // ３階真実回想１
        public bool TruthRecollection2 { get; set; } // ２階真実回想
        public bool TruthRecollection1 { get; set; } // １階真実回想

        public int KillingEnemy { get; set; } // 敵を倒した数

        public int DuelWin { get; set; } // DUELに勝利した回数
        public int DuelLose { get; set; } // DUELに負けた回数

        // ３階、正解ルートXの記憶
        public int TruthWay95 {get; set; }
        public int TruthWay96 {get; set; }
        public int TruthWay97 {get; set; }
        public int TruthWay98 {get; set; }
        public int TruthWay99 {get; set; }
        public int TruthWay100 {get; set; }
        public int TruthWay101 {get; set; }
        public int TruthWay102 {get; set; }
        public int TruthWay103 {get; set; }
        public int TruthWay104 {get; set; }
        public int TruthWay105 {get; set; }
        public int TruthWay106 {get; set; }
        public int TruthWay107 {get; set; }
        public int TruthWay108 {get; set; }
        public int TruthWay109 {get; set; }
        public int TruthWay110 {get; set; }
        public int TruthWay111 {get; set; }
        public int TruthWay112 {get; set; }
        public int TruthWay113 {get; set; }
        public int TruthWay114 {get; set; }
        public int TruthWay115 {get; set; }
        public int TruthWay116 {get; set; }
        public int TruthWay117 {get; set; }
        public int TruthWay118 {get; set; }
        public int TruthWay119 {get; set; }
        public int TruthWay120 {get; set; }
        public int TruthWay121 {get; set; }
        public int TruthWay122 {get; set; }
        public int TruthWay123 {get; set; }
        public int TruthWay124 {get; set; }
        public int TruthWay125 {get; set; }
        public int TruthWay126 {get; set; }
        public int TruthWay127 {get; set; }
        public int TruthWay128 {get; set; }
        public int TruthWay129 {get; set; }
        public int TruthWay130 {get; set; }
        public int TruthWay131 {get; set; }
        public int TruthWay132 {get; set; }
        public int TruthWay133 {get; set; }
        public int TruthWay134 {get; set; }
        public int TruthWay135 {get; set; }
        public int TruthWay136 {get; set; }
        public int TruthWay137 {get; set; }
        public int TruthWay138 {get; set; }
        public int TruthWay139 {get; set; }
        public int TruthWay140 {get; set; }
        public int TruthWay141 {get; set; }
        public int TruthWay142 {get; set; }
        public int TruthWay143 {get; set; }
        public int TruthWay144 {get; set; }
        public int TruthWay145 {get; set; }
        public int TruthWay146 {get; set; }
        public int TruthWay147 {get; set; }
        public int TruthWay148 {get; set; }
        public int TruthWay149 {get; set; }
        public int TruthWay150 {get; set; }

        public int TruthWay3_1 { get; set; }
        public int TruthWay3_2 { get; set; }
        public int TruthWay3_3 { get; set; }
        public int TruthWay3_4 { get; set; }
        public int TruthWay3_5 { get; set; }

        // ２階、８レバーの正解判定
        public bool TruthAnswer2_1 { get; set; }
        public bool TruthAnswer2_2 { get; set; }
        public bool TruthAnswer2_3 { get; set; }
        public bool TruthAnswer2_4 { get; set; }
        public bool TruthAnswer2_5 { get; set; }
        public bool TruthAnswer2_6 { get; set; }
        public bool TruthAnswer2_7 { get; set; }
        public bool TruthAnswer2_8 { get; set; }
        public bool TruthAnswer2_OK { get; set; } // ８レバー全てOKの場合Trueとし、TruthRecollection2へとつなげる。

        public bool WinOnceSinikiaKahlHanz { get; set; } // カールハンツDUEL勝利
        public bool WinOnceOlLandis { get; set; } // オル・ランディスDUEL時１回で成功

        public bool TruthAnswerSuccess { get; set; } // ２階、神の詩を歌うのに成功
        public bool TruthAnswerFail { get; set; } // ２階、神の詩を歌うのに失敗
        public bool TruthWillSuccess { get; set; } // ３階、神の呼び声への応対に成功
        public bool TruthWillFail { get; set; } // ３階、神の呼び声への応対に失敗

        public bool TruthBadEnd5 { get; set; } // ２階制覇時のバッドエンド２
        public bool TruthBadEnd4 { get; set; } // ２階制覇時のバッドエンド２
        public bool TruthBadEnd3 { get; set; } // ２階制覇時のバッドエンド２
        public bool TruthBadEnd2 { get; set; } // ２階制覇時のバッドエンド２
        public bool TruthBadEnd1 { get; set; } // １階制覇時のバッドエンド１

        // 素材収集した数を記憶する。
        public int EquipMaterial_11 { get; set; }
        public int EquipMaterial_12 { get; set; }
        public int EquipMaterial_13 { get; set; }
        public int EquipMaterial_14 { get; set; }
        public int EquipMaterial_15 { get; set; }
        public int EquipMaterial_16 { get; set; }
        public int EquipMaterial_17 { get; set; }
        public int EquipMaterial_18 { get; set; }
        public int EquipMaterial_19 { get; set; }
        public int EquipMaterial_110 { get; set; }
        public int EquipMaterial_111 { get; set; }
        public int EquipMaterial_21 { get; set; } // COMMON_WHITE_MAGATAMA
        public int EquipMaterial_22 { get; set; } // COMMON_BLUE_MAGATAMA
        public int EquipMaterial_23 { get; set; } // COMMON_WASI_BLUE_FEATHER
        public int EquipMaterial_24 { get; set; } // COMMON_BLUEWHITE_SHARP_TOGE
        public int EquipMaterial_25 { get; set; } // COMMON_GOTUGOTU_KARA
        public int EquipMaterial_26 { get; set; } // RARE_SEKIKASSYOKU_HASAMI
        public int EquipMaterial_27 { get; set; } // RARE_JOE_ARM
        public int EquipMaterial_28 { get; set; } // COMMON_AOSAME_KENSHI
        public int EquipMaterial_29 { get; set; } // COMMON_KOUSITUKA_MATERIAL
        public int EquipMaterial_210 { get; set; } // COMMON_AOSAME_UROKO
        public int EquipMaterial_211 { get; set; } // COMMON_HALF_TRANSPARENT_ROCK_ASH
        public int EquipMaterial_31 { get; set; } // 雪猫の毛皮
        public int EquipMaterial_32 { get; set; } // リザードの鱗
        public int EquipMaterial_33 { get; set; } // ゴツゴツした棍棒
        public int EquipMaterial_34 { get; set; } // エムブレム・オブ・ペンギン
        public int EquipMaterial_35 { get; set; } // アルゴニアンの紫鱗
        public int EquipMaterial_36 { get; set; } // ベアクローの欠片
        public int EquipMaterial_37 { get; set; } // エッセンス・オヴ・アース
        public int EquipMaterial_38 { get; set; } // ウルフの毛皮
        public int EquipMaterial_39 { get; set; } // 古代ツンドラ鹿の角
        public int EquipMaterial_310 { get; set; } // 古代栄樹の幹の断片
        public int EquipMaterial_41 { get; set; } // ハンターの七つ道具
        public int EquipMaterial_42 { get; set; } // 猛獣の毛皮
        public int EquipMaterial_43 { get; set; } // 執行人の汚れたローブ
        public int EquipMaterial_44 { get; set; } // 天使のシルク
        public int EquipMaterial_45 { get; set; } // 錆付いたガラクタ武具
        public int EquipMaterial_46 { get; set; } // エッセンス・オブ・ダーク
        public int EquipMaterial_47 { get; set; } // シーカーの頭蓋骨
        public int EquipMaterial_48 { get; set; } // マスターブレイドの破片
        public int EquipMaterial_49 { get; set; } // エッセンス・オブ・フレイム
        public int EquipMaterial_410 { get; set; } // 豪華なジュエルクラウン
        public int EquipMaterial_411 { get; set; } // 怨霊箱
        public int EquipMaterial_412 { get; set; } // 組み立て素材　天秤
        public int EquipMaterial_413 { get; set; } // 組み立て素材　天分銅
        public int EquipMaterial_414 { get; set; } // 組み立て素材　天秤棒
        public int EquipMaterial_415 { get; set; } // ドゥームブリンガーの柄
        public int EquipMaterial_416 { get; set; } // ドゥームブリンガーの欠片
        public int EquipMaterial_417 { get; set; } // ドゥームブリンガー
        public int EquipMaterial_418 { get; set; } // 浄火の鍛造
        public int EquipMaterial_419 { get; set; } // エッセンス・オブ・アダマンティ

        public int PotionMaterial_11 { get; set; } 
        public int PotionMaterial_12 { get; set; }
        public int PotionMaterial_13 { get; set; }
        public int PotionMaterial_14 { get; set; }
        public int PotionMaterial_15 { get; set; }
        public int PotionMaterial_16 { get; set; }
        public int PotionMaterial_17 { get; set; }
        public int PotionMaterial_18 { get; set; }
        public int PotionMaterial_19 { get; set; }
        public int PotionMaterial_110 { get; set; }
        public int PotionMaterial_111 { get; set; }
        public int PotionMaterial_21 { get; set; } // COMMON_GANGAME_EGG
        public int PotionMaterial_22 { get; set; } // COMMON_NANAIRO_SYOKUSYU
        public int PotionMaterial_23 { get; set; } // COMMON_EIGHTEIGHT_KUROSUMI
        public int PotionMaterial_31 { get; set; } // 妖精パウダー
        public int PotionMaterial_32 { get; set; } // エッセンス・オブ・ウィンド
        public int PotionMaterial_33 { get; set; } // 凍結した心臓
        public int PotionMaterial_34 { get; set; } // 鋭く尖った虎牙
        public int PotionMaterial_35 { get; set; } // ピュア・クリスタル
        public int PotionMaterial_41 { get; set; } // 血塗られたダガーの破片
        public int PotionMaterial_42 { get; set; } // デーモンホーン
        public int PotionMaterial_43 { get; set; } // エッセンス・オブ・シャイン
        public int PotionMaterial_44 { get; set; } // 黒の印鑑
        public int PotionMaterial_45 { get; set; } // 混沌の雫

        public int FoodMaterial_11 { get; set; }
        public int FoodMaterial_12 { get; set; }
        public int FoodMaterial_13 { get; set; }
        public int FoodMaterial_14 { get; set; }
        public int FoodMaterial_21 { get; set; } // COMMON_DAGGERFISH_UROKO
        public int FoodMaterial_22 { get; set; } // COMMON_SIPPUU_HIRE
        public int FoodMaterial_23 { get; set; } // COMMON_KURIONE_ZOUMOTU
        public int FoodMaterial_24 { get; set; } // COMMON_RENEW_AKAMI
        public int FoodMaterial_25 { get; set; } // COMMON_SEA_WASI_KUTIBASI
        public int FoodMaterial_26 { get; set; } // RARE_JOE_TONGUE
        public int FoodMaterial_27 { get; set; } // RARE_JOE_LEG
        public int FoodMaterial_28 { get; set; } // COMMON_SOFT_BIG_HIRE
        public int FoodMaterial_29 { get; set; } // COMMON_PURE_WHITE_BIGEYE
        public int FoodMaterial_210 { get; set; } // COMMON_EIGHTEIGHT_KYUUBAN
        public int FoodMaterial_31 { get; set; } // 白アザラシの肉
        public int FoodMaterial_32 { get; set; } // 結晶化した海水塩
        public int FoodMaterial_33 { get; set; } // オークのもも肉
        public int FoodMaterial_34 { get; set; } // 赤タマネギ
        public int FoodMaterial_35 { get; set; } // おおきな蹄
        public int FoodMaterial_36 { get; set; } // ホワイト・パウダー
        public int FoodMaterial_37 { get; set; } // 蒼い弾丸の欠片
        public int FoodMaterial_38 { get; set; } // 透明な雪結晶
        public int FoodMaterial_41 { get; set; } // 黒く変色した岩状の塊
        public int FoodMaterial_42 { get; set; } // フェブル・アニス
        public int FoodMaterial_43 { get; set; } // マスターブレイドの残り火
        public int FoodMaterial_44 { get; set; } // スモーキー・ハニー
        public int FoodMaterial_45 { get; set; } // エンジェル・ダスト
        public int FoodMaterial_46 { get; set; } // サン・タラゴン
        public int FoodMaterial_47 { get; set; } // エコービーストのもも肉
        public int FoodMaterial_48 { get; set; } // カオス・ワーデンの舌

        // 素材収集結果、調合できる条件が揃った日を記憶する。
        public int EquipMixtureDay_11 { get; set; }
        public int EquipMixtureDay_12 { get; set; }
        public int EquipMixtureDay_13 { get; set; }
        public int EquipMixtureDay_14 { get; set; }
        public int EquipMixtureDay_15 { get; set; }
        public int EquipMixtureDay_21 { get; set; }
        public int EquipMixtureDay_22 { get; set; }
        public int EquipMixtureDay_23 { get; set; }
        public int EquipMixtureDay_24 { get; set; }
        public int EquipMixtureDay_25 { get; set; }
        public int EquipMixtureDay_26 { get; set; }
        public int EquipMixtureDay_31 { get; set; }
        public int EquipMixtureDay_32 { get; set; }
        public int EquipMixtureDay_33 { get; set; }
        public int EquipMixtureDay_34 { get; set; }
        public int EquipMixtureDay_35 { get; set; }
        public int EquipMixtureDay_36 { get; set; }
        public int EquipMixtureDay_37 { get; set; }
        public int EquipMixtureDay_38 { get; set; }
        public int EquipMixtureDay_41 { get; set; }
        public int EquipMixtureDay_42 { get; set; }
        public int EquipMixtureDay_43 { get; set; }
        public int EquipMixtureDay_44 { get; set; }
        public int EquipMixtureDay_45 { get; set; }
        public int EquipMixtureDay_46 { get; set; }
        public int EquipMixtureDay_47 { get; set; }
        public int EquipMixtureDay_48 { get; set; }
        public int EquipMixtureDay_49 { get; set; }
        public int EquipMixtureDay_410 { get; set; }
        public int EquipMixtureDay_51 { get; set; }
        public int EquipMixtureDay_52 { get; set; }
        public int EquipMixtureDay_53 { get; set; }
        public int EquipMixtureDay_54 { get; set; }
        public int EquipMixtureDay_55 { get; set; }
        public int EquipMixtureDay_56 { get; set; }

        public int PotionMixtureDay_11 { get; set; }
        public int PotionMixtureDay_12 { get; set; }
        public int PotionMixtureDay_13 { get; set; }
        public int PotionMixtureDay_14 { get; set; }
        public int PotionMixtureDay_15 { get; set; }
        public int PotionMixtureDay_21 { get; set; }
        public int PotionMixtureDay_22 { get; set; }
        public int PotionMixtureDay_23 { get; set; }
        public int PotionMixtureDay_31 { get; set; }
        public int PotionMixtureDay_32 { get; set; }
        public int PotionMixtureDay_33 { get; set; }
        public int PotionMixtureDay_41 { get; set; }
        public int PotionMixtureDay_42 { get; set; }
        public int PotionMixtureDay_43 { get; set; }
        public int PotionMixtureDay_44 { get; set; }
        public int PotionMixtureDay_45 { get; set; }
        public int PotionMixtureDay_46 { get; set; }
        public int PotionMixtureDay_47 { get; set; }
        public int PotionMixtureDay_48 { get; set; }
        public int PotionMixtureDay_49 { get; set; }
        public int PotionMixtureDay_410 { get; set; }
        public int PotionMixtureDay_51 { get; set; }
        public int PotionMixtureDay_52 { get; set; }
        public int PotionMixtureDay_53 { get; set; }
        public int PotionMixtureDay_54 { get; set; }

        public int FoodMixtureDay_11 { get; set; }
        public int FoodMixtureDay_12 { get; set; }
        public int FoodMixtureDay_13 { get; set; }
        public int FoodMixtureDay_21 { get; set; }
        public int FoodMixtureDay_22 { get; set; }
        public int FoodMixtureDay_23 { get; set; }
        public int FoodMixtureDay_24 { get; set; }
        public int FoodMixtureDay_31 { get; set; }
        public int FoodMixtureDay_32 { get; set; }
        public int FoodMixtureDay_33 { get; set; }
        public int FoodMixtureDay_34 { get; set; }
        public int FoodMixtureDay_41 { get; set; }
        public int FoodMixtureDay_42 { get; set; }
        public int FoodMixtureDay_43 { get; set; }
        public int FoodMixtureDay_44 { get; set; }
        public int FoodMixtureDay_51 { get; set; }
        public int FoodMixtureDay_52 { get; set; }
        public int FoodMixtureDay_53 { get; set; }
        public int FoodMixtureDay_54 { get; set; }

        // 調合結果を店に展示したかどうかを記憶する。
        public bool EquipAvailable_11 { get; set; }
        public bool EquipAvailable_12 { get; set; }
        public bool EquipAvailable_13 { get; set; }
        public bool EquipAvailable_14 { get; set; }
        public bool EquipAvailable_15 { get; set; }
        public bool EquipAvailable_21 { get; set; }
        public bool EquipAvailable_22 { get; set; }
        public bool EquipAvailable_23 { get; set; }
        public bool EquipAvailable_24 { get; set; }
        public bool EquipAvailable_25 { get; set; }
        public bool EquipAvailable_26 { get; set; }
        public bool EquipAvailable_31 { get; set; }
        public bool EquipAvailable_32 { get; set; }
        public bool EquipAvailable_33 { get; set; }
        public bool EquipAvailable_34 { get; set; }
        public bool EquipAvailable_35 { get; set; }
        public bool EquipAvailable_36 { get; set; }
        public bool EquipAvailable_37 { get; set; }
        public bool EquipAvailable_38 { get; set; }
        public bool EquipAvailable_41 { get; set; }
        public bool EquipAvailable_42 { get; set; }
        public bool EquipAvailable_43 { get; set; }
        public bool EquipAvailable_44 { get; set; }
        public bool EquipAvailable_45 { get; set; }
        public bool EquipAvailable_46 { get; set; }
        public bool EquipAvailable_47 { get; set; }
        public bool EquipAvailable_48 { get; set; }
        public bool EquipAvailable_49 { get; set; }
        public bool EquipAvailable_410 { get; set; }
        public bool EquipAvailable_51 { get; set; }
        public bool EquipAvailable_52 { get; set; }
        public bool EquipAvailable_53 { get; set; }
        public bool EquipAvailable_54 { get; set; }
        public bool EquipAvailable_55 { get; set; }
        public bool EquipAvailable_56 { get; set; }

        public bool PotionAvailable_11 { get; set; }
        public bool PotionAvailable_12 { get; set; }
        public bool PotionAvailable_13 { get; set; }
        public bool PotionAvailable_14 { get; set; }
        public bool PotionAvailable_15 { get; set; }
        public bool PotionAvailable_21 { get; set; }
        public bool PotionAvailable_22 { get; set; }
        public bool PotionAvailable_23 { get; set; }
        public bool PotionAvailable_31 { get; set; }
        public bool PotionAvailable_32 { get; set; }
        public bool PotionAvailable_33 { get; set; }
        public bool PotionAvailable_34 { get; set; }
        public bool PotionAvailable_41 { get; set; }
        public bool PotionAvailable_42 { get; set; }
        public bool PotionAvailable_43 { get; set; }
        public bool PotionAvailable_44 { get; set; }
        public bool PotionAvailable_45 { get; set; }
        public bool PotionAvailable_46 { get; set; }
        public bool PotionAvailable_47 { get; set; }
        public bool PotionAvailable_48 { get; set; }
        public bool PotionAvailable_49 { get; set; }
        public bool PotionAvailable_410 { get; set; }
        public bool PotionAvailable_51 { get; set; }
        public bool PotionAvailable_52 { get; set; }
        public bool PotionAvailable_53 { get; set; }
        public bool PotionAvailable_54 { get; set; }

        public bool FoodAvailable_11 { get; set; }
        public bool FoodAvailable_12 { get; set; }
        public bool FoodAvailable_13 { get; set; }
        public bool FoodAvailable_21 { get; set; }
        public bool FoodAvailable_22 { get; set; }
        public bool FoodAvailable_23 { get; set; }
        public bool FoodAvailable_24 { get; set; }
        public bool FoodAvailable_31 { get; set; }
        public bool FoodAvailable_32 { get; set; }
        public bool FoodAvailable_33 { get; set; }
        public bool FoodAvailable_34 { get; set; }
        public bool FoodAvailable_41 { get; set; }
        public bool FoodAvailable_42 { get; set; }
        public bool FoodAvailable_43 { get; set; }
        public bool FoodAvailable_44 { get; set; }
        public bool FoodAvailable_51 { get; set; }
        public bool FoodAvailable_52 { get; set; }
        public bool FoodAvailable_53 { get; set; }
        public bool FoodAvailable_54 { get; set; }
    }
}
