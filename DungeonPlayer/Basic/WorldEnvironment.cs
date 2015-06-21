using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public class WorldEnvironment
    {
        // 640 x 480ƒŒƒCƒAƒEƒg‚©‚ç1024 x 768‚Ö•ÏX‚µ‚½ÛAƒvƒŒƒCƒ„[‚Ìƒ_ƒ“ƒWƒ‡ƒ“ˆÊ’uî•ñ‚ÌŒİŠ·«‚ª
        // •Û‚Ä‚È‚­‚È‚Á‚Ä‚µ‚Ü‚Á‚½‚½‚ßA¡Œãƒo[ƒWƒ‡ƒ“ŠÇ—‚ğs‚¤‚±‚Æ‚ÅŒİŠ·‚ğ•Û‚ÂB
        protected int version = 0; // Œã•Ò’Ç‰Á

        // ƒ_ƒ“ƒWƒ‡ƒ“‹AŠÒŒãAƒ_ƒ“ƒWƒ‡ƒ“‹AŠÒŒã‚ÌƒCƒxƒ“ƒgŠ®—¹ŒãAh‰®h”‘Œã‚ÌƒZ[ƒuŒãAƒ_ƒ“ƒWƒ‡ƒ“o”­’¼‘O‚ÌƒZ[ƒuŒãA
        // ‚È‚ÇAƒZ[ƒuEƒ[ƒh‚ğs‚¤–‚ÅƒCƒxƒ“ƒg‚ªi‚ñ‚Å‚µ‚Ü‚¤Œ‚ğC³‚·‚é‚½‚ß‚Ìƒtƒ‰ƒO
        protected bool alreadyShownEvent = false; // Œã•Ò’Ç‰Á

        #region "‘O•Ò"
        protected int gameDay; // ƒQ[ƒ€“à‚ÅŒo‰ß‚µ‚½“ú”
        protected bool saveByDungeon; // ƒ_ƒ“ƒWƒ‡ƒ““à‚ÅƒZ[ƒu
        protected int dungeonPosX; // ƒ_ƒ“ƒWƒ‡ƒ““à‚ÅƒZ[ƒu‚µ‚½êŠX
        protected int dungeonPosY; // ƒ_ƒ“ƒWƒ‡ƒ““à‚ÅƒZ[ƒu‚µ‚½êŠY
        protected int dungeonPosX2; // ƒ_ƒ“ƒWƒ‡ƒ“ˆÚ“®‰ü‘PŒã‚ÌƒZ[ƒu‚µ‚½êŠX
        protected int dungeonPosY2; // ƒ_ƒ“ƒWƒ‡ƒ“ˆÚ“®‰ü‘PŒã‚ÌƒZ[ƒu‚µ‚½êŠY
        protected bool alreadyRest; // ƒz[ƒ€ƒ^ƒEƒ“‚Å‹x‘§Ï‚İ
        protected bool alreadyCommunicate; // ƒz[ƒ€ƒ^ƒEƒ“‚Åƒ‰ƒi‚Æ‰ï˜bÏ
        protected bool alreadyEquipShop; // ƒz[ƒ€ƒ^ƒEƒ“‚ÅƒKƒ“ƒc‚Æ‰ï˜bÏ
        protected bool oneDeny; // ƒ‰ƒiƒCƒxƒ“ƒg‚P
        protected bool communicationFirstContact2; // ƒ‰ƒiƒCƒxƒ“ƒg‚Q
        protected bool communicationSuccess2; // ƒ‰ƒiƒCƒxƒ“ƒg‚Q|‚Q
        protected bool availableEquipShop; // ƒKƒ“ƒc•‹ï‰®ƒI[ƒvƒ“iƒ_ƒ“ƒWƒ‡ƒ“‚PŠK—p‚ğ‰ğ‹Öj
        protected bool availableEquipShop2; // ƒKƒ“ƒc•‹ï‰®ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK—p‚ğ‰ğ‹Ö
        protected bool availableEquipShop3; // ƒKƒ“ƒc•‹ï‰®ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK—p‚ğ‰ğ‹Ö
        protected bool availableEquipShop4; // ƒKƒ“ƒc•‹ï‰®ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK—p‚ğ‰ğ‹Ö
        protected bool availableEquipShop5; // ƒKƒ“ƒc•‹ï‰®ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK—p‚ğ‰ğ‹Ö
        protected bool availableFirstCharacter; // ‚Pl–Ú‚Ìƒp[ƒeƒB’Ç‰Á
        protected bool availableSecondCharacter; // ‚Ql–Ú‚Ìƒp[ƒeƒB’Ç‰Á
        protected bool availableThirdCharacter; // ‚Rl–Ú‚Ìƒp[ƒeƒB’Ç‰Á
        protected bool communicationThirdChara1; // ƒ”ƒFƒ‹ƒ[ƒCƒxƒ“ƒg‚P
        protected bool communicationEnterFourArea; // ‚SŠK‘wn‚ß‚ÄŒü‚©‚¤‚ÌƒCƒxƒ“ƒg
        protected bool availableItemSort; // ƒAƒCƒeƒ€ƒ\[ƒg‰Â”\

        // [î•ñ]F”z—ñ‚ª—Ç‚¢‚©‚à’m‚ê‚È‚¢‚ªA‚w‚l‚k˜AŒg‚Æ‰Â“Ç«‚ğd‹‚·‚é–‚Æ‚·‚éB
        #region "ˆê”Ê‰ï˜b—p"
        // ƒ‰ƒiˆê”Ê‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationLana1;
        protected bool communicationLana2;
        protected bool communicationLana3;
        protected bool communicationLana4;
        protected bool communicationLana5;
        protected bool communicationLana6;
        protected bool communicationLana7;
        protected bool communicationLana8;
        protected bool communicationLana9;
        protected bool communicationLana10;
        protected bool communicationLana11;
        protected bool communicationLana12;
        protected bool communicationLana13;
        protected bool communicationLana14;
        protected bool communicationLana15;
        protected bool communicationLana16;
        protected bool communicationLana17;
        protected bool communicationLana18;
        protected bool communicationLana19;
        protected bool communicationLana20;
        protected bool communicationLana21;
        protected bool communicationLana22;
        protected bool communicationLana23;
        protected bool communicationLana24;
        protected bool communicationLana25;
        protected bool communicationLana26;
        protected bool communicationLana27;
        protected bool communicationLana28;
        protected bool communicationLana29;
        protected bool communicationLana30;
        protected bool communicationLana31;
        protected bool communicationLana32;
        protected bool communicationLana33;
        protected bool communicationLana34;
        protected bool communicationLana35;
        protected bool communicationLana36;
        protected bool communicationLana37;
        protected bool communicationLana38;
        protected bool communicationLana39;
        protected bool communicationLana40;
        protected bool communicationLana41;
        protected bool communicationLana42;
        protected bool communicationLana43;
        protected bool communicationLana44;
        protected bool communicationLana45;
        protected bool communicationLana46;
        protected bool communicationLana47;
        protected bool communicationLana48;
        protected bool communicationLana49;
        protected bool communicationLana50;
        protected bool communicationLana51;
        protected bool communicationLana52;
        protected bool communicationLana53;
        protected bool communicationLana54;
        protected bool communicationLana55;
        protected bool communicationLana56;
        protected bool communicationLana57;
        protected bool communicationLana58;
        protected bool communicationLana59;
        protected bool communicationLana60;
        protected bool communicationLana61;
        protected bool communicationLana62;
        protected bool communicationLana63;
        protected bool communicationLana64;
        protected bool communicationLana65;
        protected bool communicationLana66;
        protected bool communicationLana67;
        protected bool communicationLana68;
        protected bool communicationLana69;
        protected bool communicationLana70;
        protected bool communicationLana71;
        protected bool communicationLana72;
        protected bool communicationLana73;
        protected bool communicationLana74;
        protected bool communicationLana75;
        protected bool communicationLana76;
        protected bool communicationLana77;
        protected bool communicationLana78;
        protected bool communicationLana79;
        protected bool communicationLana80;
        protected bool communicationLana81;
        protected bool communicationLana82;
        protected bool communicationLana83;
        protected bool communicationLana84;
        protected bool communicationLana85;
        protected bool communicationLana86;
        protected bool communicationLana87;
        protected bool communicationLana88;
        protected bool communicationLana89;
        protected bool communicationLana90;
        protected bool communicationLana91;
        protected bool communicationLana92;
        protected bool communicationLana93;
        protected bool communicationLana94;
        protected bool communicationLana95;
        protected bool communicationLana96;
        protected bool communicationLana97;
        protected bool communicationLana98;
        protected bool communicationLana99;
        protected bool communicationLana100;

        // ƒKƒ“ƒcˆê”Ê‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationGanz1;
        protected bool communicationGanz2;
        protected bool communicationGanz3;
        protected bool communicationGanz4;
        protected bool communicationGanz5;
        protected bool communicationGanz6;
        protected bool communicationGanz7;
        protected bool communicationGanz8;
        protected bool communicationGanz9;
        protected bool communicationGanz10;
        protected bool communicationGanz11;
        protected bool communicationGanz12;
        protected bool communicationGanz13;
        protected bool communicationGanz14;
        protected bool communicationGanz15;
        protected bool communicationGanz16;
        protected bool communicationGanz17;
        protected bool communicationGanz18;
        protected bool communicationGanz19;
        protected bool communicationGanz20;
        protected bool communicationGanz21;
        protected bool communicationGanz22;
        protected bool communicationGanz23;
        protected bool communicationGanz24;
        protected bool communicationGanz25;
        protected bool communicationGanz26;
        protected bool communicationGanz27;
        protected bool communicationGanz28;
        protected bool communicationGanz29;
        protected bool communicationGanz30;
        protected bool communicationGanz31;
        protected bool communicationGanz32;
        protected bool communicationGanz33;
        protected bool communicationGanz34;
        protected bool communicationGanz35;
        protected bool communicationGanz36;
        protected bool communicationGanz37;
        protected bool communicationGanz38;
        protected bool communicationGanz39;
        protected bool communicationGanz40;
        protected bool communicationGanz41;
        protected bool communicationGanz42;
        protected bool communicationGanz43;
        protected bool communicationGanz44;
        protected bool communicationGanz45;
        protected bool communicationGanz46;
        protected bool communicationGanz47;
        protected bool communicationGanz48;
        protected bool communicationGanz49;
        protected bool communicationGanz50;
        protected bool communicationGanz51;
        protected bool communicationGanz52;
        protected bool communicationGanz53;
        protected bool communicationGanz54;
        protected bool communicationGanz55;
        protected bool communicationGanz56;
        protected bool communicationGanz57;
        protected bool communicationGanz58;
        protected bool communicationGanz59;
        protected bool communicationGanz60;
        protected bool communicationGanz61;
        protected bool communicationGanz62;
        protected bool communicationGanz63;
        protected bool communicationGanz64;
        protected bool communicationGanz65;
        protected bool communicationGanz66;
        protected bool communicationGanz67;
        protected bool communicationGanz68;
        protected bool communicationGanz69;
        protected bool communicationGanz70;
        protected bool communicationGanz71;
        protected bool communicationGanz72;
        protected bool communicationGanz73;
        protected bool communicationGanz74;
        protected bool communicationGanz75;
        protected bool communicationGanz76;
        protected bool communicationGanz77;
        protected bool communicationGanz78;
        protected bool communicationGanz79;
        protected bool communicationGanz80;
        protected bool communicationGanz81;
        protected bool communicationGanz82;
        protected bool communicationGanz83;
        protected bool communicationGanz84;
        protected bool communicationGanz85;
        protected bool communicationGanz86;
        protected bool communicationGanz87;
        protected bool communicationGanz88;
        protected bool communicationGanz89;
        protected bool communicationGanz90;
        protected bool communicationGanz91;
        protected bool communicationGanz92;
        protected bool communicationGanz93;
        protected bool communicationGanz94;
        protected bool communicationGanz95;
        protected bool communicationGanz96;
        protected bool communicationGanz97;
        protected bool communicationGanz98;
        protected bool communicationGanz99;
        protected bool communicationGanz100;

        // ƒnƒ“ƒiˆê”Ê‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationHanna1;
        protected bool communicationHanna2;
        protected bool communicationHanna3;
        protected bool communicationHanna4;
        protected bool communicationHanna5;
        protected bool communicationHanna6;
        protected bool communicationHanna7;
        protected bool communicationHanna8;
        protected bool communicationHanna9;
        protected bool communicationHanna10;
        protected bool communicationHanna11;
        protected bool communicationHanna12;
        protected bool communicationHanna13;
        protected bool communicationHanna14;
        protected bool communicationHanna15;
        protected bool communicationHanna16;
        protected bool communicationHanna17;
        protected bool communicationHanna18;
        protected bool communicationHanna19;
        protected bool communicationHanna20;
        protected bool communicationHanna21;
        protected bool communicationHanna22;
        protected bool communicationHanna23;
        protected bool communicationHanna24;
        protected bool communicationHanna25;
        protected bool communicationHanna26;
        protected bool communicationHanna27;
        protected bool communicationHanna28;
        protected bool communicationHanna29;
        protected bool communicationHanna30;
        protected bool communicationHanna31;
        protected bool communicationHanna32;
        protected bool communicationHanna33;
        protected bool communicationHanna34;
        protected bool communicationHanna35;
        protected bool communicationHanna36;
        protected bool communicationHanna37;
        protected bool communicationHanna38;
        protected bool communicationHanna39;
        protected bool communicationHanna40;
        protected bool communicationHanna41;
        protected bool communicationHanna42;
        protected bool communicationHanna43;
        protected bool communicationHanna44;
        protected bool communicationHanna45;
        protected bool communicationHanna46;
        protected bool communicationHanna47;
        protected bool communicationHanna48;
        protected bool communicationHanna49;
        protected bool communicationHanna50;
        protected bool communicationHanna51;
        protected bool communicationHanna52;
        protected bool communicationHanna53;
        protected bool communicationHanna54;
        protected bool communicationHanna55;
        protected bool communicationHanna56;
        protected bool communicationHanna57;
        protected bool communicationHanna58;
        protected bool communicationHanna59;
        protected bool communicationHanna60;
        protected bool communicationHanna61;
        protected bool communicationHanna62;
        protected bool communicationHanna63;
        protected bool communicationHanna64;
        protected bool communicationHanna65;
        protected bool communicationHanna66;
        protected bool communicationHanna67;
        protected bool communicationHanna68;
        protected bool communicationHanna69;
        protected bool communicationHanna70;
        protected bool communicationHanna71;
        protected bool communicationHanna72;
        protected bool communicationHanna73;
        protected bool communicationHanna74;
        protected bool communicationHanna75;
        protected bool communicationHanna76;
        protected bool communicationHanna77;
        protected bool communicationHanna78;
        protected bool communicationHanna79;
        protected bool communicationHanna80;
        protected bool communicationHanna81;
        protected bool communicationHanna82;
        protected bool communicationHanna83;
        protected bool communicationHanna84;
        protected bool communicationHanna85;
        protected bool communicationHanna86;
        protected bool communicationHanna87;
        protected bool communicationHanna88;
        protected bool communicationHanna89;
        protected bool communicationHanna90;
        protected bool communicationHanna91;
        protected bool communicationHanna92;
        protected bool communicationHanna93;
        protected bool communicationHanna94;
        protected bool communicationHanna95;
        protected bool communicationHanna96;
        protected bool communicationHanna97;
        protected bool communicationHanna98;
        protected bool communicationHanna99;
        protected bool communicationHanna100;
        #endregion

        protected bool alreadyLvUpEmpty11;
        protected bool alreadyLvUpEmpty12;
        protected bool alreadyLvUpEmpty13;
        protected bool alreadyLvUpEmpty14;
        protected bool alreadyLvUpEmpty15;
        protected bool alreadyLvUpEmpty21;
        protected bool alreadyLvUpEmpty22;
        protected bool alreadyLvUpEmpty23;
        protected bool alreadyLvUpEmpty24;
        protected bool alreadyLvUpEmpty25;
        protected bool alreadyLvUpEmpty31;
        protected bool alreadyLvUpEmpty32;
        protected bool alreadyLvUpEmpty33;
        protected bool alreadyLvUpEmpty34;
        protected bool alreadyLvUpEmpty35;

        protected bool treasure1; // ‚PŠKF•ó” ‚P
        protected bool treasure2; // ‚PŠKF•ó” ‚Q
        protected bool treasure3; // ‚PŠKF•ó” ‚R
        protected bool treasure4; // ‚QŠKF•ó” ‚P
        protected bool treasure5; // ‚QŠKF•ó” ‚Q
        protected bool treasure6; // ‚QŠKF•ó” ‚R
        protected bool treasure7; // ‚QŠKF•ó” ‚S
        protected bool treasure8; // ‚RŠKF•ó” ‚P
        protected bool treasure9; // ‚RŠKF•ó” ‚Q
        protected bool treasure10; // ‚RŠKF•ó” ‚R
        protected bool treasure11; // ‚RŠKF•ó” ‚S
        protected bool treasure12; // ‚RŠKF•ó” ‚T
        protected bool treasure121; // ‚RŠKF•ó” ‚U
        protected bool treasure122; // ‚RŠKF•ó” ‚V
        protected bool treasure123; // ‚RŠKF•ó” ‚W
        protected bool treasure41; // ‚SŠKF•ó” ‚P
        protected bool treasure42; // ‚SŠKF•ó” ‚Q
        protected bool treasure43; // ‚SŠKF•ó” ‚R
        protected bool treasure44; // ‚SŠKF•ó” ‚S
        protected bool treasure45; // ‚SŠKF•ó” ‚T
        protected bool treasure46; // ‚SŠKF•ó” ‚U
        protected bool treasure47; // ‚SŠKF•ó” ‚V
        protected bool treasure48; // ‚SŠKF•ó” ‚W
        protected bool treasure49; // ‚SŠKF•ó” ‚X
        protected bool treasure51; // ‚TŠKF•ó” ‚P
        protected bool treasure52; // ‚TŠKF•ó” ‚Q
        protected bool treasure53; // ‚TŠKF•ó” ‚R
        protected bool treasure54; // ‚TŠKF•ó” ‚S
        protected bool treasure55; // ‚TŠKF•ó” ‚T
        protected bool treasure56; // ‚TŠKF•ó” ‚U
        protected bool treasure57; // ‚TŠKF•ó” ‚V

        protected int dungeonArea; // ƒ_ƒ“ƒWƒ‡ƒ“‰½ŠK‚ÅƒZ[ƒu‚µ‚½‚©‚ğ¦‚·’l
        protected bool completeSlayBoss1; // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK‚Ìƒ{ƒX‚ğŒ‚”j
        protected bool completeSlayBoss2; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìƒ{ƒX‚ğŒ‚”j
        protected bool completeSlayBoss3; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ{ƒX‚ğŒ‚”j
        protected bool completeSlayBoss4; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ìƒ{ƒX‚ğŒ‚”j
        protected bool completeSlayBoss5; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK‚Ìƒ{ƒX‚ğŒ‚”j
        protected bool completeArea1; // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK‚ğ§”eÏ
        protected bool completeArea2; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ğ§”eÏ
        protected bool completeArea3; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚ğ§”eÏ
        protected bool completeArea4; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚ğ§”eÏ
        protected bool completeArea5; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK‚ğ§”eÏ
        protected int completeArea1Day; // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK§”e‚µ‚½“ú
        protected int completeArea2Day; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK§”e‚µ‚½“ú
        protected int completeArea3Day; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK§”e‚µ‚½“ú
        protected int completeArea4Day; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK§”e‚µ‚½“ú
        protected int completeArea5Day; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK§”e‚µ‚½“ú
        protected bool communicationCompArea1; // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationCompArea2; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationCompArea3; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationCompArea4; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationCompArea5; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        protected bool communicationFirstHomeTown; // ‰‚ß‚Äƒz[ƒ€ƒ^ƒEƒ“‚É–ß‚Á‚½‚Ì‹­§‰ï˜bƒCƒxƒ“ƒg

        protected bool truthWord1; // ^À‚ÌŒ¾—t‚P‚ğ‰{——‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
        protected bool truthWord2; // ^À‚ÌŒ¾—t‚Q‚ğ‰{——‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
        protected bool truthWord3; // ^À‚ÌŒ¾—t‚R‚ğ‰{——‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
        protected bool truthWord4; // ^À‚ÌŒ¾—t‚S‚ğ‰{——‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
        protected bool truthWord5; // ^À‚ÌŒ¾—t‚T‚ğ‰{——‚µ‚Ä‚¢‚é‚©‚Ç‚¤‚©
        protected bool trueEnding1; // ^ƒGƒ“ƒfƒBƒ“ƒO‚Ì‚½‚ß‚Ì‰ï˜bAƒNƒŠƒAÏ‚Å‚PŠK§”e‚Å”­¶‰Â”\

        protected bool infoArea11; // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK‚Ì‰B‚µ’Ê˜H”­Œ©
        protected bool infoArea21; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚P
        protected bool infoArea22; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚Q
        protected bool infoArea221;// ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚Q|‚P
        protected bool infoArea222;// ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚Q|‚Q
        protected bool infoArea23; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚R
        protected bool infoArea24; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚S
        protected bool infoArea240; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚S|‚O
        protected bool infoArea25; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚T
        protected bool infoArea26; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìî•ñ‚»‚Ì‚U
        protected bool solveArea21; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚PƒNƒŠƒA
        protected bool solveArea22; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚QƒNƒŠƒA
        protected bool solveArea221; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚Q|‚PƒNƒŠƒA
        protected bool solveArea222; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚Q|‚QƒNƒŠƒA
        protected bool solveArea23; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚RƒNƒŠƒA
        protected bool solveArea24; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚SƒNƒŠƒA
        protected bool solveArea25; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚TƒNƒŠƒA
        protected bool solveArea26; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚UƒNƒŠƒA
        protected bool failArea221; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚Q|‚P¸”sŒoŒ±‚ ‚è
        protected bool failArea222; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚Q|‚Q¸”sŒoŒ±‚ ‚è
        protected bool failArea23; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚R¸”sŒoŒ±‚ ‚è
        protected bool failArea24; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚S¸”sŒoŒ±‚ ‚è
        protected bool failArea241; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚S¸”sŒoŒ±‚Q‰ñ–Ú
        protected bool failArea242; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚S¸”sŒoŒ±‚R‰ñ–Ú
        protected bool failArea26; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚U¸”sŒoŒ±‚P‰ñ–Ú
        protected bool failArea261; //  ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚U¸”sŒoŒ±‚Q‰ñ–Ú
        protected bool failArea262; //  ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚U¸”sŒoŒ±‚R‰ñ–Ú
        protected bool failArea263; //  ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚U¸”sŒoŒ±‚S‰ñ–Ú
        protected bool failArea264; //  ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚U¸”sŒoŒ±‚T‰ñ–Ú
        protected bool progressArea241; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚P
        protected bool progressArea2412; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚P
        protected bool progressArea2413; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚P
        protected bool progressArea242; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚Q
        protected bool progressArea2422; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚Q
        protected bool progressArea243; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚R
        protected bool progressArea2432; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚R
        protected bool progressArea244; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚S
        protected bool progressArea2442; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚S
        protected bool progressArea245; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚T
        protected bool progressArea2452; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚T
        protected bool progressArea246; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚S‚U
        protected bool progressArea26; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U
        protected bool progressArea261; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P
        protected bool progressArea262; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚Q
        protected bool progressArea263; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚R
        protected bool progressArea264; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚S
        protected bool progressArea265; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚T
        protected bool progressArea266; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚U
        protected bool progressArea267; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚V
        protected bool progressArea268; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚W
        protected bool progressArea269; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚X
        protected bool progressArea2610; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚O
        protected bool progressArea2611; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚P
        protected bool progressArea2612; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚Q
        protected bool progressArea2613; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚R
        protected bool progressArea2614; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚S
        protected bool progressArea2615; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚T
        protected bool progressArea2616; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H‚U‚P‚U
        protected bool firstProcessArea24; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì’Ê‰ßŒo˜H’Tõ‚P‰ñ–Ú
        protected bool completeArea21; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚PŠ®—¹
        protected bool completeArea22; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚QŠ®—¹
        protected bool completeArea23; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚RŠ®—¹
        protected bool completeArea24; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚SŠ®—¹
        protected bool completeArea25; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚TŠ®—¹
        protected bool completeArea26; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ÌƒGƒŠƒA‚»‚Ì‚UŠ®—¹
        protected bool infoArea27; // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ì‰B‚µ’Ê˜H”­Œ©
        protected bool infoArea31; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvî•ñ‚P
        protected bool infoArea311s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚P‚P
        protected bool infoArea311e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚P‚P
        protected bool infoArea312s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚P‚Q
        protected bool infoArea312e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚P‚Q
        protected bool infoArea313s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚P‚R
        protected bool infoArea313e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚P‚R
        protected bool infoArea324s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚S
        protected bool infoArea324e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚S
        protected bool infoArea325s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚T
        protected bool infoArea325e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚T
        protected bool infoArea326s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚U
        protected bool infoArea326e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚U
        protected bool infoArea327s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚V
        protected bool infoArea327e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚V
        protected bool infoArea328s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚W
        protected bool infoArea328e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚W
        protected bool infoArea329s;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚X
        protected bool infoArea329e;  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚X
        protected bool infoArea3210s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚O
        protected bool infoArea3210e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚O
        protected bool infoArea3211s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚P
        protected bool infoArea3211e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚P
        protected bool infoArea3212s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚Q
        protected bool infoArea3212e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚Q
        protected bool infoArea3213s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚R
        protected bool infoArea3213e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚R
        protected bool infoArea3214s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚S
        protected bool infoArea3214e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒgî•ñ‚Q‚P‚S
        protected bool failArea321; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ“ñŠÖ–å¸”s‚P
        protected bool failArea322; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ“ñŠÖ–å¸”s‚Q
        protected bool failArea323; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ“ñŠÖ–å¸”s‚R
        protected bool failArea3241; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ“ñŠÖ–å¸”s‚S‚P
        protected bool failArea3242; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ“ñŠÖ–å¸”s‚S‚Q
        protected bool failArea3243; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ“ñŠÖ–å¸”s‚S‚R
        protected bool completeArea32; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‚S“’B—pŠÅ”Â
        protected bool infoArea3315s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚P‚T
        protected bool infoArea3315e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚P‚T
        protected bool infoArea3316s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚P‚U
        protected bool infoArea3316e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚P‚U
        protected bool infoArea3317s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚P‚V
        protected bool infoArea3317e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚P‚V
        protected bool infoArea3318s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚P‚W
        protected bool infoArea3318e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚P‚W
        protected bool infoArea3319s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚P‚X
        protected bool infoArea3319e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚P‚X
        protected bool infoArea3320s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚O
        protected bool infoArea3320e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚O
        protected bool infoArea3321s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚P
        protected bool infoArea3321e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚P
        protected bool infoArea3322s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚Q
        protected bool infoArea3322e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚Q
        protected bool infoArea3323s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚R
        protected bool infoArea3323e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚R
        protected bool infoArea3324s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚S
        protected bool infoArea3324e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚S
        protected bool infoArea3325s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚T
        protected bool infoArea3325e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚T
        protected bool infoArea3326s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚U
        protected bool infoArea3326e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚U
        protected bool infoArea3327s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚V
        protected bool infoArea3327e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚V
        protected bool infoArea3328s; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvŠJnƒ|ƒCƒ“ƒg‚R‚Q‚W
        protected bool infoArea3328e; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒvI—¹ƒ|ƒCƒ“ƒg‚R‚Q‚W
        protected bool progressArea3316; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚P‚U
        protected bool progressArea3317; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚P‚V
        protected bool progressArea3318; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚P‚W
        protected bool progressArea3319; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚P‚X
        protected bool progressArea3320; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚O
        protected bool progressArea3321; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚P
        protected bool progressArea3322; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚Q
        protected bool progressArea3323; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚R
        protected bool progressArea3324; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚S
        protected bool progressArea3325; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚T
        protected bool progressArea3326; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚U
        protected bool progressArea3327; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv’Ê‰ßŒo˜H‚R‚Q‚V
        protected bool failArea331; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚P‰ñ–Ú
        protected bool failArea332; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚Q‰ñ–Ú
        protected bool failArea333; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚R‰ñ–Ú
        protected bool failArea334; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚S‰ñ–Ú
        protected bool failArea335; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚T‰ñ–Ú
        protected bool failArea336; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚U‰ñ–Ú
        protected bool failArea337; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¸”s‚V‰ñ–Ú
        protected bool completeArea33; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æOŠÖ–å¬Œ÷
        protected bool infoArea34; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ‚SŠÖ–åî•ñ
        protected bool solveArea34; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘æ‚SŠÖ–å‰ğŒˆ
        protected bool completeArea34; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘ælŠÖ–åI—¹
        protected bool completeJumpArea34; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ[ƒv‘ælŠÖ–åƒWƒƒƒ“ƒv
        protected bool infoArea35; // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ì‰B‚µ’Ê˜H”­Œ©
        protected bool infoArea41; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P
        protected bool infoArea42; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚Q
        protected bool infoArea43; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚R
        protected bool infoArea44; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚S
        protected bool infoArea45; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚T
        protected bool infoArea46; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚U
        protected bool infoArea47; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚V
        protected bool infoArea48; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚W
        protected bool infoArea49; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚X
        protected bool infoArea410; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚O
        protected bool infoArea411; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚P
        protected bool infoArea412; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚Q
        protected bool infoArea413; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚R
        protected bool infoArea414; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚S
        protected bool infoArea415; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚T
        protected bool infoArea416; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚U
        protected bool infoArea417; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚V
        protected bool infoArea418; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚W
        protected bool infoArea419; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚P‚X
        protected bool infoArea420; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‰ï˜b‚Q‚O
        protected bool progressArea4211; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‹ß“¹‘I‘ğ‚É‚æ‚é¸”sŒo‰ß‚P
        protected bool progressArea4212; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‹ß“¹‘I‘ğ‚É‚æ‚é¸”sŒo‰ß‚Q
        protected bool failArea4211; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‹ß“¹‘I‘ğ‚É‚æ‚é¸”s‚P
        protected bool failArea4212; // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ì‹ß“¹‘I‘ğ‚É‚æ‚é¸”s‚Q
        protected bool infoArea51; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK“’BA‹­§’¬–ß‚è
        protected bool infoArea52; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠKƒ‰ƒXƒ{ƒX’¼‘O‰ï˜b
        protected bool infoArea53; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK‰B‚µ’Ê˜H”­Œ©

        protected bool specialInfo1; // ‚Qü–Ú“Ë“üŒãA‚s‚q‚t‚d‚d‚m‚cƒgƒŠƒK[‚Æ‚È‚éƒXƒyƒVƒƒƒ‹î•ñ‚P
        protected bool specialInfo2; // ‚Qü–Ú“Ë“üŒãA‚s‚q‚t‚d‚d‚m‚cƒgƒŠƒK[‚Æ‚È‚éƒXƒyƒVƒƒƒ‹î•ñ‚Q
        protected bool specialInfo3; // ‚Qü–Ú“Ë“üŒãA‚s‚q‚t‚d‚d‚m‚cƒgƒŠƒK[‚Æ‚È‚éƒXƒyƒVƒƒƒ‹î•ñ‚R
        protected bool specialInfo4; // ‚Qü–Ú“Ë“üŒãA‚s‚q‚t‚d‚d‚m‚cƒgƒŠƒK[‚Æ‚È‚éƒXƒyƒVƒƒƒ‹î•ñ‚S
        protected bool defeatVerze; // ‚Qü–Ú“Ë“üŒãAƒ”ƒFƒ‹ƒ[‚ğŒ‚”j‚µ‚Ä‚¢‚é–‚É‚æ‚é^Eƒ‰ƒXƒ{ƒX‹­‰»‚Ìƒtƒ‰ƒO
        protected bool specialTreasure1; // ‚Qü–Ú“Ë“üŒã‚ÌƒXƒyƒVƒƒƒ‹ƒAƒCƒeƒ€“üèƒtƒ‰ƒO
        protected bool truthEventForLana; // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠKAuƒ‰ƒi‚ÌƒCƒ„ƒŠƒ“ƒOv‚ğæ“¾
        protected bool enterSecondGame; // ‚Qü–Ú“Ë“üƒtƒ‰ƒO

        protected bool alreadyUseSyperSaintWater = false;
        protected bool alreadyUseRevivePotion = false;
        protected bool alreadyUsePureWater = false; // Œã•Ò’Ç‰Á

        // s Œã•Ò’Ç‰Á
        public int Version
        {
            get { return version; }
            set { version = value; }
        }
        // e Œã•Ò’Ç‰Á

        // s Œã•Ò’Ç‰Á
        public bool AlreadyShownEvent
        {
            get { return alreadyShownEvent; }
            set { alreadyShownEvent = value; }
        }
        // e Œã•Ò’Ç‰Á

        public int GameDay
        {
            get { return gameDay; }
            set { gameDay = value; }
        }
        public bool SaveByDungeon
        {
            get { return saveByDungeon; }
            set { saveByDungeon = value; }
        }
        public int DungeonPosX
        {
            get { return dungeonPosX; }
            set { dungeonPosX = value; }
        }
        public int DungeonPosY
        {
            get { return dungeonPosY; }
            set { dungeonPosY = value; }
        }
        public int DungeonPosX2
        {
            get { return dungeonPosX2; }
            set { dungeonPosX2 = value; }
        }
        public int DungeonPosY2
        {
            get { return dungeonPosY2; }
            set { dungeonPosY2 = value; }
        }
        public bool AlreadyCommunicate
        {
            get { return alreadyCommunicate; }
            set { alreadyCommunicate = value; }
        }
        public bool AlreadyRest
        {
            get { return alreadyRest; }
            set { alreadyRest = value; }
        }
        public bool AlreadyEquipShop
        {
            get { return alreadyEquipShop; }
            set { alreadyEquipShop = value; }
        }
        public bool OneDeny
        {
            get { return oneDeny; }
            set { oneDeny = value; }
        }
        public bool CommunicationFirstContact2
        {
            get { return communicationFirstContact2; }
            set { communicationFirstContact2 = value; }
        }
        public bool CommunicationSuccess2
        {
            get { return communicationSuccess2; }
            set { communicationSuccess2 = value; }
        }



        public bool CommunicationLana1
        {
            get { return communicationLana1; }
            set { communicationLana1 = value; }
        }
        public bool CommunicationLana2
        {
            get { return communicationLana2; }
            set { communicationLana2 = value; }
        }
        public bool CommunicationLana3
        {
            get { return communicationLana3; }
            set { communicationLana3 = value; }
        }
        public bool CommunicationLana4
        {
            get { return communicationLana4; }
            set { communicationLana4 = value; }
        }
        public bool CommunicationLana5
        {
            get { return communicationLana5; }
            set { communicationLana5 = value; }
        }
        public bool CommunicationLana6
        {
            get { return communicationLana6; }
            set { communicationLana6 = value; }
        }
        public bool CommunicationLana7
        {
            get { return communicationLana7; }
            set { communicationLana7 = value; }
        }
        public bool CommunicationLana8
        {
            get { return communicationLana8; }
            set { communicationLana8 = value; }
        }
        public bool CommunicationLana9
        {
            get { return communicationLana9; }
            set { communicationLana9 = value; }
        }
        public bool CommunicationLana10
        {
            get { return communicationLana10; }
            set { communicationLana10 = value; }
        }
        public bool CommunicationLana11
        {
            get { return communicationLana11; }
            set { communicationLana11 = value; }
        }
        public bool CommunicationLana12
        {
            get { return communicationLana12; }
            set { communicationLana12 = value; }
        }
        public bool CommunicationLana13
        {
            get { return communicationLana13; }
            set { communicationLana13 = value; }
        }
        public bool CommunicationLana14
        {
            get { return communicationLana14; }
            set { communicationLana14 = value; }
        }
        public bool CommunicationLana15
        {
            get { return communicationLana15; }
            set { communicationLana15 = value; }
        }
        public bool CommunicationLana16
        {
            get { return communicationLana16; }
            set { communicationLana16 = value; }
        }
        public bool CommunicationLana17
        {
            get { return communicationLana17; }
            set { communicationLana17 = value; }
        }
        public bool CommunicationLana18
        {
            get { return communicationLana18; }
            set { communicationLana18 = value; }
        }
        public bool CommunicationLana19
        {
            get { return communicationLana19; }
            set { communicationLana19 = value; }
        }
        public bool CommunicationLana20
        {
            get { return communicationLana20; }
            set { communicationLana20 = value; }
        }
        public bool CommunicationLana21
        {
            get { return communicationLana21; }
            set { communicationLana21 = value; }
        }
        public bool CommunicationLana22
        {
            get { return communicationLana22; }
            set { communicationLana22 = value; }
        }
        public bool CommunicationLana23
        {
            get { return communicationLana23; }
            set { communicationLana23 = value; }
        }
        public bool CommunicationLana24
        {
            get { return communicationLana24; }
            set { communicationLana24 = value; }
        }
        public bool CommunicationLana25
        {
            get { return communicationLana25; }
            set { communicationLana25 = value; }
        }
        public bool CommunicationLana26
        {
            get { return communicationLana26; }
            set { communicationLana26 = value; }
        }
        public bool CommunicationLana27
        {
            get { return communicationLana27; }
            set { communicationLana27 = value; }
        }
        public bool CommunicationLana28
        {
            get { return communicationLana28; }
            set { communicationLana28 = value; }
        }
        public bool CommunicationLana29
        {
            get { return communicationLana29; }
            set { communicationLana29 = value; }
        }
        public bool CommunicationLana30
        {
            get { return communicationLana30; }
            set { communicationLana30 = value; }
        }
        public bool CommunicationLana31
        {
            get { return communicationLana31; }
            set { communicationLana31 = value; }
        }
        public bool CommunicationLana32
        {
            get { return communicationLana32; }
            set { communicationLana32 = value; }
        }
        public bool CommunicationLana33
        {
            get { return communicationLana33; }
            set { communicationLana33 = value; }
        }
        public bool CommunicationLana34
        {
            get { return communicationLana34; }
            set { communicationLana34 = value; }
        }
        public bool CommunicationLana35
        {
            get { return communicationLana35; }
            set { communicationLana35 = value; }
        }
        public bool CommunicationLana36
        {
            get { return communicationLana36; }
            set { communicationLana36 = value; }
        }
        public bool CommunicationLana37
        {
            get { return communicationLana37; }
            set { communicationLana37 = value; }
        }
        public bool CommunicationLana38
        {
            get { return communicationLana38; }
            set { communicationLana38 = value; }
        }
        public bool CommunicationLana39
        {
            get { return communicationLana39; }
            set { communicationLana39 = value; }
        }
        public bool CommunicationLana40
        {
            get { return communicationLana40; }
            set { communicationLana40 = value; }
        }
        public bool CommunicationLana41
        {
            get { return communicationLana41; }
            set { communicationLana41 = value; }
        }
        public bool CommunicationLana42
        {
            get { return communicationLana42; }
            set { communicationLana42 = value; }
        }
        public bool CommunicationLana43
        {
            get { return communicationLana43; }
            set { communicationLana43 = value; }
        }
        public bool CommunicationLana44
        {
            get { return communicationLana44; }
            set { communicationLana44 = value; }
        }
        public bool CommunicationLana45
        {
            get { return communicationLana45; }
            set { communicationLana45 = value; }
        }
        public bool CommunicationLana46
        {
            get { return communicationLana46; }
            set { communicationLana46 = value; }
        }
        public bool CommunicationLana47
        {
            get { return communicationLana47; }
            set { communicationLana47 = value; }
        }
        public bool CommunicationLana48
        {
            get { return communicationLana48; }
            set { communicationLana48 = value; }
        }
        public bool CommunicationLana49
        {
            get { return communicationLana49; }
            set { communicationLana49 = value; }
        }
        public bool CommunicationLana50
        {
            get { return communicationLana50; }
            set { communicationLana50 = value; }
        }
        public bool CommunicationLana51
        {
            get { return communicationLana51; }
            set { communicationLana51 = value; }
        }
        public bool CommunicationLana52
        {
            get { return communicationLana52; }
            set { communicationLana52 = value; }
        }
        public bool CommunicationLana53
        {
            get { return communicationLana53; }
            set { communicationLana53 = value; }
        }
        public bool CommunicationLana54
        {
            get { return communicationLana54; }
            set { communicationLana54 = value; }
        }
        public bool CommunicationLana55
        {
            get { return communicationLana55; }
            set { communicationLana55 = value; }
        }
        public bool CommunicationLana56
        {
            get { return communicationLana56; }
            set { communicationLana56 = value; }
        }
        public bool CommunicationLana57
        {
            get { return communicationLana57; }
            set { communicationLana57 = value; }
        }
        public bool CommunicationLana58
        {
            get { return communicationLana58; }
            set { communicationLana58 = value; }
        }
        public bool CommunicationLana59
        {
            get { return communicationLana59; }
            set { communicationLana59 = value; }
        }
        public bool CommunicationLana60
        {
            get { return communicationLana60; }
            set { communicationLana60 = value; }
        }
        public bool CommunicationLana61
        {
            get { return communicationLana61; }
            set { communicationLana61 = value; }
        }
        public bool CommunicationLana62
        {
            get { return communicationLana62; }
            set { communicationLana62 = value; }
        }
        public bool CommunicationLana63
        {
            get { return communicationLana63; }
            set { communicationLana63 = value; }
        }
        public bool CommunicationLana64
        {
            get { return communicationLana64; }
            set { communicationLana64 = value; }
        }
        public bool CommunicationLana65
        {
            get { return communicationLana65; }
            set { communicationLana65 = value; }
        }
        public bool CommunicationLana66
        {
            get { return communicationLana66; }
            set { communicationLana66 = value; }
        }
        public bool CommunicationLana67
        {
            get { return communicationLana67; }
            set { communicationLana67 = value; }
        }
        public bool CommunicationLana68
        {
            get { return communicationLana68; }
            set { communicationLana68 = value; }
        }
        public bool CommunicationLana69
        {
            get { return communicationLana69; }
            set { communicationLana69 = value; }
        }
        public bool CommunicationLana70
        {
            get { return communicationLana70; }
            set { communicationLana70 = value; }
        }
        public bool CommunicationLana71
        {
            get { return communicationLana71; }
            set { communicationLana71 = value; }
        }
        public bool CommunicationLana72
        {
            get { return communicationLana72; }
            set { communicationLana72 = value; }
        }
        public bool CommunicationLana73
        {
            get { return communicationLana73; }
            set { communicationLana73 = value; }
        }
        public bool CommunicationLana74
        {
            get { return communicationLana74; }
            set { communicationLana74 = value; }
        }
        public bool CommunicationLana75
        {
            get { return communicationLana75; }
            set { communicationLana75 = value; }
        }
        public bool CommunicationLana76
        {
            get { return communicationLana76; }
            set { communicationLana76 = value; }
        }
        public bool CommunicationLana77
        {
            get { return communicationLana77; }
            set { communicationLana77 = value; }
        }
        public bool CommunicationLana78
        {
            get { return communicationLana78; }
            set { communicationLana78 = value; }
        }
        public bool CommunicationLana79
        {
            get { return communicationLana79; }
            set { communicationLana79 = value; }
        }
        public bool CommunicationLana80
        {
            get { return communicationLana80; }
            set { communicationLana80 = value; }
        }
        public bool CommunicationLana81
        {
            get { return communicationLana81; }
            set { communicationLana81 = value; }
        }
        public bool CommunicationLana82
        {
            get { return communicationLana82; }
            set { communicationLana82 = value; }
        }
        public bool CommunicationLana83
        {
            get { return communicationLana83; }
            set { communicationLana83 = value; }
        }
        public bool CommunicationLana84
        {
            get { return communicationLana84; }
            set { communicationLana84 = value; }
        }
        public bool CommunicationLana85
        {
            get { return communicationLana85; }
            set { communicationLana85 = value; }
        }
        public bool CommunicationLana86
        {
            get { return communicationLana86; }
            set { communicationLana86 = value; }
        }
        public bool CommunicationLana87
        {
            get { return communicationLana87; }
            set { communicationLana87 = value; }
        }
        public bool CommunicationLana88
        {
            get { return communicationLana88; }
            set { communicationLana88 = value; }
        }
        public bool CommunicationLana89
        {
            get { return communicationLana89; }
            set { communicationLana89 = value; }
        }
        public bool CommunicationLana90
        {
            get { return communicationLana90; }
            set { communicationLana90 = value; }
        }
        public bool CommunicationLana91
        {
            get { return communicationLana91; }
            set { communicationLana91 = value; }
        }
        public bool CommunicationLana92
        {
            get { return communicationLana92; }
            set { communicationLana92 = value; }
        }
        public bool CommunicationLana93
        {
            get { return communicationLana93; }
            set { communicationLana93 = value; }
        }
        public bool CommunicationLana94
        {
            get { return communicationLana94; }
            set { communicationLana94 = value; }
        }
        public bool CommunicationLana95
        {
            get { return communicationLana95; }
            set { communicationLana95 = value; }
        }
        public bool CommunicationLana96
        {
            get { return communicationLana96; }
            set { communicationLana96 = value; }
        }
        public bool CommunicationLana97
        {
            get { return communicationLana97; }
            set { communicationLana97 = value; }
        }
        public bool CommunicationLana98
        {
            get { return communicationLana98; }
            set { communicationLana98 = value; }
        }
        public bool CommunicationLana99
        {
            get { return communicationLana99; }
            set { communicationLana99 = value; }
        }
        public bool CommunicationLana100
        {
            get { return communicationLana100; }
            set { communicationLana100 = value; }
        }

        public bool CommunicationGanz1
        {
            get { return communicationGanz1; }
            set { communicationGanz1 = value; }
        }
        public bool CommunicationGanz2
        {
            get { return communicationGanz2; }
            set { communicationGanz2 = value; }
        }
        public bool CommunicationGanz3
        {
            get { return communicationGanz3; }
            set { communicationGanz3 = value; }
        }
        public bool CommunicationGanz4
        {
            get { return communicationGanz4; }
            set { communicationGanz4 = value; }
        }
        public bool CommunicationGanz5
        {
            get { return communicationGanz5; }
            set { communicationGanz5 = value; }
        }
        public bool CommunicationGanz6
        {
            get { return communicationGanz6; }
            set { communicationGanz6 = value; }
        }
        public bool CommunicationGanz7
        {
            get { return communicationGanz7; }
            set { communicationGanz7 = value; }
        }
        public bool CommunicationGanz8
        {
            get { return communicationGanz8; }
            set { communicationGanz8 = value; }
        }
        public bool CommunicationGanz9
        {
            get { return communicationGanz9; }
            set { communicationGanz9 = value; }
        }
        public bool CommunicationGanz10
        {
            get { return communicationGanz10; }
            set { communicationGanz10 = value; }
        }
        public bool CommunicationGanz11
        {
            get { return communicationGanz11; }
            set { communicationGanz11 = value; }
        }
        public bool CommunicationGanz12
        {
            get { return communicationGanz12; }
            set { communicationGanz12 = value; }
        }
        public bool CommunicationGanz13
        {
            get { return communicationGanz13; }
            set { communicationGanz13 = value; }
        }
        public bool CommunicationGanz14
        {
            get { return communicationGanz14; }
            set { communicationGanz14 = value; }
        }
        public bool CommunicationGanz15
        {
            get { return communicationGanz15; }
            set { communicationGanz15 = value; }
        }
        public bool CommunicationGanz16
        {
            get { return communicationGanz16; }
            set { communicationGanz16 = value; }
        }
        public bool CommunicationGanz17
        {
            get { return communicationGanz17; }
            set { communicationGanz17 = value; }
        }
        public bool CommunicationGanz18
        {
            get { return communicationGanz18; }
            set { communicationGanz18 = value; }
        }
        public bool CommunicationGanz19
        {
            get { return communicationGanz19; }
            set { communicationGanz19 = value; }
        }
        public bool CommunicationGanz20
        {
            get { return communicationGanz20; }
            set { communicationGanz20 = value; }
        }
        public bool CommunicationGanz21
        {
            get { return communicationGanz21; }
            set { communicationGanz21 = value; }
        }
        public bool CommunicationGanz22
        {
            get { return communicationGanz22; }
            set { communicationGanz22 = value; }
        }
        public bool CommunicationGanz23
        {
            get { return communicationGanz23; }
            set { communicationGanz23 = value; }
        }
        public bool CommunicationGanz24
        {
            get { return communicationGanz24; }
            set { communicationGanz24 = value; }
        }
        public bool CommunicationGanz25
        {
            get { return communicationGanz25; }
            set { communicationGanz25 = value; }
        }
        public bool CommunicationGanz26
        {
            get { return communicationGanz26; }
            set { communicationGanz26 = value; }
        }
        public bool CommunicationGanz27
        {
            get { return communicationGanz27; }
            set { communicationGanz27 = value; }
        }
        public bool CommunicationGanz28
        {
            get { return communicationGanz28; }
            set { communicationGanz28 = value; }
        }
        public bool CommunicationGanz29
        {
            get { return communicationGanz29; }
            set { communicationGanz29 = value; }
        }
        public bool CommunicationGanz30
        {
            get { return communicationGanz30; }
            set { communicationGanz30 = value; }
        }
        public bool CommunicationGanz31
        {
            get { return communicationGanz31; }
            set { communicationGanz31 = value; }
        }
        public bool CommunicationGanz32
        {
            get { return communicationGanz32; }
            set { communicationGanz32 = value; }
        }
        public bool CommunicationGanz33
        {
            get { return communicationGanz33; }
            set { communicationGanz33 = value; }
        }
        public bool CommunicationGanz34
        {
            get { return communicationGanz34; }
            set { communicationGanz34 = value; }
        }
        public bool CommunicationGanz35
        {
            get { return communicationGanz35; }
            set { communicationGanz35 = value; }
        }
        public bool CommunicationGanz36
        {
            get { return communicationGanz36; }
            set { communicationGanz36 = value; }
        }
        public bool CommunicationGanz37
        {
            get { return communicationGanz37; }
            set { communicationGanz37 = value; }
        }
        public bool CommunicationGanz38
        {
            get { return communicationGanz38; }
            set { communicationGanz38 = value; }
        }
        public bool CommunicationGanz39
        {
            get { return communicationGanz39; }
            set { communicationGanz39 = value; }
        }
        public bool CommunicationGanz40
        {
            get { return communicationGanz40; }
            set { communicationGanz40 = value; }
        }
        public bool CommunicationGanz41
        {
            get { return communicationGanz41; }
            set { communicationGanz41 = value; }
        }
        public bool CommunicationGanz42
        {
            get { return communicationGanz42; }
            set { communicationGanz42 = value; }
        }
        public bool CommunicationGanz43
        {
            get { return communicationGanz43; }
            set { communicationGanz43 = value; }
        }
        public bool CommunicationGanz44
        {
            get { return communicationGanz44; }
            set { communicationGanz44 = value; }
        }
        public bool CommunicationGanz45
        {
            get { return communicationGanz45; }
            set { communicationGanz45 = value; }
        }
        public bool CommunicationGanz46
        {
            get { return communicationGanz46; }
            set { communicationGanz46 = value; }
        }
        public bool CommunicationGanz47
        {
            get { return communicationGanz47; }
            set { communicationGanz47 = value; }
        }
        public bool CommunicationGanz48
        {
            get { return communicationGanz48; }
            set { communicationGanz48 = value; }
        }
        public bool CommunicationGanz49
        {
            get { return communicationGanz49; }
            set { communicationGanz49 = value; }
        }
        public bool CommunicationGanz50
        {
            get { return communicationGanz50; }
            set { communicationGanz50 = value; }
        }
        public bool CommunicationGanz51
        {
            get { return communicationGanz51; }
            set { communicationGanz51 = value; }
        }
        public bool CommunicationGanz52
        {
            get { return communicationGanz52; }
            set { communicationGanz52 = value; }
        }
        public bool CommunicationGanz53
        {
            get { return communicationGanz53; }
            set { communicationGanz53 = value; }
        }
        public bool CommunicationGanz54
        {
            get { return communicationGanz54; }
            set { communicationGanz54 = value; }
        }
        public bool CommunicationGanz55
        {
            get { return communicationGanz55; }
            set { communicationGanz55 = value; }
        }
        public bool CommunicationGanz56
        {
            get { return communicationGanz56; }
            set { communicationGanz56 = value; }
        }
        public bool CommunicationGanz57
        {
            get { return communicationGanz57; }
            set { communicationGanz57 = value; }
        }
        public bool CommunicationGanz58
        {
            get { return communicationGanz58; }
            set { communicationGanz58 = value; }
        }
        public bool CommunicationGanz59
        {
            get { return communicationGanz59; }
            set { communicationGanz59 = value; }
        }
        public bool CommunicationGanz60
        {
            get { return communicationGanz60; }
            set { communicationGanz60 = value; }
        }
        public bool CommunicationGanz61
        {
            get { return communicationGanz61; }
            set { communicationGanz61 = value; }
        }
        public bool CommunicationGanz62
        {
            get { return communicationGanz62; }
            set { communicationGanz62 = value; }
        }
        public bool CommunicationGanz63
        {
            get { return communicationGanz63; }
            set { communicationGanz63 = value; }
        }
        public bool CommunicationGanz64
        {
            get { return communicationGanz64; }
            set { communicationGanz64 = value; }
        }
        public bool CommunicationGanz65
        {
            get { return communicationGanz65; }
            set { communicationGanz65 = value; }
        }
        public bool CommunicationGanz66
        {
            get { return communicationGanz66; }
            set { communicationGanz66 = value; }
        }
        public bool CommunicationGanz67
        {
            get { return communicationGanz67; }
            set { communicationGanz67 = value; }
        }
        public bool CommunicationGanz68
        {
            get { return communicationGanz68; }
            set { communicationGanz68 = value; }
        }
        public bool CommunicationGanz69
        {
            get { return communicationGanz69; }
            set { communicationGanz69 = value; }
        }
        public bool CommunicationGanz70
        {
            get { return communicationGanz70; }
            set { communicationGanz70 = value; }
        }
        public bool CommunicationGanz71
        {
            get { return communicationGanz71; }
            set { communicationGanz71 = value; }
        }
        public bool CommunicationGanz72
        {
            get { return communicationGanz72; }
            set { communicationGanz72 = value; }
        }
        public bool CommunicationGanz73
        {
            get { return communicationGanz73; }
            set { communicationGanz73 = value; }
        }
        public bool CommunicationGanz74
        {
            get { return communicationGanz74; }
            set { communicationGanz74 = value; }
        }
        public bool CommunicationGanz75
        {
            get { return communicationGanz75; }
            set { communicationGanz75 = value; }
        }
        public bool CommunicationGanz76
        {
            get { return communicationGanz76; }
            set { communicationGanz76 = value; }
        }
        public bool CommunicationGanz77
        {
            get { return communicationGanz77; }
            set { communicationGanz77 = value; }
        }
        public bool CommunicationGanz78
        {
            get { return communicationGanz78; }
            set { communicationGanz78 = value; }
        }
        public bool CommunicationGanz79
        {
            get { return communicationGanz79; }
            set { communicationGanz79 = value; }
        }
        public bool CommunicationGanz80
        {
            get { return communicationGanz80; }
            set { communicationGanz80 = value; }
        }
        public bool CommunicationGanz81
        {
            get { return communicationGanz81; }
            set { communicationGanz81 = value; }
        }
        public bool CommunicationGanz82
        {
            get { return communicationGanz82; }
            set { communicationGanz82 = value; }
        }
        public bool CommunicationGanz83
        {
            get { return communicationGanz83; }
            set { communicationGanz83 = value; }
        }
        public bool CommunicationGanz84
        {
            get { return communicationGanz84; }
            set { communicationGanz84 = value; }
        }
        public bool CommunicationGanz85
        {
            get { return communicationGanz85; }
            set { communicationGanz85 = value; }
        }
        public bool CommunicationGanz86
        {
            get { return communicationGanz86; }
            set { communicationGanz86 = value; }
        }
        public bool CommunicationGanz87
        {
            get { return communicationGanz87; }
            set { communicationGanz87 = value; }
        }
        public bool CommunicationGanz88
        {
            get { return communicationGanz88; }
            set { communicationGanz88 = value; }
        }
        public bool CommunicationGanz89
        {
            get { return communicationGanz89; }
            set { communicationGanz89 = value; }
        }
        public bool CommunicationGanz90
        {
            get { return communicationGanz90; }
            set { communicationGanz90 = value; }
        }
        public bool CommunicationGanz91
        {
            get { return communicationGanz91; }
            set { communicationGanz91 = value; }
        }
        public bool CommunicationGanz92
        {
            get { return communicationGanz92; }
            set { communicationGanz92 = value; }
        }
        public bool CommunicationGanz93
        {
            get { return communicationGanz93; }
            set { communicationGanz93 = value; }
        }
        public bool CommunicationGanz94
        {
            get { return communicationGanz94; }
            set { communicationGanz94 = value; }
        }
        public bool CommunicationGanz95
        {
            get { return communicationGanz95; }
            set { communicationGanz95 = value; }
        }
        public bool CommunicationGanz96
        {
            get { return communicationGanz96; }
            set { communicationGanz96 = value; }
        }
        public bool CommunicationGanz97
        {
            get { return communicationGanz97; }
            set { communicationGanz97 = value; }
        }
        public bool CommunicationGanz98
        {
            get { return communicationGanz98; }
            set { communicationGanz98 = value; }
        }
        public bool CommunicationGanz99
        {
            get { return communicationGanz99; }
            set { communicationGanz99 = value; }
        }
        public bool CommunicationGanz100
        {
            get { return communicationGanz100; }
            set { communicationGanz100 = value; }
        }

        public bool CommunicationHanna1
        {
            get { return communicationHanna1; }
            set { communicationHanna1 = value; }
        }
        public bool CommunicationHanna2
        {
            get { return communicationHanna2; }
            set { communicationHanna2 = value; }
        }
        public bool CommunicationHanna3
        {
            get { return communicationHanna3; }
            set { communicationHanna3 = value; }
        }
        public bool CommunicationHanna4
        {
            get { return communicationHanna4; }
            set { communicationHanna4 = value; }
        }
        public bool CommunicationHanna5
        {
            get { return communicationHanna5; }
            set { communicationHanna5 = value; }
        }
        public bool CommunicationHanna6
        {
            get { return communicationHanna6; }
            set { communicationHanna6 = value; }
        }
        public bool CommunicationHanna7
        {
            get { return communicationHanna7; }
            set { communicationHanna7 = value; }
        }
        public bool CommunicationHanna8
        {
            get { return communicationHanna8; }
            set { communicationHanna8 = value; }
        }
        public bool CommunicationHanna9
        {
            get { return communicationHanna9; }
            set { communicationHanna9 = value; }
        }
        public bool CommunicationHanna10
        {
            get { return communicationHanna10; }
            set { communicationHanna10 = value; }
        }
        public bool CommunicationHanna11
        {
            get { return communicationHanna11; }
            set { communicationHanna11 = value; }
        }
        public bool CommunicationHanna12
        {
            get { return communicationHanna12; }
            set { communicationHanna12 = value; }
        }
        public bool CommunicationHanna13
        {
            get { return communicationHanna13; }
            set { communicationHanna13 = value; }
        }
        public bool CommunicationHanna14
        {
            get { return communicationHanna14; }
            set { communicationHanna14 = value; }
        }
        public bool CommunicationHanna15
        {
            get { return communicationHanna15; }
            set { communicationHanna15 = value; }
        }
        public bool CommunicationHanna16
        {
            get { return communicationHanna16; }
            set { communicationHanna16 = value; }
        }
        public bool CommunicationHanna17
        {
            get { return communicationHanna17; }
            set { communicationHanna17 = value; }
        }
        public bool CommunicationHanna18
        {
            get { return communicationHanna18; }
            set { communicationHanna18 = value; }
        }
        public bool CommunicationHanna19
        {
            get { return communicationHanna19; }
            set { communicationHanna19 = value; }
        }
        public bool CommunicationHanna20
        {
            get { return communicationHanna20; }
            set { communicationHanna20 = value; }
        }
        public bool CommunicationHanna21
        {
            get { return communicationHanna21; }
            set { communicationHanna21 = value; }
        }
        public bool CommunicationHanna22
        {
            get { return communicationHanna22; }
            set { communicationHanna22 = value; }
        }
        public bool CommunicationHanna23
        {
            get { return communicationHanna23; }
            set { communicationHanna23 = value; }
        }
        public bool CommunicationHanna24
        {
            get { return communicationHanna24; }
            set { communicationHanna24 = value; }
        }
        public bool CommunicationHanna25
        {
            get { return communicationHanna25; }
            set { communicationHanna25 = value; }
        }
        public bool CommunicationHanna26
        {
            get { return communicationHanna26; }
            set { communicationHanna26 = value; }
        }
        public bool CommunicationHanna27
        {
            get { return communicationHanna27; }
            set { communicationHanna27 = value; }
        }
        public bool CommunicationHanna28
        {
            get { return communicationHanna28; }
            set { communicationHanna28 = value; }
        }
        public bool CommunicationHanna29
        {
            get { return communicationHanna29; }
            set { communicationHanna29 = value; }
        }
        public bool CommunicationHanna30
        {
            get { return communicationHanna30; }
            set { communicationHanna30 = value; }
        }
        public bool CommunicationHanna31
        {
            get { return communicationHanna31; }
            set { communicationHanna31 = value; }
        }
        public bool CommunicationHanna32
        {
            get { return communicationHanna32; }
            set { communicationHanna32 = value; }
        }
        public bool CommunicationHanna33
        {
            get { return communicationHanna33; }
            set { communicationHanna33 = value; }
        }
        public bool CommunicationHanna34
        {
            get { return communicationHanna34; }
            set { communicationHanna34 = value; }
        }
        public bool CommunicationHanna35
        {
            get { return communicationHanna35; }
            set { communicationHanna35 = value; }
        }
        public bool CommunicationHanna36
        {
            get { return communicationHanna36; }
            set { communicationHanna36 = value; }
        }
        public bool CommunicationHanna37
        {
            get { return communicationHanna37; }
            set { communicationHanna37 = value; }
        }
        public bool CommunicationHanna38
        {
            get { return communicationHanna38; }
            set { communicationHanna38 = value; }
        }
        public bool CommunicationHanna39
        {
            get { return communicationHanna39; }
            set { communicationHanna39 = value; }
        }
        public bool CommunicationHanna40
        {
            get { return communicationHanna40; }
            set { communicationHanna40 = value; }
        }
        public bool CommunicationHanna41
        {
            get { return communicationHanna41; }
            set { communicationHanna41 = value; }
        }
        public bool CommunicationHanna42
        {
            get { return communicationHanna42; }
            set { communicationHanna42 = value; }
        }
        public bool CommunicationHanna43
        {
            get { return communicationHanna43; }
            set { communicationHanna43 = value; }
        }
        public bool CommunicationHanna44
        {
            get { return communicationHanna44; }
            set { communicationHanna44 = value; }
        }
        public bool CommunicationHanna45
        {
            get { return communicationHanna45; }
            set { communicationHanna45 = value; }
        }
        public bool CommunicationHanna46
        {
            get { return communicationHanna46; }
            set { communicationHanna46 = value; }
        }
        public bool CommunicationHanna47
        {
            get { return communicationHanna47; }
            set { communicationHanna47 = value; }
        }
        public bool CommunicationHanna48
        {
            get { return communicationHanna48; }
            set { communicationHanna48 = value; }
        }
        public bool CommunicationHanna49
        {
            get { return communicationHanna49; }
            set { communicationHanna49 = value; }
        }
        public bool CommunicationHanna50
        {
            get { return communicationHanna50; }
            set { communicationHanna50 = value; }
        }
        public bool CommunicationHanna51
        {
            get { return communicationHanna51; }
            set { communicationHanna51 = value; }
        }
        public bool CommunicationHanna52
        {
            get { return communicationHanna52; }
            set { communicationHanna52 = value; }
        }
        public bool CommunicationHanna53
        {
            get { return communicationHanna53; }
            set { communicationHanna53 = value; }
        }
        public bool CommunicationHanna54
        {
            get { return communicationHanna54; }
            set { communicationHanna54 = value; }
        }
        public bool CommunicationHanna55
        {
            get { return communicationHanna55; }
            set { communicationHanna55 = value; }
        }
        public bool CommunicationHanna56
        {
            get { return communicationHanna56; }
            set { communicationHanna56 = value; }
        }
        public bool CommunicationHanna57
        {
            get { return communicationHanna57; }
            set { communicationHanna57 = value; }
        }
        public bool CommunicationHanna58
        {
            get { return communicationHanna58; }
            set { communicationHanna58 = value; }
        }
        public bool CommunicationHanna59
        {
            get { return communicationHanna59; }
            set { communicationHanna59 = value; }
        }
        public bool CommunicationHanna60
        {
            get { return communicationHanna60; }
            set { communicationHanna60 = value; }
        }
        public bool CommunicationHanna61
        {
            get { return communicationHanna61; }
            set { communicationHanna61 = value; }
        }
        public bool CommunicationHanna62
        {
            get { return communicationHanna62; }
            set { communicationHanna62 = value; }
        }
        public bool CommunicationHanna63
        {
            get { return communicationHanna63; }
            set { communicationHanna63 = value; }
        }
        public bool CommunicationHanna64
        {
            get { return communicationHanna64; }
            set { communicationHanna64 = value; }
        }
        public bool CommunicationHanna65
        {
            get { return communicationHanna65; }
            set { communicationHanna65 = value; }
        }
        public bool CommunicationHanna66
        {
            get { return communicationHanna66; }
            set { communicationHanna66 = value; }
        }
        public bool CommunicationHanna67
        {
            get { return communicationHanna67; }
            set { communicationHanna67 = value; }
        }
        public bool CommunicationHanna68
        {
            get { return communicationHanna68; }
            set { communicationHanna68 = value; }
        }
        public bool CommunicationHanna69
        {
            get { return communicationHanna69; }
            set { communicationHanna69 = value; }
        }
        public bool CommunicationHanna70
        {
            get { return communicationHanna70; }
            set { communicationHanna70 = value; }
        }
        public bool CommunicationHanna71
        {
            get { return communicationHanna71; }
            set { communicationHanna71 = value; }
        }
        public bool CommunicationHanna72
        {
            get { return communicationHanna72; }
            set { communicationHanna72 = value; }
        }
        public bool CommunicationHanna73
        {
            get { return communicationHanna73; }
            set { communicationHanna73 = value; }
        }
        public bool CommunicationHanna74
        {
            get { return communicationHanna74; }
            set { communicationHanna74 = value; }
        }
        public bool CommunicationHanna75
        {
            get { return communicationHanna75; }
            set { communicationHanna75 = value; }
        }
        public bool CommunicationHanna76
        {
            get { return communicationHanna76; }
            set { communicationHanna76 = value; }
        }
        public bool CommunicationHanna77
        {
            get { return communicationHanna77; }
            set { communicationHanna77 = value; }
        }
        public bool CommunicationHanna78
        {
            get { return communicationHanna78; }
            set { communicationHanna78 = value; }
        }
        public bool CommunicationHanna79
        {
            get { return communicationHanna79; }
            set { communicationHanna79 = value; }
        }
        public bool CommunicationHanna80
        {
            get { return communicationHanna80; }
            set { communicationHanna80 = value; }
        }
        public bool CommunicationHanna81
        {
            get { return communicationHanna81; }
            set { communicationHanna81 = value; }
        }
        public bool CommunicationHanna82
        {
            get { return communicationHanna82; }
            set { communicationHanna82 = value; }
        }
        public bool CommunicationHanna83
        {
            get { return communicationHanna83; }
            set { communicationHanna83 = value; }
        }
        public bool CommunicationHanna84
        {
            get { return communicationHanna84; }
            set { communicationHanna84 = value; }
        }
        public bool CommunicationHanna85
        {
            get { return communicationHanna85; }
            set { communicationHanna85 = value; }
        }
        public bool CommunicationHanna86
        {
            get { return communicationHanna86; }
            set { communicationHanna86 = value; }
        }
        public bool CommunicationHanna87
        {
            get { return communicationHanna87; }
            set { communicationHanna87 = value; }
        }
        public bool CommunicationHanna88
        {
            get { return communicationHanna88; }
            set { communicationHanna88 = value; }
        }
        public bool CommunicationHanna89
        {
            get { return communicationHanna89; }
            set { communicationHanna89 = value; }
        }
        public bool CommunicationHanna90
        {
            get { return communicationHanna90; }
            set { communicationHanna90 = value; }
        }
        public bool CommunicationHanna91
        {
            get { return communicationHanna91; }
            set { communicationHanna91 = value; }
        }
        public bool CommunicationHanna92
        {
            get { return communicationHanna92; }
            set { communicationHanna92 = value; }
        }
        public bool CommunicationHanna93
        {
            get { return communicationHanna93; }
            set { communicationHanna93 = value; }
        }
        public bool CommunicationHanna94
        {
            get { return communicationHanna94; }
            set { communicationHanna94 = value; }
        }
        public bool CommunicationHanna95
        {
            get { return communicationHanna95; }
            set { communicationHanna95 = value; }
        }
        public bool CommunicationHanna96
        {
            get { return communicationHanna96; }
            set { communicationHanna96 = value; }
        }
        public bool CommunicationHanna97
        {
            get { return communicationHanna97; }
            set { communicationHanna97 = value; }
        }
        public bool CommunicationHanna98
        {
            get { return communicationHanna98; }
            set { communicationHanna98 = value; }
        }
        public bool CommunicationHanna99
        {
            get { return communicationHanna99; }
            set { communicationHanna99 = value; }
        }
        public bool CommunicationHanna100
        {
            get { return communicationHanna100; }
            set { communicationHanna100 = value; }
        }


        
        public bool AvailableEquipShop
        {
            get { return availableEquipShop; }
            set { availableEquipShop = value; }
        }
        public bool AvailableEquipShop2
        {
            get { return availableEquipShop2; }
            set { availableEquipShop2 = value; }
        }
        public bool AvailableEquipShop3
        {
            get { return availableEquipShop3; }
            set { availableEquipShop3 = value; }
        }
        public bool AvailableEquipShop4
        {
            get { return availableEquipShop4; }
            set { availableEquipShop4 = value; }
        }
        public bool AvailableEquipShop5
        {
            get { return availableEquipShop5; }
            set { availableEquipShop5 = value; }
        }

        public bool AvailableFirstCharacter
        {
            get { return availableFirstCharacter; }
            set { availableFirstCharacter = value; }
        }
        public bool AvailableSecondCharacter
        {
            get { return availableSecondCharacter; }
            set { availableSecondCharacter = value; }
        }
        public bool AvailableThirdCharacter
        {
            get { return availableThirdCharacter; }
            set { availableThirdCharacter = value; }
        }
        
        public bool CommunicationThirdChara1
        {
            get { return communicationThirdChara1; }
            set { communicationThirdChara1 = value; }
        }

        public bool CommunicationEnterFourArea
        {
            get { return communicationEnterFourArea; }
            set { communicationEnterFourArea = value; }
        }
        public bool AvailableItemSort
        {
            get { return availableItemSort; }
            set { availableItemSort = value; }
        }

        public bool AlreadyLvUpEmpty11
        {
            get { return alreadyLvUpEmpty11; }
            set { alreadyLvUpEmpty11 = value; }
        }
        public bool AlreadyLvUpEmpty12
        {
            get { return alreadyLvUpEmpty12; }
            set { alreadyLvUpEmpty12 = value; }
        }
        public bool AlreadyLvUpEmpty13
        {
            get { return alreadyLvUpEmpty13; }
            set { alreadyLvUpEmpty13 = value; }
        }
        public bool AlreadyLvUpEmpty14
        {
            get { return alreadyLvUpEmpty14; }
            set { alreadyLvUpEmpty14 = value; }
        }
        public bool AlreadyLvUpEmpty15
        {
            get { return alreadyLvUpEmpty15; }
            set { alreadyLvUpEmpty15 = value; }
        }
        public bool AlreadyLvUpEmpty21
        {
            get { return alreadyLvUpEmpty21; }
            set { alreadyLvUpEmpty21 = value; }
        }
        public bool AlreadyLvUpEmpty22
        {
            get { return alreadyLvUpEmpty22; }
            set { alreadyLvUpEmpty22 = value; }
        }
        public bool AlreadyLvUpEmpty23
        {
            get { return alreadyLvUpEmpty23; }
            set { alreadyLvUpEmpty23 = value; }
        }
        public bool AlreadyLvUpEmpty24
        {
            get { return alreadyLvUpEmpty24; }
            set { alreadyLvUpEmpty24 = value; }
        }
        public bool AlreadyLvUpEmpty25
        {
            get { return alreadyLvUpEmpty25; }
            set { alreadyLvUpEmpty25 = value; }
        }
        public bool AlreadyLvUpEmpty31
        {
            get { return alreadyLvUpEmpty31; }
            set { alreadyLvUpEmpty31 = value; }
        }
        public bool AlreadyLvUpEmpty32
        {
            get { return alreadyLvUpEmpty32; }
            set { alreadyLvUpEmpty32 = value; }
        }
        public bool AlreadyLvUpEmpty33
        {
            get { return alreadyLvUpEmpty33; }
            set { alreadyLvUpEmpty33 = value; }
        }
        public bool AlreadyLvUpEmpty34
        {
            get { return alreadyLvUpEmpty34; }
            set { alreadyLvUpEmpty34 = value; }
        }
        public bool AlreadyLvUpEmpty35
        {
            get { return alreadyLvUpEmpty35; }
            set { alreadyLvUpEmpty35 = value; }
        }

        public bool Treasure1
        {
            get { return treasure1; }
            set { treasure1 = value; }
        }
        public bool Treasure2
        {
            get { return treasure2; }
            set { treasure2 = value; }
        }
        public bool Treasure3
        {
            get { return treasure3; }
            set { treasure3 = value; }
        }
        public bool Treasure4
        {
            get { return treasure4; }
            set { treasure4 = value; }
        }
        public bool Treasure5
        {
            get { return treasure5; }
            set { treasure5 = value; }
        }
        public bool Treasure6
        {
            get { return treasure6; }
            set { treasure6 = value; }
        }
        public bool Treasure7
        {
            get { return treasure7; }
            set { treasure7 = value; }
        }
        public bool Treasure8
        {
            get { return treasure8; }
            set { treasure8 = value; }
        }
        public bool Treasure9
        {
            get { return treasure9; }
            set { treasure9 = value; }
        }
        public bool Treasure10
        {
            get { return treasure10; }
            set { treasure10 = value; }
        }
        public bool Treasure11
        {
            get { return treasure11; }
            set { treasure11 = value; }
        }
        public bool Treasure12
        {
            get { return treasure12; }
            set { treasure12 = value; }
        }
        public bool Treasure121
        {
            get { return treasure121; }
            set { treasure121 = value; }
        }
        public bool Treasure122
        {
            get { return treasure122; }
            set { treasure122 = value; }
        }
        public bool Treasure123
        {
            get { return treasure123; }
            set { treasure123 = value; }
        }
        public bool Treasure41
        {
            get { return treasure41; }
            set { treasure41 = value; }
        }
        public bool Treasure42
        {
            get { return treasure42; }
            set { treasure42 = value; }
        }
        public bool Treasure43
        {
            get { return treasure43; }
            set { treasure43 = value; }
        }
        public bool Treasure44
        {
            get { return treasure44; }
            set { treasure44 = value; }
        }
        public bool Treasure45
        {
            get { return treasure45; }
            set { treasure45 = value; }
        }
        public bool Treasure46
        {
            get { return treasure46; }
            set { treasure46 = value; }
        }
        public bool Treasure47
        {
            get { return treasure47; }
            set { treasure47 = value; }
        }
        public bool Treasure48
        {
            get { return treasure48; }
            set { treasure48 = value; }
        }
        public bool Treasure49
        {
            get { return treasure49; }
            set { treasure49 = value; }
        }
        public bool Treasure51
        {
            get { return treasure51; }
            set { treasure51 = value; }
        }
        public bool Treasure52
        {
            get { return treasure52; }
            set { treasure52 = value; }
        }
        public bool Treasure53
        {
            get { return treasure53; }
            set { treasure53 = value; }
        }
        public bool Treasure54
        {
            get { return treasure54; }
            set { treasure54 = value; }
        }
        public bool Treasure55
        {
            get { return treasure55; }
            set { treasure55 = value; }
        }
        public bool Treasure56
        {
            get { return treasure56; }
            set { treasure56 = value; }
        }
        public bool Treasure57
        {
            get { return treasure57; }
            set { treasure57 = value; }
        }

        public int DungeonArea
        {
            get { return dungeonArea; }
            set { dungeonArea = value; }
        }

        public bool CompleteSlayBoss1
        {
            get { return completeSlayBoss1; }
            set { completeSlayBoss1 = value; }
        }
        public bool CompleteSlayBoss2
        {
            get { return completeSlayBoss2; }
            set { completeSlayBoss2 = value; }
        }
        public bool CompleteSlayBoss3
        {
            get { return completeSlayBoss3; }
            set { completeSlayBoss3 = value; }
        }
        public bool CompleteSlayBoss4
        {
            get { return completeSlayBoss4; }
            set { completeSlayBoss4 = value; }
        }
        public bool CompleteSlayBoss5
        {
            get { return completeSlayBoss5; }
            set { completeSlayBoss5 = value; }
        }

        public bool CompleteArea1
        {
            get { return completeArea1; }
            set { completeArea1 = value; }
        }
        public bool CompleteArea2
        {
            get { return completeArea2; }
            set { completeArea2 = value; }
        }
        public bool CompleteArea3
        {
            get { return completeArea3; }
            set { completeArea3 = value; }
        }
        public bool CompleteArea4
        {
            get { return completeArea4; }
            set { completeArea4 = value; }
        }
        public bool CompleteArea5
        {
            get { return completeArea5; }
            set { completeArea5 = value; }
        }

        public int CompleteArea1Day
        {
            get { return completeArea1Day; }
            set { completeArea1Day = value; }
        }
        public int CompleteArea2Day
        {
            get { return completeArea2Day; }
            set { completeArea2Day = value; }
        }
        public int CompleteArea3Day
        {
            get { return completeArea3Day; }
            set { completeArea3Day = value; }
        }
        public int CompleteArea4Day
        {
            get { return completeArea4Day; }
            set { completeArea4Day = value; }
        }
        public int CompleteArea5Day
        {
            get { return completeArea5Day; }
            set { completeArea5Day = value; }
        }

        public bool CommunicationCompArea1
        {
            get { return communicationCompArea1; }
            set { communicationCompArea1 = value; }
        }

        public bool CommunicationCompArea2
        {
            get { return communicationCompArea2; }
            set { communicationCompArea2 = value; }
        }

        public bool CommunicationCompArea3
        {
            get { return communicationCompArea3; }
            set { communicationCompArea3 = value; }
        }
        public bool CommunicationCompArea4
        {
            get { return communicationCompArea4; }
            set { communicationCompArea4 = value; }
        }
        public bool CommunicationCompArea5
        {
            get { return communicationCompArea5; }
            set { communicationCompArea5 = value; }
        }

        public bool CommunicationFirstHomeTown
        {
            get { return communicationFirstHomeTown; }
            set { communicationFirstHomeTown = value; }
        }

        public bool TruthWord1
        {
            get { return truthWord1; }
            set { truthWord1 = value; }
        }
        public bool TruthWord2
        {
            get { return truthWord2; }
            set { truthWord2 = value; }
        }
        public bool TruthWord3
        {
            get { return truthWord3; }
            set { truthWord3 = value; }
        }
        public bool TruthWord4
        {
            get { return truthWord4; }
            set { truthWord4 = value; }
        }
        public bool TruthWord5
        {
            get { return truthWord5; }
            set { truthWord5 = value; }
        }
        public bool TrueEnding1
        {
            get { return trueEnding1; }
            set { trueEnding1 = value; }
        }

        public bool InfoArea11
        {
            get { return infoArea11; }
            set { infoArea11 = value; }
        }

        public bool InfoArea21
        {
            get { return infoArea21; }
            set { infoArea21 = value; }
        }
        public bool InfoArea22
        {
            get { return infoArea22; }
            set { infoArea22 = value; }
        }
        public bool InfoArea221
        {
            get { return infoArea221; }
            set { infoArea221 = value; }
        }
        public bool InfoArea222
        {
            get { return infoArea222; }
            set { infoArea222 = value; }
        }
        public bool InfoArea23
        {
            get { return infoArea23; }
            set { infoArea23 = value; }
        }
        public bool InfoArea24
        {
            get { return infoArea24; }
            set { infoArea24 = value; }
        }
        public bool InfoArea240
        {
            get { return infoArea240; }
            set { infoArea240 = value; }
        }
        public bool InfoArea25
        {
            get { return infoArea25; }
            set { infoArea25 = value; }
        }
        public bool InfoArea26
        {
            get { return infoArea26; }
            set { infoArea26 = value; }
        }
        public bool SolveArea21
        {
            get { return solveArea21; }
            set { solveArea21 = value; }
        }
        public bool SolveArea22
        {
            get { return solveArea22; }
            set { solveArea22 = value; }
        }
        public bool SolveArea221
        {
            get { return solveArea221; }
            set { solveArea221 = value; }
        }
        public bool SolveArea222
        {
            get { return solveArea222; }
            set { solveArea222 = value; }
        }
        public bool SolveArea23
        {
            get { return solveArea23; }
            set { solveArea23 = value; }
        }
        public bool SolveArea24
        {
            get { return solveArea24; }
            set { solveArea24 = value; }
        }
        public bool SolveArea25
        {
            get { return solveArea25; }
            set { solveArea25 = value; }
        }
        public bool SolveArea26
        {
            get { return solveArea26; }
            set { solveArea26 = value; }
        }
        public bool FailArea221
        {
            get { return failArea221; }
            set { failArea221 = value; }
        }
        public bool FailArea222
        {
            get { return failArea222; }
            set { failArea222 = value; }
        }
        public bool FailArea23
        {
            get { return failArea23; }
            set { failArea23 = value; }
        }
        public bool FailArea24
        {
            get { return failArea24; }
            set { failArea24 = value; }
        }
        public bool FailArea241
        {
            get { return failArea241; }
            set { failArea241 = value; }
        }
        public bool FailArea242
        {
            get { return failArea242; }
            set { failArea242 = value; }
        }
        public bool FailArea26
        {
            get { return failArea26; }
            set { failArea26 = value; }
        }
        public bool FailArea261
        {
            get { return failArea261; }
            set { failArea261 = value; }
        }
        public bool FailArea262
        {
            get { return failArea262; }
            set { failArea262 = value; }
        }
        public bool FailArea263
        {
            get { return failArea263; }
            set { failArea263 = value; }
        }
        public bool FailArea264
        {
            get { return failArea264; }
            set { failArea264 = value; }
        }
        public bool ProgressArea241
        {
            get { return progressArea241; }
            set { progressArea241 = value; }
        }
        public bool ProgressArea2412
        {
            get { return progressArea2412; }
            set { progressArea2412 = value; }
        }
        public bool ProgressArea2413
        {
            get { return progressArea2413; }
            set { progressArea2413 = value; }
        }
        public bool ProgressArea242
        {
            get { return progressArea242; }
            set { progressArea242 = value; }
        }
        public bool ProgressArea2422
        {
            get { return progressArea2422; }
            set { progressArea2422 = value; }
        }
        public bool ProgressArea243
        {
            get { return progressArea243; }
            set { progressArea243 = value; }
        }
        public bool ProgressArea2432
        {
            get { return progressArea2432; }
            set { progressArea2432 = value; }
        }
        public bool ProgressArea244
        {
            get { return progressArea244; }
            set { progressArea244 = value; }
        }
        public bool ProgressArea2442
        {
            get { return progressArea2442; }
            set { progressArea2442 = value; }
        }
        public bool ProgressArea245
        {
            get { return progressArea245; }
            set { progressArea245 = value; }
        }
        public bool ProgressArea2452
        {
            get { return progressArea2452; }
            set { progressArea2452 = value; }
        }
        public bool ProgressArea246
        {
            get { return progressArea246; }
            set { progressArea246 = value; }
        }
        public bool ProgressArea26
        {
            get { return progressArea26; }
            set { progressArea26 = value; }
        }        
        public bool ProgressArea261
        {
            get { return progressArea261; }
            set { progressArea261 = value; }
        }
        public bool ProgressArea262
        {
            get { return progressArea262; }
            set { progressArea262 = value; }
        }
        public bool ProgressArea263
        {
            get { return progressArea263; }
            set { progressArea263 = value; }
        }
        public bool ProgressArea264
        {
            get { return progressArea264; }
            set { progressArea264 = value; }
        }
        public bool ProgressArea265
        {
            get { return progressArea265; }
            set { progressArea265 = value; }
        }
        public bool ProgressArea266
        {
            get { return progressArea266; }
            set { progressArea266 = value; }
        }
        public bool ProgressArea267
        {
            get { return progressArea267; }
            set { progressArea267 = value; }
        }
        public bool ProgressArea268
        {
            get { return progressArea268; }
            set { progressArea268 = value; }
        }
        public bool ProgressArea269
        {
            get { return progressArea269; }
            set { progressArea269 = value; }
        }
        public bool ProgressArea2610
        {
            get { return progressArea2610; }
            set { progressArea2610 = value; }
        }
        public bool ProgressArea2611
        {
            get { return progressArea2611; }
            set { progressArea2611 = value; }
        }
        public bool ProgressArea2612
        {
            get { return progressArea2612; }
            set { progressArea2612 = value; }
        }
        public bool ProgressArea2613
        {
            get { return progressArea2613; }
            set { progressArea2613 = value; }
        }
        public bool ProgressArea2614
        {
            get { return progressArea2614; }
            set { progressArea2614 = value; }
        }
        public bool ProgressArea2615
        {
            get { return progressArea2615; }
            set { progressArea2615 = value; }
        }
        public bool ProgressArea2616
        {
            get { return progressArea2616; }
            set { progressArea2616 = value; }
        }
        public bool FirstProcessArea24
        {
            get { return firstProcessArea24; }
            set { firstProcessArea24 = value; }
        }
        public bool CompleteArea21
        {
            get { return completeArea21; }
            set { completeArea21 = value; }
        }
        public bool CompleteArea22
        {
            get { return completeArea22; }
            set { completeArea22 = value; }
        }
        public bool CompleteArea23
        {
            get { return completeArea23; }
            set { completeArea23 = value; }
        }
        public bool CompleteArea24
        {
            get { return completeArea24; }
            set { completeArea24 = value; }
        }
        public bool CompleteArea25
        {
            get { return completeArea25; }
            set { completeArea25 = value; }
        }
        public bool CompleteArea26
        {
            get { return completeArea26; }
            set { completeArea26 = value; }
        }
        public bool InfoArea27
        {
            get { return infoArea27; }
            set { infoArea27 = value; }
        }


        public bool InfoArea31
        {
            get { return infoArea31; }
            set { infoArea31 = value; }
        }
        public bool InfoArea311S
        {
            get { return infoArea311s; }
            set { infoArea311s = value; }
        }
        public bool InfoArea311E
        {
            get { return infoArea311e; }
            set { infoArea311e = value; }
        }
        public bool InfoArea312S
        {
            get { return infoArea312s; }
            set { infoArea312s = value; }
        }
        public bool InfoArea312E
        {
            get { return infoArea312e; }
            set { infoArea312e = value; }
        }
        public bool InfoArea313S
        {
            get { return infoArea313s; }
            set { infoArea313s = value; }
        }
        public bool InfoArea313E
        {
            get { return infoArea313e; }
            set { infoArea313e = value; }
        }
        public bool InfoArea324S
        {
            get { return infoArea324s; }
            set { infoArea324s = value; }
        }
        public bool InfoArea324E
        {
            get { return infoArea324e; }
            set { infoArea324e = value; }
        }
        public bool InfoArea325S
        {
            get { return infoArea325s; }
            set { infoArea325s = value; }
        }
        public bool InfoArea325E
        {
            get { return infoArea325e; }
            set { infoArea325e = value; }
        }
        public bool InfoArea326S
        {
            get { return infoArea326s; }
            set { infoArea326s = value; }
        }
        public bool InfoArea326E
        {
            get { return infoArea326e; }
            set { infoArea326e = value; }
        }
        public bool InfoArea327S
        {
            get { return infoArea327s; }
            set { infoArea327s = value; }
        }
        public bool InfoArea327E
        {
            get { return infoArea327e; }
            set { infoArea327e = value; }
        }
        public bool InfoArea328S
        {
            get { return infoArea328s; }
            set { infoArea328s = value; }
        }
        public bool InfoArea328E
        {
            get { return infoArea328e; }
            set { infoArea328e = value; }
        }
        public bool InfoArea329S
        {
            get { return infoArea329s; }
            set { infoArea329s = value; }
        }
        public bool InfoArea329E
        {
            get { return infoArea329e; }
            set { infoArea329e = value; }
        }
        public bool InfoArea3210S
        {
            get { return infoArea3210s; }
            set { infoArea3210s = value; }
        }
        public bool InfoArea3210E
        {
            get { return infoArea3210e; }
            set { infoArea3210e = value; }
        }
        public bool InfoArea3211S
        {
            get { return infoArea3211s; }
            set { infoArea3211s = value; }
        }
        public bool InfoArea3211E
        {
            get { return infoArea3211e; }
            set { infoArea3211e = value; }
        }
        public bool InfoArea3212S
        {
            get { return infoArea3212s; }
            set { infoArea3212s = value; }
        }
        public bool InfoArea3212E
        {
            get { return infoArea3212e; }
            set { infoArea3212e = value; }
        }
        public bool InfoArea3213S
        {
            get { return infoArea3213s; }
            set { infoArea3213s = value; }
        }
        public bool InfoArea3213E
        {
            get { return infoArea3213e; }
            set { infoArea3213e = value; }
        }
        public bool InfoArea3214S
        {
            get { return infoArea3214s; }
            set { infoArea3214s = value; }
        }
        public bool InfoArea3214E
        {
            get { return infoArea3214e; }
            set { infoArea3214e = value; }
        }
        public bool FailArea321
        {
            get { return failArea321; }
            set { failArea321 = value; }
        }
        public bool FailArea322
        {
            get { return failArea322; }
            set { failArea322 = value; }
        }
        public bool FailArea323
        {
            get { return failArea323; }
            set { failArea323 = value; }
        }
        public bool FailArea3241
        {
            get { return failArea3241; }
            set { failArea3241 = value; }
        }
        public bool FailArea3242
        {
            get { return failArea3242; }
            set { failArea3242 = value; }
        }
        public bool FailArea3243
        {
            get { return failArea3243; }
            set { failArea3243 = value; }
        }
        public bool CompleteArea32
        {
            get { return completeArea32; }
            set { completeArea32 = value; }
        }

        public bool InfoArea3315S
        {
            get { return infoArea3315s; }
            set { infoArea3315s = value; }
        }
        public bool InfoArea3315E
        {
            get { return infoArea3315e; }
            set { infoArea3315e = value; }
        }
        public bool InfoArea3316S
        {
            get { return infoArea3316s; }
            set { infoArea3316s = value; }
        }
        public bool InfoArea3316E
        {
            get { return infoArea3316e; }
            set { infoArea3316e = value; }
        }
        public bool InfoArea3317S
        {
            get { return infoArea3317s; }
            set { infoArea3317s = value; }
        }
        public bool InfoArea3317E
        {
            get { return infoArea3317e; }
            set { infoArea3317e = value; }
        }
        public bool InfoArea3318S
        {
            get { return infoArea3318s; }
            set { infoArea3318s = value; }
        }
        public bool InfoArea3318E
        {
            get { return infoArea3318e; }
            set { infoArea3318e = value; }
        }
        public bool InfoArea3319S
        {
            get { return infoArea3319s; }
            set { infoArea3319s = value; }
        }
        public bool InfoArea3319E
        {
            get { return infoArea3319e; }
            set { infoArea3319e = value; }
        }
        public bool InfoArea3320S
        {
            get { return infoArea3320s; }
            set { infoArea3320s = value; }
        }
        public bool InfoArea3320E
        {
            get { return infoArea3320e; }
            set { infoArea3320e = value; }
        }
        public bool InfoArea3321S
        {
            get { return infoArea3321s; }
            set { infoArea3321s = value; }
        }
        public bool InfoArea3321E
        {
            get { return infoArea3321e; }
            set { infoArea3321e = value; }
        }
        public bool InfoArea3322S
        {
            get { return infoArea3322s; }
            set { infoArea3322s = value; }
        }
        public bool InfoArea3322E
        {
            get { return infoArea3322e; }
            set { infoArea3322e = value; }
        }
        public bool InfoArea3323S
        {
            get { return infoArea3323s; }
            set { infoArea3323s = value; }
        }
        public bool InfoArea3323E
        {
            get { return infoArea3323e; }
            set { infoArea3323e = value; }
        }
        public bool InfoArea3324S
        {
            get { return infoArea3324s; }
            set { infoArea3324s = value; }
        }
        public bool InfoArea3324E
        {
            get { return infoArea3324e; }
            set { infoArea3324e = value; }
        }
        public bool InfoArea3325S
        {
            get { return infoArea3325s; }
            set { infoArea3325s = value; }
        }
        public bool InfoArea3325E
        {
            get { return infoArea3325e; }
            set { infoArea3325e = value; }
        }
        public bool InfoArea3326S
        {
            get { return infoArea3326s; }
            set { infoArea3326s = value; }
        }
        public bool InfoArea3326E
        {
            get { return infoArea3326e; }
            set { infoArea3326e = value; }
        }
        public bool InfoArea3327S
        {
            get { return infoArea3327s; }
            set { infoArea3327s = value; }
        }
        public bool InfoArea3327E
        {
            get { return infoArea3327e; }
            set { infoArea3327e = value; }
        }
        public bool InfoArea3328S
        {
            get { return infoArea3328s; }
            set { infoArea3328s = value; }
        }
        public bool InfoArea3328E
        {
            get { return infoArea3328e; }
            set { infoArea3328e = value; }
        }
        public bool ProgressArea3316
        {
            get { return progressArea3316; }
            set { progressArea3316 = value; }
        }
        public bool ProgressArea3317
        {
            get { return progressArea3317; }
            set { progressArea3317 = value; }
        }
        public bool ProgressArea3318
        {
            get { return progressArea3318; }
            set { progressArea3318 = value; }
        }
        public bool ProgressArea3319
        {
            get { return progressArea3319; }
            set { progressArea3319 = value; }
        }
        public bool ProgressArea3320
        {
            get { return progressArea3320; }
            set { progressArea3320 = value; }
        }
        public bool ProgressArea3321
        {
            get { return progressArea3321; }
            set { progressArea3321 = value; }
        }
        public bool ProgressArea3322
        {
            get { return progressArea3322; }
            set { progressArea3322 = value; }
        }
        public bool ProgressArea3323
        {
            get { return progressArea3323; }
            set { progressArea3323 = value; }
        }
        public bool ProgressArea3324
        {
            get { return progressArea3324; }
            set { progressArea3324 = value; }
        }
        public bool ProgressArea3325
        {
            get { return progressArea3325; }
            set { progressArea3325 = value; }
        }
        public bool ProgressArea3326
        {
            get { return progressArea3326; }
            set { progressArea3326 = value; }
        }
        public bool ProgressArea3327
        {
            get { return progressArea3327; }
            set { progressArea3327 = value; }
        }
        public bool FailArea331
        {
            get { return failArea331; }
            set { failArea331 = value; }
        }
        public bool FailArea332
        {
            get { return failArea332; }
            set { failArea332 = value; }
        }
        public bool FailArea333
        {
            get { return failArea333; }
            set { failArea333 = value; }
        }
        public bool FailArea334
        {
            get { return failArea334; }
            set { failArea334 = value; }
        }
        public bool FailArea335
        {
            get { return failArea335; }
            set { failArea335 = value; }
        }
        public bool FailArea336
        {
            get { return failArea336; }
            set { failArea336 = value; }
        }
        public bool FailArea337
        {
            get { return failArea337; }
            set { failArea337 = value; }
        }
        public bool CompleteArea33
        {
            get { return completeArea33; }
            set { completeArea33 = value; }
        }
        public bool InfoArea34
        {
            get { return infoArea34; }
            set { infoArea34 = value; }
        }
        public bool SolveArea34
        {
            get { return solveArea34; }
            set { solveArea34 = value; }
        }
        public bool CompleteArea34
        {
            get { return completeArea34; }
            set { completeArea34 = value; }
        }
        public bool CompleteJumpArea34
        {
            get { return completeJumpArea34; }
            set { completeJumpArea34 = value; }
        }
        public bool InfoArea35
        {
            get { return infoArea35; }
            set { infoArea35 = value; }
        }

        public bool InfoArea41
        {
            get { return infoArea41; }
            set { infoArea41 = value; }
        }
        public bool InfoArea42
        {
            get { return infoArea42; }
            set { infoArea42 = value; }
        }
        public bool InfoArea43
        {
            get { return infoArea43; }
            set { infoArea43 = value; }
        }
        public bool InfoArea44
        {
            get { return infoArea44; }
            set { infoArea44 = value; }
        }
        public bool InfoArea45
        {
            get { return infoArea45; }
            set { infoArea45 = value; }
        }
        public bool InfoArea46
        {
            get { return infoArea46; }
            set { infoArea46 = value; }
        }
        public bool InfoArea47
        {
            get { return infoArea47; }
            set { infoArea47 = value; }
        }
        public bool InfoArea48
        {
            get { return infoArea48; }
            set { infoArea48 = value; }
        }
        public bool InfoArea49
        {
            get { return infoArea49; }
            set { infoArea49 = value; }
        }
        public bool InfoArea410
        {
            get { return infoArea410; }
            set { infoArea410 = value; }
        }
        public bool InfoArea411
        {
            get { return infoArea411; }
            set { infoArea411 = value; }
        }
        public bool InfoArea412
        {
            get { return infoArea412; }
            set { infoArea412 = value; }
        }
        public bool InfoArea413
        {
            get { return infoArea413; }
            set { infoArea413 = value; }
        }
        public bool InfoArea414
        {
            get { return infoArea414; }
            set { infoArea414 = value; }
        }
        public bool InfoArea415
        {
            get { return infoArea415; }
            set { infoArea415 = value; }
        }
        public bool InfoArea416
        {
            get { return infoArea416; }
            set { infoArea416 = value; }
        }
        public bool InfoArea417
        {
            get { return infoArea417; }
            set { infoArea417 = value; }
        }
        public bool InfoArea418
        {
            get { return infoArea418; }
            set { infoArea418 = value; }
        }
        public bool InfoArea419
        {
            get { return infoArea419; }
            set { infoArea419 = value; }
        }
        public bool InfoArea420
        {
            get { return infoArea420; }
            set { infoArea420 = value; }
        }

        public bool ProgressArea4211
        {
            get { return progressArea4211; }
            set { progressArea4211 = value; }
        }

        public bool ProgressArea4212
        {
            get { return progressArea4212; }
            set { progressArea4212 = value; }
        }

        public bool FailArea4211
        {
            get { return failArea4211; }
            set { failArea4211 = value; }
        }

        public bool FailArea4212
        {
            get { return failArea4212; }
            set { failArea4212 = value; }
        }

        public bool InfoArea51
        {
            get { return infoArea51; }
            set { infoArea51 = value; }
        }
        public bool InfoArea52
        {
            get { return infoArea52; }
            set { infoArea52 = value; }
        }
        public bool InfoArea53
        {
            get { return infoArea53; }
            set { infoArea53 = value; }
        }

        public bool SpecialInfo1
        {
            get { return specialInfo1; }
            set { specialInfo1 = value; }
        }

        public bool SpecialInfo2
        {
            get { return specialInfo2; }
            set { specialInfo2 = value; }
        }
        public bool SpecialInfo3
        {
            get { return specialInfo3; }
            set { specialInfo3 = value; }
        }
        public bool SpecialInfo4
        {
            get { return specialInfo4; }
            set { specialInfo4 = value; }
        }
        public bool DefeatVerze
        {
            get { return defeatVerze; }
            set { defeatVerze = value; }
        }

        public bool SpecialTreasure1
        {
            get { return specialTreasure1; }
            set { specialTreasure1 = value; }
        }

        public bool TruthEventForLana
        {
            get { return truthEventForLana; }
            set { truthEventForLana = value; }
        }

        public bool EnterSecondGame
        {
            get { return enterSecondGame; }
            set { enterSecondGame = value; }
        }

        public bool AlreadyUseSyperSaintWater
        {
            get { return alreadyUseSyperSaintWater; }
            set { alreadyUseSyperSaintWater = value; }
        }

        public bool AlreadyUseRevivePotion
        {
            get { return alreadyUseRevivePotion; }
            set { alreadyUseRevivePotion = value; }
        }
        #endregion

        #region "Œã•Ò"

        public bool AlreadyDuelComplete { get; set; }

        public bool AlreadyGetOneDayItem { get; set; }
        public bool AlreadyGetMonsterHunt { get; set; }

        public bool AlreadyUsePureWater
        {
            get { return alreadyUsePureWater; }
            set { alreadyUsePureWater = value; }
        }


        #region "‘O•Ò‚Ìî•ñ‚ğˆø‚«Œp‚¢‚¾ƒtƒ‰ƒO"
        public bool BeforeSpecialInfo1 { get; set; }
        public bool BeforeSpecialInfo2 { get; set; }
        public bool BeforeSpecialInfo3 { get; set; }
        public bool BeforeSpecialInfo4 { get; set; }
        public bool BeforeSpecialInfo5 { get; set; }    
        #endregion

        #region "Œã•Ò^EƒGƒ“ƒfƒBƒ“ƒOê—pƒtƒ‰ƒO"
        public bool CompleteTruth1 { get; set; }
        #endregion

        public bool TruthDuelMatch1 { get; set; } // DUELƒ}ƒbƒ`1l–Ú
        public bool TruthDuelMatch2 { get; set; } // DUELƒ}ƒbƒ`2l–Ú
        public bool TruthDuelMatch3 { get; set; } // DUELƒ}ƒbƒ`3l–Ú
        public bool TruthDuelMatch4 { get; set; } // DUELƒ}ƒbƒ`4l–Ú
        public bool TruthDuelMatch5 { get; set; } // DUELƒ}ƒbƒ`5l–Ú
        public bool TruthDuelMatch6 { get; set; } // DUELƒ}ƒbƒ`6l–Ú
        public bool TruthDuelMatch7 { get; set; } // DUELƒ}ƒbƒ`7l–Ú
        public bool TruthDuelMatch8 { get; set; } // DUELƒ}ƒbƒ`8l–Ú
        public bool TruthDuelMatch9 { get; set; } // DUELƒ}ƒbƒ`9l–Ú
        public bool TruthDuelMatch10 { get; set; } // DUELƒ}ƒbƒ`10l–Ú
        public bool TruthDuelMatch11 { get; set; } // DUELƒ}ƒbƒ`11l–Ú
        public bool TruthDuelMatch12 { get; set; } // DUELƒ}ƒbƒ`12l–Ú
        public bool TruthDuelMatch13 { get; set; } // DUELƒ}ƒbƒ`13l–Ú
        public bool TruthDuelMatch14 { get; set; } // DUELƒ}ƒbƒ`14l–Ú
        public bool TruthDuelMatch15 { get; set; } // DUELƒ}ƒbƒ`15l–Ú
        public bool TruthDuelMatch16 { get; set; } // DUELƒ}ƒbƒ`16l–Ú
        public bool TruthDuelMatch17 { get; set; } // DUELƒ}ƒbƒ`17l–Ú
        public bool TruthDuelMatch18 { get; set; } // DUELƒ}ƒbƒ`18l–Ú
        public bool TruthDuelMatch19 { get; set; } // DUELƒ}ƒbƒ`19l–Ú
        public bool TruthDuelMatch20 { get; set; } // DUELƒ}ƒbƒ`20l–Ú
        public bool TruthDuelMatch21 { get; set; } // DUELƒ}ƒbƒ`21l–Ú

        public bool TruthCompleteSlayBoss1 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK‚Ìƒ{ƒX‚ğŒ‚”j
        public bool TruthCompleteSlayBoss2 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚Ìƒ{ƒX‚ğŒ‚”j
        public bool TruthCompleteSlayBoss3 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚Ìƒ{ƒX‚ğŒ‚”j
        public bool TruthCompleteSlayBoss4 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚Ìƒ{ƒX‚ğŒ‚”j
        public bool TruthCompleteSlayBoss5 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK‚Ìƒ{ƒX‚ğŒ‚”j
        public bool TruthCompleteArea1 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK‚ğ§”eÏ
        public bool TruthCompleteArea2 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK‚ğ§”eÏ
        public bool TruthCompleteArea3 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK‚ğ§”eÏ
        public bool TruthCompleteArea4 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK‚ğ§”eÏ
        public bool TruthCompleteArea5 { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK‚ğ§”eÏ
        public int TruthCompleteArea1Day { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK§”e‚µ‚½“ú
        public int TruthCompleteArea2Day { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK§”e‚µ‚½“ú
        public int TruthCompleteArea3Day { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK§”e‚µ‚½“ú
        public int TruthCompleteArea4Day { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK§”e‚µ‚½“ú
        public int TruthCompleteArea5Day { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK§”e‚µ‚½“ú
        public bool TruthCommunicationCompArea1 { get; set; }  // ƒ_ƒ“ƒWƒ‡ƒ“‚PŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        public bool TruthCommunicationCompArea2 { get; set; }  // ƒ_ƒ“ƒWƒ‡ƒ“‚QŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        public bool TruthCommunicationCompArea3 { get; set; }  // ƒ_ƒ“ƒWƒ‡ƒ“‚RŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        public bool TruthCommunicationCompArea4 { get; set; }  // ƒ_ƒ“ƒWƒ‡ƒ“‚SŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg
        public bool TruthCommunicationCompArea5 { get; set; }  // ƒ_ƒ“ƒWƒ‡ƒ“‚TŠK§”e‚É‚æ‚é‹­§‰ï˜bƒCƒxƒ“ƒg

        #region "‚SŠK"
        public bool dungeonEvent489 { get; set; }
        public bool dungeonEvent488 { get; set; }
        public bool dungeonEvent487 { get; set; }
        public bool dungeonEvent486 { get; set; }
        public bool dungeonEvent485 { get; set; }
        public bool dungeonEvent4_key42_1 { get; set; }
        public bool dungeonEvent4_key42_2 { get; set; }
        public bool dungeonEvent4_key42_3 { get; set; }
        public bool dungeonEvent4_key42_4 { get; set; }
        public bool dungeonEvent4_key42_5 { get; set; }
        public bool dungeonEvent4_key42_6 { get; set; }
        public bool dungeonEvent4_key42_7 { get; set; }
        public bool dungeonEvent4_key42_8 { get; set; }
        public bool dungeonEvent484 { get; set; }
        public bool dungeonEvent483 { get; set; }
        public bool dungeonEvent482 { get; set; }
        public bool dungeonEvent481 { get; set; }
        public bool dungeonEvent480 { get; set; }
        public bool dungeonEvent4_key41_1 { get; set; }
        public bool dungeonEvent4_key41_2 { get; set; }
        public bool dungeonEvent4_key41_3 { get; set; }
        public bool dungeonEvent4_key41_4 { get; set; }
        public bool dungeonEvent4_key41_5 { get; set; }
        public bool dungeonEvent479 { get; set; }
        public bool dungeonEvent478 { get; set; }
        public bool dungeonEvent477 { get; set; }
        public bool dungeonEvent476 { get; set; }
        public bool dungeonEvent4_SlayBoss3 { get; set; }
        public bool dungeonEvent475 { get; set; }
        public bool dungeonEvent474 { get; set; }
        public bool dungeonEvent473 { get; set; }
        public bool dungeonEvent472 { get; set; }
        public bool dungeonEvent4_Area3_2_Fail { get; set; }
        public bool dungeonEvent471 { get; set; }
        public bool dungeonEvent470 { get; set; }
        public bool dungeonEvent469 { get; set; }
        public bool dungeonEvent468 { get; set; }
        public bool dungeonEvent467 { get; set; }
        public bool dungeonEvent466 { get; set; }
        public bool dungeonEvent465 { get; set; }
        public bool dungeonEvent464 { get; set; }
        public bool dungeonEvent463 { get; set; }
        public bool dungeonEvent462 { get; set; }
        public bool dungeonEvent461_storyok { get; set; }
        public bool dungeonEvent460 { get; set; }
        public bool dungeonEvent459 { get; set; }
        public bool dungeonEvent458 { get; set; }
        public bool dungeonEvent457 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_5 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_4 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_3 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail_2 { get; set; }
        public bool dungeonEvent4_Area3_1_Fail { get; set; }
        public bool dungeonEvent456 { get; set; }
        public bool dungeonEvent455 { get; set; }
        public bool dungeonEvent454 { get; set; }
        public bool dungeonEvent453 { get; set; }
        public bool dungeonEvent452 { get; set; }
        public bool dungeonEvent451 { get; set; }
        public bool dungeonEvent450 { get; set; }
        public bool dungeonEvent449 { get; set; }
        public bool dungeonEvent448 { get; set; }
        public bool dungeonEvent447 { get; set; }
        public bool dungeonEvent446 { get; set; }
        public bool dungeonEvent445 { get; set; }
        public bool dungeonEvent444_storyok { get; set; }
        public bool dungeonEvent443 { get; set; }
        public bool dungeonEvent442 { get; set; }
        public bool dungeonEvent441 { get; set; }
        public bool dungeonEvent440 { get; set; }
        public bool dungeonEvent4_SlayBoss2 { get; set; }
        public bool dungeonEvent439 { get; set; }
        public bool dungeonEvent438 { get; set; }
        public bool dungeonEvent437 { get; set; }
        public bool dungeonEvent436 { get; set; }
        public bool dungeonEvent4_key23_1 { get; set; }
        public bool dungeonEvent4_key23_2 { get; set; }
        public bool dungeonEvent4_key23_3 { get; set; }
        public bool dungeonEvent4_key23_4 { get; set; }
        public bool dungeonEvent4_key23_5 { get; set; }
        public bool dungeonEvent435 { get; set; }
        public bool dungeonEvent434 { get; set; }
        public bool dungeonEvent433 { get; set; }
        public bool dungeonEvent432 { get; set; }
        public bool dungeonEvent4_key22_1 { get; set; }
        public bool dungeonEvent4_key22_2 { get; set; }
        public bool dungeonEvent4_key22_3 { get; set; }
        public bool dungeonEvent4_key22_4 { get; set; }
        public bool dungeonEvent4_key22_5 { get; set; }
        public bool dungeonEvent431 { get; set; }
        public bool dungeonEvent430 { get; set; }
        public bool dungeonEvent429 { get; set; }
        public bool dungeonEvent428 { get; set; }
        public bool dungeonEvent4_key2_1 { get; set; }
        public bool dungeonEvent4_key2_2 { get; set; }
        public bool dungeonEvent4_key2_3 { get; set; }
        public bool dungeonEvent4_key2_4 { get; set; }
        public bool dungeonEvent4_key2_5 { get; set; }
        public bool dungeonEvent427 { get; set; }
        public bool dungeonEvent426 { get; set; }
        public bool dungeonEvent425 { get; set; }
        public bool dungeonEvent4_key1_1 { get; set; }
        public bool dungeonEvent4_key1_1_open { get; set; }
        public bool dungeonEvent4_key1_2 { get; set; }
        public bool dungeonEvent4_key1_2_open { get; set; }
        public bool dungeonEvent4_key1_3 { get; set; }
        public bool dungeonEvent4_key1_3_open { get; set; }
        public bool dungeonEvent4_key1_4 { get; set; }
        public bool dungeonEvent4_key1_4_open { get; set; }
        public bool dungeonEvent4_key1_5 { get; set; }
        public bool dungeonEvent4_key1_5_open { get; set; }
        public bool dungeonEvent4_key1_6 { get; set; }
        public bool dungeonEvent4_key1_6_open { get; set; }
        public bool dungeonEvent4_key1_7 { get; set; }
        public bool dungeonEvent4_key1_7_open { get; set; }
        public bool dungeonEvent4_key1_8 { get; set; }
        public bool dungeonEvent4_key1_8_open { get; set; }
        public bool dungeonEvent4_key1_9 { get; set; }
        public bool dungeonEvent4_key1_9_open { get; set; }
        public bool dungeonEvent4_SlayBoss1 { get; set; }
        public bool dungeonEvent4_CommunicateBoss1 { get; set; }
        public bool dungeonEvent424 { get; set; }
        public bool dungeonEvent423 { get; set; }
        public bool dungeonEvent422 { get; set; }
        public bool dungeonEvent421 { get; set; }
        public bool dungeonEvent420 { get; set; }
        public bool dungeonEvent419 { get; set; }
        public bool dungeonEvent418 { get; set; }
        public bool dungeonEvent417 { get; set; }
        public bool dungeonEvent416 { get; set; }
        public bool dungeonEvent415 { get; set; }
        public bool dungeonEvent414 { get; set; }
        public bool dungeonEvent413 { get; set; }
        public bool dungeonEvent412 { get; set; }
        public bool dungeonEvent411 { get; set; }
        public bool dungeonEvent410 { get; set; }
        public bool dungeonEvent409 { get; set; }
        public bool dungeonEvent408 { get; set; }
        public bool dungeonEvent407 { get; set; }
        public bool dungeonEvent406 { get; set; }
        public bool dungeonEvent405 { get; set; }
        public bool dungeonEvent404 { get; set; }
        public bool dungeonEvent403 { get; set; }
        public bool dungeonEvent402 { get; set; }
        public bool dungeonEvent401 { get; set; }
        #endregion
        #region "‚RŠK"
        public bool dungeonEvent333 { get; set; }
        public bool dungeonEvent332 { get; set; }
        public bool dungeonEvent332_1 { get; set; }

        public bool dungeonEvent331 { get; set; }

        public bool dungeonEvent330 { get; set; }

        public bool dungeonEvent329 { get; set; }

        public bool dungeonEvent328 { get; set; }

        public bool dungeonEvent327 { get; set; }

        public bool dungeonEvent326 { get; set; }

        public bool dungeonEvent325 { get; set; }
        public bool dungeonEvent324 { get; set; }
        public bool dungeonEvent323 { get; set; }
        public bool dungeonEvent322 { get; set; }
        public bool dungeonEvent321 { get; set; }
        public bool dungeonEvent320 { get; set; }

        public bool dungeonEvent3_SlayBoss { get; set; }

        public bool dungeonEvent319KeyOpen { get; set; }
        public bool dungeonEvent319 { get; set; }

        public bool dungeonEvent318 { get; set; }
        public bool dungeonEvent317_2 { get; set; }
        public bool dungeonEvent317 { get; set; }
        public bool dungeonEvent316_2 { get; set; }
        public bool dungeonEvent316 { get; set; }
        public bool dungeonEvent315_2 { get; set; }
        public bool dungeonEvent315 { get; set; }
        public bool dungeonEvent314_2 { get; set; }
        public bool dungeonEvent314 { get; set; }

        public bool dungeonEvent313 { get; set; }
        public bool dungeonEvent312_2 { get; set; }
        public bool dungeonEvent312 { get; set; }
        public bool dungeonEvent311 { get; set; }
        public bool dungeonEvent310 { get; set; }
        public bool dungeonEvent309 { get; set; }
        public bool dungeonEvent308 { get; set; }
        public bool dungeonEvent307 { get; set; }

        public bool dungeonEvent306 { get; set; }
        public bool dungeonEvent305 { get; set; }
        public bool dungeonEvent304_6 { get; set; }
        public bool dungeonEvent304_5 { get; set; }
        public bool dungeonEvent304_4 { get; set; }
        public bool dungeonEvent304_3 { get; set; }
        public bool dungeonEvent304_2 { get; set; }
        public bool dungeonEvent304_1 { get; set; }
        public bool dungeonEvent303 { get; set; }
        public bool dungeonEvent302_3 { get; set; }
        public bool dungeonEvent302_2 { get; set; }
        public bool dungeonEvent302_1 { get; set; }
        public bool dungeonEvent302 { get; set; }
        public bool dungeonEvent301 { get; set; }
        #endregion
        #region "‚QŠK"
        public bool dungeonEvent263_KeyOpen { get; set; }
        public bool dungeonEvent262 { get; set; }
        public bool dungeonEvent261 { get; set; }
        public bool dungeonEvent260 { get; set; }
        public bool dungeonEvent259 { get; set; }
        public bool dungeonEvent258 { get; set; }
        public bool dungeonEvent257 { get; set; }
        public bool dungeonEvent256 { get; set; }

        public bool dungeonEvent255_SlayBoss { get; set; }
        public bool dungeonEvent255 { get; set; }
        public bool dungeonEvent254_SlayBoss { get; set; }
        public bool dungeonEvent254 { get; set; }
        public bool dungeonEvent253_SlayBoss { get; set; }
        public bool dungeonEvent253 { get; set; }
        public bool dungeonEvent252_SlayBoss { get; set; }
        public bool dungeonEvent252 { get; set; }
        public bool dungeonEvent251_SlayBoss { get; set; }
        public bool dungeonEvent251 { get; set; }
        public bool dungeonEvent250_SlayBoss { get; set; }
        public bool dungeonEvent250 { get; set; }

        public bool dungeonEvent249 { get; set; }
        public bool dungeonEvent248 { get; set; }
        public bool dungeonEvent247 { get; set; }
        public bool dungeonEvent246 { get; set; }
        public bool dungeonEvent245 { get; set; }
        public bool dungeonEvent244 { get; set; }
        public bool dungeonEvent243 { get; set; }
        public bool dungeonEvent242 { get; set; }
        public bool dungeonEvent241 { get; set; }
        public bool dungeonEvent240 { get; set; }
        public bool dungeonEvent239 { get; set; }
        public bool dungeonEvent238 { get; set; }

        public bool dungeonEvent237 { get; set; }
        public bool dungeonEvent237_Complete { get; set; }
        public bool dungeonEvent237_Fail3 { get; set; }
        public bool dungeonEvent237_Fail2 { get; set; }
        public bool dungeonEvent237_Fail1 { get; set; }

        public bool dungeonEvent236 { get; set; }
        public bool dungeonEvent236_Complete { get; set; }
        public bool dungeonEvent236_Fail3 { get; set; }
        public bool dungeonEvent236_Fail2 { get; set; }
        public bool dungeonEvent236_Fail1 { get; set; }

        public bool dungeonEvent235 { get; set; }
        public bool dungeonEvent235_Complete { get; set; }
        public bool dungeonEvent235_Fail3 { get; set; }
        public bool dungeonEvent235_Fail2 { get; set; }
        public bool dungeonEvent235_Fail1 { get; set; }

        public bool dungeonEvent234 { get; set; }
        public bool dungeonEvent234_Complete { get; set; }
        public bool dungeonEvent234_Fail3 { get; set; }
        public bool dungeonEvent234_Fail2 { get; set; }
        public bool dungeonEvent234_Fail1 { get; set; }

        public bool dungeonEvent233_Complete { get; set; }
        public bool dungeonEvent233_Fail3 { get; set; }
        public bool dungeonEvent233_Fail2 { get; set; }
        public bool dungeonEvent233_Fail1 { get; set; }
        public bool dungeonEvent233 { get; set; }

        public bool dungeonEvent232 { get; set; }
        public bool dungeonEvent231 { get; set; }
        public bool dungeonEvent230_8 { get; set; }
        public bool dungeonEvent230_72 { get; set; }
        public bool dungeonEvent230_71 { get; set; }
        public bool dungeonEvent230_7 { get; set; }
        public bool dungeonEvent230_64 { get; set; }
        public bool dungeonEvent230_63 { get; set; }
        public bool dungeonEvent230_62 { get; set; }
        public bool dungeonEvent230_61 { get; set; }
        public bool dungeonEvent230_6 { get; set; }
        public bool dungeonEvent230_52 { get; set; }
        public bool dungeonEvent230_51 { get; set; }
        public bool dungeonEvent230_5 { get; set; }
        public bool dungeonEvent230_42 { get; set; }
        public bool dungeonEvent230_41 { get; set; }
        public bool dungeonEvent230_4 { get; set; }
        public bool dungeonEvent230_32 { get; set; }
        public bool dungeonEvent230_31 { get; set; }
        public bool dungeonEvent230_3 { get; set; }
        public bool dungeonEvent230_21 { get; set; }
        public bool dungeonEvent230_2 { get; set; }
        public bool dungeonEvent230_12 { get; set; }
        public bool dungeonEvent230_11 { get; set; }
        public bool dungeonEvent230_1 { get; set; }
        public bool dungeonEvent230_01 { get; set; }
        public bool dungeonEvent230_0 { get; set; }
        public bool dungeonEvent230 { get; set; }
        public bool dungeonEvent229 { get; set; }
        public bool dungeonEvent228 { get; set; }

        public bool dungeonEvent227 { get; set; }
        public bool dungeonEvent226 { get; set; }
        public bool dungeonEvent225 { get; set; }
        public bool dungeonEvent224 { get; set; }
        public bool dungeonEvent223 { get; set; }
        public bool dungeonEvent222 { get; set; }
        public bool dungeonEvent221 { get; set; }
        public bool dungeonEvent220 { get; set; }
        public bool dungeonEvent219 { get; set; }
        public bool dungeonEvent218_2 { get; set; }
        public bool dungeonEvent218 { get; set; }
        public bool dungeonEvent217_2 { get; set; }
        public bool dungeonEvent217 { get; set; }
        public bool dungeonEvent216_2 { get; set; }
        public bool dungeonEvent216 { get; set; }
        public bool dungeonEvent215 { get; set; }
        public bool dungeonEvent214 { get; set; }
        public bool dungeonEvent213 { get; set; }
        public bool dungeonEvent212 { get; set; }
        public bool dungeonEvent211FailEvent2 { get; set; }
        public bool dungeonEvent211FailEvent { get; set; }
        public bool dungeonEvent211Fail { get; set; }
        public bool dungeonEvent211 { get; set; }
        public bool dungeonEvent210 { get; set; }
        public bool dungeonEvent209FailEvent2 { get; set; }
        public bool dungeonEvent209FailEvent { get; set; }
        public bool dungeonEvent209Fail { get; set; }
        public bool dungeonEvent209 { get; set; }
        public bool dungeonEvent208 { get; set; }
        public bool dungeonEvent207FailEvent2 { get; set; }
        public bool dungeonEvent207FailEvent { get; set; }
        public bool dungeonEvent207Fail { get; set; }
        public bool dungeonEvent207 { get; set; }
        public bool dungeonEvent206 { get; set; }
        public bool dungeonEvent205 { get; set; }
        public bool dungeonEvent204 { get; set; }
        public bool dungeonEvent203 { get; set; }
        public bool dungeonEvent202 { get; set; }
        public bool dungeonEvent201 { get; set; }
        #endregion
        #region "‚PŠK"
        public bool dungeonEvent31 { get; set; }
        public bool dungeonEvent30 { get; set; }
        public bool dungeonEvent29 { get; set; }
        public bool dungeonEvent28KeyOpen { get; set; }
        public bool dungeonEvent27 { get; set; }
        public bool dungeonEvent26 { get; set; }
        public bool dungeonEvent25 { get; set; }
        public bool dungeonEvent24KeyOpen { get; set; }
        public bool dungeonEvent24NotOpen { get; set; }
        public bool dungeonEvent23KeyOpen { get; set; }
        public bool dungeonEvent23NotOpen { get; set; }
        public bool dungeonEvent22KeyOpen { get; set; }
        public bool dungeonEvent22NotOpen { get; set; }
        public bool dungeonEvent21KeyOpen { get; set; }
        public bool dungeonEvent21NotOpen { get; set; }
        public bool dungeonEvent20 { get; set; }
        public bool dungeonEvent19 { get; set; }
        public bool dungeonEvent18 { get; set; }
        public bool dungeonEvent17 { get; set; }
        public bool dungeonEvent16_4NotOpen { get; set; }
        public bool dungeonEvent16_3NotOpen { get; set; }
        public bool dungeonEvent16_2NotOpen { get; set; }
        public bool dungeonEvent16_1NotOpen { get; set; }
        public bool dungeonEvent16 { get; set; }
        public bool dungeonEvent15 { get; set; }
        public bool dungeonEvent14KeyOpen { get; set; }
        public bool dungeonEvent14NotOpen { get; set; }
        public bool dungeonEvent13KeyOpen { get; set; }
        public bool dungeonEvent13NotOpen { get; set; }
        public bool dungeonEvent12KeyOpen { get; set; }
        public bool dungeonEvent12NotOpen { get; set; }
        public bool dungeonEvent11KeyOpen { get; set; }
        public bool dungeonEvent11NotOpen { get; set; }
        #endregion

        public bool TruthTreasure11 { get; set; } // ƒGƒŠƒA‚P
        public bool TruthTreasure12 { get; set; } // ƒGƒŠƒA‚P
        public bool TruthTreasure13 { get; set; } // ƒGƒŠƒA‚P
        public bool TruthTreasure14 { get; set; } // ƒGƒŠƒA‚P
        public bool TruthTreasure15 { get; set; } // ƒGƒŠƒA‚P
        public bool TruthTreasure121 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure122 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure123 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure124 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure125 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure126 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure127 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure128 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure129 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure1210 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure1211 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure1212 { get; set; } // ƒGƒŠƒA‚Q
        public bool TruthTreasure131 { get; set; } // ƒGƒŠƒA‚R
        public bool TruthTreasure132 { get; set; } // ƒGƒŠƒA‚R
        public bool TruthTreasure133 { get; set; } // ƒGƒŠƒA‚R
        public bool TruthTreasure134 { get; set; } // ƒGƒŠƒA‚R
        public bool TruthTreasure141 { get; set; } // ƒGƒŠƒA‚S
        public bool TruthTreasure142 { get; set; } // ƒGƒŠƒA‚S
        public bool TruthTreasure21 { get; set; } // ’m‚Ì•”‰®‚P
        public bool TruthTreasure22 { get; set; } // ’m‚Ì•”‰®‚Q
        public bool TruthTreasure23 { get; set; } // ’m‚Ì•”‰®‚R
        public bool TruthTreasure24 { get; set; } // ‹Z‚Ì•”‰®‚P
        public bool TruthTreasure25 { get; set; } // ‹Z‚Ì•”‰®‚Q
        public bool TruthTreasure26 { get; set; } // ‹Z‚Ì•”‰®‚R
        public bool TruthTreasure27 { get; set; } // ‹Z‚Ì•”‰®‚S
        public bool TruthTreasure28 { get; set; } // ‹Z‚Ì•”‰®‚T
        public bool TruthTreasure29 { get; set; } // ‹Z‚Ì•”‰®‚U
        public bool TruthTreasure210 { get; set; } // ‹Z‚Ì•”‰®‚V
        public bool TruthTreasure211 { get; set; } // S‚Ì•”‰®‚P
        public bool TruthTreasure212 { get; set; } // S‚Ì•”‰®‚Q
        public bool TruthTreasure213 { get; set; } // —Í‚Ì•”‰®‚P
        public bool TruthTreasure214 { get; set; } // —Í‚Ì•”‰®‚Q
        public bool TruthTreasure215 { get; set; } // —Í‚Ì•”‰®‚R
        public bool TruthTreasure216 { get; set; } // —Í‚Ì•”‰®‚S
        public bool TruthTreasure217 { get; set; } // —Í‚Ì•”‰®‚T
        public bool TruthTreasure218 { get; set; } // —Í‚Ì•”‰®‚U
        public bool TruthTreasure301 { get; set; } // ‹¾ƒGƒŠƒA‚P
        public bool TruthTreasure302 { get; set; } // ‹¾ƒGƒŠƒA‚P
        public bool TruthTreasure303 { get; set; } // ‹¾ƒGƒŠƒA‚P
        public bool TruthTreasure304 { get; set; } // ‹¾ƒGƒŠƒA‚P
        public bool TruthTreasure305 { get; set; } // ‹¾ƒGƒŠƒA‚P
        public bool TruthTreasure306 { get; set; } // ‹¾ƒGƒŠƒA‚P
        public bool TruthTreasure307 { get; set; } // ‹¾ƒGƒŠƒA‚Q
        public bool TruthTreasure308 { get; set; } // ‹¾ƒGƒŠƒA‚Q
        public bool TruthTreasure309 { get; set; } // ‹¾ƒGƒŠƒA‚Q
        public bool TruthTreasure310 { get; set; } // ‹¾ƒGƒŠƒA‚Q
        public bool TruthTreasure311 { get; set; } // ‹¾ƒGƒŠƒA‚Q
        public bool TruthTreasure312 { get; set; } // ‹¾ƒGƒŠƒA‚Q
        public bool TruthTreasure401 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚P
        public bool TruthTreasure402 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚Q
        public bool TruthTreasure403 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚R
        public bool TruthTreasure404 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚S
        public bool TruthTreasure405 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚T
        public bool TruthTreasure406 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚U
        public bool TruthTreasure407 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚V
        public bool TruthTreasure408 { get; set; } // ƒGƒXƒ~ƒŠƒA‘Œ´‹æˆæ‚W
        public bool TruthTreasure409 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚P
        public bool TruthTreasure410 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚Q
        public bool TruthTreasure411 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚R
        public bool TruthTreasure412 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚S
        public bool TruthTreasure413 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚T
        public bool TruthTreasure414 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚U
        public bool TruthTreasure415 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚V
        public bool TruthTreasure416 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚W
        public bool TruthTreasure417 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚X
        public bool TruthTreasure418 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚P‚O
        public bool TruthTreasure419 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚P‚P
        public bool TruthTreasure420 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚P‚Q
        public bool TruthTreasure421 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚P‚R
        public bool TruthTreasure422 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉX‚P‚S
        public bool TruthTreasure423 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚P
        public bool TruthTreasure424 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚Q
        public bool TruthTreasure425 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚R
        public bool TruthTreasure426 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚S
        public bool TruthTreasure427 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚T
        public bool TruthTreasure428 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚U
        public bool TruthTreasure429 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚V
        public bool TruthTreasure430 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚W
        public bool TruthTreasure431 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚X
        public bool TruthTreasure432 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚P‚O
        public bool TruthTreasure433 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚P‚P
        public bool TruthTreasure434 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉY‚P‚Q
        public bool TruthTreasure435 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚P
        public bool TruthTreasure436 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚Q
        public bool TruthTreasure437 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚R
        public bool TruthTreasure438 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚S
        public bool TruthTreasure439 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚T
        public bool TruthTreasure440 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚U
        public bool TruthTreasure441 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚V
        public bool TruthTreasure442 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a•ó•¨ŒÉZ‚W
        public bool TruthTreasure443 { get; set; } // –À‚ÌŒn•ˆ‚P
        public bool TruthTreasure444 { get; set; } // –À‚ÌŒn•ˆ‚Q
        public bool TruthTreasure445 { get; set; } // –À‚ÌŒn•ˆ‚R
        public bool TruthTreasure446 { get; set; } // –À‚ÌŒn•ˆ‚S
        public bool TruthTreasure447 { get; set; } // –À‚ÌŒn•ˆ‚T
        public bool TruthTreasure448 { get; set; } // –À‚ÌŒn•ˆ‚U
        public bool TruthTreasure449 { get; set; } // –À‚ÌŒn•ˆ‚V
        public bool TruthTreasure450 { get; set; } // –À‚ÌŒn•ˆ‚W
        public bool TruthTreasure451 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚P
        public bool TruthTreasure452 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚Q
        public bool TruthTreasure453 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚R
        public bool TruthTreasure454 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚S
        public bool TruthTreasure455 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚T
        public bool TruthTreasure456 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚U
        public bool TruthTreasure457 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚V
        public bool TruthTreasure458 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚W
        public bool TruthTreasure459 { get; set; } // ƒ‰ƒXƒgƒtƒFƒCƒY‚X

        public bool GanzGift1 { get; set; } // ŒÃ‘ã‰h÷‚ÌŠ²‚Ì’f•Ğ‘fŞ‚ğˆø‚«“n‚·

        public bool TruthSpecialInfo1 { get; set; }
        public bool BoardInfo14 { get; set; }
        public bool BoardInfo13 { get; set; }
        public bool BoardInfo12 { get; set; }
        public bool BoardInfo11 { get; set; }
        public bool BoardInfo10 { get; set; }
        public bool MeetOlLandisBeforeGanz { get; set; }
        public bool MeetOlLandisBeforeHanna { get; set; }
        public bool MeetOlLandisBeforeLana { get; set; }
        public bool MeetOlLandis { get; set; }
        public bool AvailableDuelMatch { get; set; }
        public bool AvailableDuelColosseum { get; set; }
        public bool AvailablePotionshop { get; set; }
        public bool AvailableBattleSettingMenu { get; set; }
        public bool AvailableInstantCommand { get; set; }
        public bool AvailableMixSpellSkill { get; set; }
        public bool availableArchetypeCommand { get; set; } // öİ‰œ‹`‚Ì”­“®‰Â”\
        public bool AvailableBackGate { get; set; }

        public bool AlreadyCommunicateFazilCastle { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a‚ÅƒCƒxƒ“ƒgÏ
        public bool alreadyCommunicateCahlhanz { get; set; } // ƒz[ƒ€ƒ^ƒEƒ“‚ÅƒJ[ƒ‹İ‚Æ‰ï˜bÏ

        public bool Truth_CommunicationFifthHomeTown { get; set; }
        public bool Truth_CommunicationFourthHomeTown { get; set; }
        public bool Truth_CommunicationThirdHomeTown { get; set; }
        public bool Truth_CommunicationSecondHomeTown { get; set; }

        public bool Truth_Communication_Dungeon11 { get; set; }
        public bool Truth_CommunicationJoinPartyLana { get; set; }
        public bool Truth_CommunicationNotJoinLana { get; set; }
        public bool Truth_CommunicationFirstHomeTown { get; set; }

        // ƒvƒŒƒCƒ„[ƒXƒe[ƒ^ƒX‚âƒ_ƒ“ƒWƒ‡ƒ“isó‹µ‚É‰‚¶‚Ä‰Â•Ï‚Èƒtƒ‰ƒO
        public bool Truth_CommunicationLana1_1 { get; set; }
        public bool Truth_CommunicationLana1_2 { get; set; }

        // ‚»‚êˆÈŠO‚Ì‘O•Ò“¯—l“úí
        public bool Truth_CommunicationLana1 { get; set; }
        public bool Truth_CommunicationLana2 { get; set; }
        public bool Truth_CommunicationLana3 { get; set; }
        public bool Truth_CommunicationLana4 { get; set; }
        public bool Truth_CommunicationLana5 { get; set; }
        public bool Truth_CommunicationLana6 { get; set; }
        public bool Truth_CommunicationLana7 { get; set; }
        public bool Truth_CommunicationLana8 { get; set; }
        public bool Truth_CommunicationLana9 { get; set; }
        public bool Truth_CommunicationLana10 { get; set; }
        public bool Truth_CommunicationLana21 { get; set; } // ‚QŠKˆÈ~
        public bool Truth_CommunicationLana22 { get; set; }
        public bool Truth_CommunicationLana31 { get; set; } // ‚RŠKˆÈ~
        public bool Truth_CommunicationLana41 { get; set; } // ‚SŠKˆÈ~

        public bool Truth_CommunicationGanz1 { get; set; }
        public bool Truth_CommunicationGanz2 { get; set; }
        public bool Truth_CommunicationGanz3 { get; set; }
        public bool Truth_CommunicationGanz4 { get; set; }
        public bool Truth_CommunicationGanz5 { get; set; }
        public bool Truth_CommunicationGanz6 { get; set; }
        public bool Truth_CommunicationGanz7 { get; set; }
        public bool Truth_CommunicationGanz8 { get; set; }
        public bool Truth_CommunicationGanz9 { get; set; }
        public bool Truth_CommunicationGanz10 { get; set; }
        public bool Truth_CommunicationGanz21 { get; set; } // ‚QŠKˆÈ~
        public bool Truth_CommunicationGanz31 { get; set; } // ‚RŠKˆÈ~
        public bool Truth_CommunicationGanz41 { get; set; } // ‚SŠKˆÈ~

        public bool Truth_CommunicationHanna1 { get; set; }
        public bool Truth_CommunicationHanna2 { get; set; }
        public bool Truth_CommunicationHanna3 { get; set; }
        public bool Truth_CommunicationHanna4 { get; set; }
        public bool Truth_CommunicationHanna5 { get; set; }
        public bool Truth_CommunicationHanna6 { get; set; }
        public bool Truth_CommunicationHanna7 { get; set; }
        public bool Truth_CommunicationHanna8 { get; set; }
        public bool Truth_CommunicationHanna9 { get; set; }
        public bool Truth_CommunicationHanna10 { get; set; }
        public bool Truth_CommunicationHanna21 { get; set; } // ‚QŠKˆÈ~
        public bool Truth_CommunicationHanna31 { get; set; } // ‚RŠKˆÈ~
        public bool Truth_CommunicationHanna31_2 { get; set; }
        public bool Truth_CommunicationHanna41 { get; set; } // ‚SŠKˆÈ~

        public bool Truth_CommunicationOl21 { get; set; }
        public bool Truth_CommunicationOl22 { get; set; }
        public bool Truth_CommunicationOl22Fail { get; set; }
        public bool Truth_CommunicationOl22Progress1 { get; set; }
        public bool Truth_CommunicationOl22Progress2 { get; set; }
        public bool Truth_CommunicationOl22DuelFail { get; set; }
        public int Truth_CommunicationOl22DuelFailCount { get; set; }
        public bool Truth_CommunicationOl31 { get; set; } // ‚RŠKˆÈ~
        public bool Truth_CommunicationOl41 { get; set; } // ‚RŠKˆÈ~

        public bool Truth_CommunicationSinikia31 { get; set; } // ‚RŠKˆÈ~
        public bool Truth_CommunicationSinikia30DuelFail { get; set; }
        public int Truth_CommunicationSinikia30DuelFailCount { get; set; }
        public bool Truth_CommunicationSinikia41 { get; set; } // ‚RŠKˆÈ~

        public bool Truth_Communication_FC31 { get; set; } // ƒtƒ@[ƒWƒ‹‹{“a
        public bool Truth_Communication_FC32 { get; set; }

        public bool Truth_GiveLanaEarring { get; set; }

        public bool Truth_FirstOneDayItem { get; set; }

        public int dungeonViewPointX { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ““à‚ÅƒZ[ƒu‚µ‚½ƒrƒ…[ƒ|ƒCƒ“ƒgX
        public int dungeonViewPointY { get; set; } // ƒ_ƒ“ƒWƒ‡ƒ““à‚ÅƒZ[ƒu‚µ‚½ƒrƒ…[ƒ|ƒCƒ“ƒgY

        //public bool AvailableFood1 { get; set; } // n‚ß‚©‚ç‰ğ‹Ö‚µ‚Ä‚¢‚é‚½‚ßA•s—v‚Èƒtƒ‰ƒO
        public bool AvailableFood2 { get; set; }
        public bool AvailableFood3 { get; set; }
        public bool AvailableFood4 { get; set; }
        public bool AvailableFood5 { get; set; }

        //public bool AvailablePotion1 { get; set; } // n‚ß‚©‚ç‰ğ‹Ö‚µ‚Ä‚¢‚é‚½‚ßA•s—v‚Èƒtƒ‰ƒO
        public bool AvailablePotion2 { get; set; }
        public bool AvailablePotion3 { get; set; }
        public bool AvailablePotion4 { get; set; }
        public bool AvailablePotion5 { get; set; }

        public bool AvailableItemBank { get; set; }
        public bool AvailableFazilCastle { get; set; }
        public bool AvailableOneDayItem { get; set; }

        public bool DuelWinZalge { get; set; } // ƒUƒ‹ƒQ‚Æ‚ÌDUELí‚ÅŸ—˜‚µ‚½ê‡ATrue

        public string ItemBank1 { get; set; }
        public string ItemBank1Stack { get; set; }
        public string ItemBank2 { get; set; }
        public string ItemBank2Stack { get; set; }
        public string ItemBank3 { get; set; }
        public string ItemBank3Stack { get; set; }
        public string ItemBank4 { get; set; }
        public string ItemBank4Stack { get; set; }
        public string ItemBank5 { get; set; }
        public string ItemBank5Stack { get; set; }
        public string ItemBank6 { get; set; }
        public string ItemBank6Stack { get; set; }
        public string ItemBank7 { get; set; }
        public string ItemBank7Stack { get; set; }
        public string ItemBank8 { get; set; }
        public string ItemBank8Stack { get; set; }
        public string ItemBank9 { get; set; }
        public string ItemBank9Stack { get; set; }
        public string ItemBank10 { get; set; }
        public string ItemBank10Stack { get; set; }
        public string ItemBank11 { get; set; }
        public string ItemBank11Stack { get; set; }
        public string ItemBank12 { get; set; }
        public string ItemBank12Stack { get; set; }
        public string ItemBank13 { get; set; }
        public string ItemBank13Stack { get; set; }
        public string ItemBank14 { get; set; }
        public string ItemBank14Stack { get; set; }
        public string ItemBank15 { get; set; }
        public string ItemBank15Stack { get; set; }
        public string ItemBank16 { get; set; }
        public string ItemBank16Stack { get; set; }
        public string ItemBank17 { get; set; }
        public string ItemBank17Stack { get; set; }
        public string ItemBank18 { get; set; }
        public string ItemBank18Stack { get; set; }
        public string ItemBank19 { get; set; }
        public string ItemBank19Stack { get; set; }
        public string ItemBank20 { get; set; }
        public string ItemBank20Stack { get; set; }
        public string ItemBank21 { get; set; }
        public string ItemBank21Stack { get; set; }
        public string ItemBank22 { get; set; }
        public string ItemBank22Stack { get; set; }
        public string ItemBank23 { get; set; }
        public string ItemBank23Stack { get; set; }
        public string ItemBank24 { get; set; }
        public string ItemBank24Stack { get; set; }
        public string ItemBank25 { get; set; }
        public string ItemBank25Stack { get; set; }
        public string ItemBank26 { get; set; }
        public string ItemBank26Stack { get; set; }
        public string ItemBank27 { get; set; }
        public string ItemBank27Stack { get; set; }
        public string ItemBank28 { get; set; }
        public string ItemBank28Stack { get; set; }
        public string ItemBank29 { get; set; }
        public string ItemBank29Stack { get; set; }
        public string ItemBank30 { get; set; }
        public string ItemBank30Stack { get; set; }
        public string ItemBank31 { get; set; }
        public string ItemBank31Stack { get; set; }
        public string ItemBank32 { get; set; }
        public string ItemBank32Stack { get; set; }
        public string ItemBank33 { get; set; }
        public string ItemBank33Stack { get; set; }
        public string ItemBank34 { get; set; }
        public string ItemBank34Stack { get; set; }
        public string ItemBank35 { get; set; }
        public string ItemBank35Stack { get; set; }
        public string ItemBank36 { get; set; }
        public string ItemBank36Stack { get; set; }
        public string ItemBank37 { get; set; }
        public string ItemBank37Stack { get; set; }
        public string ItemBank38 { get; set; }
        public string ItemBank38Stack { get; set; }
        public string ItemBank39 { get; set; }
        public string ItemBank39Stack { get; set; }
        public string ItemBank40 { get; set; }
        public string ItemBank40Stack { get; set; }
        public string ItemBank41 { get; set; }
        public string ItemBank41Stack { get; set; }
        public string ItemBank42 { get; set; }
        public string ItemBank42Stack { get; set; }
        public string ItemBank43 { get; set; }
        public string ItemBank43Stack { get; set; }
        public string ItemBank44 { get; set; }
        public string ItemBank44Stack { get; set; }
        public string ItemBank45 { get; set; }
        public string ItemBank45Stack { get; set; }
        public string ItemBank46 { get; set; }
        public string ItemBank46Stack { get; set; }
        public string ItemBank47 { get; set; }
        public string ItemBank47Stack { get; set; }
        public string ItemBank48 { get; set; }
        public string ItemBank48Stack { get; set; }
        public string ItemBank49 { get; set; }
        public string ItemBank49Stack { get; set; }
        public string ItemBank50 { get; set; }
        public string ItemBank50Stack { get; set; }
        public string ItemBank51 { get; set; }
        public string ItemBank51Stack { get; set; }
        public string ItemBank52 { get; set; }
        public string ItemBank52Stack { get; set; }
        public string ItemBank53 { get; set; }
        public string ItemBank53Stack { get; set; }
        public string ItemBank54 { get; set; }
        public string ItemBank54Stack { get; set; }
        public string ItemBank55 { get; set; }
        public string ItemBank55Stack { get; set; }
        public string ItemBank56 { get; set; }
        public string ItemBank56Stack { get; set; }
        public string ItemBank57 { get; set; }
        public string ItemBank57Stack { get; set; }
        public string ItemBank58 { get; set; }
        public string ItemBank58Stack { get; set; }
        public string ItemBank59 { get; set; }
        public string ItemBank59Stack { get; set; }
        public string ItemBank60 { get; set; }
        public string ItemBank60Stack { get; set; }
        public string ItemBank61 { get; set; }
        public string ItemBank61Stack { get; set; }
        public string ItemBank62 { get; set; }
        public string ItemBank62Stack { get; set; }
        public string ItemBank63 { get; set; }
        public string ItemBank63Stack { get; set; }
        public string ItemBank64 { get; set; }
        public string ItemBank64Stack { get; set; }
        public string ItemBank65 { get; set; }
        public string ItemBank65Stack { get; set; }
        public string ItemBank66 { get; set; }
        public string ItemBank66Stack { get; set; }
        public string ItemBank67 { get; set; }
        public string ItemBank67Stack { get; set; }
        public string ItemBank68 { get; set; }
        public string ItemBank68Stack { get; set; }
        public string ItemBank69 { get; set; }
        public string ItemBank69Stack { get; set; }
        public string ItemBank70 { get; set; }
        public string ItemBank70Stack { get; set; }
        public string ItemBank71 { get; set; }
        public string ItemBank71Stack { get; set; }
        public string ItemBank72 { get; set; }
        public string ItemBank72Stack { get; set; }
        public string ItemBank73 { get; set; }
        public string ItemBank73Stack { get; set; }
        public string ItemBank74 { get; set; }
        public string ItemBank74Stack { get; set; }
        public string ItemBank75 { get; set; }
        public string ItemBank75Stack { get; set; }
        public string ItemBank76 { get; set; }
        public string ItemBank76Stack { get; set; }
        public string ItemBank77 { get; set; }
        public string ItemBank77Stack { get; set; }
        public string ItemBank78 { get; set; }
        public string ItemBank78Stack { get; set; }
        public string ItemBank79 { get; set; }
        public string ItemBank79Stack { get; set; }
        public string ItemBank80 { get; set; }
        public string ItemBank80Stack { get; set; }
        public string ItemBank81 { get; set; }
        public string ItemBank81Stack { get; set; }
        public string ItemBank82 { get; set; }
        public string ItemBank82Stack { get; set; }
        public string ItemBank83 { get; set; }
        public string ItemBank83Stack { get; set; }
        public string ItemBank84 { get; set; }
        public string ItemBank84Stack { get; set; }
        public string ItemBank85 { get; set; }
        public string ItemBank85Stack { get; set; }
        public string ItemBank86 { get; set; }
        public string ItemBank86Stack { get; set; }
        public string ItemBank87 { get; set; }
        public string ItemBank87Stack { get; set; }
        public string ItemBank88 { get; set; }
        public string ItemBank88Stack { get; set; }
        public string ItemBank89 { get; set; }
        public string ItemBank89Stack { get; set; }
        public string ItemBank90 { get; set; }
        public string ItemBank90Stack { get; set; }
        public string ItemBank91 { get; set; }
        public string ItemBank91Stack { get; set; }
        public string ItemBank92 { get; set; }
        public string ItemBank92Stack { get; set; }
        public string ItemBank93 { get; set; }
        public string ItemBank93Stack { get; set; }
        public string ItemBank94 { get; set; }
        public string ItemBank94Stack { get; set; }
        public string ItemBank95 { get; set; }
        public string ItemBank95Stack { get; set; }
        public string ItemBank96 { get; set; }
        public string ItemBank96Stack { get; set; }
        public string ItemBank97 { get; set; }
        public string ItemBank97Stack { get; set; }
        public string ItemBank98 { get; set; }
        public string ItemBank98Stack { get; set; }
        public string ItemBank99 { get; set; }
        public string ItemBank99Stack { get; set; }
        public string ItemBank100 { get; set; }
        public string ItemBank100Stack { get; set; }
        #endregion

        public void LoadItemBankData(ref string[] items, ref int[] stack)
        {
            items[0] = Convert.ToString(ItemBank1);
            stack[0] = Convert.ToInt32( ItemBank1Stack);
            items[1] = Convert.ToString(ItemBank2);
            stack[1] = Convert.ToInt32( ItemBank2Stack);
            items[2] = Convert.ToString(ItemBank3);
            stack[2] = Convert.ToInt32( ItemBank3Stack);
            items[3] = Convert.ToString(ItemBank4);
            stack[3] = Convert.ToInt32( ItemBank4Stack);
            items[4] = Convert.ToString(ItemBank5);
            stack[4] = Convert.ToInt32( ItemBank5Stack);
            items[5] = Convert.ToString(ItemBank6);
            stack[5] = Convert.ToInt32( ItemBank6Stack);
            items[6] = Convert.ToString(ItemBank7);
            stack[6] = Convert.ToInt32( ItemBank7Stack);
            items[7] = Convert.ToString(ItemBank8);
            stack[7] = Convert.ToInt32( ItemBank8Stack);
            items[8] = Convert.ToString(ItemBank9);
            stack[8] = Convert.ToInt32( ItemBank9Stack);
            items[9] = Convert.ToString(ItemBank10);
            stack[9] = Convert.ToInt32( ItemBank10Stack);

            items[10] = Convert.ToString(ItemBank11);
            stack[10] = Convert.ToInt32( ItemBank11Stack);
            items[11] = Convert.ToString(ItemBank12);
            stack[11] = Convert.ToInt32( ItemBank12Stack);
            items[12] = Convert.ToString(ItemBank13);
            stack[12] = Convert.ToInt32( ItemBank13Stack);
            items[13] = Convert.ToString(ItemBank14);
            stack[13] = Convert.ToInt32( ItemBank14Stack);
            items[14] = Convert.ToString(ItemBank15);
            stack[14] = Convert.ToInt32( ItemBank15Stack);
            items[15] = Convert.ToString(ItemBank16);
            stack[15] = Convert.ToInt32( ItemBank16Stack);
            items[16] = Convert.ToString(ItemBank17);
            stack[16] = Convert.ToInt32( ItemBank17Stack);
            items[17] = Convert.ToString(ItemBank18);
            stack[17] = Convert.ToInt32( ItemBank18Stack);
            items[18] = Convert.ToString(ItemBank19);
            stack[18] = Convert.ToInt32( ItemBank19Stack);
            items[19] = Convert.ToString(ItemBank20);
            stack[19] = Convert.ToInt32( ItemBank20Stack);

            items[20] = Convert.ToString(ItemBank21);
            stack[20] = Convert.ToInt32( ItemBank21Stack);
            items[21] = Convert.ToString(ItemBank22);
            stack[21] = Convert.ToInt32( ItemBank22Stack);
            items[22] = Convert.ToString(ItemBank23);
            stack[22] = Convert.ToInt32( ItemBank23Stack);
            items[23] = Convert.ToString(ItemBank24);
            stack[23] = Convert.ToInt32( ItemBank24Stack);
            items[24] = Convert.ToString(ItemBank25);
            stack[24] = Convert.ToInt32( ItemBank25Stack);
            items[25] = Convert.ToString(ItemBank26);
            stack[25] = Convert.ToInt32( ItemBank26Stack);
            items[26] = Convert.ToString(ItemBank27);
            stack[26] = Convert.ToInt32( ItemBank27Stack);
            items[27] = Convert.ToString(ItemBank28);
            stack[27] = Convert.ToInt32( ItemBank28Stack);
            items[28] = Convert.ToString(ItemBank29);
            stack[28] = Convert.ToInt32( ItemBank29Stack);
            items[29] = Convert.ToString(ItemBank30);
            stack[29] = Convert.ToInt32( ItemBank30Stack);

            items[30] = Convert.ToString(ItemBank31);
            stack[30] = Convert.ToInt32( ItemBank31Stack);
            items[31] = Convert.ToString(ItemBank32);
            stack[31] = Convert.ToInt32( ItemBank32Stack);
            items[32] = Convert.ToString(ItemBank33);
            stack[32] = Convert.ToInt32( ItemBank33Stack);
            items[33] = Convert.ToString(ItemBank34);
            stack[33] = Convert.ToInt32( ItemBank34Stack);
            items[34] = Convert.ToString(ItemBank35);
            stack[34] = Convert.ToInt32( ItemBank35Stack);
            items[35] = Convert.ToString(ItemBank36);
            stack[35] = Convert.ToInt32( ItemBank36Stack);
            items[36] = Convert.ToString(ItemBank37);
            stack[36] = Convert.ToInt32( ItemBank37Stack);
            items[37] = Convert.ToString(ItemBank38);
            stack[37] = Convert.ToInt32( ItemBank38Stack);
            items[38] = Convert.ToString(ItemBank39);
            stack[38] = Convert.ToInt32( ItemBank39Stack);
            items[39] = Convert.ToString(ItemBank40);
            stack[39] = Convert.ToInt32( ItemBank40Stack);

            items[40] = Convert.ToString(ItemBank41);
            stack[40] = Convert.ToInt32( ItemBank41Stack);
            items[41] = Convert.ToString(ItemBank42);
            stack[41] = Convert.ToInt32( ItemBank42Stack);
            items[42] = Convert.ToString(ItemBank43);
            stack[42] = Convert.ToInt32( ItemBank43Stack);
            items[43] = Convert.ToString(ItemBank44);
            stack[43] = Convert.ToInt32( ItemBank44Stack);
            items[44] = Convert.ToString(ItemBank45);
            stack[44] = Convert.ToInt32( ItemBank45Stack);
            items[45] = Convert.ToString(ItemBank46);
            stack[45] = Convert.ToInt32( ItemBank46Stack);
            items[46] = Convert.ToString(ItemBank47);
            stack[46] = Convert.ToInt32( ItemBank47Stack);
            items[47] = Convert.ToString(ItemBank48);
            stack[47] = Convert.ToInt32( ItemBank48Stack);
            items[48] = Convert.ToString(ItemBank49);
            stack[48] = Convert.ToInt32( ItemBank49Stack);
            items[49] = Convert.ToString(ItemBank50);
            stack[49] = Convert.ToInt32( ItemBank50Stack);

            items[50] = Convert.ToString(ItemBank51);
            stack[50] = Convert.ToInt32( ItemBank51Stack);
            items[51] = Convert.ToString(ItemBank52);
            stack[51] = Convert.ToInt32( ItemBank52Stack);
            items[52] = Convert.ToString(ItemBank53);
            stack[52] = Convert.ToInt32( ItemBank53Stack);
            items[53] = Convert.ToString(ItemBank54);
            stack[53] = Convert.ToInt32( ItemBank54Stack);
            items[54] = Convert.ToString(ItemBank55);
            stack[54] = Convert.ToInt32( ItemBank55Stack);
            items[55] = Convert.ToString(ItemBank56);
            stack[55] = Convert.ToInt32( ItemBank56Stack);
            items[56] = Convert.ToString(ItemBank57);
            stack[56] = Convert.ToInt32( ItemBank57Stack);
            items[57] = Convert.ToString(ItemBank58);
            stack[57] = Convert.ToInt32( ItemBank58Stack);
            items[58] = Convert.ToString(ItemBank59);
            stack[58] = Convert.ToInt32( ItemBank59Stack);
            items[59] = Convert.ToString(ItemBank60);
            stack[59] = Convert.ToInt32( ItemBank60Stack);

            items[60] = Convert.ToString(ItemBank61);
            stack[60] = Convert.ToInt32( ItemBank61Stack);
            items[61] = Convert.ToString(ItemBank62);
            stack[61] = Convert.ToInt32( ItemBank62Stack);
            items[62] = Convert.ToString(ItemBank63);
            stack[62] = Convert.ToInt32( ItemBank63Stack);
            items[63] = Convert.ToString(ItemBank64);
            stack[63] = Convert.ToInt32( ItemBank64Stack);
            items[64] = Convert.ToString(ItemBank65);
            stack[64] = Convert.ToInt32( ItemBank65Stack);
            items[65] = Convert.ToString(ItemBank66);
            stack[65] = Convert.ToInt32( ItemBank66Stack);
            items[66] = Convert.ToString(ItemBank67);
            stack[66] = Convert.ToInt32( ItemBank67Stack);
            items[67] = Convert.ToString(ItemBank68);
            stack[67] = Convert.ToInt32( ItemBank68Stack);
            items[68] = Convert.ToString(ItemBank69);
            stack[68] = Convert.ToInt32( ItemBank69Stack);
            items[69] = Convert.ToString(ItemBank70);
            stack[69] = Convert.ToInt32( ItemBank70Stack);

            items[70] = Convert.ToString(ItemBank71);
            stack[70] = Convert.ToInt32( ItemBank71Stack);
            items[71] = Convert.ToString(ItemBank72);
            stack[71] = Convert.ToInt32( ItemBank72Stack);
            items[72] = Convert.ToString(ItemBank73);
            stack[72] = Convert.ToInt32( ItemBank73Stack);
            items[73] = Convert.ToString(ItemBank74);
            stack[73] = Convert.ToInt32( ItemBank74Stack);
            items[74] = Convert.ToString(ItemBank75);
            stack[74] = Convert.ToInt32( ItemBank75Stack);
            items[75] = Convert.ToString(ItemBank76);
            stack[75] = Convert.ToInt32( ItemBank76Stack);
            items[76] = Convert.ToString(ItemBank77);
            stack[76] = Convert.ToInt32( ItemBank77Stack);
            items[77] = Convert.ToString(ItemBank78);
            stack[77] = Convert.ToInt32( ItemBank78Stack);
            items[78] = Convert.ToString(ItemBank79);
            stack[78] = Convert.ToInt32( ItemBank79Stack);
            items[79] = Convert.ToString(ItemBank80);
            stack[79] = Convert.ToInt32( ItemBank80Stack);

            items[80] = Convert.ToString(ItemBank81);
            stack[80] = Convert.ToInt32( ItemBank81Stack);
            items[81] = Convert.ToString(ItemBank82);
            stack[81] = Convert.ToInt32( ItemBank82Stack);
            items[82] = Convert.ToString(ItemBank83);
            stack[82] = Convert.ToInt32( ItemBank83Stack);
            items[83] = Convert.ToString(ItemBank84);
            stack[83] = Convert.ToInt32( ItemBank84Stack);
            items[84] = Convert.ToString(ItemBank85);
            stack[84] = Convert.ToInt32( ItemBank85Stack);
            items[85] = Convert.ToString(ItemBank86);
            stack[85] = Convert.ToInt32( ItemBank86Stack);
            items[86] = Convert.ToString(ItemBank87);
            stack[86] = Convert.ToInt32( ItemBank87Stack);
            items[87] = Convert.ToString(ItemBank88);
            stack[87] = Convert.ToInt32( ItemBank88Stack);
            items[88] = Convert.ToString(ItemBank89);
            stack[88] = Convert.ToInt32( ItemBank89Stack);
            items[89] = Convert.ToString(ItemBank90);
            stack[89] = Convert.ToInt32( ItemBank90Stack);

            items[90] = Convert.ToString(ItemBank91);
            stack[90] = Convert.ToInt32( ItemBank91Stack);
            items[91] = Convert.ToString(ItemBank92);
            stack[91] = Convert.ToInt32( ItemBank92Stack);
            items[92] = Convert.ToString(ItemBank93);
            stack[92] = Convert.ToInt32( ItemBank93Stack);
            items[93] = Convert.ToString(ItemBank94);
            stack[93] = Convert.ToInt32( ItemBank94Stack);
            items[94] = Convert.ToString(ItemBank95);
            stack[94] = Convert.ToInt32( ItemBank95Stack);
            items[95] = Convert.ToString(ItemBank96);
            stack[95] = Convert.ToInt32( ItemBank96Stack);
            items[96] = Convert.ToString(ItemBank97);
            stack[96] = Convert.ToInt32( ItemBank97Stack);
            items[97] = Convert.ToString(ItemBank98);
            stack[97] = Convert.ToInt32( ItemBank98Stack);
            items[98] = Convert.ToString(ItemBank99);
            stack[98] = Convert.ToInt32( ItemBank99Stack);
            items[99] = Convert.ToString(ItemBank100);
            stack[99] = Convert.ToInt32( ItemBank100Stack);

        }
        public void UpdateItemBankData(string[] items, int[] stack)
        {
            ItemBank1       = items[0];
            ItemBank1Stack  = stack[0].ToString();
            ItemBank2       = items[1];
            ItemBank2Stack  = stack[1].ToString();
            ItemBank3       = items[2];
            ItemBank3Stack  = stack[2].ToString();
            ItemBank4       = items[3];
            ItemBank4Stack  = stack[3].ToString();
            ItemBank5       = items[4];
            ItemBank5Stack  = stack[4].ToString();
            ItemBank6       = items[5];
            ItemBank6Stack  = stack[5].ToString();
            ItemBank7       = items[6];
            ItemBank7Stack  = stack[6].ToString();
            ItemBank8       = items[7];
            ItemBank8Stack  = stack[7].ToString();
            ItemBank9       = items[8];
            ItemBank9Stack  = stack[8].ToString();
            ItemBank10      = items[9];
            ItemBank10Stack = stack[9].ToString();

            ItemBank11       = items[10];
            ItemBank11Stack  = stack[10].ToString();
            ItemBank12       = items[11];
            ItemBank12Stack  = stack[11].ToString();
            ItemBank13       = items[12];
            ItemBank13Stack  = stack[12].ToString();
            ItemBank14       = items[13];
            ItemBank14Stack  = stack[13].ToString();
            ItemBank15       = items[14];
            ItemBank15Stack  = stack[14].ToString();
            ItemBank16       = items[15];
            ItemBank16Stack  = stack[15].ToString();
            ItemBank17       = items[16];
            ItemBank17Stack  = stack[16].ToString();
            ItemBank18       = items[17];
            ItemBank18Stack  = stack[17].ToString();
            ItemBank19       = items[18];
            ItemBank19Stack  = stack[18].ToString();
            ItemBank20       = items[19];
            ItemBank20Stack  = stack[19].ToString();

            ItemBank21       = items[20];
            ItemBank21Stack  = stack[20].ToString();
            ItemBank22       = items[21];
            ItemBank22Stack  = stack[21].ToString();
            ItemBank23       = items[22];
            ItemBank23Stack  = stack[22].ToString();
            ItemBank24       = items[23];
            ItemBank24Stack  = stack[23].ToString();
            ItemBank25       = items[24];
            ItemBank25Stack  = stack[24].ToString();
            ItemBank26       = items[25];
            ItemBank26Stack  = stack[25].ToString();
            ItemBank27       = items[26];
            ItemBank27Stack  = stack[26].ToString();
            ItemBank28       = items[27];
            ItemBank28Stack  = stack[27].ToString();
            ItemBank29       = items[28];
            ItemBank29Stack  = stack[28].ToString();
            ItemBank30       = items[29];
            ItemBank30Stack  = stack[29].ToString();

            ItemBank31       = items[30];
            ItemBank31Stack  = stack[30].ToString();
            ItemBank32       = items[31];
            ItemBank32Stack  = stack[31].ToString();
            ItemBank33       = items[32];
            ItemBank33Stack  = stack[32].ToString();
            ItemBank34       = items[33];
            ItemBank34Stack  = stack[33].ToString();
            ItemBank35       = items[34];
            ItemBank35Stack  = stack[34].ToString();
            ItemBank36       = items[35];
            ItemBank36Stack  = stack[35].ToString();
            ItemBank37       = items[36];
            ItemBank37Stack  = stack[36].ToString();
            ItemBank38       = items[37];
            ItemBank38Stack  = stack[37].ToString();
            ItemBank39       = items[38];
            ItemBank39Stack  = stack[38].ToString();
            ItemBank40       = items[39];
            ItemBank40Stack  = stack[39].ToString();

            ItemBank41       = items[40];
            ItemBank41Stack  = stack[40].ToString();
            ItemBank42       = items[41];
            ItemBank42Stack  = stack[41].ToString();
            ItemBank43       = items[42];
            ItemBank43Stack  = stack[42].ToString();
            ItemBank44       = items[43];
            ItemBank44Stack  = stack[43].ToString();
            ItemBank45       = items[44];
            ItemBank45Stack  = stack[44].ToString();
            ItemBank46       = items[45];
            ItemBank46Stack  = stack[45].ToString();
            ItemBank47       = items[46];
            ItemBank47Stack  = stack[46].ToString();
            ItemBank48       = items[47];
            ItemBank48Stack  = stack[47].ToString();
            ItemBank49       = items[48];
            ItemBank49Stack  = stack[48].ToString();
            ItemBank50       = items[49];
            ItemBank50Stack  = stack[49].ToString();

            ItemBank51       = items[50];
            ItemBank51Stack  = stack[50].ToString();
            ItemBank52       = items[51];
            ItemBank52Stack  = stack[51].ToString();
            ItemBank53       = items[52];
            ItemBank53Stack  = stack[52].ToString();
            ItemBank54       = items[53];
            ItemBank54Stack  = stack[53].ToString();
            ItemBank55       = items[54];
            ItemBank55Stack  = stack[54].ToString();
            ItemBank56       = items[55];
            ItemBank56Stack  = stack[55].ToString();
            ItemBank57       = items[56];
            ItemBank57Stack  = stack[56].ToString();
            ItemBank58       = items[57];
            ItemBank58Stack  = stack[57].ToString();
            ItemBank59       = items[58];
            ItemBank59Stack  = stack[58].ToString();
            ItemBank60       = items[59];
            ItemBank60Stack  = stack[59].ToString();        

            ItemBank61       = items[60];
            ItemBank61Stack  = stack[60].ToString();
            ItemBank62       = items[61];
            ItemBank62Stack  = stack[61].ToString();
            ItemBank63       = items[62];
            ItemBank63Stack  = stack[62].ToString();
            ItemBank64       = items[63];
            ItemBank64Stack  = stack[63].ToString();
            ItemBank65       = items[64];
            ItemBank65Stack  = stack[64].ToString();
            ItemBank66       = items[65];
            ItemBank66Stack  = stack[65].ToString();
            ItemBank67       = items[66];
            ItemBank67Stack  = stack[66].ToString();
            ItemBank68       = items[67];
            ItemBank68Stack  = stack[67].ToString();
            ItemBank69       = items[68];
            ItemBank69Stack  = stack[68].ToString();
            ItemBank70       = items[69];
            ItemBank70Stack  = stack[69].ToString();

            ItemBank71       = items[70];
            ItemBank71Stack  = stack[70].ToString();
            ItemBank72       = items[71];
            ItemBank72Stack  = stack[71].ToString();
            ItemBank73       = items[72];
            ItemBank73Stack  = stack[72].ToString();
            ItemBank74       = items[73];
            ItemBank74Stack  = stack[73].ToString();
            ItemBank75       = items[74];
            ItemBank75Stack  = stack[74].ToString();
            ItemBank76       = items[75];
            ItemBank76Stack  = stack[75].ToString();
            ItemBank77       = items[76];
            ItemBank77Stack  = stack[76].ToString();
            ItemBank78       = items[77];
            ItemBank78Stack  = stack[77].ToString();
            ItemBank79       = items[78];
            ItemBank79Stack  = stack[78].ToString();
            ItemBank80       = items[79];
            ItemBank80Stack  = stack[79].ToString();

            ItemBank81       = items[80];
            ItemBank81Stack  = stack[80].ToString();
            ItemBank82       = items[81];
            ItemBank82Stack  = stack[81].ToString();
            ItemBank83       = items[82];
            ItemBank83Stack  = stack[82].ToString();
            ItemBank84       = items[83];
            ItemBank84Stack  = stack[83].ToString();
            ItemBank85       = items[84];
            ItemBank85Stack  = stack[84].ToString();
            ItemBank86       = items[85];
            ItemBank86Stack  = stack[85].ToString();
            ItemBank87       = items[86];
            ItemBank87Stack  = stack[86].ToString();
            ItemBank88       = items[87];
            ItemBank88Stack  = stack[87].ToString();
            ItemBank89       = items[88];
            ItemBank89Stack  = stack[88].ToString();
            ItemBank90       = items[89];
            ItemBank90Stack  = stack[89].ToString();

            ItemBank91       = items[90];
            ItemBank91Stack  = stack[90].ToString();
            ItemBank92       = items[91];
            ItemBank92Stack  = stack[91].ToString();
            ItemBank93       = items[92];
            ItemBank93Stack  = stack[92].ToString();
            ItemBank94       = items[93];
            ItemBank94Stack  = stack[93].ToString();
            ItemBank95       = items[94];
            ItemBank95Stack  = stack[94].ToString();
            ItemBank96       = items[95];
            ItemBank96Stack  = stack[95].ToString();
            ItemBank97       = items[96];
            ItemBank97Stack  = stack[96].ToString();
            ItemBank98       = items[97];
            ItemBank98Stack  = stack[97].ToString();
            ItemBank99       = items[98];
            ItemBank99Stack  = stack[98].ToString();
            ItemBank100      = items[99];
            ItemBank100Stack = stack[99].ToString();
        }
    }
}
