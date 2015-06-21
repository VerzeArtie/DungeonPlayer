using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DungeonPlayer
{
    public class ItemBackPack : IComparable
    {
        public enum ItemType
        {
            None,
            Weapon_Light,
            Weapon_Middle,
            Weapon_Heavy,
            Weapon_TwoHand,
            Weapon_Rod, // ��Ғǉ�
            Armor_Light,
            Armor_Middle,
            Armor_Heavy,
            Shield,
            Accessory,
            Material_Equip,
            Material_Potion,
            Material_Food,
            Use_Potion,
            Use_Any,
            Useless,
        }
        public enum Equipable
        {
            All,
            Ein,
            Lana,
            Verze,
            Ol, // ��Ғǉ�
            Kahl, // ��Ғǉ�
        }
        public enum RareLevel
        {
            Poor,
            Common,
            Rare,
            Epic,
            Legendary,
        }

        // s ��Ғǉ�
        protected int stackValue = 1; // �����������_�łP�̃I�u�W�F�N�g�����邽�߁A�����I�ɂP��錾
        protected int limitValue = Database.MAX_ITEM_STACK_SIZE; // �I�u�W�F�N�g���X�^�b�N�ł���ő吔

        // [comment] �A�C�e�����Օi���ARARE_EPIC�������ꍇ�̓X�^�b�N�P�Ƃ��邱�ƁB
        //           �����i��RARE_EPIC�Ɠ����ŋC�ɂ��Ȃ��ėǂ��B
        public const int USING_ITEM_STACK_SIZE = 5;
        public const int RARE_EPIC_ITEM_STACK_SIZE = 1;
        public const int EQUIP_ITEM_STACK_SIZE = 1;
        public const int MATERIAL_ITEM_STACK_SIZE = 10;
        public const int OTHER_ITEM_STACK_SIZE = 10;
        // e ��Ғǉ�

        protected string name = String.Empty;
        protected string description = string.Empty;
        protected int minValue = 0;
        protected int maxValue = 0;
        protected int cost = 0;
        protected ItemType type = ItemType.None;
        protected Equipable equipablePerson = Equipable.All;
        protected RareLevel rareLevel = RareLevel.Poor;
        protected int buffUpStrength = 0;
        protected int buffUpAgility = 0;
        protected int buffUpIntelligence = 0;
        protected int buffUpStamina = 0;
        protected int buffUpMind = 0;
        protected double amplifyPhysicalAttack = 0.0f; // ��Ғǉ�
        protected double amplifyPhysicalDefense = 0.0f; // ��Ғǉ�
        protected double amplifyMagicAttack = 0.0f; // ��Ғǉ�
        protected double amplifyMagicDefense = 0.0f; // ��Ғǉ�
        protected double amplifyBattleSpeed = 0.0f; // ��Ғǉ�
        protected double amplifyBattleResponse = 0.0f; // ��Ғǉ�
        protected double amplifyPotential = 0.0f; // ��Ғǉ�
        protected double amplifyLight = 0.0f; // ��Ғǉ�
        protected double amplifyShadow = 0.0f; // ��Ғǉ�
        protected double amplifyFire = 0.0f; // ��Ғǉ�
        protected double amplifyIce = 0.0f; // ��Ғǉ�
        protected double amplifyForce = 0.0f; // ��Ғǉ�
        protected double amplifyWill = 0.0f; // ��Ғǉ�

        protected double effectValue1 = 0; // ��Ғǉ�(�ő�X�L���|�C���g����)
        protected double manaCostReduction = 0; // ��Ғǉ�(���@����y��)
        protected double manaCostReductionLight = 0; // ��Ғǉ�
        protected double manaCostReductionShadow = 0; // ��Ғǉ�
        protected double manaCostReductionFire = 0; // ��Ғǉ�
        protected double manaCostReductionIce = 0; // ��Ғǉ�
        protected double manaCostReductionForce = 0; // ��Ғǉ�
        protected double manaCostReductionWill = 0; // ��Ғǉ�
        protected double skillCostReduction = 0; // ��Ғǉ��i�X�L������y���j
        protected double skillCostReductionActive = 0; // ��Ғǉ�
        protected double skillCostReductionPassive = 0; // ��Ғǉ�
        protected double skillCostReductionSoft = 0; // ��Ғǉ�
        protected double skillCostReductionHard = 0; // ��Ғǉ�
        protected double skillCostReductionTruth = 0; // ��Ғǉ�
        protected double skillCostReductionVoid = 0; // ��Ғǉ�

        protected bool switchStatus1 = false; // ��Ғǉ��i���C�Y�E�L���[�u�̕���/���@�̑Ώې؂�ւ��ɂ������l)

        protected string information = String.Empty;
        protected bool useSpecialAbility = false;
        protected bool afterBroken = false; // �W�����N�E�^���X�}���������A�퓬�I����ɃA�C�e���j�����邽�߂ɗp�ӂ����t���O
        protected bool onlyOnce = false; // �f�^�b�`�����g�E�I�[�u�ɂ��A�퓬���Ɉ�x���������ł��Ȃ����߂ɗp�ӂ����t���O
        protected string imprintCommand = String.Empty; // ���������̚�ɂ��A�L�����Z���Ώۖ��@�̖��O���o���邽�߂ɗp�ӂ���
        protected bool effectStatus = false; // �ʎ蔠�w�H�ʁx�ɂ��A���S����x�����h�����邽�߂ɗp�ӂ����t���O

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }
        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }
        public int MagicMinValue { get; set; } // ��Ғǉ�
        public int MagicMaxValue { get; set; } // ��Ғǉ�

        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public ItemType Type
        {
            get { return type; }
            set { type = value; }
        }
        public Equipable EquipablePerson
        {
            get { return equipablePerson; }
            set { equipablePerson = value; }
        }
        public RareLevel Rare
        {
            get { return rareLevel; }
            set { rareLevel = value; }
        }
        // s ��Ғǉ�
        public int StackValue
        {
            get { return stackValue; }
            set { stackValue = value; }
        }
        public int LimitValue
        {
            get { return limitValue; }
            set { limitValue = value; }
        }
        // e ��Ғǉ�
        public int BuffUpStrength
        {
            get { return buffUpStrength; }
            set { buffUpStrength = value; }
        }
        public int BuffUpAgility
        {
            get { return buffUpAgility; }
            set { buffUpAgility = value; }
        }
        public int BuffUpIntelligence
        {
            get { return buffUpIntelligence; }
            set { buffUpIntelligence = value; }
        }
        public int BuffUpStamina
        {
            get { return buffUpStamina; }
            set { buffUpStamina = value; }
        }
        public int BuffUpMind
        {
            get { return buffUpMind; }
            set { buffUpMind = value; }
        }
        // s ��Ғǉ�
        public double AmplifyPhysicalAttack
        {
            get { return amplifyPhysicalAttack; }
            set { amplifyPhysicalAttack = value; }
        }
        public double AmplifyPhysicalDefense
        {
            get { return amplifyPhysicalDefense; }
            set { amplifyPhysicalDefense = value; }
        }
        public double AmplifyMagicAttack
        {
            get { return amplifyMagicAttack; }
            set { amplifyMagicAttack = value; }
        }
        public double AmplifyMagicDefense
        {
            get { return amplifyMagicDefense; }
            set { amplifyMagicDefense = value; }
        }
        public double AmplifyBattleSpeed
        {
            get { return amplifyBattleSpeed; }
            set { amplifyBattleSpeed = value; }
        }
        public double AmplifyBattleResponse
        {
            get { return amplifyBattleResponse; }
            set { amplifyBattleResponse = value; }
        }
        public double AmplifyPotential
        {
            get { return amplifyPotential; }
            set { amplifyPotential = value; }
        }
        public double AmplifyLight
        {
            get { return amplifyLight; }
            set { amplifyLight = value; }
        }
        public double AmplifyShadow
        {
            get { return amplifyShadow; }
            set { amplifyShadow = value; }
        }
        public double AmplifyFire
        {
            get { return amplifyFire; }
            set { amplifyFire = value; }
        }
        public double AmplifyIce
        {
            get { return amplifyIce; }
            set { amplifyIce = value; }
        }
        public double AmplifyForce
        {
            get { return amplifyForce; }
            set { amplifyForce = value; }
        }
        public double AmplifyWill
        {
            get { return amplifyWill; }
            set { amplifyWill = value; }
        }
        public double EffectValue1
        {
            get { return effectValue1; }
            set { effectValue1 = value; }
        }
        public double ManaCostReduction
        {
            get { return manaCostReduction; }
            set { manaCostReduction = value; }
        }

        public double ManaCostReductionLight
        {
            get { return manaCostReductionLight; }
            set { manaCostReductionLight = value; }
        }
        public double ManaCostReductionShadow
        {
            get { return manaCostReductionShadow; }
            set { manaCostReductionShadow = value; }
        }
        public double ManaCostReductionFire
        {
            get { return manaCostReductionFire; }
            set { manaCostReductionFire = value; }
        }
        public double ManaCostReductionIce
        {
            get { return manaCostReductionIce; }
            set { manaCostReductionIce = value; }
        }
        public double ManaCostReductionForce
        {
            get { return manaCostReductionForce; }
            set { manaCostReductionForce = value; }
        }
        public double ManaCostReductionWill
        {
            get { return manaCostReductionWill; }
            set { manaCostReductionWill = value; }
        }

        public double SkillCostReduction
        {
            get { return skillCostReduction; }
            set { skillCostReduction = value; }
        }
        public double SkillCostReductionActive
        {
            get { return skillCostReductionActive; }
            set { skillCostReductionActive = value; }
        }
        public double SkillCostReductionPassive
        {
            get { return skillCostReductionPassive; }
            set { skillCostReductionPassive = value; }
        }
        public double SkillCostReductionSoft
        {
            get { return skillCostReductionSoft; }
            set { skillCostReductionSoft = value; }
        }
        public double SkillCostReductionHard
        {
            get { return skillCostReductionHard; }
            set { skillCostReductionHard = value; }
        }
        public double SkillCostReductionTruth
        {
            get { return skillCostReductionTruth; }
            set { skillCostReductionTruth = value; }
        }
        public double SkillCostReductionVoid
        {
            get { return skillCostReductionVoid; }
            set { skillCostReductionVoid = value; }
        }

        public bool SwitchStatus1
        {
            get { return switchStatus1; }
            set { switchStatus1 = value; }
        }
        // e ��Ғǉ�

        public string Information
        {
            get { return information; }
            set { information = value; }
        }
        // s ��Ғǉ�
        public int ResistFire { get; set; }
        public int ResistIce { get; set; }
        public int ResistLight { get; set; }
        public int ResistShadow { get; set; }
        public int ResistForce { get; set; }
        public int ResistWill { get; set; }
        // e ��Ғǉ�
        public bool UseSpecialAbility
        {
            get { return useSpecialAbility; }
            set { useSpecialAbility = value; }
        }
        // s ��Ғǉ�
        public bool AfterBroken
        {
            get { return afterBroken; }
            set { afterBroken = value; }
        }
        public bool EffectStatus
        {
            get { return effectStatus; }
            set { effectStatus = value; }�@
        }
        public bool OnlyOnce
        {
            get { return onlyOnce; }
            set { onlyOnce = value; }
        }
        public string ImprintCommand
        {
            get { return imprintCommand; }
            set { imprintCommand = value; }
        }
        // e ��Ғǉ�

        // s ��Ғǉ�
        public bool ResistStun { get; set; }
        public bool ResistSilence { get; set; }
        public bool ResistPoison { get; set; }
        public bool ResistTemptation { get; set; }
        public bool ResistFrozen { get; set; }
        public bool ResistParalyze { get; set; }
        public bool ResistSlow { get; set; }
        public bool ResistBlind { get; set; }
        public bool ResistSlip { get; set; }
        public bool ResistNoResurrection { get; set; }
        // e ��Ғǉ�

        public int UseIt()
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            return rd.Next(minValue, maxValue + 1);
        }

        protected void AdditionalDescription(ItemType s_type)
        {
            this.type = s_type;
            if (s_type == ItemType.Material_Equip)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_EQUIP_MATERIAL);
            }
            else if (s_type == ItemType.Material_Food)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_FOOD_MATERIAL);
            }
            else if (s_type == ItemType.Material_Potion)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_POTION_MATERIAL);
            }
            else if (s_type == ItemType.Useless || type == ItemType.None)
            {
                this.description = this.description.Insert(0, Database.DESCRIPTION_SELL_ONLY);
            }
        }

        /// <summary>
        /// �A�C�e���̃C���X�^���X�𐶐����܂��B�V�����ǉ�����ꍇ�AStatusPlayer�AEquipmentShop�AMainCharacter�ɂ����f���Ă����Ă��������B
        /// </summary>
        /// <param name="createName"></param>
        public ItemBackPack(string createName)
        {
            this.name = createName;
            switch (createName)
            {
                #region "�|�[�V�����n"
                case "�������ԃ|�[�V����":
                    description = "�����߂ɍ��ꂽ���C�t�񕜗p�̖�B�񕜗ʂV�O�`�P�P�O";
                    minValue = 70;
                    maxValue = 110;
                    cost = 100;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Poor;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "���ʂ̐ԃ|�[�V����":
                    description = "�W���I�ȑ傫���ō��ꂽ���C�t�񕜗p�̖�B�񕜗ʂP�S�O�`�Q�P�O";
                    minValue = 140;
                    maxValue = 210;
                    cost = 500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "�傫�Ȑԃ|�[�V����":
                    description = "��r�I�傫�߂ɍ��ꂽ���C�t�񕜗p�̖�B�񕜗ʂR�R�O�`�S�T�O";
                    minValue = 330;
                    maxValue = 450;
                    cost = 2500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "����ԃ|�[�V����":
                    description = "����T�C�Y�ō��ꂽ���C�t�񕜗p�̖�B�񕜗ʂW�P�O�`�P�O�Q�O";
                    minValue = 810;
                    maxValue = 1020;
                    cost = 7000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "���؂Ȑԃ|�[�V����":
                    description = "���؂ȑ�r�ō��ꂽ���C�t�񕜗p�̖�B�񕜗ʂP�T�O�O�`�Q�T�O�O";
                    minValue = 1500;
                    maxValue = 2500;
                    cost = 22000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case "�������|�[�V����":
                    description = "�����߂ɍ��ꂽ�}�i�񕜗p�̖�B�񕜗ʂT�O�`�W�O";
                    minValue = 50;
                    maxValue = 80;
                    cost = 100;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Poor;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "���ʂ̐|�[�V����":
                    description = "�W���I�ȑ傫���ō��ꂽ�}�i�񕜗p�̖�B�񕜗ʂP�S�O�`�Q�P�O";
                    minValue = 140;
                    maxValue = 210;
                    cost = 500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "�傫�Ȑ|�[�V����":
                    description = "��r�I�傫�߂ɍ��ꂽ�}�i�񕜗p�̖�B�񕜗ʂR�R�O�`�S�T�O";
                    minValue = 330;
                    maxValue = 450;
                    cost = 2500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "����|�[�V����":
                    description = "����T�C�Y�ō��ꂽ�}�i�񕜗p�̖�B�񕜗ʂW�P�O�`�P�O�Q�O";
                    minValue = 810;
                    maxValue = 1020;
                    cost = 7000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case "���؂Ȑ|�[�V����":
                    description = "���؂ȑ�r�ō��ꂽ�}�i�񕜗p�̖�B�񕜗ʂP�T�O�O�`�Q�T�O�O";
                    minValue = 1500;
                    maxValue = 2500;
                    cost = 22000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case "���O���ƂĂ��������ɂ͂܂��������ɗ������A���̌��ʂ��������Ȃ��𗧂����ł���ɂ�������炸�f�R���[�V���������������؂ȃX�[�p�[�~���N���|�[�V����":
                    description = "�f�o�b�O�p�B�񕜗ʂO�`�P";
                    minValue = 0;
                    maxValue = 1;
                    cost = 99999999;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Poor;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case "���[�x�X�g�����N�|�[�V����":
                    description = "�J�Řb��ƂȂ����Ŗ�i�H�j�B���܂��ꂽ����͗U�f�ɂ����Ă��܂��B�@�퓬����p�B�u�U�f�v��t�^";
                    minValue = 100;
                    maxValue = 100;
                    cost = 2000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case "�����@�C���|�[�V����":
                    description = "�����P�x�����A�_���W�������Ŏ��S�����p�[�e�B�����o�[�𕜊��������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 40000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_GOLD_POTION:
                    description = "���C�t�^�}�i�^�X�L�������ׂāA�S�񕜂���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�O�ҁF�_���W������E�K���c����A�C�e��"
                case "�u���[�}�e���A��": // �P�K�A�C�e��
                    description = "���F�̗����́B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1000;
                    AdditionalDescription(ItemType.None);
                    rareLevel = RareLevel.Common;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_CHARM_OF_FIRE_ANGEL: // �P�K�A�C�e��
                    description = "�����i��Ƃ�w�������V�g�̌아�B�Αϐ��T";
                    minValue = 5;
                    maxValue = 5;
                    cost = 350;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�`���N���I�[�u": // �P�K�A�C�e��
                    description = "���_�`���N�����f���o���I�[�u�B�m���{�T";
                    minValue = 5;
                    maxValue = 5;
                    buffUpIntelligence = 5;
                    cost = 400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���ׂȃp���[�����O": // �K���c�̕���̔��i�_���W�����P�K�j
                    description = "�����҂̂��C�������������郊���O�B�r�́{�T";
                    minValue = 5;
                    maxValue = 5;
                    buffUpStrength = 5;
                    cost = 500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "��̍���": // �Q�K�A�C�e��
                    description = "��̎p���`����Ă��鍏��B�r�́{�P�O�@�m���{�P�O";
                    minValue = 10;
                    maxValue = 10;
                    buffUpStrength = 10;
                    buffUpIntelligence = 10;
                    cost = 2900;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�_����": // �Q�K�A�C�e��
                    description = "�񑩂��ꂽ�񕜖�B�����P�x�������C�t�A�X�L���A�}�i�����ꂼ��30%�񕜁B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 5200;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case "�g���킵�̃}���g": // �Q�K�A�C�e��
                    description = "�{�l�̈ӎv�Ɋ֌W�Ȃ��A�ɂ܂�ɓG�̍U���������}���g�B";
                    minValue = 30;
                    maxValue = 30;
                    cost = 1700;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���ɂ̃X�^�[�G���u����": // �K���c�̕���̔��i�_���W�����Q�K�j
                    description = "���^�ɍ��ɂ̃C���[�W���悹���G���u�����B�m���{�P�O�A�S�{�P�O";
                    minValue = 10;
                    maxValue = 10;
                    buffUpIntelligence = 10;
                    buffUpMind = 10;
                    cost = 2800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�����o���h": // �K���c�̕���̔��i�_���W�����Q�K�j
                    description = "�����҂̂��C��������������o���h�B�r�́{�P�W";
                    minValue = 18;
                    maxValue = 18;
                    buffUpStrength = 18;
                    cost = 4200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���b�h�}�e���A��": // �R�K�A�C�e��
                    description = "���ԐF�̗����́B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 10000;
                    AdditionalDescription(ItemType.None);
                    rareLevel = RareLevel.Common;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;
                case "���C�I���n�[�g": // �R�K�A�C�e��
                    description = "�S�b�̉��̗͂��h����Ă���y���_���g�B�r�́{�Q�T�A�Z�p�{�Q�T";
                    minValue = 25;
                    maxValue = 25;
                    buffUpStrength = 25;
                    BuffUpAgility = 25;
                    cost = 5600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�I�[�K�̘r��": // �R�K�A�C�e��
                    description = "�I�[�K�̗̑͂��N���o��r�́B�r�́{�Q�O�A�̗́{�P�Q";
                    minValue = 20;
                    maxValue = 12;
                    buffUpStrength = 20;
                    buffUpStamina = 12;
                    cost = 6600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�|�S�̐Α�": // �R�K�A�C�e��
                    description = "�|�S�̐��_���h���Ă���Α��B���܂ɃX�^�����ʂ�h������A�X�^����Ԃ��畜�A����B";
                    minValue = 30;
                    maxValue = 30;
                    cost = 4800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�t�@���l�M�̃V�[��": // �R�K�A�C�e��
                    description = "���܃t�@���l�ւ̐M�i�ϐM�j�̏؂������V�[���B�S�{�T�O";
                    minValue = 50;
                    maxValue = 50;
                    buffUpMind = 50;
                    cost = 7600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�v���[�g�E�A�[�}�[": // �R�K�A�C�e��
                    description = "�����|�f�ނ����ɂ��āA�܂�ڂ𖳂����悤�ɍ��ꂽ�Z�B�h��͂Q�S�`�R�P";
                    minValue = 24;
                    maxValue = 31;
                    cost = 9600;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�������E�A�[�}�[": // �R�K�A�C�e��
                    description = "���߂̍|���q�����킹�A�����Ȃ��̗ǂ��Ɗђʌn�ɑ΂���h��𗼗��������Z�B�h��͂Q�P�`�Q�V";
                    minValue = 21;
                    maxValue = 27;
                    cost = 8100;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V�����V�[��": // �R�K�A�C�e��
                    description = "�ђʌn�U���ł͂Ȃ��A�Ȑ��ɗ����͂������o�����悤�ɍ��ꂽ���B�U���͂S�O�`�U�T";
                    minValue = 40;
                    maxValue = 65;
                    cost = 9000;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�E�F���j�b�P�̘r��": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "�����E�F���j�b�P�̑f�ނ��g���Đ������ꂽ�r�ցB�̗́{�Q�O";
                    minValue = 20;
                    maxValue = 20;
                    buffUpStamina = 20;
                    cost = 7200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���҂̊ዾ": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "���@�X�^�ꂩ�瑗���Ă����M�d�Ȉ�i�B�Z�{�R�O�A�m�́{�Q�T";
                    minValue = 30;
                    maxValue = 25;
                    buffUpAgility = 30;
                    buffUpIntelligence = 25;
                    cost = 9500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���F�v���Y���o���h": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "�����҂̑S�̓I�Ȕ\�͂������o�����߂ɐ������ꂽ�A�N�Z�T���B�S�\�́{�Q�O";
                    minValue = 20;
                    maxValue = 20;
                    buffUpStrength = 20;
                    buffUpAgility = 20;
                    buffUpIntelligence = 20;
                    buffUpStamina = 20;
                    buffUpMind = 20;
                    cost = 53000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�Đ��̖��": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "�����҂̐������������o���A�N�Z�T���B�́{�S�O�A�^�[���I�����A���C�t����΂����񕜂���B";
                    minValue = 40; // �̗́{�S�O
                    maxValue = 7; // ���R�񕜂V��
                    buffUpStamina = 40;
                    information = "���R�񕜁{�V��";
                    cost = 52000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V�[���I�u�A�N�A���t�@�C�A": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "�y�u���I�A�N�A���t�@�C�A�z�W���[�X�����Ă̌��ƂȂ��Ă���A�N�Z�T���B�@�Αϐ��R�O���A���ϐ��R�O��";
                    minValue = 30;
                    maxValue = 30;
                    cost = 48000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�h���S���̃x���g": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "�h���S���̗؂Ő������ꂽ�x���g�B�@�́{�R�T�A�m�{�S�O";
                    minValue = 35;
                    maxValue = 40;
                    buffUpStrength = 35;
                    buffUpIntelligence = 40;
                    cost = 65000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�G�X�p�_�X": // �_���W�����S�K�̃A�C�e��
                    description = "���ẪG�X�p�[�_�푰���h��������ɍ��ꂽ���B�U���͂P�R�P�`�P�S�T";
                    minValue = 131;
                    maxValue = 145;
                    cost = 9200;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�O���[���}�e���A��": // �_���W�����S�K�̃A�C�e��
                    description = "���ΐF�̗����́B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 22000;
                    AdditionalDescription(ItemType.None);
                    rareLevel = RareLevel.Common;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;
                case "�A���H�C�h�E�N���X": // �_���W�����S�K�̃A�C�e��
                    description = "�퓬���̉�������ɂ����č쐬���ꂽ�����߁B�h��͂Q�S�`�Q�X";
                    minValue = 24;
                    maxValue = 29;
                    cost = 14000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�u���K���_�B��": // �_���W�����S�K�̃A�C�e��
                    description = "���^�y���ł��ϋv�������߂�����ЂŖD��ꂽ�Z�B�h��͂Q�U�`�R�P";
                    minValue = 26;
                    maxValue = 31;
                    cost = 11000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�\�[�h�E�I�u�E�u���[���[�W��": // �_���W�����S�K�̃A�C�e��
                    description = "����ʂƐԂ����т��t�^����Ă��錕�B�U���͂P�Q�V�`�P�U�P";
                    minValue = 127;
                    maxValue = 161;
                    cost = 78000;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�����̈��": // �_���W�����S�K�̃A�C�e��
                    description = "��z�͂�{�����߂ɕt������V�[���B �m�́{�R�T�A�S�{�R�O";
                    minValue = 35;
                    maxValue = 30;
                    buffUpIntelligence = 35;
                    buffUpMind = 30;
                    cost = 9400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V�g�̌_��": // �_���W�����S�K�̃A�C�e��
                    description = "�V�g�̉���𓾂邽�߂̌_�񏑁B�́{�T�O�O";
                    description += "\r\n�y����\�́z�@�^�[���I�����A�X�^��/����/�ғ�/�U�f/����/���/���|/�݉�/�Èł��瑦�����A�ł���B"; // ��ҕҏW�i��ԉ����Ƒ́{�T�O�O��ǋL�j
                    minValue = 100;
                    maxValue = 100;
                    buffUpStamina = 500;
                    cost = 61000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�����J�E�Z�O�����^�[�^": // �_���W�����S�K�̃A�C�e��
                    description = "����ȑŌ��E���Ōn�ɑς�����Z�B�h��͂R�T�`�R�X";
                    minValue = 35;
                    maxValue = 39;
                    cost = 13000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�X��̃u���X���b�g": // ���i��������
                    description = "�X��̍ގ��ō��ꂽ�u���X���b�g�B�S�{�Q";
                    minValue = 2;
                    maxValue = 2;
                    buffUpMind = 2;
                    cost = 100;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Lana;
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V��̗��i���v���J�j": // ���F���[��������
                    description = "���F���[�������̓����𓾂邽�߂Ɏ��O�ō쐬�������v���J�B�Z�p�{�T�O";
                    minValue = 50;
                    maxValue = 50;
                    BuffUpAgility = 50;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�����̐���": // �������i��b�C�x���g�œ���A�C�e��
                    description = "�_���W�������E���鎖���ł�������B���x�g���Ă������Ȃ�Ȃ��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Any);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case "����̓y���_���g": // ���i���x���A�b�v���ł��炦��A�C�e��
                    description = "���i���O��Ő��삵�����̖�͂��������y���_���g�B�́{�P�T�A�S�{�P�T";
                    minValue = 15;
                    maxValue = 15;
                    buffUpStrength = 15;
                    buffUpMind = 15;
                    cost = 8500;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Ein;
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // s ��ҕҏW
                case "�G���~�E�W�����W���@�t�@�[�W�����Ƃ̍���":
                    description = "FiveSeeker�̈�l�G���~�E�W�����W����p���󂪒����Ă��郊���O�B�S�p�����^�{�P�O�T�Q";
                    minValue = 1052;
                    maxValue = 1052;
                    MagicMinValue = 1052;
                    MagicMaxValue = 1052;
                    buffUpStrength = 1052;
                    buffUpAgility = 1052;
                    buffUpIntelligence = 1052;
                    buffUpStamina = 1052;
                    buffUpMind = 1052;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�t�@���E�t���[���@�V�g�̃y���_���g":
                    description = "FiveSeeker�̈�l�t�@���E�t���[�����g�ɒ����Ă��锼�����̃y���_���g�B�S�{�T�W�X�Q";
                    minValue = 5892;
                    maxValue = 5892;
                    buffUpMind = 5892;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V�j�L�A�E�J�[���n���c�@�����f�r���A�C":
                    description = "FiveSeeker�̈�l�V�j�L�A�E�J�[���n���c�����ڊ�ɑ������Ă���ł̋[��B�m�{�T�S�U�W";
                    minValue = 5468;
                    maxValue = 5468;
                    buffUpIntelligence = 5468;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.LEGENDARY_GOD_FIRE_GLOVE: // "�I���E�����f�B�X�@���_�O���[�u": ��ҕҏW
                    description = "FiveSeeker�̈�l�I���E�����f�B�X�̉E����펞���ŕ��ł���O���[�u�B�́{�P�U�X�X�A�U���͂Q�Q�O�O�`�Q�U�O�O";
                    minValue = 2200;
                    maxValue = 2600;
                    buffUpStrength = 1699;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���F���[�E�A�[�e�B�@�V��̗�":
                    description = "FiveSeeker�̈�l���F���[�E�A�[�e�B���󒆂𑖂邽�߂ɗp���Ă���u�[�c�B�Z�{�T�U�X�P";
                    minValue = 5691;
                    maxValue = 5691;
                    BuffUpAgility = 5691;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // e ��ҕҏW

                case "���K�p�̌�": // �A�C����������
                    description = "���߂ă_���W�����ɖK���҂̂��߂ɍ��ꂽ���B�U���͂P�`�R";
                    minValue = 1;
                    maxValue = 3;
                    cost = 100;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���K�p�̃O���[�u":
                case "�i�b�N��": // ���i��������
                    description = "���߂ă_���W�����ɖK���҂̂��߂ɍ��ꂽ�O���[�u�B�U���͂P�`�Q";
                    minValue = 1;
                    maxValue = 2;
                    cost = 100;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "����̌��i���v���J�j": // ���F���[��������
                    description = "���F���[���ȑO�������Ă����������O�ō쐬�������v���J�B�U���͂R�V�`�T�W";
                    minValue = 37;
                    maxValue = 58;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V���[�g�\�[�h": // �K���c�̕���̔��i�_���W�����P�K�j
                    description = "����肪�悭�����W���I�Ȍ��B�U���͂S�`�X";
                    minValue = 4;
                    maxValue = 9;
                    cost = 500;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�������ꂽ�����O�\�[�h": // �K���c�̕���̔��i�_���W�����P�K�j
                    description = "������̗͂��������鎖�ŏ\���ȗ͂𔭊��ł��錕�B�U���͂P�O�`�Q�O";
                    minValue = 10;
                    maxValue = 20;
                    cost = 1200;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���̌�": // �K���c�̕���̔��i�_���W�����Q�K�j
                    description = "���̍ގ���ǍD�Ɉ����o�������B�U���͂Q�T�`�R�V";
                    minValue = 25;
                    maxValue = 37;
                    cost = 3200;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���^���t�B�X�g": // �K���c�̕���̔��i�_���W�����Q�K�j
                    description = "���^�����̍ޗ����O���[�u�̌`�Ɏd���ďグ����i�B�U���͂Q�Q�`�R�R";
                    minValue = 22;
                    maxValue = 33;
                    cost = 2600;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�v���`�i�\�[�h": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "�v���`�i���ō쐬���ꂽ���B�U���͂S�Q�`�U�W";
                    minValue = 42;
                    maxValue = 68;
                    cost = 7700;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�t�@���V�I��": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "�@�ׂȐ؂ꖡ�����A�@���a�鎖�ɓ����������B�U���͂R�T�`�V�V";
                    minValue = 35;
                    maxValue = 77;
                    cost = 8200;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�A�C�A���N���[": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "�S���̂����܂��t�^����Ă���O���[�u�B�U���͂S�T�`�T�T";
                    minValue = 45;
                    maxValue = 55;
                    cost = 6900;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���C�g�v���Y�}�u���[�h": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "���ƈ�Ȃ����̒��ɏh�点���B�K���c�����̈�i�B�U���͂P�Q�R�`�P�T�P";
                    minValue = 123;
                    maxValue = 151;
                    cost = 32000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�C�X���A���t�B�X�g": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "��z�����w�����`�[�t�ɂ������I���n���R�����O���[�u�B�U���͂P�S�S�`�P�U�V";
                    minValue = 144;
                    maxValue = 167;
                    cost = 28000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // s ��ҕҏW
                case Database.LEGENDARY_FELTUS:
                    description = "�������A�����薳���A���`�̐_���B�S��L����҂����̐^���𔭊��ł���B�U���͂P�`�W�X�V�S";
                    description += "\r\n�y����\�́z�C�ӂ̍s�����s�����тɁA�_�̒~�σJ�E���^�[���������BUFF�Ƃ��Ē~�ς���B�~�ς��ꂽ�J�E���^�[�̕������A�y�S�z�p�����^���P�O�O�㏸����B�ő�30�܂Œ~�ς��s����B";
                    minValue = 1;
                    maxValue = 8974;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Legendary;
                    equipablePerson = Equipable.Ein;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�o��  �W���m�Z���X�e":
                    description = "�b�艮���@�X�^�O���̈�B�t�E���A�O�E��A���E�߁A�����̌��B�o�̎��_���K�v�B�y����\�́F�L�z�U���͂P�O�T�V�`�Q�W�X�U";
                    UseSpecialAbility = true;
                    minValue = 1057;
                    maxValue = 2896;
                    cost = 1120520;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�Ɍ�  �[�����M�A�X":
                    description = "�b�艮���@�X�^�O���̈�B�́E�Z�E�m�E�́E�S�A�܂̌��B�S�\�͂��K�v�B�y����\�́F�L�z�U���͂P�U�P�U�`�P�U�Q�O";
                    UseSpecialAbility = true;
                    minValue = 1616;
                    maxValue = 1620;
                    cost = 1053170;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�N���m�X�E���}�e�B�b�h�E�\�[�h":
                    description = "�b�艮�K���c�̍ō�����̈�B���Ԏ��𒴂����U�����\�Ƃ��錕�B�y����\�́F�L�z�U���͂Q�O�P�Q�`�Q�T�X�R";
                    UseSpecialAbility = true;
                    minValue = 2012;
                    maxValue = 2593;
                    cost = 1299420;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�w�p�C�X�g�X�E�p�i�b�T���C�j":
                    description = "�b�艮�K���c�̍ō�����̈�B���Ԏ��Ɋւ��閂�@�ƃX�L���𖳌�������Z�B�y����\�́F�L�z�h��͂P�Q�S�P�`�P�R�O�X";
                    UseSpecialAbility = true;
                    minValue = 1241;
                    maxValue = 1309;
                    cost = 1516190;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // e ��ҕҏW

                case "�^�C���E�I�u�E���[�Z": // �_���W�����T�K�̉B���A�C�e��
                    description = "�b�艮�K���c�̍ō�����𐶂ݏo�����߂̑f�ށB�@���p��p�i";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case "���߂̃`���j�b�N":
                case "�R�[�g�E�I�u�E�v���[�g": // �A�C����������
                    description = "���߂ă_���W�����ɖK���҂̂��߂ɍ��ꂽ�`���j�b�N�B�h��͂P�`�Q";
                    minValue = 1;
                    maxValue = 2;
                    cost = 100;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�y�߂̕�����":
                case "���C�g�E�N���X": // ���i��������
                    description = "���߂ă_���W�����ɖK���҂̂��߂ɍ��ꂽ�퓬�p�̈߁B�h��͂O�`�P";
                    minValue = 0;
                    maxValue = 1;
                    cost = 100;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���^��̊Z�i���v���J�j": // ���F���[��������
                    description = "���F���[���ȑO�������Ă����Z�����O�ō쐬�������v���J�B�h��͂P�T�`�P�W";
                    minValue = 15;
                    maxValue = 18;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�`���җp�̍������т�": // �K���c�̕���̔��i�_���W�����P�K�j
                    description = "�`���҂��悭�D��Ŏg���������т�B�h��͂P�`�R";
                    minValue = 1;
                    maxValue = 3;
                    cost = 400;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "���̊Z": // �K���c�̕���̔��i�_���W�����P�K�j
                    description = "�荠�ȏd���ł���A�����X�^�[�̍U�����悭�󂯎~�߂���Z�B�h��͂R�`�T";
                    minValue = 3;
                    maxValue = 5;
                    cost = 1500;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�^�J�̊Z": // �Q�K�A�C�e��
                    description = "�^�J���ŏo�����Z�B�h��͂S�`�W";
                    minValue = 4;
                    maxValue = 8;
                    cost = 1900;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "����̂���S�̃v���[�g": // �K���c�̕���̔��i�_���W�����Q�K�j
                    description = "�኱�̌��򂪍̗p����Ă���A���Ă�����̂����S������Z�v���[�g�B�h��͂P�P�`�P�T";
                    minValue = 11;
                    maxValue = 15;
                    cost = 3700;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V���N�̕�����": // �K���c�̕���̔��i�_���W�����Q�K�j
                case "�V���N���[�u": // �X�p�C�_�[�V���N�A�P�K�œ��肵���f�ނ��Q�K���i�Q���ȍ~�ŃK���c����̔��ɂȂ�B
                    description = "�V���N���Ő������ꂽ���ȕ����߁B�h��͂T�`�X";
                    minValue = 5;
                    maxValue = 9;
                    cost = 3100;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�V���o�[�A�[�}�[": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "����ւ̂������Ő������ꂽ�Z�B�h��͂Q�Q�`�R�O";
                    minValue = 22;
                    maxValue = 30;
                    cost = 7600;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�b�琻�̕�����": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "�b�̔��D���č쐬���ꂽ�����߁B�h��͂P�W�`�Q�T";
                    minValue = 18;
                    maxValue = 25;
                    cost = 7100;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�t�B�X�g�E�N���X": // �K���c�̕���̔��i�_���W�����R�K�j
                    description = "��ɑŌ��n�ɑ΂��ċ�������Ă���߁B�h��͂Q�Q�`�Q�V";
                    minValue = 22;
                    maxValue = 27;
                    cost = 10000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�v���Y�}�e�B�b�N�A�[�}�[": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "�v���Y���̎d�g�݂��������ɑg�ݍ��킹�č쐬���ꂽ�����̈�i�B�h��͂R�U�`�S�P";
                    minValue = 36;
                    maxValue = 41;
                    cost = 36000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�ɔ��������̉H��": // �K���c�̕���̔��i�_���W�����S�K�j
                    description = "�����ގ����ɗ͔������ĉH�߂ɂ��������̈�i�B�h��͂R�Q�`�R�V";
                    minValue = 32;
                    maxValue = 37;
                    cost = 40000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_OVER_SHIFTING: //"�I�[�o�[�V�t�e�B���O": // �_���W�����T�K
                    description = "�Ώۂ̐l���{����������\�͂�ύX����B�k�u���̃p�����^����U����ăZ�b�g����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case "���i�̃C�������O": // �_���W�����T�K�i���i�̃C�x���g�j
                    description = "���i�������t���Ă������C�ɓ���̃C�������O�B�p�r�s���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Lana;
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "���W�F���h�E���b�h�z�[�X": // �_���W�����T�K
                    description = "�Ԃ̓��n�̈ӎu���h���Ă���`���̖�́B�퓬����ΐ�U�ƂȂ�B";
                    minValue = 100;
                    maxValue = 100;
                    cost = 120000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "���i�E�G�O�[�L���[�W���i�[": // �_���W�����T�K
                    description = "���̋P�����h�点�����B�a���̂��тɁA�����P���Č�����Ƃ����B�U���͂Q�P�P�`�Q�S�W";
                    minValue = 211;
                    maxValue = 248;
                    cost = 140000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�����E�X��ւ̒�": // �_���W�����T�K
                    description = "�O��������`������Ă����ւ̌��Ɨ؂�f�ނƂ��č��ꂽ�����܁B�U���͂Q�R�X�`�Q�U�P";
                    minValue = 239;
                    maxValue = 261;
                    cost = 170000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�t�@�[�W���E�W�E�G�X�y�����U": // �_���W�����T�K
                    description = "�t�@�[�W���{�a���̐����鍑��̈�Ƃ��ď����Ă��錕�B�U���͂Q�O�V�`�Q�S�S";
                    minValue = 207;
                    maxValue = 244;
                    cost = 150000;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case "�v���C�h�E�I�u�E�V�[�J�[": // �K���c�̕���̔��i�_���W�����T�K�j
                    description = "�_���W���������҂����߂�e���������������B�S�{�X�X";
                    minValue = 99;
                    maxValue = 99;
                    buffUpMind = 99;
                    cost = 98000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "��������̌���": // �K���c�̕���̔��i�_���W�����T�K�j
                    description = "�_���W�����T���҂����߂�e���������������B�m�{�X�X";
                    minValue = 99;
                    maxValue = 99;
                    buffUpIntelligence = 99;
                    cost = 98000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�f�B�Z���V�����u�[�c": // �K���c�̕���̔��i�_���W�����T�K�j
                    description = "�_���W�����T���҂����߂�e���������������B�Z�{�X�X";
                    minValue = 99;
                    maxValue = 99;
                    buffUpAgility = 99;
                    cost = 98000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case "�n�[�g�u���[�J�[": // �K���c�̕���̔��i�_���W�����T�K�j
                    description = "�_���W�����T���҂����߂�e���������������B�́{�X�X";
                    minValue = 99;
                    maxValue = 99;
                    buffUpStrength = 99;
                    cost = 98000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;


                // s ��Ғǉ�
                case Database.POOR_PRACTICE_SHILED: // �A�C�����������i��ҁj
                    description = "���S�Ҍ����̏��B�y���Ď����₷�����ϋv���͖����B�h��͂P�`�P";
                    minValue = 1;
                    maxValue = 1;
                    cost = 100;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�P�K�F�����_���h���b�v"
                // �P��UP
                case Database.POOR_HINJAKU_ARMRING: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�ق�̂�p���[����������r�ցB�́{�Q";
                    BuffUpStrength = 2;
                    cost = 210;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_USUYOGORETA_FEATHER: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�݂��ڂ炵���t���H�B���������y������������B�Z�{�Q";
                    BuffUpAgility = 2;
                    cost = 210;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_NON_BRIGHT_ORB: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�m�����͊����Ă���I�[�u�B�m�{�Q";
                    BuffUpIntelligence = 2;
                    cost = 210;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_KUKEI_BANGLE: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�ۂ݂�ттĂȂ����߁A�������ɂ����o���O���B�́{�P";
                    BuffUpStamina = 1;
                    cost = 180;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_SUTERARESHI_EMBLEM: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�S�s�����҂��̂ĂĂ����������Ȃ���́B�S�{�R";
                    BuffUpMind = 3;
                    cost = 210;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �Q�ӏ�UP
                case Database.POOR_ARIFURETA_STATUE: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���ɂ���Ƃ����������̖��������B�Z�{�P�A�S�{�P";
                    BuffUpAgility = 1;
                    BuffUpMind = 1;
                    cost = 240;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_NON_ADJUST_BELT: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�t���S�n�̈����x���g�B�́{�P�A�Z�{�P";
                    BuffUpStrength = 1;
                    BuffUpAgility = 1;
                    cost = 240;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_SIMPLE_EARRING: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�P�Ɋۂ��`�����Ă�C�������O�B�m�{�P�A�S�{�P";
                    BuffUpIntelligence = 1;
                    BuffUpMind = 1;
                    cost = 240;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_KATAKUZURESHITA_FINGERRING: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���łɌ^������Ă���͂߂ɂ����w�ցB�Z�{�P�A�m�{�P";
                    BuffUpAgility = 1;
                    BuffUpIntelligence = 1;
                    cost = 240;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �R�ӏ�UP
                case Database.POOR_IROASETA_CHOKER: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���̐F��������Ȃ����炢�ɐF�򂹂��`���[�J�[�B�́{�P�A�m�{�P�A�́{�P";
                    BuffUpStrength = 1;
                    BuffUpIntelligence = 1;
                    BuffUpStamina = 1;
                    cost = 310;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_YOREYORE_MANTLE: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���Ɏ���ꂪ����Ă��Ȃ��}���g�B�͂��ȉ�������������B�Z�{�P�A�m�{�P�A�S�{�P";
                    BuffUpAgility = 1;
                    BuffUpIntelligence = 1;
                    BuffUpMind = 1;
                    cost = 310;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_NON_HINSEI_CROWN: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�t���Ă���ƃ_�T�C���A�ق�̂�p���[��������B�́{�P�A�Z�{�P�A�m�{�P";
                    BuffUpStrength = 1;
                    BuffUpAgility = 1;
                    BuffUpIntelligence = 1;
                    cost = 310;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �P��FCommon
                case Database.COMMON_RED_PENDANT: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�ԐF�������y���_���g�B�ق̂��Ɂy�́z��������邱�Ƃ��o����B�́{�T";
                    BuffUpStrength = 5;
                    cost = 520;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_PENDANT: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�F�������y���_���g�B�ق̂��Ɂy�Z�z��������邱�Ƃ��o����B�Z�{�T";
                    BuffUpAgility = 5;
                    cost = 520;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_PENDANT: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���F�������y���_���g�B�ق̂��Ɂy�m�z��������邱�Ƃ��o����B�m�{�T";
                    BuffUpIntelligence = 5;
                    cost = 520;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_PENDANT: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���F�������y���_���g�B�ق̂��Ɂy�́z��������邱�Ƃ��o����B�́{�T";
                    BuffUpStamina = 5;
                    cost = 520;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_PENDANT: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���F�������y���_���g�B�ق̂��Ɂy�S�z��������邱�Ƃ��o����B�S�{�T";
                    BuffUpMind = 5;
                    cost = 520;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SISSO_ARMRING: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���f�ł͂��邪�A�m���ȍ�肪�{����Ă���r�ցB�́{�R�A�́{�P";
                    BuffUpStrength = 3;
                    BuffUpStamina = 1;
                    cost = 400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FINE_FEATHER: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�t���Ă���ƁA�Z�̐؂ꖡ���㏸���銴��������B�Z�{�R�A�S�{�P";
                    BuffUpAgility = 3;
                    BuffUpMind = 1;
                    cost = 400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KIREINA_ORB: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�I�[�u�Ƃ��Ă̊�{������������Ă���B�Z�{�P�A�m�{�R";
                    BuffUpAgility = 1;
                    BuffUpIntelligence = 3;
                    cost = 400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FIT_BANGLE: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�e�͂�����A�t�B�b�g���₷���o���O���B�́{�Q�A�́{�Q";
                    BuffUpStrength = 2;
                    BuffUpStamina = 2;
                    cost = 400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PRISM_EMBLEM: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�����͖������A�قǗǂ��`�������G���u�����B�́{�P�A�Z�{�P�A�m�{�P�A�́{�P�A�S�{�P";
                    BuffUpStrength = 1;
                    BuffUpAgility = 1;
                    BuffUpIntelligence = 1;
                    BuffUpStamina = 1;
                    BuffUpMind = 1;
                    cost = 450;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                
                // Rare
                case Database.RARE_JOUSITU_BLUE_POWERRING:
                    description = "��{�I�Ȏ��̍���������������p���[�����O�B�́{�V�A�Z�{�S�A�S�{�R";
                    BuffUpStrength = 7;
                    BuffUpAgility = 4;
                    BuffUpMind = 3;
                    cost = 880;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_KOUJOUSINYADORU_RED_ORB:
                    description = "��{�I�Ȍ���S�����߂Ă����I�[�u�B�m�{�V�A�́{�Q�A�S�{�S";
                    BuffUpIntelligence = 7;
                    BuffUpStamina = 2;
                    BuffUpMind = 4;
                    cost = 880;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MAGICIANS_MANTLE:
                    description = "��{���@�ɏn�B�����҂���������}���g�B�m�{�P�Q�A���͂T�`�P�O";
                    BuffUpIntelligence = 12;
                    MagicMinValue = 5;
                    MagicMaxValue = 10;
                    cost = 980;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BEATRUSH_BANGLE:
                    description = "�U���������߂�b�����`�[�t�ɂ����퓬�o���O���B�́{�X�A�Z�{�T�A�m�{�Q";
                    BuffUpStrength = 9;
                    BuffUpAgility = 5;
                    BuffUpIntelligence = 2;
                    cost = 960;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ����FPoor
                case Database.POOR_TUKAIFURUSARETA_SWORD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�n���ڂꂪ�����A�����h���Ȃ��Ă��錕�B�U���͂Q�`�S";
                    minValue = 2;
                    maxValue = 4;
                    cost = 150;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_TUKAINIKUI_LONGSWORD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�������������Œ��߂̌����ĂȂ������B�U���͂O�`�P�Q";
                    minValue = 0;
                    maxValue = 12;
                    cost = 200;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ����FCommon
                case Database.COMMON_FINE_SWORD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���Ȃ��g���錕�B�U���͂T�`�W";
                    minValue = 5;
                    maxValue = 8;
                    cost = 560;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_TWEI_SWORD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���茕��p�B�d�ʊ�������A�U����Ɉ�H�v���K�v�B�U���͂R�`�P�W";
                    minValue = 3;
                    maxValue = 18;
                    cost = 610;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ����FRare
                case Database.RARE_AERO_BLADE: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "����̐U��ŁA��Ȃ���؂�����\�ȃu���[�h�B�y����\�́F�L�z�U���͂P�O�`�P�T";
                    useSpecialAbility = true;
                    minValue = 10;
                    maxValue = 15;
                    cost = 1600;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �h��(Poor)
                case Database.POOR_GATAGAKITERU_ARMOR:
                    description = "�{���̐��\���o���Ă��Ȃ��Z�B�h��͂Q�`�R";
                    minValue = 2;
                    maxValue = 3;
                    cost = 300;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_FESTERING_ARMOR:
                    description = "���̈ꕔ���j�����Ă���A�����ꂽ�Z�B�h��͂O�`�S";
                    minValue = 0;
                    maxValue = 4;
                    cost = 300;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �h��(Common)
                case Database.COMMON_FINE_ARMOR: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���Ȃ��g����Z�B�h��͂R�`�U";
                    minValue = 3;
                    maxValue = 6;
                    cost = 590;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GOTHIC_PLATE: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�i�����d�񂶂����p�̊Z�B�h��͂S�`�V";
                    minValue = 4;
                    maxValue = 7;
                    cost = 800;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ��(Poor)
                case Database.POOR_HINSO_SHIELD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�ːi�����ƁA�����ɉ�ꂻ���ȏ��B�h��͂P�`�Q";
                    minValue = 1;
                    maxValue = 2;
                    cost = 150;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_MUDANIOOKII_SHIELD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�傫�����Ă͂��邪�A�����؂炢���B�h��͂O�`�R";
                    minValue = 0;
                    maxValue = 3;
                    cost = 140;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ��(Common)
                case Database.COMMON_FINE_SHIELD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "���Ȃ��g���鏂�B�h��͂R�`�S";
                    minValue = 3;
                    maxValue = 4;
                    cost = 550;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GRIPPING_SHIELD: // �P�K�F�G���A�P�F�����_���h���b�v
                    description = "�U���ɔ������̐��Ŏ����Ȃ��ƁA�g���ɂ������c�鏂�B�h��͂Q�`�U";
                    minValue = 2;
                    maxValue = 6;
                    cost = 550;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;


                // �P�K�F�G���A�R�|�S // �����_���h���b�v
                // �o���o���̂S�F
                case Database.POOR_NO_CONCEPT_RING:
                    description = "����Ƃ����������������A���r���[�ȃp���[��������B�́{�R�A�Z�{�Q�A�́{�S�A�S�{�R";
                    BuffUpStrength = 3;
                    BuffUpAgility = 2;
                    BuffUpStamina = 4;
                    BuffUpMind = 3;
                    cost = 800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_HIGHCOLOR_MANTLE:
                    description = "���܂��܂ȐF�����������A�ǂ̐F���Ⴆ�Ȃ��}���g�B�Z�{�R�A�m�{�S�A�́{�R�A�S�{�Q";
                    BuffUpAgility = 3;
                    BuffUpIntelligence = 4;
                    BuffUpStamina = 3;
                    BuffUpMind = 2;
                    cost = 800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_EIGHT_PENDANT:
                    description = "�W�p�`�̃y���_���g�����A����̗͂��������Ȃ��B�́{�S�A�m�{�Q�A�́{�R�A�S�{�R";
                    BuffUpStrength = 4;
                    BuffUpIntelligence = 2;
                    BuffUpStamina = 3;
                    BuffUpMind = 3;
                    cost = 800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_GOJASU_BELT:
                    description = "���т₩�Ŕ��ɖڗ��x���g�����A��₱�����͂�������B�́{�Q�A�Z�{�R�A�m�{�S�A�́{�R";
                    BuffUpStrength = 2;
                    BuffUpAgility = 3;
                    BuffUpIntelligence = 4;
                    BuffUpStamina = 3;
                    cost = 800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;                case Database.POOR_EGARA_HUMEI_EMBLEM:
                    description = "��㏑���̕��������̃f�U�C�������������Ă��́B�́{�Q�A�Z�{�S�A�m�{�Q�A�S�{�S";
                    BuffUpStrength = 2;
                    BuffUpAgility = 4;
                    BuffUpIntelligence = 2;
                    BuffUpMind = 4;
                    cost = 800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_HAYATOTIRI_ORB:
                    description = "�S�ϐ�����������I�[�u�����A��b�\�͂������B�Αϐ��T�A���ϐ��T�A���ϐ��T�A�őϐ��T";
                    ResistFire = 5;
                    ResistIce = 5;
                    ResistLight = 5;
                    ResistShadow = 5;
                    cost = 800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ��ʖ��̂łQ��ނt�o
                case Database.COMMON_COPPER_RING_TORA:
                    description = "���f�ނō��ꂽ�r�ցB�Ղ̍��󂪂��Ă���B�́{�X�A�Z�{�P�Q";
                    buffUpStrength = 9;
                    buffUpAgility = 12;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_IRUKA:
                    description = "���f�ނō��ꂽ�r�ցB�C���J�̍��󂪂��Ă���B�́{�P�Q�A�m�{�X";
                    buffUpStrength = 12;
                    buffUpIntelligence = 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_UMA:
                    description = "���f�ނō��ꂽ�r�ցB�n�̍��󂪂��Ă���B�́{�P�Q�A�́{�X";
                    buffUpStrength = 12;
                    buffUpStamina = 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_KUMA:
                    description = "���f�ނō��ꂽ�r�ցB�F�̍��󂪂��Ă���B�́{�P�Q�A�S�{�X";
                    buffUpStrength = 12;
                    buffUpMind = 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_HAYABUSA:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�Z�{�P�Q�A�m�{�X";
                    buffUpAgility = 12;
                    buffUpIntelligence = 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_TAKO:
                    description = "���f�ނō��ꂽ�r�ցB�^�R�̍��󂪂��Ă���B�Z�{�P�Q�A�́{�X";
                    buffUpAgility = 12;
                    buffUpStamina= 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_USAGI:
                    description = "���f�ނō��ꂽ�r�ցB�e�̍��󂪂��Ă���B�Z�{�X�A�S�{�P�Q";
                    buffUpAgility = 9;
                    buffUpMind = 12;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_KUMO:
                    description = "���f�ނō��ꂽ�r�ցB�w偂̍��󂪂��Ă���B�m�{�X�A�́{�P�Q";
                    buffUpIntelligence = 9;
                    buffUpStamina = 12;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_SHIKA:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�m�{�P�Q�A�S�{�X";
                    buffUpIntelligence = 12;
                    buffUpMind = 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_RING_ZOU:
                    description = "���f�ނō��ꂽ�r�ցB�ۂ̍��󂪂��Ă���B�́{�P�Q�A�S�{�X";
                    buffUpStamina = 12;
                    buffUpMind = 9;
                    cost = 1650;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RED_AMULET:
                    description = "���ԐF�̃A�~�����b�g�B����Ȃ�Ɂy�́z��������邱�Ƃ��o����B�́{�P�W";
                    buffUpStrength = 18;
                    cost = 2000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_AMULET:
                    description = "���F�̃A�~�����b�g�B����Ȃ�Ɂy�Z�z��������邱�Ƃ��o����B�Z�{�P�W";
                    buffUpAgility = 18;
                    cost = 2000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_AMULET:
                    description = "�����F�̃A�~�����b�g�B����Ȃ�Ɂy�m�z��������邱�Ƃ��o����B�m�{�P�W";
                    buffUpIntelligence = 18;
                    cost = 2000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_AMULET:
                    description = "���ΐF�̃A�~�����b�g�B����Ȃ�Ɂy�́z��������邱�Ƃ��o����B�́{�P�W";
                    buffUpStamina = 18;
                    cost = 2000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_AMULET:
                    description = "�����F�̃A�~�����b�g�B����Ȃ�Ɂy�S�z��������邱�Ƃ��o����B�S�{�P�W";
                    buffUpMind = 18;
                    cost = 2000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // ����FPoor
                case Database.POOR_OLD_USELESS_ROD:
                    description = "�{���̖��͂������Ă����Ԃ̌Âڂ�����B���͂P�`�R";
                    MagicMinValue = 1;
                    MagicMaxValue = 3;
                    cost = 160;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_KISSAKI_MARUI_TUME:
                    description = "�؂肩���������̐؂ꖡ�������܁B�U���͂Q�`�S";
                    minValue = 2;
                    maxValue = 4;
                    cost = 200;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �h��(Poor)
                case Database.POOR_BATTLE_HUMUKI_BUTOUGI:
                    description = "�퓬�ł͂Ȃ��A�x��q�����̕����߁B�h��͂P�`�Q";
                    minValue = 1;
                    maxValue = 2;
                    cost = 600;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_SIZE_AWANAI_ROBE:
                    description = "�K���ȃT�C�Y�ō쐬���ꂽ���[�u�B�h��͂O�`�Q�B�őϐ��P�O";
                    minValue = 0;
                    maxValue = 2;
                    this.ResistShadow = 10;
                    cost = 650;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ����FCommon
                case Database.COMMON_SHORT_SWORD:
                    description = "���Ȃ��g���錕�B�U���͂X�`�P�Q";
                    minValue = 9;
                    maxValue = 12;
                    cost = 1050;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BASTARD_SWORD:
                    description = "���茕��p�B�ӂ蕝�͑傫���A�З͂��o���ɂ͂�����x�̗͂��K�v�B�U���͂V�`�S�O";
                    minValue = 7;
                    maxValue = 40;
                    cost = 1000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_LIGHT_CLAW:
                    description = "���ʂ̌������ō쐬���ꂽ�܁B�U���͂T�`�V";
                    minValue = 5;
                    maxValue = 7;
                    cost = 550;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SHARP_CLAW:
                    description = "�ʏ�̒܂�菭�������d�ʊ����y�������܁B�U���͂T�`�P�R";
                    minValue = 5;
                    maxValue = 13;
                    cost = 1030;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_WOOD_ROD:
                    description = "���؂̈ꕔ��؂����č쐬���ꂽ��B���͂T�`�P�O";
                    MagicMinValue = 12;
                    MagicMaxValue = 15;
                    cost = 1400;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �h��(Common)
                case Database.COMMON_LETHER_CLOTHING:
                    description = "�W���I�ȃT�C�Y�ō쐬���ꂽ���U�[���̈߁B�h��͂S�`�V";
                    minValue = 4;
                    maxValue = 7;
                    cost = 500;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COTTON_ROBE:
                    description = "�ؖȂ�҂ݍ��킹�����[�u�B�h��͂R�`�V�B�Αϐ��T�B���ϐ��T";
                    minValue = 3;
                    maxValue = 7;
                    ResistFire = 5;
                    ResistIce = 5;
                    cost = 950;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_COPPER_ARMOR:
                    description = "���̑f�ނ��ӂ񂾂�Ɏg�����Z�B�h��͂U�`�P�O�B";
                    minValue = 6;
                    maxValue = 10;
                    cost = 1000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_HEAVY_ARMOR:
                    description = "�d�ʊ����ӎ����č��ꂽ�Z�B�h��͂W�`�P�Q�B";
                    minValue = 8;
                    maxValue = 12;
                    cost = 1400;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ��(Common)
                case Database.COMMON_IRON_SHIELD:
                    description = "�S���̏��B����Ȃ�ɃK�b�`�����Ă���B�h��͂T�`�W";
                    minValue = 5;
                    maxValue = 8;
                    cost = 1020;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // Rare (�R�F�t�o�j
                case Database.RARE_SINTYUU_RING_KUROHEBI:
                    description = "�^�J�f�ނō��ꂽ�r�ցB���ւ̍��󂪂��Ă���B�m�{�P�U�A�́{�T�A�S�{�X";
                    buffUpIntelligence = 14;
                    buffUpStamina = 5;
                    buffUpMind = 9;
                    cost = 3400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SINTYUU_RING_HAKUTYOU:
                    description = "�^�J�f�ނō��ꂽ�r�ցB�����̍��󂪂��Ă���B�Z�{�U�A�m�{�W�A�S�{�P�U";
                    buffUpAgility = 6;
                    buffUpIntelligence = 12;
                    buffUpMind = 10;
                    cost = 3400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SINTYUU_RING_AKAHYOU:
                    description = "�^�J�f�ނō��ꂽ�r�ցB�ԕ^�̍��󂪂��Ă���B�́{�P�V�A�Z�{�X�A�S�{�S";
                    buffUpStrength = 15;
                    buffUpAgility = 9;
                    buffUpMind = 4;
                    cost = 3400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ����iRare)
                case Database.RARE_ICE_SWORD:
                    description = "�������Ŏa�鎖���\�Ȍ��B�y����\�́F�L�z�U���͂P�W�`�Q�T";
                    useSpecialAbility = true;
                    minValue = 18;
                    maxValue = 25;
                    cost = 2100;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RISING_KNUCKLE:
                    description = "�͂̉��������y�����G�ŁA�U��̑����������ł���܁B�y����\�́F�L�z�U���͂Q�O�`�Q�T";
                    useSpecialAbility = true;
                    minValue = 20;
                    maxValue = 25;
                    cost = 3400;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_AUTUMN_ROD:
                    description = "�H�ɐ����΂������؂̎}���̗p������B�y����\�́F�L�z���͂P�W�`�Q�Q";
                    description += "\r\n�y����\�́z�@MP���񕜂���B";
                    UseSpecialAbility = true;
                    MagicMinValue = 18;
                    MagicMaxValue = 22;
                    cost = 2800;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �h��iRare)
                case Database.RARE_SUN_BRAVE_ARMOR:
                    description = "���z�̌�����������Ă���Z�B�h��͂P�S�`�P�W�B���@�h��͂P�O�`�P�Q�B�Αϐ��Q�O�A���ϐ��Q�O";
                    minValue = 14;
                    maxValue = 18;
                    MagicMinValue = 10;
                    MagicMaxValue = 12;
                    ResistFire = 20;
                    ResistLight = 20;
                    cost = 3000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ��(Rare)
                case Database.RARE_ESMERALDA_SHIELD:
                    description = "�Ԃ��R�[�e�B���O�Əd�ʊ��̂��鏂�B�h��͂W�`�P�Q�A�Αϐ��Q�O";
                    minValue = 8;
                    maxValue = 12;
                    ResistFire = 20;
                    cost = 2200;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�Q�K�F�����_���h���b�v"
                // �P��UP
                case Database.POOR_HUANTEI_RING:
                    description = "�m���Ȋ��G�͂��邪�A���X�s���ɂ�����r�ցB�́{�P�O�A�m�{�S�A�́{�U";
                    buffUpStrength = 10;
                    buffUpIntelligence = 4;
                    buffUpStamina = 6;
                    cost = 1600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_DEPRESS_FEATHER:
                    description = "�c�͒ʂ��Ă��邪�A�s�^�Ȋ��o�����܂Ƃ��H����B�́{�X�A�Z�{�P�O�A�S�{�P";
                    buffUpStrength = 9;
                    buffUpAgility = 10;
                    buffUpMind = 1;
                    cost = 1600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_DAMAGED_ORB:
                    description = "�P���̂���I�[�u�����A���X�������Ă���B�Z�{�V�A�m�{�P�O�A�́{�R";
                    buffUpAgility = 7;
                    buffUpIntelligence = 10;
                    buffUpStamina = 3;
                    cost = 1600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_SHIMETSUKE_BELT:
                    description = "�������܂�x���g�����A�����L�c�����銴��������B�m�{�S�A�́{�P�O�A�S�{�U";
                    buffUpIntelligence = 4;
                    buffUpStamina = 10;
                    buffUpMind = 6;
                    cost = 1600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_NOGENKEI_EMBLEM:
                    description = "�C���[�W��z�N�������͂����A���^�𗯂߂ĂȂ��B�́{�P�O�A�́{�W�A�S�{�Q";
                    buffUpStrength = 10;
                    buffUpStamina = 8;
                    buffUpMind = 2;
                    cost = 1600;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_MAGICLIGHT_FIRE:
                    description = "�΂̎c�e���h���Ă���}�W�b�N���C�g�B�Αϐ��P�O�O";
                    ResistFire = 100;
                    cost = 4000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_MAGICLIGHT_ICE:
                    description = "���̎c�e���h���Ă���}�W�b�N���C�g�B���ϐ��P�O�O";
                    ResistIce = 100;
                    cost = 4000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_MAGICLIGHT_SHADOW:
                    description = "�ł̎c�e���h���Ă���}�W�b�N���C�g�B�őϐ��P�O�O";
                    ResistShadow = 100;
                    cost = 4000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_MAGICLIGHT_LIGHT:
                    description = "���̎c�e���h���Ă���}�W�b�N���C�g�B���ϐ��P�O�O";
                    ResistLight = 100;
                    cost = 4000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RED_CHARM:
                    description = "�Ԃ̕��l�����ݍ��܂�Ă���y�́z�������`���[���B�́{�R�O";
                    BuffUpStrength = 30;
                    cost = 7800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_CHARM:
                    description = "�̕��l�����ݍ��܂�Ă���y�Z�z�������`���[���B�Z�{�R�O";
                    BuffUpAgility = 30;
                    cost = 7800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_CHARM:
                    description = "���̕��l�����ݍ��܂�Ă���y�m�z�������`���[���B�m�{�R�O";
                    BuffUpIntelligence = 30;
                    cost = 7800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_CHARM:
                    description = "�΂̕��l�����ݍ��܂�Ă���y�́z�������`���[���B�́{�R�O";
                    BuffUpStamina = 30;
                    cost = 7800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_CHARM:
                    description = "���̕��l�����ݍ��܂�Ă���y�S�z�������`���[���B�S�{�R�O";
                    BuffUpMind = 30;
                    cost = 7800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_THREE_COLOR_COMPASS:
                    description = "�ǎ����������̂ɗp�ӂ��ꂽ�O�F�̃R���p�X�B�m�{�P�T�A�́{�P�O�A�S�{�P�O";
                    BuffUpIntelligence = 15;
                    BuffUpStamina = 10;
                    buffUpMind = 10;
                    cost = 6700;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SANGO_CROWN:
                    description = "�X��̌��Ђ����킳��A���܂��܊��̌`�ƂȂ������́B�́{�P�T�A�Z�{�P�O�A�m�{�P�O";
                    BuffUpStrength = 15;
                    BuffUpAgility = 10;
                    BuffUpIntelligence = 10;
                    cost = 8500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMOOTHER_BOOTS:
                    description = "�C�����芊�炩�ɂ��A�y�₩�ȓ����������B�Z�{�P�T�A�́{�P�O�A�S�{�P�O";
                    BuffUpAgility = 15;
                    BuffUpStamina = 10;
                    BuffUpMind = 10;
                    cost = 7200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SHIOKAZE_MANTLE:
                    description = "�������ӂ񂾂�ɟ��ݍ��܂����}���g�B�́{�P�O�A�m�{�Q�O�A�S�{�T";
                    BuffUpStrength = 10;
                    BuffUpIntelligence = 20;
                    BuffUpMind = 5;
                    cost = 8800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �A�N�Z�T���iPoor2)
                case Database.POOR_CURSE_EARRING:
                    description = "�Y��ȃC�������O�����A�ǂ����G�͂��Ȃ��B�Z�{�X�A�m�{�X�A�S�{�Q";
                    buffUpAgility = 9;
                    buffUpIntelligence = 9;
                    buffUpMind = 2;
                    cost = 7000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_CURSE_BOOTS:
                    description = "�������ǂ��A�����͉̂\�����A�����͏d���B�Z�{�V�A�́{�P�O�A�S�{�R";
                    buffUpAgility = 7;
                    buffUpStamina = 10;
                    buffUpMind = 3;
                    cost = 7000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_BLOODY_STATUE:
                    description = "�͂̑��������������钤�������A�C���͐���Ȃ��B�́{�P�O�A�Z�{�V�A�́{�R";
                    buffUpStrength = 10;
                    buffUpAgility = 7;
                    buffUpMind = 3;
                    cost = 7000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_FALLEN_MANTLE:
                    description = "�����p�҂��p���Ă����}���g�B�́{�V�A�m�{�P�Q�A�S�{�P";
                    buffUpStrength = 7;
                    buffUpIntelligence = 12;
                    buffUpMind = 1;
                    cost = 7000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_SIHAIRYU_SIKOTU:
                    description = "�E�F�N�X���[�e�n�ɐ��ށu�x�z���v�̎w�̍��B���U���{�R��";
                    amplifyPhysicalAttack = 1.03f;
                    cost = 8000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_OLD_TREE_KAREHA:
                    description = "�E�F�N�X���[��n�ɖ���u�Ñ�h���v�̌͂�t�B���U���{�R��";
                    amplifyMagicAttack = 1.03f;
                    cost = 8000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_GALEWIND_KONSEKI:
                    description = "�E�F�N�X���[�R���̐_�u�Q�C���E�E�B���h�v�̍��ՁB�푬���{�R��";
                    amplifyBattleSpeed = 1.03f;
                    cost = 8000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_SIN_CRYSTAL_KAKERA:
                    description = "�E�F�N�X���[�Ñ�Z�p�u�V���E�N���X�^���v�̌��ЁB�퉞���{�R��";
                    amplifyBattleResponse = 1.03f;
                    ResistWill = 1200;
                    cost = 8000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_EVERMIND_ZANSHI:
                    description = "�E�F�N�X���[�V��̎�u�G�o�[�E�}�C���h�v�̎c���v�O�B���͗��{�R��";
                    amplifyPotential = 1.03f;
                    cost = 8000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �A�N�Z�T���iCommon2�j
                case Database.COMMON_BRONZE_RING_KIBA:
                    description = "���f�ނō��ꂽ�r�ցB��̍��󂪂��Ă���B�́{�Q�S�A�Z�{�P�U";
                    buffUpStrength = 24;
                    buffUpAgility = 16;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_SASU:
                    description = "���f�ނō��ꂽ�r�ցB�h�̍��󂪂��Ă���B�́{�P�U�A�m�{�Q�S";
                    buffUpStrength = 16;
                    buffUpIntelligence = 24;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_KU:
                    description = "���f�ނō��ꂽ�r�ցB��̍��󂪂��Ă���B�́{�Q�S�A�́{�P�U";
                    buffUpStrength = 24;
                    buffUpStamina = 16;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_NAGURI:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�́{�P�U�A�S�{�Q�S";
                    buffUpStrength = 16;
                    buffUpMind = 24;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_TOBI:
                    description = "���f�ނō��ꂽ�r�ցB��̍��󂪂��Ă���B�Z�{�Q�S�A�m�{�P�U";
                    buffUpAgility = 24;
                    buffUpIntelligence = 16;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_KARAMU:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�Z�{�P�U�A�́{�Q�S";
                    buffUpAgility = 16;
                    buffUpStamina = 24;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_HANERU:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�Z�{�Q�S�A�S�{�P�U";
                    buffUpAgility = 24;
                    buffUpMind = 16;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_TORU:
                    description = "���f�ނō��ꂽ�r�ցB��̍��󂪂��Ă���B�m�{�Q�S�A�́{�P�U";
                    buffUpIntelligence = 24;
                    buffUpStamina = 16;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_MIRU:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�m�{�P�U�A�S�{�Q�S";
                    buffUpIntelligence = 16;
                    buffUpMind = 24;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRONZE_RING_KATAI:
                    description = "���f�ނō��ꂽ�r�ցB���̍��󂪂��Ă���B�́{�Q�S�A�S�{�P�U";
                    buffUpStamina = 24;
                    buffUpMind = 16;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RED_KOKUIN:
                    description = "�Ԃ��h�点�Ă��鍏��A����́y�́z�������B�́{�T�O";
                    BuffUpStrength = 50;
                    cost = 15000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_KOKUIN:
                    description = "���h�点�Ă��鍏��A����́y�Z�z�������B�Z�{�T�O";
                    BuffUpAgility = 50;
                    cost = 15000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_KOKUIN:
                    description = "�����h�点�Ă��鍏��A����́y�m�z�������B�m�{�T�O";
                    BuffUpIntelligence = 50;
                    cost = 15000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_KOKUIN:
                    description = "�΂��h�点�Ă��鍏��A����́y�́z�������B�́{�T�O";
                    BuffUpStamina = 50;
                    cost = 15000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_KOKUIN:
                    description = "�����h�点�Ă��鍏��A����́y�S�z�������B�S�{�T�O";
                    BuffUpMind = 50;
                    cost = 15000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SISSEI_MANTLE:
                    description = "����������p���Ă��錵�i�ȃ}���g�B�́{�P�T�A�m�{�R�O�A�S�{�P�T";
                    buffUpStrength = 15;
                    buffUpAgility = 30;
                    buffUpMind = 15;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KAISEI_EMBLEM:
                    description = "�`�����ނƁA�󂪌��n����`��̖�́B�́{�R�O�A�́{�P�T�A�S�{�P�T";
                    buffUpStrength = 30;
                    buffUpStamina = 15;
                    buffUpMind = 15;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SAZANAMI_EARRING:
                    description = "���ɑ�������Ə��g�̉����������Ă���B�Z�{�P�T�A�m�{�P�T�A�S�{�R�O";
                    buffUpAgility = 15;
                    buffUpIntelligence = 15;
                    buffUpMind = 30;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_AMEODORI_STATUE:
                    description = "���̔򖗂����Y���悭���������Ă���钤���B�́{�P�T�A�Z�{�R�O�A�́{�P�T";
                    buffUpStrength = 15;
                    buffUpAgility = 30;
                    buffUpStamina = 15;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �A�N�Z�T���iRare2�j
                case Database.RARE_RING_BRONZE_RING_KONSHIN:
                    description = "�Ӑ��f�ނō��ꂽ�r�ցB�Ӑg�̍��󂪂���Ă���B�́{�S�W�A�Z�{�P�R�A�́{�P�Q�A�S�{�P�V";
                    buffUpStrength = 48;
                    buffUpAgility = 13;
                    buffUpStamina = 12;
                    buffUpMind = 17;
                    cost = 35000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RING_BRONZE_RING_SYUNSOKU:
                    description = "�Ӑ��f�ނō��ꂽ�r�ցB�r���̍��󂪂���Ă���B�Z�{�S�V�A�m�{�P�P�A�́{�P�R�A�S�{�P�X";
                    buffUpAgility = 47;
                    buffUpIntelligence = 11;
                    buffUpStamina = 13;
                    buffUpMind = 19;
                    cost = 35000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RING_BRONZE_RING_JUKURYO:
                    description = "�Ӑ��f�ނō��ꂽ�r�ցB�n���̍��󂪂���Ă���B�́{�P�Q�A�Z�{�P�V�A�m�{�S�T�A�́{�P�U";
                    buffUpStrength = 12;
                    buffUpAgility = 17;
                    buffUpIntelligence = 45;
                    buffUpStamina = 16;
                    cost = 35000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RING_BRONZE_RING_SOUGEN:
                    description = "�Ӑ��f�ނō��ꂽ�r�ցB�u���̍��󂪂���Ă���B�́{�P�R�A�m�{�P�T�A�́{�S�U�A�S�{�P�U";
                    buffUpStrength = 13;
                    buffUpIntelligence = 15;
                    buffUpStamina = 46;
                    buffUpMind = 116;
                    cost = 35000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RING_BRONZE_RING_YUUDAI:
                    description = "�Ӑ��f�ނō��ꂽ�r�ցB�Y��̍��󂪂���Ă���B�́{�P�P�A�Z�{�P�W�A�m�{�P�Q�A�S�{�S�X";
                    buffUpStrength = 11;
                    buffUpAgility = 18;
                    buffUpIntelligence = 12;
                    buffUpMind = 49;
                    cost = 35000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MEIUN_BOX:
                    description = "�Ȃ̖��^���|���ĊJ����锠�B�y����\�́F�L�z�́{�Q�O�A�Z�{�Q�O�A�m�{�Q�O�A�́{�Q�O�A�S�{�Q�O";
                    buffUpStrength = 20;
                    buffUpAgility = 20;
                    buffUpIntelligence = 20;
                    buffUpStamina = 20;
                    buffUpMind = 20;
                    cost = 41000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_WILL_HOLY_HAT:
                    description = "�����ӎu������I�[���𗁂т��}�W�V�����Y�n�b�g�B�m�{�S�T�A�S�{�S�T�A���ϐ��{�R�O�O";
                    buffUpIntelligence = 45;
                    buffUpMind = 45;
                    ResistLight = 300;
                    cost = 43000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_EMBLEM_BLUESTAR:
                    description = "�C�̉��q���Y�ݏo�����ƌ����Ă���C���̖�́B�́{�S�T�A�m�{�S�T�A���ϐ��{�R�O�O";
                    buffUpStrength = 45;
                    buffUpIntelligence = 45;
                    ResistIce = 300;
                    cost = 43000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SEAL_OF_DEATH:
                    description = "���ւ̒�R�͂���������҂��v�����A�Y�ݏo����������i�B�́{�S�T�A�S�{�S�T�A�őϐ��{�R�O�O";
                    buffUpStamina = 45;
                    buffUpMind = 45;
                    ResistShadow = 300;
                    cost = 43000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �A�N�Z�T���i�K���c���Łj�Q�K
                case Database.RARE_WILD_HEART_SPADE:
                    description = "���̏ے��Ƃ��ăX�y�[�h�̍�������񂾃u���X���b�g�B�́{�U�T�A�S�{�R�T";
                    buffUpStrength = 65;
                    buffUpMind = 35;
                    cost = 30000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �A�N�Z�T���i�K���c�����j�Q�K
                case Database.COMMON_WHITE_WAVE_RING: // ���̌��ʁA�̌���
                    description = "���ʂ���G�b�Z���X�������o���A�����O�`��Ƃ��č��������B�́{�P�O�A�m�{�P�O�A�́{�T�O";
                    buffUpStrength = 10;
                    buffUpIntelligence = 10;
                    buffUpStamina = 50;
                    cost = 20000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_NEEDLE_FEATHER: // ���̉s���g�Q�A�h�̐H
                    description = "�s���g�Q�ƍK�^���ĂԐH�����܂��Z���������t���H�B�́{�P�O�A�Z�{�T�O�A�S�{�P�O";
                    buffUpStrength = 10;
                    buffUpAgility = 50;
                    buffUpMind = 10;
                    cost = 23000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_KOUSHITU_ORB: // �S�c�S�c�����k
                    description = "����̊k��n�����A��̋���Ɏd���Ă�������i�B�Z�{�P�O�A�m�{�T�O�A�́{�P�O";
                    BuffUpAgility = 10;
                    BuffUpIntelligence = 50;
                    BuffUpStamina = 10;
                    cost = 21000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                // �󔠂Q�K
                case Database.COMMON_PUZZLE_BOX:
                    description = "�p�Y���̎킪��R���[����Ă��锠�B�m�{�S�O�A�S�{�P�O";
                    buffUpIntelligence = 40;
                    buffUpMind = 10;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_CHIENOWA_RING:
                    description = "�����m�͂�������郊���O�B�l���Ă���Ɨ͂������B�y����\�́F�L�z�m�{�R�O";
                    useSpecialAbility = true;
                    buffUpIntelligence = 30;
                    cost = 19500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.RARE_MASTER_PIECE:
                    description = "�m�͂̂�����̂��Y�ݏo��������i�B�́{�P�O�A�Z�{�P�O�A�m�{�U�O�A�́{�P�O�A�S�{�P�O";
                    buffUpStrength = 10;
                    buffUpAgility = 10;
                    buffUpIntelligence = 60;
                    buffUpStamina = 10;
                    buffUpMind = 10;
                    cost = 33000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    break;
                case Database.COMMON_TUMUJIKAZE_BOX:
                    description = "�ނ��������[����Ă��锠�B�Z�{�S�O�A�S�{�P�O";
                    buffUpAgility = 40;
                    buffUpMind = 10;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_ROCKET_DASH:
                    description = "���P�b�g�^�̃u�[�c�B�g���ƕ��������x�ŋt���˂���E�E�E�B�y����\�́F�L�z�Z�{�R�O";
                    useSpecialAbility = true;
                    buffUpAgility = 30;
                    cost = 14000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_CLAW_OF_SPRING:
                    description = "�t�̕���Y�킹��܁B�U��Ƃق�̂����������炵���B�U���͂R�Q�`�S�Q�A�Z�{�Q�O";
                    minValue = 32;
                    maxValue = 42;
                    buffUpAgility = 20;
                    cost = 15000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_BREEZE_CROSS:
                    description = "���敗���ق̂��Ɋ������A�g���y�₩�ɓ������镑���߁B�h��͂Q�O�`�Q�Q�A�Z�{�Q�O";
                    minValue = 20;
                    maxValue = 22;
                    buffUpAgility = 20;
                    cost = 17500;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_GUST_SWORD:
                    description = "�˕��̔@���˂��o���Ă��܂����B�U���͂R�Q�`�S�Q�A�Z�{�Q�O";
                    minValue = 32;
                    maxValue = 42;
                    buffUpAgility = 20;
                    cost = 17500;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_BLANK_BOX:
                    description = "����������������Ȏ��ɁA�S�̎x������������B�́{�P�O�A�S�{�S�O";
                    buffUpStamina = 10;
                    buffUpMind = 40;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.RARE_SPIRIT_OF_HEART:
                    description = "���̐��t�̒��ɂ͐S�����߂��Ă����搂��Ă���B�́{�P�O�A�Z�{�P�O�A�m�{�P�O�A�́{�P�O�A�S�{�U�O";
                    buffUpStrength = 10;
                    buffUpAgility = 10;
                    buffUpIntelligence = 10;
                    buffUpStamina = 10;
                    buffUpMind = 60;
                    cost = 33000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    break;
                case Database.COMMON_FUSION_BOX:
                    description = "�͂̌����Z������Ă��锠�B�́{�S�O�A�S�{�P�O";
                    buffUpStrength = 40;
                    buffUpMind = 10;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_WAR_DRUM:
                    description = "�p���[�A�b�v�p�̉��y�𑦋��ŏo����h�����B�y����\�́F�L�z�́{�R�O";
                    useSpecialAbility = true;
                    buffUpStrength = 30;
                    cost = 16000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_KOBUSHI_OBJE:
                    description = "���̌`�������I�u�W�F�B���ƂȂ��p���[��������B�́{�S�T�A�́{�T�A�S�{�T";
                    buffUpStrength = 45;
                    buffUpStamina = 5;
                    buffUpMind = 5;
                    cost = 17200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_TIGER_BLADE:
                    description = "����U��������A�Ղ̖i���鐺�Ɏ�����������B�U���͂V�Q�`�V�W�A�́{�Q�O";
                    minValue = 72;
                    maxValue = 78;
                    buffUpStrength = 20;
                    cost = 24000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.RARE_ROD_OF_STRENGTH:
                    description = "�y�́z���̂��̂��h�点�Ă��閂�@�̏�B�y����\�́F�L�z���͂X�Q�`�P�O�P�A�́{�R�O";
                    MagicMinValue = 92;
                    MagicMaxValue = 101;
                    buffUpStrength = 30;
                    cost = 41000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    break;
                case Database.COMMON_SOUKAI_DRINK_SS:
                    description = "�X�b�L���u���I�iSpeedy & Splash���L�ڂ���Ă���j";
                    cost = 10000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_TUUKAI_DRINK_DD:
                    description = "�X�b�L���u���I�iDont DoIt! �ƋL�ڂ���Ă���j";
                    cost = 10000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.RARE_SOUJUTENSHI_NO_GOFU:
                    description = "�����V�g����̋��͂ȉ���𓾂�B�̗́{�Q�O�@�y���فz�y�X�^���z�ϐ���t�^�B";
                    buffUpStamina = 20;
                    ResistSilence = true;
                    ResistStun = true;
                    cost = 44000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    break;


                // �I���E�����f�B�X��������
                case Database.COMMON_FATE_RING:
                    description = "�����҂̃I�[�������̃����O�֗l�X���ꂱ��ł���悤�Ɋ�������B�́{�R�O�A�m�{�R�O";
                    buffUpStrength = 30;
                    buffUpIntelligence = 30;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    equipablePerson = Equipable.Ol;
                    break;
                case Database.EPIC_FATE_RING_OMEGA:
                    description = "�����f�B�X�̐��_�g�����I�[���Ƃ��ď�Ƀ����O�ɗ��ꑱ���Ă���B�́{�V�T�O�A�́{�U�T�O�A�U�f�ϐ��A�݉��ϐ��A�Èőϐ��A�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�����U�����q�b�g���邽�тɁA�y���z�̒~�σJ�E���^�[���P������BUFF�Ƃ��Ē~�ς���B�~�ς��ꂽ�J�E���^�[�̕������A�����U�����Q�����㏸����B�ő�10�܂Œ~�ς��s����B";
                    buffUpStrength = 750;
                    buffUpStamina = 650;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    equipablePerson = Equipable.Ol;
                    break;
                case Database.COMMON_LOYAL_RING:
                    description = "���̃����O�ɂ͑����҂̐������h��ƌ����Ă���B�S�{�V�O";
                    buffUpMind = 70;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    equipablePerson = Equipable.Ol;
                    break;
                case Database.EPIC_LOYAL_RING_OMEGA:
                    description = "�����f�B�X�̐������͂�U�邤���_�������O�ɗ���Â��Ă���B�m�{�W�O�O�A�S�{�S�O�O�A�X�^���ϐ��A��ბϐ��A�����ϐ��A�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�����U�����q�b�g����x�ɁA�X�L���|�C���g���񕜂���B";
                    buffUpIntelligence = 800;
                    buffUpMind = 400;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    equipablePerson = Equipable.Ol;
                    break;

                // ����(Common)
                case Database.COMMON_SMART_SWORD:
                    description = "�T�b�Ɨǂ��a�ꖡ�̂��錕�B�U���͂S�O�`�T�O";
                    minValue = 40;
                    maxValue = 50;
                    cost = 6000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_CLAW:
                    description = "�T�N�T�N���ƐS�n�ǂ������~����܁B�U���͂R�O�`�S�O";
                    minValue = 30;
                    maxValue = 40;
                    cost = 5000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_ROD:
                    description = "�d�ʂ��y���A�q���C�q���C�ƐU�邱�Ƃ��o�����B���͂R�T�`�S�T";
                    MagicMinValue = 35;
                    MagicMaxValue = 45;
                    cost = 4500;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RAUGE_SWORD:
                    description = "���Ȃ莿��������d�������A�З͂͊��҂ł��闼�茕�B�U���͂Q�O�`�W�O";
                    minValue = 20;
                    maxValue = 80;
                    cost = 7000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // ����iRare�j
                case Database.RARE_WRATH_SERVEL_CLAW:
                    description = "���{�̃I�[�����h�����؂���̃X���h�C�܁B�y����\�́F�L�z�U���͂T�T�`�V�O";
                    useSpecialAbility = true;
                    minValue = 55;
                    maxValue = 70;
                    cost = 13000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BLUE_LIGHTNING:
                    description = "���M�����h���Ă��錕�B�y����\�́F�L�z�U���͂U�R�`�W�T";
                    useSpecialAbility = true;
                    minValue = 63;
                    maxValue = 85;
                    cost = 15500;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BURNING_CLAYMORE:
                    description = "�N�₩�ȐԂ��΂̕��������^�J���N���C���A�B�y����\�́F�L�z�U���͂S�O�`�P�Q�O";
                    useSpecialAbility = true;
                    minValue = 40;
                    maxValue = 120;
                    cost = 14000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // ����iCommon2�j
                case Database.COMMON_SMASH_BLADE:
                    description = "�f�U�背�x���ł��Ō��������Ă錕�B�U���͂T�T�`�V�O";
                    minValue = 55;
                    maxValue = 70;
                    cost = 7500;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_POWERED_BUSTER:
                    description = "����t�̗͂����߂ĕ��ĂΈЗ͂̓f�J�C�I�U���͂T�O�`�P�S�T";
                    minValue = 50;
                    maxValue = 145;
                    cost = 15000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_STONE_CLAW:
                    description = "�ΐ��ł���A�g�y�ɐG���܁B�U���͂S�Q�`�T�S";
                    minValue = 42;
                    maxValue = 54;
                    cost = 6700;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ZALGE_CLAW:
                    description = "�X�g�[���E�N���[�̐؂���ɓł��h���Ă���܁B�U���͂S�Q�`�T�S�y�ǉ����ʁF�ғŁz";
                    minValue = 42;
                    maxValue = 54;
                    cost = 20000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_DENDOU_ROD:
                    description = "���Ə��������d�C�������B���͂S�U�`�U�Q";
                    MagicMinValue = 46;
                    MagicMaxValue = 62;
                    cost = 6000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // ����iRare2�j
                case Database.RARE_DARKNESS_SWORD:
                    description = "�ł���荞�ݖ��͂�L���Ă��錕�B�U���͂U�O�`�W�O�A���͂U�O�`�W�O";
                    minValue = 60;
                    maxValue = 80;
                    MagicMinValue = 60;
                    MagicMaxValue = 80;
                    cost = 15000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BLUE_RED_ROD:
                    description = "��������ԂƑ��̖��͂��h������B�y����\�́F�L�z���͂V�O�`�W�T";
                    useSpecialAbility = true;
                    MagicMinValue = 70;
                    MagicMaxValue = 85;
                    cost = 11500;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // ����i�K���c�Q�K�j
                case Database.COMMON_SMART_SWORD_2:
                    description = "�T�b�Ɨǂ��a�ꖡ�̂��錕���K���c�����������B�U���͂S�O(+8)�`�T�O(+8)";
                    minValue = 48;
                    maxValue = 58;
                    cost = 6500;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_CLAW_2:
                    description = "�T�N�T�N���ƐS�n�ǂ������~����܂��K���c�����������B�U���͂R�O(+7)�`�S�O(+7)";
                    minValue = 37;
                    maxValue = 47;
                    cost = 5800;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_ROD_2:
                    description = "�d�ʂ��y���A�q���C�q���C�ƐU�邱�Ƃ��o�������K���c�����������B���͂R�T(+6)�`�S�T(+6)";
                    MagicMinValue = 41;
                    MagicMaxValue = 51;
                    cost = 5200;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_PLATE_2:
                    description = "�K�b�`�������Z������ɃK���c�����������Z�B�h��͂R�O(+5)�`�R�T(+5)";
                    minValue = 35;
                    maxValue = 40;
                    cost = 6000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;                    
                case Database.COMMON_SMART_CLOTHING_2:
                    description = "���S�n���ǂ��A�����₷�������Q�̕����߁B�h��͂Q�T(+4)�`�Q�W(+4)";
                    minValue = 29;
                    maxValue = 32;
                    cost = 5300;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_ROBE_2:
                    description = "�X�����Ƃ����f�U�C����ǋ������퓬�������[�u�B�h��͂P�O(+4)�`�P�Q(+4)�B���@�h��Q�O(+5)�`�Q�Q(+5)";
                    minValue = 14;
                    maxValue = 16;
                    MagicMinValue = 25;
                    MagicMaxValue = 27;
                    cost = 6400;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RAUGE_SWORD_2:
                    description = "���Ȃ莿��������d�������A�З͂͊��҂ł��闼�茕�B�U���͂Q�O(+10)�`�W�O(+15)";
                    minValue = 30;
                    maxValue = 95;
                    cost = 8000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_SHIELD_2:
                    description = "�����₷���A�������X�b�ƕς����鏂���K���c�����������B�h��͂P�Q(+3)�`�P�T(+3)";
                    minValue = 15;
                    maxValue = 18;
                    cost = 4100;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    break;
                
                case Database.COMMON_STEEL_SWORD:
                    description = "�K���c���O�O�ɖ����グ���X�`�[�����̌��B�U���͂W�O(+8)�`�X�O(+9)";
                    minValue = 88;
                    maxValue = 99;
                    cost = 16000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FACILITY_CLAW:
                    description = "�K���c���`�̉��ǂ��d�˂Ċ������ꂽ�܁B�U���͂U�T(+2)�`�V�O(+5)";
                    minValue = 67;
                    maxValue = 76;
                    cost = 13500;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_MIX_HINOKI_ROD:
                    description = "�w�f�ނɋ������f�ނ�������������B���͂W�Q�`�X�U";
                    MagicMinValue = 82;
                    MagicMaxValue = 96;
                    cost = 13000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RED_ARM_BLADE:
                    description = "���r�ȃW���[�̘r�����H���A�Ԋ��F�ŃR�[�e�B���O���{�������B�U���͂P�O�P�`�P�P�R";
                    minValue = 101;
                    maxValue = 113;
                    cost = 27000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_STRONG_SERPENT_CLAW:
                    description = "���łȐL�̌������X�ɍ����������A���M�Ŗ������܁B�U���͂V�T�`�X�P";
                    minValue = 75;
                    maxValue = 91;
                    cost = 24000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_GOD_FIRE_GLOVE_REPLICA:
                    description = "�����f�B�X���I���I���A�ł����邽�߂Ɏ��O�ō쐬�������v���J�B�U���͂P�P�O�`�P�P�W";
                    minValue = 110;
                    maxValue = 118;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Poor;
                    equipablePerson = Equipable.Ol;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�R�K�F�����_���h���b�v"
                case Database.POOR_DIRTY_ANGEL_CONTRACT:
                    description = "���p���l����������鎖�Ȃ��A�ł��̂Ă�ꂽ�_�񏑁B�S�ϐ��t�^�B�퓬���Ɉ�x�ł����ʔ��������ꍇ�A�퓬�I����ɉ���B";
                    cost = 100000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_JUNK_TARISMAN_FROZEN:
                    description = "���ɂ���ꂻ���ȁy�����z�ی�^���X�}���B�y�����z�ϐ��t�^�B�퓬���Ɉ�x�ł����ʔ��������ꍇ�A�퓬�I����ɉ���B";
                    cost = 80000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_JUNK_TARISMAN_PARALYZE:
                    description = "���ɂ���ꂻ���ȁy��Ⴡz�ی�^���X�}���B�y��Ⴡz�ϐ��t�^�B�퓬���Ɉ�x�ł����ʔ��������ꍇ�A�퓬�I����ɉ���B";
                    cost = 80000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_JUNK_TARISMAN_STUN:
                    description = "���ɂ���ꂻ���ȁy�X�^���z�ی�^���X�}���B�y�X�^���z�ϐ��t�^�B�퓬���Ɉ�x�ł����ʔ��������ꍇ�A�퓬�I����ɉ���B";
                    cost = 80000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RED_STONE:
                    description = "���͂ȐԂ̌ۓ�������������΁B�́{�V�T";
                    buffUpStrength = 75;
                    cost = 140000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BLUE_STONE:
                    description = "���͂Ȑ̌ۓ�������������΁B�Z�{�V�T";
                    buffUpAgility = 75;
                    cost = 140000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_PURPLE_STONE:
                    description = "���͂Ȏ��̌ۓ�������������΁B�m�{�V�T";
                    buffUpIntelligence = 75;
                    cost = 140000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_GREEN_STONE:
                    description = "���͂ȗ΂̌ۓ�������������΁B�́{�V�T";
                    buffUpStamina = 75;
                    cost = 140000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_YELLOW_STONE:
                    description = "���͂ȉ��̌ۓ�������������΁B�S�{�V�T";
                    buffUpMind = 75;
                    cost = 140000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_MIGAWARI_DOOL:
                    description = "�����҂̐g������ʂ����������̐l�`�B�����Ҏ��S���A���C�t�P�Ő����c��B��x���ʔ�������Ɖ���B";
                    cost = 180000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_ONE_DROPLET_KESSYOU:
                    description = "��H�̎��������҂̏����ƂȂ�B���݃}�i�ʂ��P�O����؂����ꍇ�A�}�i���T�O���܂ŉ񕜂���B��x���ʔ�������Ɖ���B";
                    cost = 180000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_MOMENTARY_FLASH_LIGHT:
                    description = "��u�̌��������҂̏����ƂȂ�B���݃��C�t�ʂ��P�O����؂����ꍇ�A���C�t���T�O���܂ŉ񕜂���B��x���ʔ�������Ɖ���B";
                    cost = 180000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.POOR_SUN_YUME_KAKERA:
                    description = "�ꐡ�̖��������҂̏����ƂȂ�B���݃X�L���ʂ��P�O����؂����ꍇ�A�X�L�����T�O���܂ŉ񕜂���B��x���ʔ�������Ɖ���B";
                    cost = 180000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_1:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�p���[�x�̈󎚂����܂�Ă���B�́{�S�T�A�Z�{�R�O";
                    buffUpStrength = 45;
                    buffUpAgility = 30;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_2:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�Z���X�x�̈󎚂����܂�Ă���B�́{�R�O�A�m�{�S�T";
                    buffUpStrength = 30;
                    buffUpIntelligence = 45;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_3:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�^�t�x�̈󎚂����܂�Ă���B�́{�S�T�A�́{�R�O";
                    buffUpStrength = 45;
                    buffUpStamina = 30;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_4:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w���b�N�x�̈󎚂����܂�Ă���B�́{�R�O�A�S�{�S�T";
                    buffUpStrength = 30;
                    buffUpMind = 45;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_5:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�t�@�X�g�x�̈󎚂����܂�Ă���B�Z�{�S�T�A�m�{�R�O";
                    buffUpAgility = 45;
                    buffUpIntelligence = 30;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_6:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�V���[�v�x�̈󎚂����܂�Ă���B�Z�{�R�O�A�́{�S�T";
                    buffUpAgility = 30;
                    buffUpStamina = 45;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_7:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�n�C�x�̈󎚂����܂�Ă���B�Z�{�S�T�A�S�{�R�O";
                    buffUpAgility = 45;
                    buffUpMind = 30;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_8:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�f�B�[�v�x�̈󎚂����܂�Ă���B�m�{�S�T�A�́{�R�O";
                    buffUpIntelligence = 45;
                    buffUpStamina = 30;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_9:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�o�E���h�x�̈󎚂����܂�Ă���B�m�{�R�O�A�S�{�S�T";
                    buffUpIntelligence = 30;
                    buffUpMind = 45;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_RING_10:
                    description = "�|�̑f�ނō��ꂽ�r�ցB�w�G���[�g�x�̈󎚂����܂�Ă���B�́{�S�T�A�S�{�R�O";
                    buffUpStamina = 45;
                    buffUpMind = 30;
                    cost = 200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RED_MASEKI:
                    description = "�z�ƌ���Ԃ����΂���͎��R�ɗN���o��悤�ȁy�́z��������B�́{�P�O�T";
                    buffUpStrength = 105;
                    cost = 220000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BLUE_MASEKI:
                    description = "�z�ƌ��鎇�̖��΂���͎��R�ɗN���o��悤�ȁy�Z�z��������B�Z�{�P�O�T";
                    buffUpAgility = 105;
                    cost = 220000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_PURPLE_MASEKI:
                    description = "�z�ƌ��鎇�̖��΂���͎��R�ɗN���o��悤�ȁy�m�z��������B�m�{�P�O�T";
                    buffUpIntelligence = 105;
                    cost = 220000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_GREEN_MASEKI:
                    description = "�z�ƌ���΂̖��΂���͎��R�ɗN���o��悤�ȁy�́z��������B�́{�P�O�T";
                    buffUpStamina = 105;
                    cost = 220000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_YELLOW_MASEKI:
                    description = "�z�ƌ��鉩�̖��΂���͎��R�ɗN���o��悤�ȁy�S�z��������B�S�{�P�O�T";
                    buffUpMind = 105;
                    cost = 220000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WING_STEP_FEATHER:
                    description = "�y�₩�Ƀ��_�Ȃ������H�����u�[�c�B�Z�{�U�O�A�X�^���ϐ�";
                    buffUpAgility = 60;
                    ResistStun = true;
                    cost = 210000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SNOW_FAIRY_BREATH:
                    description = "��̗d������̂����₩�ȉ������������B�S�{�U�O�A���ّϐ�";
                    buffUpMind = 60;
                    ResistSilence = true;
                    cost = 215000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SILENT_BOWL:
                    description = "�����ȐU���𔺂��A�o�E���h���Ă��S���������Ȃ��{�[���B�m�{�U�O�A��ბϐ�";
                    buffUpIntelligence = 60;
                    ResistParalyze = true;
                    cost = 220000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STASIS_RING:
                    description = "�g�p���ꂽ�`�����Ȃ��A���Ԃ��~�܂��Ă��܂������̂悤�ȃ����O�B�́{�U�O�A�X�^���ϐ�";
                    buffUpStamina = 60;
                    ResistStun = true;
                    cost = 225000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WATERY_GROBE:
                    description = "�����Ȑ��̋��̂��A�g�̂̎��͂����������񂷂�B���ّϐ��A��ბϐ��A�X�^���ϐ�";
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistSilence = true;
                    cost = 230000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_FROZEN_LAVA:
                    description = "�ˏo���ꂽ�n�₪�u�ԓI�ɓ������ꂽ���́B�y����\�́F�L�z�m�{�T�O�A�S�{�T�O�A���ϐ��T�O�O�A�Αϐ��T�O�O";
                    description += "\r\n�y����\�́z�@���@�u�t���C���E�X�g���C�N�v�܂��͖��@�u�t���[�Y���E�����X�v�𔭓�����B";
                    buffUpIntelligence = 50;
                    buffUpMind = 50;
                    ResistIce = 500;
                    ResistFire = 500;
                    useSpecialAbility = true;
                    cost = 400000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SEAL_OF_ICE:
                    description = "�ˏo���ꂽ�n�₪�u�ԓI�ɓ������ꂽ���́B�y����\�́F�L�z���ϐ��P�O�O�O�A��ბϐ��A�����ϐ�";
                    buffUpIntelligence = 70;
                    buffUpStamina = 70;
                    ResistIce = 1000;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    cost = 405000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_TAMATEBAKO_AKIDAMA:
                    description = "�J���ꂽ�ʎ蔠�A���̒��ɂ͋����̌��ʂ������Ă���B�y����\�́F�L�z�́{�V�O�A�S�{�V�O";
                    description += "\r\n�y����\�́z�@�퓬����x���������\�B�Ώۂ𕜊������A���C�t���P�O���܂ŉ񕜂�����B";
                    buffUpStamina = 70;
                    buffUpMind = 70;
                    useSpecialAbility = true;
                    cost = 410000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_WHITE_TIGER_ANGEL_GOHU:
                    description = "�ߌՓV�g����̋��͂ȉ���𓾂�B�́{�P�O�O�A���ϐ��P�S�O�O�A�y�X�^���z�y��Ⴡz�y�����z�y�U�f�z�ϐ�";
                    buffUpStamina = 100;
                    ResistIce = 1400;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    ResistTemptation = true;
                    cost = 600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_POWER_STEEL_RING_SOLID:
                    description = "���c�|�f�ނ̘r�ցA�y�\���b�h�z�̈󎚂��{����Ă���B�́{�V�O�A�Z�{�T�O�A�S�{�S�O";
                    buffUpStrength = 70;
                    buffUpAgility = 50;
                    buffUpMind = 40;
                    cost = 380000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_POWER_STEEL_RING_VAPOR:
                    description = "���c�|�f�ނ̘r�ցA�y���F�C�p�[�z�̈󎚂��{����Ă���B�Z�{�V�O�A�m�{�T�O�A�́{�S�O";
                    buffUpAgility = 70;
                    buffUpIntelligence = 50;
                    buffUpStamina = 40;
                    cost = 380000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_POWER_STEEL_RING_ERASTIC:
                    description = "���c�|�f�ނ̘r�ցA�y�G���X�g�z�̈󎚂��{����Ă���B�m�{�S�O�A�́{�V�O�A�S�{�T�O";
                    buffUpIntelligence = 40;
                    buffUpStamina = 70;
                    buffUpMind = 50;
                    cost = 380000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_POWER_STEEL_RING_TORAREITION:
                    description = "���c�|�f�ނ̘r�ցA�y�g�����C�X�z�̈󎚂��{����Ă���B�́{�T�O�A�m�{�V�O�A�S�{�S�O";
                    buffUpStrength = 50;
                    buffUpIntelligence = 70;
                    buffUpMind = 40;
                    cost = 380000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SIHAIRYU_KIBA:
                    description = "�E�F�N�X���[�e�n�ɐ��ށu�x�z���v�̉�B�́{�T�O�A�S�{�T�O�A���U���{�V��";
                    buffUpStrength = 50;
                    buffUpMind = 50;
                    amplifyPhysicalAttack = 1.07f;
                    cost = 250000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_OLD_TREE_JUSHI:
                    description = "�E�F�N�X���[��n�ɖ���u�Ñ�h���v�̎����B�m�{�T�O�A�S�{�T�O�A���U���{�V��";
                    buffUpIntelligence = 50;
                    buffUpMind = 50;
                    amplifyMagicAttack = 1.07f;
                    cost = 250000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_GALEWIND_KIZUATO:
                    description = "�E�F�N�X���[�R���̐_�u�Q�C���E�E�B���h�v�̏��ՁB�Z�{�T�O�A�S�{�T�O�A�푬���{�V��";
                    buffUpAgility = 50;
                    buffUpMind = 50;
                    amplifyBattleSpeed = 1.07f;
                    cost = 250000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SIN_CRYSTAL_QUATZ:
                    description = "�E�F�N�X���[�ߑ�Z�p�u�V���E�N���X�^���v�̃N�H�[�c�B�́{�P�O�O�A�퉞���{�V��";
                    buffUpStamina = 100;
                    amplifyBattleResponse = 1.07f;
                    cost = 250000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EVERMIND_OMEN:
                    description = "�E�F�N�X���[�V��̎�u�G�o�[�E�}�C���h�v�̃I�[�����B�S�{�P�O�O�A���͗��{�V��";
                    buffUpMind = 100;
                    amplifyPotential = 1.07f;
                    cost = 250000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SYUURENSYA_KUROOBI:
                    description = "�C���҂̋ɂ݂������̍��̍��сB�y����\�́F�L�z�A�́{�P�Q�O�A�ő�X�L���|�C���g�{�P�O";
                    description += "\r\n�y����\�́z�@�X�L���u�C���i�[�E�C���X�s���[�V�����v�𔭓�����B";
                    buffUpStamina = 120;
                    effectValue1 = 10;
                    useSpecialAbility = true;
                    cost = 410000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SHIHANDAI_KUROOBI:
                    description = "�t�͑�̋Ɉӂ����܂�Ă��鍕�сB�y����\�́F�L�z�A�́{�P�Q�O�A�ő�X�L���|�C���g�{�P�O";
                    description += "\r\n�y����\�́z�@�X�L���u�X�^���X�E�I�u�E�A�C�Y�v�𔭓�����B";
                    buffUpStamina = 120;
                    effectValue1 = 10;
                    useSpecialAbility = true;
                    cost = 410000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SYUUDOUSOU_KUROOBI:
                    description = "�C���m�̑�z�������_���������сB�y����\�́F�L�z�A�́{�P�Q�O�A�ő�X�L���|�C���g�{�P�O";
                    description += "\r\n�y����\�́z�@�X�L���u�s���A�E�s�����t�@�C�P�[�V�����v�𔭓�����B";
                    buffUpStamina = 120;
                    effectValue1 = 10;
                    useSpecialAbility = true;
                    cost = 410000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_KUGYOUSYA_KUROOBI:
                    description = "��s�҂̒B�ς��ꂽ�n�_��\�����сB�y����\�́F�L�z�A�́{�P�Q�O�A�ő�X�L���|�C���g�{�P�O";
                    description += "\r\n�y����\�́z�@�X�L���u�j�Q�C�g�v�𔭓�����B";
                    buffUpStamina = 120;
                    effectValue1 = 10;
                    useSpecialAbility = true;
                    cost = 410000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_TEARS_END:
                    description = "�͂ꂫ��܂ł̗܂�}��Ƃ��A�S���Ăэ��ރA�N�Z�T���B�y����\�́F�L�z�A�S�{�P�Q�O";
                    description += "\r\n�y����\�́z�@���@�u���C�Y�E�I�u�E�C���[�W�v�𔭓�����B";
                    buffUpMind = 120;
                    useSpecialAbility = true;
                    cost = 420000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SKY_COLD_BOOTS:
                    description = "�󒆂ɐ�������X���������A�Z���Ăэ��ރA�N�Z�T���B�y����\�́F�L�z�A�Z�{�P�Q�O";
                    description += "\r\n�y����\�́z�@���@�u�q�[�g�E�u�[�X�g�v�𔭓�����B";
                    buffUpAgility = 120;
                    useSpecialAbility = true;
                    cost = 420000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_EARTH_BREAKERS_SIGIL:
                    description = "��n�֋T��𔭐������A�͂��Ăэ��ރA�N�Z�T���B�y����\�́F�L�z�A�́{�P�Q�O";
                    description += "\r\n�y����\�́z�@���@�u�u���b�f�B�E���F���W�F���X�v�𔭓�����B";
                    buffUpStrength = 120;
                    useSpecialAbility = true;
                    cost = 420000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_AERIAL_VORTEX:
                    description = "�n���̉Q�����R���������A�m���Ăэ��ރA�N�Z�T���B�y����\�́F�L�z�A�m�{�P�Q�O";
                    description += "\r\n�y����\�́z�@���@�u�v���~�X�h�E�i���b�W�v�𔭓�����B";
                    buffUpIntelligence = 120;
                    useSpecialAbility = true;
                    cost = 420000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_LIVING_GROWTH_SEED:
                    description = "�i�΂̂���萁���킩��A�̂��Ăэ��ރA�N�Z�T���B�y����\�́F�L�z�A�́{�P�Q�O";
                    description += "\r\n�y����\�́z�@���@�u���[�h�E�I�u�E���C�t�v�𔭓�����B";
                    buffUpStamina = 120;
                    useSpecialAbility = true;
                    cost = 420000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_SWORD:
                    description = "�����ڂ��ǂ��A�؂ꖡ�����Q�̌��B�U���͂P�Q�T�`�P�S�O";
                    minValue = 125;
                    maxValue = 140;
                    cost = 25000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_KNUCKLE:
                    description = "�����A�����ړI�ɑf���炵���A�؂ꖡ�ō��̒܁B�U���͂P�R�O�`�P�R�T";
                    minValue = 130;
                    maxValue = 135;
                    cost = 23000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_BUSTER:
                    description = "���؂ȍʐF�ł���A���A�d�������������Ȃ��ō��̗��茕�B�U���͂U�Q�`�Q�T�O";
                    minValue = 62;
                    maxValue = 250;
                    cost = 28000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_ROD:
                    description = "���f�Ȃ���㎿�A�����āA�����������Ŗ��͂��`����Ă����B���͂P�O�T�`�P�Q�Q";
                    MagicMinValue = 105;
                    MagicMaxValue = 122;
                    cost = 30000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLACK_ICE_SWORD:
                    description = "���F�ɕϐF�����X���n�`��ƂȂ�A�኱�̉ЁX���������������錕�B�y����\�́F�L�z�́{�V�O�A�m�{�V�O�A�U���͂Q�P�O�`�Q�S�O";
                    description += "\r\n�y����\�́z�@�U���𓖂Ă邽�сA�}�i�|�C���g���񕜂���B";
                    buffUpStrength = 70;
                    buffUpIntelligence = 70;
                    minValue = 210;
                    maxValue = 240;
                    cost = 62000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_MENTALIZED_FORCE_CLAW:
                    description = "���_�I�ȗ~�����͂��Ȃ���͂Ƃ��Ĕ��������܁B�y����\�́F�L�z�́{�U�T�A�Z�{�U�T�A�U���͂Q�O�O�`�Q�R�O";
                    description += "\r\n�y����\�́z�@�U���𓖂Ă邽�сA�X�L���|�C���g���񕜂���B";
                    buffUpStrength = 65;
                    buffUpAgility = 65;
                    minValue = 200;
                    maxValue = 230;
                    cost = 58000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_CLAYMORE_ZUKS:
                    description = "�K�b�c���_���[�W�𓖂ĂA����ɕt�����̗͂�D����闼�蕀�B�y����\�́F�L�z�́{�W�O�A�S�{�W�O�A�U���͂P�Q�T�`�R�X�O";
                    description += "\r\n�y����\�́z�@�U���𓖂Ă邽�сA���C�t�|�C���g���񕜂���B";
                    buffUpStrength = 80;
                    buffUpMind = 80;
                    minValue = 125;
                    maxValue = 390;
                    cost = 73000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_ADERKER_FALSE_ROD:
                    description = "�b�񂾎����̒��Ŏ��R����������Ȗ��͂�����B�y����\�́F�L�z�m�{�U�O�A�́{�U�O�A���͂P�T�T�`�P�X�O�A";
                    description += "\r\n�y����\�́z�@���@�_���[�W�𓖂Ă邽�сA�C���X�^���g�|�C���g���񕜂���B";
                    buffUpIntelligence = 60;
                    buffUpStamina = 60;
                    MagicMinValue = 155;
                    MagicMaxValue = 190;
                    cost = 72000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_DESCENED_BLADE:
                    description = "���ŉ��낷�悤�Ȍ`����������B�U���͂P�T�T�`�P�W�Q";
                    minValue = 155;
                    maxValue = 182;
                    cost = 35000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FALSET_CLAW:
                    description = "�����ȊX�t�@���Z�b�g�ł́A�b��p�̒܂����ɗ��s���Ă���炵���B�U���͂P�U�T�`�P�V�V";
                    minValue = 165;
                    maxValue = 177;
                    cost = 31000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SEKIGAN_ROD:
                    description = "�Жڂ��������Ƃ���҂����͂�����Ȃ��悤�ɍ쐬������B���͂P�R�T�`�P�T�T";
                    MagicMinValue = 135;
                    MagicMaxValue = 155;
                    cost = 40000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ROCK_BUSTER:
                    description = "�w������ӂ��I�x�Ɩ��t�����҂��������������茕�B�U���͂W�T�`�R�P�O";
                    MagicMinValue = 85;
                    MagicMaxValue = 310;
                    cost = 40000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SHARPNEL_SPIN_BLADE:
                    description = "�����ȍ����������I�ɔ����Ă���U���^�̌��B�y����\�́F�L�z�푬���{�P�O���A�U���͂Q�S�T�`�Q�V�O";
                    description += "\r\n�y����\�́z�@�퓬���g�p����ƁA�푬����1.1�{�㏸������B";
                    minValue = 245;
                    maxValue = 270;
                    amplifyBattleSpeed = 1.1f;
                    cost = 72000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLUE_LIGHT_MOON_CLAW:
                    description = "�퓬�̍\�����������A���R�Ɛ����錎��A�z������܁B�y����\�́F�L�z���U���{�P�O���A�U���͂Q�R�O�`�Q�U�O";
                    description += "\r\n�y����\�́z�@�퓬���g�p����ƁA���U����1.1�{�㏸������B";
                    minValue = 230;
                    maxValue = 260;
                    amplifyPhysicalAttack = 1.1f;
                    cost = 63000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SHAERING_BONE_CRUSHER:
                    description = "�������ӂ���΂���̈З͂������o���Ă���闼�蕀�B�y����\�́F�L�z���͗��{�P�O���A�U���͂P�T�O�`�S�V�O";
                    description += "\r\n�y����\�́z�@�퓬���g�p����ƁA���͗���1.1�{�㏸������B";
                    minValue = 150;
                    maxValue = 470;
                    amplifyPotential = 1.1f;
                    cost = 80000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLIZZARD_SNOW_ROD:
                    description = "���܂Ō����������Ă��܂��قǂ̈З͂�����B�y����\�́F�L�z���U���{�P�O���A���͂P�W�T�`�Q�Q�O";
                    description += "\r\n�y����\�́z�@�퓬���g�p����ƁA���U����1.1�{�㏸������B";
                    MagicMinValue = 185;
                    MagicMaxValue = 220;
                    amplifyMagicAttack = 1.1f;
                    cost = 78000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_ARMOR:
                    description = "����Ȃ��A�ō��i�d���Ă̊Z�B�h��͂U�O�`�V�O";
                    minValue = 60;
                    maxValue = 70;
                    cost = 23000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_CROSS:
                    description = "�y�����ō��ŁA�h��͂��㎿�̕����߁B�h��͂T�Q�`�T�W";
                    minValue = 52;
                    maxValue = 58;
                    cost = 21000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_ROBE:
                    description = "�ō��̃��[�u�p�f�ގg�p�B���ݖڂ̎�_�⋭���{����Ă���B�h��͂Q�X�`�R�R�A���@�h��S�T�`�U�O";
                    minValue = 29;
                    maxValue = 33;
                    MagicMinValue = 45;
                    MagicMaxValue = 60;
                    cost = 30000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_DRAGONSCALE_ARMOR:
                    description = "�M�d�ȃh���S���ؑf�ނ���g�����Z�͂ق̂��Ȍ�������Ă���B�h��͂X�O�`�P�O�T�A��ბϐ��A�őϐ��V�T�O";
                    minValue = 90;
                    maxValue = 105;
                    ResistParalyze = true;
                    ResistShadow = 750;
                    cost = 55000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_LIGHT_BLIZZARD_ROBE:
                    description = "�X���Ⴊ�����������ߌ`�󃍁[�u�A�W�����肪�ڂɉf��B�h��͂T�S�`�U�V�A���@�h��P�O�T�`�P�Q�O�A�X�^���ϐ��A���ϐ��V�T�O";
                    minValue = 54;
                    maxValue = 67;
                    MagicMinValue = 105;
                    MagicMaxValue = 120;
                    ResistStun = true;
                    ResistLight = 750;
                    cost = 56000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_COLD_STEEL_PLATE:
                    description = "�����͓��R�Ւf���Ă���A���A�_���[�W�ɑ΂���Ւf�����łȊZ�B�h��͂V�T�`�W�W";
                    minValue = 75;
                    maxValue = 88;
                    cost = 31000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_AIR_HARE_CROSS:
                    description = "���̕����߂𑕔����Ă���҂́A�����̋���H�Ɍ���Ƃ����B�h��͂U�X�`�V�X";
                    minValue = 69;
                    maxValue = 79;
                    cost = 29000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FLOATING_ROBE:
                    description = "���̃��[�u�𑕔����Ă���҂́A���ۂɂ͕����Ȃ����A���V����������Ƃ����B�h��͂R�S�`�S�O�A���@�h��U�T�`�W�X";
                    minValue = 34;
                    maxValue = 40;
                    MagicMinValue = 65;
                    MagicMaxValue = 89;
                    cost = 38000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SCALE_BLUERAGE:
                    description = "�����̍������߂�ꂽ�Z�B���������ɑ����҂ɏd�������������Ȃ��B�y����\�́F�L�z�h��͂P�P�O�`�P�R�Q";
                    description += "\r\n�y����\�́z�@�܂�ɕ����ɂ���_���[�W���O�ɂ���";
                    minValue = 110;
                    maxValue = 132;
                    cost = 60000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLUE_REFLECT_ROBE:
                    description = "�����̍������߂�ꂽ�߁B���̔����Ƃ͗����Ƀ_���[�W���˂��N���肤��B�y����\�́F�L�z�h��͂T�W�`�V�O�A���@�h��P�R�O�`�P�S�T";
                    description += "\r\n�y����\�́z�@�܂�ɖ��@�ɂ���_���[�W���O�ɂ���";
                    minValue = 58;
                    maxValue = 70;
                    MagicMinValue = 130;
                    MagicMaxValue = 145;
                    cost = 64000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_SHIELD:
                    description = "�g�̑S�̂�����T�C�Y�ŁA���A�ϋv���ɂ��D��Ă��鏂�B�h��͂R�V�`�S�Q";
                    minValue = 37;
                    maxValue = 42;
                    cost = 22000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SNOW_CRYSTAL_SHIELD:
                    description = "���ʂ̌����̂����`��ɂȂ������m�B���̍d���͈ꋉ�i�ł���B�h��͂S�V�`�T�O�A���@�h��T�O�`�T�T";
                    minValue = 47;
                    maxValue = 50;
                    MagicMinValue = 50;
                    MagicMaxValue = 55;
                    cost = 30000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SLIDE_THROUGH_SHIELD:
                    description = "�`�󂪔��ɑȉ~�n�̌`�����Ă���A�P���ȍU���Ȃ炳�΂����Ƃ��\�B�y����\�́F�L�z�h��͂T�Q�`�T�S";
                    description += "\r\n�y����\�́z�@�܂�ɕ����ɂ���_���[�W���O�ɂ���";
                    minValue = 52;
                    maxValue = 54;
                    cost = 50000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_ELEMENTAL_STAR_SHIELD:
                    description = "�e�����̖�l�����^�ō��܂�Ă��鏂�B�h��͂T�O�`�T�Q�A���@�h��S�W�`�T�O�A��/��/��/��/��/��ϐ��T�O�O";
                    minValue = 53;
                    maxValue = 55;
                    MagicMinValue = 48;
                    MagicMaxValue = 50;
                    ResistLight = 500;
                    ResistShadow = 500;
                    ResistIce = 500;
                    ResistFire = 500;
                    ResistForce = 500;
                    ResistWill = 500;
                    cost = 54000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                // �K���c����i�R�K�j                                       
                case Database.COMMON_EXCELLENT_SWORD_3:
                    description = "�����ڂ��ǂ��A�؂ꖡ�����Q�̌����K���c�����������B�U���͂P�Q�T(+15)�`�P�S�O(+15)";
                    minValue = 140;
                    maxValue = 155;
                    cost = 3000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                    
                case Database.COMMON_EXCELLENT_KNUCKLE_3:
                    description = "�����A�����ړI�ɑf���炵���A�؂ꖡ�ō��̒܂��K���c�����������B�U���͂P�R�O(+12)�`�P�R�T(+12)";
                    minValue = 142;
                    maxValue = 147;
                    cost = 27000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_BUSTER_3:
                    description = "���؂ȍʐF�ł���A���A�d�������������Ȃ��ō��̗��茕���K���c�����������B�U���͂U�Q(+15)�`�Q�T�O(+30)";
                    minValue = 77;
                    maxValue = 280;
                    cost = 34000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_ROD_3:
                    description = "���f�Ȃ���㎿�A�����āA�����������Ŗ��͂��`����Ă������K���c�����������B���͂P�O�T(+20)�`�P�Q�Q(+20)";
                    MagicMinValue = 125;
                    MagicMaxValue = 142;
                    cost = 35000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_ARMOR_3:
                    description = "����Ȃ��A�ō��i�d���Ă̊Z���K���c�����������B�h��͂U�O(+8)�`�V�O(+8)";
                    minValue = 68;
                    maxValue = 78;
                    cost = 27000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_CROSS_3:
                    description = "�y�����ō��ŁA�h��͂��㎿�̕����߂��K���c�����������B�h��͂T�Q(+6)�`�T�W(+6)";
                    minValue = 58;
                    maxValue = 64;
                    cost = 25000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_ROBE_3:
                    description = "�ō��̃��[�u�p�f�ގg�p�B���ݖڂ̎�_�⋭���{����Ă���A����ɃK���c�����������B�h��͂Q�X(+3)�`�R�R(+3)�A���@�h��S�T(+10)�`�U�O(+10)";
                    minValue = 32;
                    maxValue = 36;
                    MagicMinValue = 55;
                    MagicMaxValue = 70;
                    cost = 34000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_EXCELLENT_SHIELD_3:
                    description = "�g�̑S�̂�����T�C�Y�ŁA���A�ϋv���ɂ��D��Ă��鏂���K���c�����������B�h��͂R�V(+5)�`�S�Q(+5)";
                    minValue = 42;
                    maxValue = 47;
                    cost = 26000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STEEL_BLADE: // �S�c�S�c�������_
                    description = "���x�ȑf�ނ̂ݎg�p�����|�ɃK���c���X�̋Z���h�������I�U���͂Q�Q�T(+25�j�`�Q�T�T(+25)";
                    minValue = 250;
                    maxValue = 280;
                    cost = 73000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SPLASH_BARE_CLAW: // �x�A�N���[�̌���
                    description = "�S�c�S�c���ӂ��U�����N�}�̎�f�ނ��K���c�������ɕ��퉻�ɐ����I�@�U���͂Q�U�Q�`�Q�V�V";
                    minValue = 262;
                    maxValue = 277;
                    cost = 68000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_GATO_HAWL_OF_GREAT: // �Ñ�c���h�����̊p
                    description = "�Ñ㌫�҃K�g�D�Ɏd���Ă����_���̖�́B���فE�X�^���E��ბϐ��B�Z�{�W�T�A�m�{�R�Q�T�A���͂U�U�U�`�V�V�V�A���U���{�Q�O���A���͗��{�Q�O���A�őϐ�1500�A�Αϐ�1500";
                    ResistSilence = true;
                    ResistStun = true;
                    ResistParalyze = true;
                    buffUpAgility = 85;
                    buffUpIntelligence = 325;
                    MagicMinValue = 666;
                    MagicMaxValue = 777;
                    amplifyMagicAttack = 1.2f;
                    amplifyPotential = 1.2f;
                    ResistShadow = 1500;
                    ResistFire = 1500;
                    cost = 250000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_LIZARDSCALE_ARMOR: // ���U�[�h�̗�
                    description = "���U�[�h�̗؂��ׂ����ו������A�Z�`��Ɏd���ĂȂ��������́B�h��͂W�O(+25)�`�P�O�T(+25)";
                    minValue = 105;
                    maxValue = 130;
                    cost = 62000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ARGNIAN_TUNIC: // �A���S�j�A���̎���
                    description = "�A���S�j�A���̑f�ނ͎��F�̃R�[�e�B���O��������肵���h�䐫���o�₷���B�h��͂V�V�`�X�O";
                    minValue = 77;
                    maxValue = 90;
                    cost = 33000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WOLF_BATTLE_CLOTH: // �E���t�̖є�
                    description = "�쐶�E���t�̂��킲�킵�������𗎂Ƃ����ƂȂ��߂Ɏd���ĂĂ���B�́{�V�O�A�h��͂V�Q�`�V�X";
                    minValue = 72;
                    maxValue = 79;
                    buffUpStamina = 70;
                    cost = 58000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_CHILL_BONE_SHIELD:
                    description = "�X�_����ꡂ��ɉ���鉷�x�œ������������̏��B�h��͂U�T�`�V�O�A�Αϐ��V�T�O�A���ϐ��V�T�O";
                    minValue = 65;
                    maxValue = 70;
                    ResistIce = 750;
                    ResistFire = 750;
                    cost = 58000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SNOW_GUARD: // ��L�̖є�
                    description = "����΍��p�Ɍ����邪�A�A�N�Z�T���Ƃ��Ă̏㎿���͑��������҂݂̂��m��B�́{�T�O�A�S�{�T�O�A���ϐ��P�O�O�O";
                    buffUpStamina = 50;
                    buffUpMind = 50;
                    ResistIce = 1000;
                    cost = 250000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WINTERS_HORN:
                    description = "�~�̋G�߂ɁA�z�[���̉��������n��B�y����\�́F�L�z�m�{�T�O�A�́{�T�O";
                    description += "\r\n�y����\�́z�@�����S���́y�m�z�p�����^���㏸������B";
                    buffUpIntelligence = 50;
                    buffUpStamina = 50;
                    cost = 450000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_PENGUIN_OF_PENGUIN: // �G���u�����E�I�u�E�y���M��
                    description = "�y���M���̋C�������S�Ȃ����`����Ă���B�́{�R�O�A�Z�{�R�O�A�m�{�R�O�A�́{�R�O�A�S�{�R�O";
                    minValue = 0;
                    maxValue = 0;
                    buffUpStrength = 30;
                    buffUpAgility = 30;
                    buffUpIntelligence = 30;
                    buffUpStamina = 30;
                    buffUpMind = 30;
                    cost = 500000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // ��
                case Database.COMMON_ESSENCE_OF_EARTH:
                    description = "��n�̃}�e���A�������f�ށB����E�l�̗͗ʂ������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 200000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_KESSYOU_SEA_WATER_SALT:
                    description = "�Ō`�������C���̉������܂��܌����Ɏ����`��������Ԃ�ۂ��Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 210000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_STAR_DUST_RING:
                    description = "�����̌`�����������O�B��������͗��̃p���[��������B�y����\�́F�L�z�́{�U�O";
                    description += "\r\n�y����\�́z�@���@�u���[�h�E�I�u�E�p���[�v�𔭓�����B";
                    buffUpStrength = 60;
                    useSpecialAbility = true;
                    cost = 235000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RED_ONION:
                    description = "�^���Ԃȃ^�}�l�M���E�E�E����͐H�p�f�ނƂ��đ��v�Ȃ̂��H�H";
                    minValue = 0;
                    maxValue = 0;
                    cost = 215000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_HARDEST_FIT_BOOTS:
                    description = "�K�b�`���Ɗ��S�ɑ����Œ艻�����u�[�c�B�����ɂ͂�����x�̊��ꂪ�K�v�B�́{�U�O�A�S�{�S�O�A���h���P�O���A�y�X�^���z�y��Ⴡz�y�݉��z�ϐ�";
                    buffUpStamina = 60;
                    buffUpMind = 40;
                    amplifyPhysicalDefense = 1.1f;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistSlow = true;
                    cost = 420000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WHITE_POWDER:
                    description = "�������B�Ƃ��ɗ��R�͖������댯�Ȋ���������̂͋C�̂������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 260000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SWORD_OF_DIVIDE:
                    description = "��x���f���ꂽ�����ē��������ꂽ���B�ُ�ȃI�[����������B�y����\�́F�L�z�U���͂Q�R�R�`�Q�T�T";
                    description += "\r\n�y����\�́z�@�U���𓖂Ă��ہA�܂�ɑΏۂ̃��C�t��1/5���炷�B";
                    minValue = 230;
                    maxValue = 255;
                    cost = 67000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_OLD_TREE_MIKI_DANPEN:
                    description = "�Ñ�h���̊��f�ނ͐₦�鎖�̂Ȃ��i���������B�Ñ�h���Ɏd�����K�g�D�̌��t���������ɕ������Ă���悤���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Epic;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�S�K�F�����_���h���b�v"
                case Database.COMMON_RED_MEDALLION:
                    description = "���X�ƌ���Ԃ̃��_���I���B�́{�R�O�O";
                    buffUpStrength = 300;
                    cost = 510000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_MEDALLION:
                    description = "���X�ƌ���̃��_���I���B�Z�{�R�O�O";
                    buffUpAgility = 300;
                    cost = 510000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_MEDALLION:
                    description = "���X�ƌ��鎇�̃��_���I���B�m�{�R�O�O";
                    buffUpIntelligence = 300;
                    cost = 510000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_MEDALLION:
                    description = "���X�ƌ���΂̃��_���I���B�́{�R�O�O";
                    buffUpStamina = 300;
                    cost = 510000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_MEDALLION:
                    description = "���X�ƌ��鉩�̃��_���I���B�S�{�R�O�O";
                    buffUpMind = 300;
                    cost = 510000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SOCIETY_SYMBOL:
                    description = "�]���I�ȎЉ�ɑ΂��A���U�̒����𐾂������m�B�̌M�́B�́{�Q�O�O�A�́{�Q�O�O�A�X�^���ϐ��A��ბϐ�";
                    description += "\r\n�y����\�́z�@�����S���̕����U��/�����h����㏸������B�{�A�N�Z�T�����g�p�����ꍇ�A�퓬�I����ɔj�󂳂��B";
                    buffUpStrength = 200;
                    buffUpStamina = 200;
                    ResistStun = true;
                    ResistParalyze = true;
                    UseSpecialAbility = true;
                    cost = 490000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SILVER_FEATHER_BRACELET:
                    description = "���R�Љ�Ő������т邽�߁A�����𐾂����b���B�̌M�́B�Z�{�Q�O�O�A�́{�Q�O�O�A�����ϐ��A�U�f�ϐ�";
                    description += "\r\n�y����\�́z�@�����S���̖��@�U��/���@�h����㏸������B�{�A�N�Z�T�����g�p�����ꍇ�A�퓬�I����ɔj�󂳂��B";
                    buffUpAgility = 200;
                    buffUpStamina = 200;
                    ResistFrozen = true;
                    ResistTemptation = true;
                    UseSpecialAbility = true;
                    cost = 495000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BIRD_SONG_LUTE:
                    description = "��V���l���t�ł�����́A���R�Ǝm�C�����߂Ă����B�́{�Q�O�O�A�S�{�Q�O�O�A���ّϐ��A�݉��ϐ��A�Èőϐ�";
                    description += "\r\n�y����\�́z�@�����S���́y�S�z�p�����^���㏸������B";
                    buffUpStamina = 200;
                    buffUpMind = 200;
                    ResistSilence = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    useSpecialAbility = true;
                    cost = 500000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MAZE_CUBE:
                    description = "���͂Ȗ��͂�������ꂽ�s�v�c�̔��B���̌`��A�`�Ԃ͋ɂ߂ĕs����ł���B�m�{�Q�O�O�A�́{�Q�O�O�A�őϐ��A�X���b�v�ϐ�";
                    description += "\r\n�y����\�́z�@�����U�����q�b�g����x�ɁA���@�U����2%�㏸����B���̌㖂�@�U�����q�b�g����x�ɁA�����U����2%�㏸����B\r\n���̃T�C�N���͍ō�10��܂Œ~�ς��\�ł���B";
                    buffUpIntelligence = 200;
                    buffUpStamina = 200;
                    ResistPoison = true;
                    ResistSlip = true;
                    useSpecialAbility = true;
                    cost = 505000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_LIGHT_SERVANT:
                    description = "�y���z�́A�y���̎g����z�𖣗�����B�S�{�Q�S�O";
                    description += "\r\n�y����\�́z�@��������і��@�U�����q�b�g����x�ɁA���̒~�σJ�E���^�[���P������BUFF�Ƃ��Ē~�ς���B\r\n���̒~�σJ�E���^�[���R�ݐς�����ԂŃA�N�Z�T�����g�p�����ꍇ�A�����S���̃��C�t���񕜂���B���̌�A���̒~�σJ�E���^�[��S�ď�������B";
                    buffUpMind = 240;
                    useSpecialAbility = true;
                    cost = 520000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SHADOW_SERVANT:
                    description = "�y�Łz�́A�y�ł̎g����z���x�z����B�S�{�Q�S�O";
                    description += "\r\n�y����\�́z�@��������і��@�U�����q�b�g����x�ɁA�ł̒~�σJ�E���^�[���P������BUFF�Ƃ��Ē~�ς���B\r\n�ł̒~�σJ�E���^�[���R�ݐς�����ԂŃA�N�Z�T�����g�p�����ꍇ�A�����S����DEBUFF���ʂ���������B���̌�A�ł̒~�σJ�E���^�[��S�ď�������B";
                    buffUpMind = 240;
                    useSpecialAbility = true;
                    cost = 520000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ROYAL_GUARD_RING:
                    description = "�����ɃK�[�h���ł߂����C�����K�[�h�̍��󂪂��郊���O�B�́{�Q�Q�O";
                    description += "\r\n�y����\�́z�@�����҂ɕ����U��/�����h��DOWN���ʂ����������ꍇ�A���̌��ʂ𖳌�������B";
                    buffUpStamina = 220;
                    useSpecialAbility = true;
                    cost = 525000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ELEMENTAL_GUARD_RING:
                    description = "�����ɃK�[�h���ł߂��G�������^���K�[�h�̍��󂪂��郊���O�B�́{�Q�Q�O";
                    description += "\r\n�y����\�́z�@�����҂ɖ��@�U��/���@�h��DOWN���ʂ����������ꍇ�A���̌��ʂ𖳌�������B";
                    buffUpStamina = 220;
                    useSpecialAbility = true;
                    cost = 525000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_HAYATE_GUARD_RING:
                    description = "�����ɃK�[�h���ł߂���悤�A�����K�[�h�̍��󂪂��郊���O�B�́{�Q�Q�O";
                    description += "\r\n�y����\�́z�@�����҂ɐ퓬���x/�퓬����DOWN���ʂ����������ꍇ�A���̌��ʂ𖳌�������B";
                    buffUpStamina = 220;
                    useSpecialAbility = true;
                    cost = 525000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SPELL_COMPASS:
                    description = "���̃R���p�X�̐j�́A�m�������҂ɑ΂��Ď��̖��@�r���X�^�C���������Ă����B�m�{�R�O�O�A�́{�P�S�O�A�S�{�P�S�O";
                    description += "\r\n�y����\�́z�@���������҂ɃV���h�E�E�p�N�g���������Ă��Ȃ��ꍇ�A�V���h�E�E�p�N�g�𔭓�����B\r\n���������҂Ƀv���~�X�h�E�i���b�W���������Ă��Ȃ��ꍇ�A�v���~�X�h�E�i���b�W�𔭓�����B\r\n���������҂ɃT�C�L�b�N�E�g�����X���������Ă��Ȃ��ꍇ�A�T�C�L�b�N�E�g�����X�𔭓�����B";
                    buffUpIntelligence = 300;
                    buffUpStamina = 140;
                    buffUpMind = 140;
                    useSpecialAbility = true;
                    cost = 850000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SHADOW_BIBLE:
                    description = "���̃o�C�u������́A�͂������҂ɑ΂��ĕs���̋�����^���Ă����B�́{�R�O�O�A�m�{�P�S�O�A�́{�P�S�O";
                    description += "\r\n�y����\�́z�@���������҂��퓬���ɏ��߂Ď��S�����ꍇ�A�����҂𕜊������A���C�t��S���܂ŉ񕜂���B���̌�A�h���s��BUFF���t�^�����B";
                    buffUpStrength = 300;
                    buffUpIntelligence = 140;
                    buffUpStamina = 140;
                    useSpecialAbility = true;
                    cost = 870000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_DETACHMENT_ORB:
                    description = "���̃I�[�u����́A�Z�������҂ɑ΂��ă_���[�W�������_���\�z���Ă����B�Z�{�R�O�O�A�́{�P�S�O�A�S�{�P�S�O";
                    description += "\r\n�y����\�́z�@���^�[���܂ŁA�����S���ɑ΂���_���[�W�𖳌�������B�{���ʂ͊e�퓬�ɕt����x���������ł���B";
                    buffUpAgility = 300;
                    buffUpStamina = 140;
                    buffUpMind = 140;
                    useSpecialAbility = true;
                    cost = 890000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLIND_NEEDLE:
                    description = "�Ӗڎ҂͌����Ȃ����������m���A�s�����̏������\�ɂ���B�́{�P�S�O�A�m�{�P�S�O�A�S�{�R�O�O";
                    description += "\r\n�y����\�́z�@�S��Ԉُ����������B";
                    buffUpStrength = 140;
                    buffUpIntelligence = 140;
                    buffUpMind = 300;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    useSpecialAbility = true;
                    cost = 1000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_CORE_ESSENCE_CHANNEL:
                    description = "�����҂ɏh��G�b�Z���X���ő���Ɉ����o���`���l�������O�B�́{�Q�O�O�A�Z�{�Q�O�O�A�m�{�Q�O�O�A�́{�Q�O�O�A�S�{�Q�O�O";
                    description += "\r\n�y����\�́z�@���C�t�ƃX�L���ƃ}�i���񕜂���B";
                    buffUpStrength = 200;
                    buffUpAgility = 200;
                    buffUpIntelligence = 200;
                    buffUpStamina = 200;
                    buffUpMind = 200;
                    ResistPoison = true;
                    ResistSilence = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    ResistSlip = true;
                    useSpecialAbility = true;
                    cost = 1000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_SWORD:
                    description = "�j��ō��̎����Ǝa���͂������o����Ă��錕�B�U���͂S�U�W�`�S�X�O";
                    minValue = 468;
                    maxValue = 490;
                    cost = 620000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_KNUCKLE:
                    description = "�j��ō��̌y���ƎE���͂𕹂����܁B�U���͂S�V�T�`�S�W�T";
                    minValue = 475;
                    maxValue = 485;
                    cost = 620000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_ROD:
                    description = "�j��ō��̖��͌��������o�����ɒ�������B���͂R�S�R�`�S�P�W";
                    MagicMinValue = 343;
                    MagicMaxValue = 418;
                    cost = 620000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_AXE:
                    description = "�j��ō��̏Ռ��͂𐶂ݏo���͂��^�����Ă��镀�B�U���͂Q�Q�O�`�U�P�O";
                    minValue = 220;
                    maxValue = 610;
                    cost = 620000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_ARMOR:
                    description = "�j��ō��̑ϋv���A�����āA�S�ǂ̖h��͂��ւ�Z�B�h��͂P�Q�O�`�P�S�T";
                    minValue = 120;
                    maxValue = 145;
                    cost = 580000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_CROSS:
                    description = "�j��ō��̏_��Ƒϓˌ����𗼗��������N���X�B�h��͂W�W�`�P�P�Q";
                    minValue = 88;
                    maxValue = 112;
                    cost = 580000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_ROBE:
                    description = "�j��ō��̌���Ɩ��h�����˔����郍�[�u�B�h��͂V�U�`�W�T�A���@�h��P�O�W�`�P�R�Q";
                    minValue = 76;
                    maxValue = 85;
                    MagicMinValue = 108;
                    MagicMaxValue = 132;
                    cost = 580000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MASTER_SHIELD:
                    description = "�j��ō��̋��x���ƌ��S�����������������B�h��͂U�Q�`�W�T";
                    minValue = 62;
                    maxValue = 85;
                    cost = 300000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_ASTRAL_VOID_BLADE:
                    description = "�^��̐n�ɂ��A��Ԃ��̂��̂ւ̃_���[�W��^���錕�B�U���͂U�T�Q�`�V�Q�X";
                    minValue = 652;
                    maxValue = 729;
                    cost = 1520000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_VERDANT_SONIC_CLAW:
                    description = "�����̐U��ŁA��ɋ��g����o���Ȃ��痧���U�镑����܁B�U���͂U�V�P�`�V�O�T";
                    minValue = 671;
                    maxValue = 705;
                    cost = 1520000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_PRISONER_BREAKING_AXE:
                    description = "�č��̟B�ł���A�ւ��܂�͂������o����o�J�ł������B�U���͂R�S�R�`�P�O�P�R";
                    minValue = 343;
                    maxValue = 1013;
                    cost = 1520000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_INVISIBLE_STATE_ROD:
                    description = "�^�[�Q�b�g�ɂ���Ă�����̂���A���̃��b�h�̎p�͌����Ȃ��B���͂S�W�U�`�U�V�V";
                    MagicMinValue = 486;
                    MagicMaxValue = 677;
                    cost = 1520000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_DOMINATION_BRAVE_ARMOR:
                    description = "�E���Ȃ铬�m���߂����̂��A�h��̈З͂��x�z����B�h��͂P�W�U�`�P�X�W";
                    minValue = 186;
                    maxValue = 198;
                    cost = 1120000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RED_FLOAT_STONE:
                    description = "�W���Ƃ炷�Ԃ̐΂��g�̂̎���ɕ��V����B�́{�T�Q�O";
                    buffUpStrength = 520;
                    cost = 950000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_FLOAT_STONE:
                    description = "�W���Ƃ炷�̐΂��g�̂̎���ɕ��V����B�Z�{�T�Q�O";
                    buffUpAgility = 520;
                    cost = 950000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_FLOAT_STONE:
                    description = "�W���Ƃ炷���̐΂��g�̂̎���ɕ��V����B�m�{�T�Q�O";
                    buffUpIntelligence = 520;
                    cost = 950000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_FLOAT_STONE:
                    description = "�W���Ƃ炷�΂̐΂��g�̂̎���ɕ��V����B�́{�T�Q�O";
                    buffUpStamina = 520;
                    cost = 950000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_FLOAT_STONE:
                    description = "�W���Ƃ炷���̐΂��g�̂̎���ɕ��V����B�S�{�T�Q�O";
                    buffUpMind = 520;
                    cost = 950000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SILVER_RING_1:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�Ɖ΁x�̃I�[�����Y���Ă���B�́{�R�V�O�A�Z�{�R�V�O";
                    buffUpStrength = 370;
                    buffUpAgility = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_2:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�Ôg�x�̃I�[�����Y���Ă���B�́{�R�V�O�A�m�{�R�V�O";
                    buffUpStrength = 370;
                    buffUpIntelligence = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_3:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�H�J�x�̃I�[�����Y���Ă���B�́{�R�V�O�A�́{�R�V�O";
                    buffUpStrength = 370;
                    buffUpStamina = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_4:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�M�g�x�̃I�[�����Y���Ă���B�́{�R�V�O�A�S�{�R�V�O";
                    buffUpStrength = 370;
                    buffUpMind = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_5:
                    description = "��̑f�ނō��ꂽ�r�ցB�w���x�̃I�[�����Y���Ă���B�Z�{�R�V�O�A�m�{�R�V�O";
                    buffUpAgility = 370;
                    buffUpIntelligence = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_6:
                    description = "��̑f�ނō��ꂽ�r�ցB�w����x�̃I�[�����Y���Ă���B�Z�{�R�V�O�A�́{�R�V�O";
                    buffUpAgility = 370;
                    buffUpStamina = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_7:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�����x�̃I�[�����Y���Ă���B�Z�{�R�V�O�A�S�{�R�V�O";
                    buffUpAgility = 370;
                    buffUpMind = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_8:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�����x�̃I�[�����Y���Ă���B�m�{�R�V�O�A�́{�R�V�O";
                    buffUpIntelligence = 370;
                    buffUpStamina = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_9:
                    description = "��̑f�ނō��ꂽ�r�ցB�w����x�̃I�[�����Y���Ă���B�m�{�R�V�O�A�S�{�R�V�O";
                    buffUpIntelligence = 370;
                    buffUpMind = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SILVER_RING_10:
                    description = "��̑f�ނō��ꂽ�r�ցB�w�z���x�̃I�[�����Y���Ă���B�́{�R�V�O�A�S�{�R�V�O";
                    buffUpStamina = 370;
                    buffUpMind = 370;
                    cost = 900000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MUKEI_SAKAZUKI:
                    description = "�e�̌����Ȃ��u�������ɑ��݂��Ă���A�����҂ɓ����Ȑ������ꂱ�ށB�S�{�S�O�O";
                    description += "\r\n�y����\�́z�@�^�[���I�����ƂɁA���̃^�[�����Ō��������C�t�A�}�i�A�X�L���̔����̗ʂ��e�X�񕜂���B";
                    buffUpMind = 400;
                    cost = 920000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RAINBOW_TUBE:
                    description = "�����҂̈Ӑ}�ɂ�����炸���F�̕����������o��`���[�u�B�́{�S�O�O";
                    description += "\r\n�y����\�́z�@�ȉ��̂����ꂩ�������_���ɔ�������B\r\n�@�G�S�̂ɑ΂��āy�΁z�_���[�W��^���� / �����S�̂̃��C�t���񕜂��� / �G�S�̂ɑ΂��ăX�^�����ʂ�^���� / �����S�̂�DEBUFF���ʂ���������";
                    buffUpStamina = 400;
                    useSpecialAbility = true;
                    cost = 920000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ELDER_PERSPECTIVE_GRASS:
                    description = "�����҂Ƃ��Ẳs�����@���\�ɂȂ閳�F�����̊ዾ�B�m�{�S�O�O";
                    description += "\r\n�y����\�́z�@�^�[���I�����ƂɁA�Ώۂ̐퓬���x/�퓬������DOWN������B";
                    buffUpIntelligence = 400;
                    useSpecialAbility = true;
                    cost = 920000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_DEVIL_SEALED_VASE:
                    description = "�Έ����p�̐؂�D�Ƃ��ĊJ�����ꂽ��B�w�����ꂽ�Ώۂ��ׂ��͂���߂��Ă���B�́{�S�O�O";
                    description += "\r\n�y����\�́z�@�퓬�̑O�ɁA���@�̖��̂�I�ԁB�퓬���A���̖��@�̖��̂��G����r�����ꂽ�ꍇ�A���̖��@��ł������B";
                    buffUpStrength = 400;
                    cost = 920000;
                    useSpecialAbility = true;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FLOATING_WHITE_BALL:
                    description = "�����̖������F�̕��V�^�̃{�[���B�퓬���A�s����ɂ܂΂䂢�������s���B�Z�{�S�O�O";
                    description += "\r\n�y����\�́z�@�����G�������ɑ΂��čs�����Ă����ꍇ�A�H�ɉ������B";
                    buffUpAgility = 400;
                    cost = 920000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;                    
                    break;

                case Database.RARE_SEAL_OF_ASSASSINATION:
                    description = "�ÎE�퓬�X�^�C���ɑ΂��ϐ������V�[���B�́{�T�O�O�A�S�{�T�O�O�A�Αϐ�5000�A���ϐ�5000";
                    description += "\r\n�y����\�́z�@�����ɑ΂��āA�ꌂ�Ŏ��S����A�N�V�������s��ꂽ�ꍇ�A�������������B";
                    buffUpStamina = 500;
                    ResistFire = 5000;
                    ResistForce = 5000;
                    cost = 1050000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;                    
                    break;

                case Database.RARE_EMBLEM_OF_VALKYRIE:
                    description = "�����}������郔�@���L���[�̖�́B�����҂Ƀ��@���L���[�̐M�O���h��B�́{�S�T�O�A�Z�{�R�T�O�A�́{�Q�O�O�A�S�{�Q�O�O�A���ϐ�5000�A�Αϐ�5000�A���ّϐ��A�X�^���ϐ��A��ბϐ��A�U�f�ϐ��A�݉��ϐ�";
                    description += "\r\n�y����\�́z�@�U���q�b�g���A�H�ɑΏۂ�Ⴢ�����B";
                    buffUpStrength = 450;
                    buffUpAgility = 350;
                    buffUpStamina = 200;
                    buffUpMind = 200;
                    ResistLight = 5000;
                    ResistFire = 5000;
                    cost = 1050000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;                    
                    break;
                
                case Database.RARE_EMBLEM_OF_HADES:
                    description = "�����Ăъ񂹂�n�f�X�̖�́B�����҂Ƀn�f�X�̐M�O���h��B�Z�{�R�T�O�A�m�{�S�T�O�A�́{�Q�O�O�A�S�{�Q�O�O�A�őϐ�5000�A���ϐ�5000�A�őϐ��A��ბϐ��A�����ϐ��A�Èőϐ��A�X���b�v�ϐ�";
                    description += "\r\n�y����\�́z�@�U���q�b�g���A�H�ɑΏۂ��ꌂ�Ŏ��S������B";
                    buffUpAgility = 350;
                    buffUpIntelligence = 450;
                    buffUpStamina = 500;
                    buffUpMind = 200;
                    ResistShadow = 5000;
                    ResistIce = 5000;
                    cost = 1050000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;                    
                    break;
                case Database.RARE_SIHAIRYU_KATAUDE:
                    description = "�x�z���̖ʉe����������Иr�̑��`���B�́{�U�O�O�A�́{�R�O�O�A�S�{�S�O�O�A���U���{�P�T���A���͗��{�T��";
                    buffUpStrength = 600;
                    buffUpStamina = 300;
                    buffUpMind = 400;
                    amplifyPhysicalAttack = 1.15f;
                    amplifyPotential = 1.05f;
                    cost = 1100000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;   
                    break;
                case Database.RARE_OLD_TREE_SINKI:
                    description = "�Ñ�h���̗͋�����������������c�؂̑��`���B�m�{�U�O�O�A�́{�R�O�O�A�S�{�S�O�O�A���U���{�P�T���A���͗��{�T��";
                    buffUpIntelligence = 600;
                    buffUpStamina = 300;
                    buffUpMind = 400;
                    amplifyMagicAttack = 1.15f;
                    amplifyPotential = 1.05f;
                    cost = 1100000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;   
                    break;
                case Database.RARE_GALEWIND_IBUKI:
                    description = "�m���Ȏ����̗�����������鑧�����ۂ鑢�`���B�Z�{�U�O�O�A�m�{�S�O�O�A�S�{�R�O�O�A�푬���{�P�T���A���͗��{�T��";
                    buffUpAgility = 600;
                    buffUpIntelligence = 400;
                    buffUpMind = 300;
                    amplifyBattleSpeed = 1.15f;
                    amplifyPotential = 1.05f;
                    cost = 1100000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SIN_CRYSTAL_SOLID:
                    description = "���݂����߂悤�Ƃ��������������鑢�`���B�́{�S�O�O�A�m�{�R�O�O�A�́{�U�O�O�A�퉞���{�P�T���A���͗��{�T��";
                    buffUpStrength = 400;
                    buffUpIntelligence = 300;
                    buffUpStamina = 600;
                    amplifyBattleResponse = 1.15f;
                    amplifyPotential = 1.05f;
                    cost = 1100000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_EVERMIND_SENSE:
                    description = "�i�v����A�Ȃ闆������������鑢�`���B�́{�Q�O�O�A�Z�{�Q�O�O�A�m�{�Q�O�O�A�́{�Q�O�O�A�S�{�U�O�O�A���͗��{�Q�T��";
                    buffUpStrength = 200;
                    buffUpAgility = 200;
                    buffUpIntelligence = 200;
                    buffUpStamina = 200;
                    buffUpMind = 600;
                    amplifyPotential = 1.25f;
                    cost = 1100000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DEVIL_SUMMONER_TOME:
                    description = "�ٌ`�̈������������邱�Ƃ��o����g�[���B�́{�T�O�O�A�m�{�T�O�O�A�S�{�T�O�O";
                    description += "\r\n�y����\�́z�@�g�p�����^�[���ȍ~�A���^�[�������҂��G�ɍU��������x�ɁA�����āy��/�Łz�����̒ǉ��_���[�W��^����B";
                    buffUpStrength = 500;
                    buffUpIntelligence = 500;
                    buffUpMind = 500;
                    useSpecialAbility = true;
                    cost = 1150000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ARCHANGEL_CONTRACT:
                    description = "��V�g�Ƃ̌_��𐾂��Ƃ��鎖�ŁA�ی�̉��b���󂯂�B�́{�T�O�O";
                    description += "\r\n�y����\�́z�@�^�[���I�����A�����U��/�����h��/���@�U��/���@�h��/�퓬���x/�퓬����/���ݔ\��DOWN��BUFF����������B";
                    buffUpStamina = 500;
                    useSpecialAbility = true;
                    cost = 1150000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DARKNESS_COIN:
                    description = "�ł̑��݂Ɩ��@�̌_������킵���؂Ƃ��Ĕz�z�����R�C���B";
                    description += "\r\n�y����\�́z�@�X�L���g�p���A�X�L���|�C���g���s�����Ă���ꍇ�A���C�t��1/5�����ăX�L���𔭓�����B���̌��ʂ͑������Ă������A�i������B";
                    cost = 1150000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SOUSUI_HIDENSYO:
                    description = "�E�F�N�X���[�I�ËI���瑶�݂���X�L���`���̑������L������`���B�́{�V�O�O";
                    description += "\r\n�y����\�́z�@�ő�X�L���|�C���g�{�Q�O";
                    description += "\r\n�y����\�́z�@�X�L���u�o�C�I�����g�E�X���b�V���v�𔭓�����B";
                    effectValue1 = 20;
                    BuffUpStamina = 700;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MEEK_HIDENSYO:
                    description = "�E�F�N�X���[�I�ËI���瑶�݂���X�L���`���̎�y�҂��L������`���B�́{�V�O�O";
                    description += "\r\n�y����\�́z�@�ő�X�L���|�C���g�{�Q�O";
                    description += "\r\n�y����\�́z�@�X�L���u���J�o�[�v�𔭓�����B";
                    effectValue1 = 20;
                    BuffUpStamina = 700;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_JUKUTATUSYA_HIDENSYO:
                    description = "�E�F�N�X���[�I�ËI���瑶�݂���X�L���`���̏n�B�҂��L������`���B�́{�V�O�O";
                    description += "\r\n�y����\�́z�@�ő�X�L���|�C���g�{�Q�O";
                    description += "\r\n�y����\�́z�@�X�L���u�X�E�B�t�g�E�X�e�b�v�v�𔭓�����B";
                    effectValue1 = 20;
                    BuffUpStamina = 700;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_KYUUDOUSYA_HIDENSYO:
                    description = "�E�F�N�X���[�I�ËI���瑶�݂���X�L���`���̋����҂��L������`���B�́{�V�O�O";
                    description += "\r\n�y����\�́z�@�ő�X�L���|�C���g�{�Q�O";
                    description += "\r\n�y����\�́z�@�X�L���u�t���[�`���[�E���B�W�����v�𔭓�����B";
                    effectValue1 = 20;
                    BuffUpStamina = 700;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DANZAI_ANGEL_GOHU:
                    description = "�f�ߓV�g�����҂ɑ΂��đ���͂���아�B�́{�T�T�O�A���h���{�P�O���A���h���{�P�O���A���ϐ�7500�A�őϐ�7500�A�Αϐ�7500�A���ϐ�7500�A���ϐ�7500�A��ϐ�7500�A\r\n�őϐ��A���ّϐ��A�X�^���ϐ��A��ბϐ��A�����ϐ��A�U�f�ϐ��A�݉��ϐ��A�Èőϐ��A�X���b�v�ϐ�";
                    buffUpStamina = 550;
                    ResistLight = 7500;
                    ResistShadow = 7500;
                    ResistFire = 7500;
                    ResistIce = 7500;
                    ResistForce = 7500;
                    ResistWill = 7500;
                    ResistPoison = true;
                    ResistSilence = true;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    ResistSlip = true;
                    cost = 1500000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                    
                case Database.COMMON_INITIATE_SWORD:
                    description = "���Ԃ����镔�ʂ͉���A�z������A�ٌ`�̌��B�U���͂T�W�S�`�U�T�Q";
                    minValue = 584;
                    maxValue = 652;
                    cost = 890000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BULLET_KNUCKLE:
                    description = "�n�敔���s���Ƃ����Ă���A���ڂɌ���ƒe�ۂ�A�z������܁B�U���͂T�X�P�`�U�Q�W";
                    minValue = 591;
                    maxValue = 628;
                    cost = 890000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_KENTOUSI_SWORD:
                    description = "�×���茕���m�͎��̊ԍۂ܂ŁA���𗣂��Ȃ��ƌ����`�����Ă���B�U���͂R�P�W�`�X�Q�T";
                    minValue = 318;
                    maxValue = 925;
                    cost = 890000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ELECTRO_ROD:
                    description = "�d���͂Ɩ��@�͎��Ĕ�Ȃ���̂����A���@�Ɠ����̕����ʂ𐶂ݏo����Z�p���~�ς���Ă����B���͂S�Q�T�`�T�U�O";
                    MagicMinValue = 425;
                    MagicMaxValue = 560;
                    cost = 890000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FORTIFY_SCALE:
                    description = "�����҂̑̊i�̌��^���킩��Ȃ��Ȃ�قǂ̃S�c���Z�B�h��͂P�U�Q�`�P�W�W";
                    minValue = 162;
                    maxValue = 188;
                    cost = 860000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_MURYOU_CROSS:
                    description = "�������Ă��銴�G���܂��������������Ȃ������߁B�h��͂P�S�W�`�P�T�U";
                    minValue = 148;
                    maxValue = 156;
                    cost = 860000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_COLORLESS_ROBE:
                    description = "�����҂̃C���[�W��A�z������F�����[�u�B�h��͂P�O�U�`�P�Q�Q�A���@�h��͂P�W�X�`�Q�P�Q";
                    minValue = 106;
                    maxValue = 122;
                    MagicMinValue = 189;
                    MagicMaxValue = 212;
                    cost = 860000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_LOGISTIC_SHIELD:
                   description = "���@�ɂ��_���[�W���������������ƌ����ĂĖh��\�Ƃ������B�h��͂V�T�`�X�T�A���@�h��͂V�O�`�V�Q";
                    minValue = 75;
                    maxValue = 95;
                    MagicMinValue = 70;
                    MagicMaxValue = 72;
                    cost = 340000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_ETHREAL_EDGE_SABRE:
                    description = "�C�X���A���f�ނɂ�鋭�x�Ȍ��B����U�������A�����F�������؂��悪�����Ȃ��Ȃ�B�U���͂W�Q�P�`�X�U�T";
                    minValue = 821;
                    maxValue = 965;
                    cost = 2260000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_SHINGETUEN_CLAW:
                    description = "���̋Ȑ���`���悤�ɑS�̂��ۂ��Ȃ����Ă���܁B�U���͂W�V�V�`�X�S�R";
                    minValue = 877;
                    maxValue = 943;
                    cost = 2260000;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLOODY_DIRTY_SCYTHE:
                    description = "���т����������̌������܂Ƃ����Ă���劙�B�U���͂S�U�U�`�P�S�Q�R";
                    minValue = 466;
                    maxValue = 1423;
                    cost = 2260000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_ALL_ELEMENTAL_ROD:
                    description = "�S�����̖��͂��ϓ��ɍ��߂��Ă�����B���͂V�O�T�`�W�R�X";
                    MagicMinValue = 705;
                    MagicMaxValue = 839;
                    cost = 2260000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLOOD_BLAZER_CROSS:
                    description = "�Ԃ������ɃC�X���A�����̑f�ނ��҂ݍ��܂�Ă��镑���߁B�S�̖̂͗l�����ǂ̗l�Ɍ�����B�h��͂R�Q�T�`�R�S�T";
                    minValue = 325;
                    maxValue = 345;
                    cost = 1450000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_DARK_ANGEL_ROBE:
                    description = "�^�����ȃ��[�u���܂Ƃ����p�͍��߂̓V�g��A�z������B�����h��͂P�X�O�`�Q�R�S�A���@�h��͂S�X�Q�`�U�T�Q";
                    description += "\r\n�y����\�́z�@�����@�P�O�������A�ő����P�O������";
                    minValue = 190;
                    maxValue = 234;
                    MagicMinValue = 492;
                    MagicMaxValue = 652;
                    amplifyLight = 1.1f;
                    amplifyShadow = 1.1f;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_MAJEST_HAZZARD_SHIELD:
                    description = "���Ђ̃I�[��������̔@���A���������b�̏��B�����h��͂P�O�T�`�P�R�O�A���@�h��͂P�S�O�`�P�T�O";
                    minValue = 105;
                    maxValue = 130;
                    MagicMinValue =140;
                    MagicMaxValue = 150;
                    cost = 720000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_WHITE_DIAMOND_SHIELD:
                    description = "���������ȋP�������_�C�������h�������܂񂾑傫�ȏ��B�h��͂P�Q�O�`�P�S�T";
                    minValue = 120;
                    maxValue = 145;
                    cost = 725000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_VAPOR_SOLID_SHIELD:
                    description = "���C�^�̗��q�������������A�ő̉��ɐ����������B�d�ʂ��y���Ɣ���Ⴕ�ďd���ȍ��B�h��͂P�U�R�`�P�V�T";
                    minValue = 163;
                    maxValue = 175;
                    cost = 730000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �K���c���퉮
                case Database.RARE_TYOU_KOU_SWORD:
                    description = "�������̑f�ނ����x�����x���b�������A����ɍd���������������B�U���͂P�O�O�O�`�P�Q�T�O";
                    minValue = 1000;
                    maxValue = 1250;
                    cost = 3000000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    break;
                
                case Database.RARE_TYOU_KOU_ARMOR:
                    description = "�������̑f�ނ��t���ɋ�g���A����ɍd���Z�����������B�h��͂S�S�O�`�T�T�O";
                    minValue = 440;
                    maxValue = 550;
                    cost = 2200000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    break;
                
                case Database.RARE_TYOU_KOU_SHIELD:
                    description = "�������̑f�ނ����S�̂ɍs���n�点�A����ɍd���������������B�h��͂P�X�O�`�Q�O�O";
                    minValue = 190;
                    maxValue = 200;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_SUPERIOR_CHOSEN_ROD:
                    description = "���I���ꂽ�������̑f�ނ���̐�[���Ɋی^�ɂ��ėn�ڂ������b�h�B���͂V�U�W�`�W�X�R";
                    MagicMinValue = 768;
                    MagicMaxValue = 893;
                    cost = 3000000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_WHITE_GOLD_CROSS:
                    description = "�������̑f�ނ��}�C�N���P�ʂ̔����`��ɕω������A�ߏ�̌`�Ɏd���ďグ���B�h��͂R�S�V�`�R�V�O�A���@�h��R�S�O�`�R�V�O";
                    minValue = 340;
                    maxValue = 370;
                    MagicMinValue = 340;
                    MagicMaxValue = 370;
                    cost = 2150000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_HUNTERS_EYE: // �n���^�[�̎�����
                    description = "�n���^�[�������g�ݍ��킹�č쐬���ꂽ�[��B��̓����̊J�����ɉ����ėl�X�ȃM�~�b�N����������B�Z�{�R�O�O�A�́{�R�O�O�A���ّϐ��A��ბϐ��A�݉��ϐ��A�Èőϐ�";
                    description += "\r\n�y����\�́z�@�ȉ��̂����ꂩ�������_���ɔ�������B\r\n�@�@�G�S�̂ɑ΂��āy�݉��z���ʂ�^���� / �����S�̂̂����ꂩ���g�D���X�E���B�W�������������Ă��Ȃ��ꍇ�A�g�D���X�E���B�W�����𔭓����� \r\n    / �������g�̕����U���͂Ɛ퓬���x��UP���� / �G�P�̂̕����U���͂Ɛ퓬���x��DOWN������";
                    buffUpAgility = 300;
                    buffUpStamina = 300;
                    useSpecialAbility = true;
                    ResistSilence = true;
                    ResistParalyze = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    cost = 950000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    break;
                case Database.RARE_ONEHUNDRED_BUTOUGI: // �ҏb�̖є�
                    description = "�I�肷����̏b����W�񂵁A�����₷���E�d�ʊ����d���������́B�h��͂P�U�S�`�P�V�W�A���ϐ�15000�A�őϐ�15000�A�Αϐ�15000�A���ϐ�15000";
                    description += "\r\n�y����\�́z�@�܂�ɕ���/���@�ɂ��U�����������B";
                    minValue = 164;
                    maxValue = 178;
                    ResistLight = 15000;
                    ResistShadow = 15000;
                    ResistFire = 15000;
                    ResistIce = 15000;
                    cost = 1125000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_DARKANGEL_CROSS: // ���s�l�̉��ꂽ���[�u�A�V�g�̃V���N
                    description = "���s�l�̃��[�u���獂���ȃV���N�f�ނ�E�o���A�V�g�̃V���N�ƗZ�������ĐV���ɑn�������߁B";
                    description += "\r\n�h��͂P�X�O�`�Q�R�S�A���@�h��S�X�Q�`�U�T�Q�A���ϐ�22000�A�őϐ�22000�A�őϐ��A�U�f�ϐ��A�݉��ϐ��A�Èőϐ�";
                    description += "\r\n�y����\�́z�@�����@�P�O�������A�Ŗ��@�P�O������";
                    minValue = 190;
                    maxValue = 234;
                    MagicMinValue = 492;
                    MagicMaxValue = 652;
                    ResistLight = 22000;
                    ResistShadow = 22000;
                    ResistPoison = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    AmplifyLight = 1.1f;
                    AmplifyShadow = 1.1f;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_DEVIL_KILLER: // �K�t�����K���N�^����A�G�b�Z���X�E�I�u�E�_�[�N
                    description = "�������҂�f���B�K���N�^���琶�������Ƃ͎v���Ȃ��K���c�Ӑg�̗͍�B�U���͂R�U�O�`�P�W�W�T";
                    description += "\r\n�y����\�́z�@�H�ɑ���������B";
                    minValue = 360;
                    maxValue = 1885;
                    cost = 3000000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_TRUERED_MASTER_BLADE: // �V�[�J�[�̓��W���A�}�X�^�[�u���C�h�̔j�ЁA�G�b�Z���X�E�I�u�E�t���C��
                    description = "���W����j�ӂ����f�ނ𕿂ɕt���A���̐؂���͏�ɉ΂��h��B�U���͂W�O�O�`�W�T�O�A���͂U�T�O�`�V�O�O";
                    description += "\r\n�y����\�́z�@�����U�����q�b�g����x�ɁA�H�Ƀ��[�h�E�I�u�E�p���[���ǉ����ʂŔ�������B";
                    description += "\r\n    ���@�U�����q�b�g����x�ɁA�H�ɃT�C�L�b�N�E�E�F�C�u���ǉ����ʂŔ�������B";
                    minValue = 800;
                    maxValue = 850;
                    MagicMinValue = 650;
                    MagicMaxValue = 700;
                    cost = 2400000;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_VOID_HYMNSONIA: // ���؂ȃW���G���N���E���A���씠
                    description = "���؂ȍ����^���鎖�ŉ����S�ď����������B�́{�T�O�O�A�Z�{�T�O�O�A�m�{�T�O�O�A�őϐ�10000�őϐ��A���ّϐ��A�X�^���ϐ��A��ბϐ��A�����ϐ��A�U�f�ϐ��A�݉��ϐ��A�Èőϐ��A�X���b�v�ϐ�";
                    description += "\r\n�y����\�́z�@�퓬�J�n���A�S�p�����^���P�ɂȂ�B";
                    description += "\r\n�y����\�́z�@�{�����i�ɂ��y�S�z�p�����^���P�ɂȂ�����������������B�퓬�I���܂ł��̌��ʂ͌p������B";
                    minValue = 0;
                    maxValue = 0;
                    buffUpStrength = 500;
                    buffUpAgility = 500;
                    buffUpIntelligence = 500;
                    ResistShadow = 10000;
                    ResistPoison = true;
                    ResistSilence = true;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    ResistSlip = true;
                    cost = 1180000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_SEAL_OF_BALANCE: // �g�ݗ��đf�ށ@�V���A�g�ݗ��đf�ށ@�V�����A�g�ݗ��đf�ށ@�V���_
                    description = "�V���̌`����č\�z���A��͂̌`��ɕϊ����邱�Ƃɐ����B�́{�T�O�O�A�S�{�T�O�O�A���ϐ�5000�A��ϐ�5000";
                    description += "\r\n�y����\�́z�@�����U�����󂯂��ꍇ�A�}�i���񕜂���B���@�U�����󂯂��ꍇ�A�X�L���|�C���g���񕜂���BDEBUFF�������t�^���ꂽ�ꍇ�A���̃^�[������BUFF����������B";
                    minValue = 0;
                    maxValue = 0;
                    buffUpStamina = 500;
                    buffUpMind = 500;
                    ResistIce = 5000;
                    ResistWill = 5000;
                    cost = 1040000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.RARE_DOOMBRINGER: // �h�D�[���u�����K�[�̕��A�h�D�[���u�����K�[�̌���
                    description = "�j�ł����҂։i���̈��炬�������炷���߂ɍ��ꂽ���B�U���͂S�V�R�`�P�S�U�X";
                    description += "\r\n�y����\�́z�@�����@�{�P�O�������B";
                    description += "\r\n              �퓬�J�n���A�Q�C���E�E�B���h���������g�ɂ�����B";
                    minValue = 473;
                    maxValue = 1469;
                    amplifyForce = 1.1f;
                    cost = 2400000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_MEIKOU_DOOMBRINGER: // �h�D�[���u�����K�[�A��΂̒b��
                    description = "�q�����蕥��ꂽ�ł̌��B������̈Ӑ}�Ɋւ�炸�A�����h���I�ԁB�U���͂P�Q�O�O�`�Q�S�O�O�A���͂P�Q�O�O�`�Q�S�O�O�A���U���{�Q�T���A���U���{�Q�O���A�푬���{�P�T��";
                    description += "\r\n�y����\�́z�@�����@�{�P�U�������A�����@�P�U�������B";
                    description += "\r\n              �퓬�J�n���A�Q�C���E�E�B���h���������g�ɂ�����B";
                    description += "\r\n              �퓬�J�n���A�W�F�l�V�X�̍s���L���Ɂy�Q�C���E�E�B���h�z���Z�b�g�����B";
                    minValue = 1200;
                    maxValue = 2400;
                    MagicMinValue = 1200;
                    MagicMaxValue = 2400;
                    buffUpStrength = 777;
                    buffUpAgility = 555;
                    buffUpIntelligence = 666;
                    amplifyPhysicalAttack = 1.25;
                    amplifyMagicAttack = 1.20;
                    amplifyBattleSpeed = 1.15;
                    amplifyForce = 1.16;
                    amplifyLight = 1.16;
                    cost = 10000000;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    break;

                // ��

                // EPIC

                case Database.EPIC_EZEKRIEL_ARMOR_SIGIL:
                    description = "�Ñ㌫�҃G�[�N���G�����s�N����ɕt���Ă����Z�B�h��͂P�P�Q�U�`�P�R�X�V�A���@�h��U�V�Q�`�W�R�O�A�́{�W�T�O�A�Z�{�U�Q�O�A�m�{�T�R�O�A�́{�V�S�O";
                    description += "\r\n���h���{�Q�T���A���@�h�䗦�{�Q�T���A���ϐ�35000�A�őϐ�35000�A�Αϐ�35000�A���ϐ�35000";
                    description += "\r\n�őϐ��A���ّϐ��A�X�^���ϐ��A��ბϐ��A�����ϐ��A�U�f�ϐ��A�݉��ϐ��A�Èőϐ��A�X���b�v�ϐ�";
                    description += "\r\n�y����\�́z���^�[���A���C�t�ƃ}�i�ƃX�L���|�C���g���񕜂���B";
                    description += "\r\n���@����P�O���y���B  �X�L���|�C���g����P�O���y���B";
                    minValue = 1126;
                    maxValue = 1397;
                    MagicMinValue = 672;
                    MagicMaxValue = 830;
                    buffUpStrength = 850;
                    buffUpAgility = 620;
                    buffUpIntelligence = 530;
                    buffUpStamina = 740;
                    ResistFire = 35000;
                    ResistIce = 35000;
                    ResistLight = 35000;
                    ResistShadow = 35000;
                    amplifyPhysicalDefense = 1.25;
                    amplifyMagicDefense = 1.25;
                    ResistPoison = true;
                    ResistSilence = true;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistBlind = true;
                    ResistSlip = true;
                    manaCostReduction = 0.1f;
                    skillCostReduction = 0.1f;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Epic;
                    break;

                case Database.EPIC_SHEZL_THE_MIRAGE_LANCER:
                    description = "�Ñ㌫�҃V�F�Y�����s�N����ɐU����Ă������B�U���͂P�U�W�O�`�P�W�T�T�A���@�U���P�T�Q�Q�`�P�V�Q�W�A�Z�{�V�T�O�A�m�{�X�T�O�A�S�{�T�T�O";
                    description += "\r\n���U���{�Q�T���A�퉞���{�Q�O���A���͗��{�P�T��";
                    description += "\r\n�y����\�́z���̌����畨���U�����q�b�g�����ہA�_�u���q�b�g�Ƃ��Ĉ�����B";
                    minValue = 1680;
                    maxValue = 1855;
                    MagicMinValue = 1522;
                    MagicMaxValue = 1728;
                    buffUpAgility = 750;
                    buffUpIntelligence = 950;
                    buffUpMind = 550;
                    amplifyMagicAttack = 1.25;
                    amplifyBattleResponse = 1.20;
                    amplifyPotential = 1.15;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    break;

                case Database.EPIC_JUZA_THE_PHANTASMAL_CLAW:
                    description = "�Ñ㌫�҃W���U���s�N����ɐU����Ă����܁B�U���͂P�X�W�S�`�Q�O�Q�P�A�́{�X�T�O�A�Z�{�V�T�O�A�́{�T�T�O";
                    description += "\r\n���U���{�Q�T���A�푬���{�Q�O���A�퉞���{�P�T��";
                    description += "\r\n�y����\�́z�����U�����q�b�g���邽�тɁA�D�̒~�σJ�E���^�[���P������BUFF�Ƃ��Ē~�ς���B�~�ς��ꂽ�J�E���^�[�̕������A�퓬���x���Q�����㏸����B�ő�10�܂Œ~�ς��s����B";
                    minValue = 1984;
                    maxValue = 2021;
                    buffUpStrength = 950;
                    buffUpAgility = 750;
                    buffUpStamina = 550;
                    amplifyPhysicalAttack = 1.25;
                    amplifyBattleSpeed = 1.20;
                    amplifyBattleResponse = 1.15;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Epic;
                    break;

                case Database.EPIC_ADILRING_OF_BLUE_BURN:
                    description = "�Ñ㌫�҃G�[�f�B�����s�N����ɑ������Ă����������郊���O�B�U���͂P�O�T�O�`�P�P�T�O�A���@�U���P�Q�T�O�`�P�R�T�O�B�́{�T�T�O�A�m�{�P�O�T�O�A�S�{�T�T�O�A���ّϐ��A�����ϐ��A�݉��ϐ�";
                    description += "\r\n���U���{�P�O���A���U���{�R�O���A���͗��{�P�O���A���ϐ�10000�A�őϐ�10000�A�Αϐ�10000�A���ϐ�75000�A���ϐ�10000�A��ϐ�10000";
                    description += "\r\n�y����\�́z�C�ӂ̍s�����s�����тɁA���̒~�σJ�E���^�[���P������BUFF�Ƃ��Ē~�ς���B�ő�30�܂Œ~�ς��s����B";
                    description += "\r\n�y����\�́zMP������āA�������̃_���[�W��^����B�_���[�W�ʂ͑��̒~�σJ�E���^�[�Ɉˑ�����B";
                    minValue = 1050;
                    maxValue = 1150;
                    MagicMinValue = 1250;
                    MagicMaxValue = 1350;
                    buffUpStrength = 550;
                    buffUpIntelligence = 1050;
                    buffUpMind = 550;
                    amplifyPhysicalAttack = 1.10;
                    amplifyMagicAttack = 1.30;
                    amplifyPotential = 1.10;
                    ResistLight = 10000;
                    ResistShadow = 10000;
                    ResistFire = 10000;
                    ResistIce = 75000;
                    ResistForce = 10000;
                    ResistWill = 10000;
                    ResistSilence = true;
                    ResistFrozen = true;
                    ResistSlow = true;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    break;

                case Database.EPIC_ETERNAL_HOMURA_RING:
                    description = "���̉��΂��ׂ��鎖�͖������ċN���������Ȃ��B���@�U���P�W�T�O�`�Q�O�T�O�A�m�{�P�Q�T�O�A�X�^���ϐ��A��ბϐ��A�����ϐ�";
                    description += "\r\n���U���{�R�T���A���h���R�O���A���ϐ�10000�A�őϐ�10000�A�Αϐ�75000�A���ϐ�75000�A���ϐ�10000�A��ϐ�10000";
                    description += "\r\n�y����\�́z���^�[���AMP���񕜂���B";
                    description += "\r\n�y����\�́z�SMP������āA�����MP�̕������A���������@�_���[�W��^����B";
                    useSpecialAbility = true;
                    MagicMinValue = 1850;
                    MagicMaxValue = 2050;
                    buffUpIntelligence = 1250;
                    amplifyMagicAttack = 1.35;
                    amplifyMagicDefense = 1.30;
                    ResistLight = 10000;
                    ResistShadow = 10000;
                    ResistFire = 75000;
                    ResistIce = 10000;
                    ResistForce = 10000;
                    ResistWill = 10000;
                    ResistStun = true;
                    ResistParalyze = true;
                    ResistFrozen = true;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                // �f��
                case Database.COMMON_BLACK_SALT:
                    description = "�������̒m��Ȃ������ϐF�������́E�E�E�����ɉ����c���Ƃ�������������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FEBL_ANIS:
                    description = "�t�F�u���̑�n�ɋH�ɉf����A���B�ʕ��̂悤�ȍ��肪�ق�̂肷��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SMORKY_HUNNY:
                    description = "����̂悤�Ɋg����A�Â�����𔭂���A������A���̖����t����ꂽ�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ANGEL_DUST:
                    description = "�V�g�̈߂̈ꕔ����H�p�̑@�ۂ�����o����ƌ����Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SUN_TARAGON:
                    description = "�Z������̂����̐�����A���B�T�N�T�N���Ƃ����H�������҂ł���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ECHO_BEAST_MEAT:
                    description = "������t�ł�̂����ӂȐ����G�R�[�r�[�X�g�̂������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_CHAOS_TONGUE:
                    description = "�J�I�X�E���[�f�����Ō�̒f����������������ɐ��؂���ƁA���ɔ����Ƃ���Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1200000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�T�K�F�����_���h���b�v"
                case Database.COMMON_RED_CRYSTAL:
                    description = "�i�v�̎����肻�̋P���͎����Ă��Ȃ��A�^�g�̃N���X�^���B�́{�P�S�O�O";
                    buffUpStrength = 1400;
                    cost = 2000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_CRYSTAL:
                    description = "�i�v�̎����肻�̋P���͎����Ă��Ȃ��A�ڗ��̃N���X�^���B�Z�{�P�S�O�O";
                    buffUpAgility = 1400;
                    cost = 2000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURPLE_CRYSTAL:
                    description = "�i�v�̎����肻�̋P���͎����Ă��Ȃ��A�����̃N���X�^���B�m�{�P�S�O�O";
                    buffUpIntelligence = 1400;
                    cost = 2000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_CRYSTAL:
                    description = "�i�v�̎����肻�̋P���͎����Ă��Ȃ��A�Ő��̃N���X�^���B�́{�P�S�O�O";
                    buffUpStamina = 1400;
                    cost = 2000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_CRYSTAL:
                    description = "�i�v�̎����肻�̋P���͎����Ă��Ȃ��A���߂̃N���X�^���B�S�{�P�S�O�O";
                    buffUpMind = 1400;
                    cost = 2000000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_1:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w���Ձx�̍��󂪍��܂�Ă���B�́{�U�T�O�A�Z�{�U�T�O";
                    buffUpStrength = 650;
                    buffUpAgility = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_2:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w���@���L���[�x�̍��󂪍��܂�Ă���B�́{�U�T�O�A�m�{�U�T�O";
                    buffUpStrength = 650;
                    buffUpIntelligence = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_3:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�i�C�g���A�x�̍��󂪍��܂�Ă���B�́{�U�T�O�A�́{�U�T�O";
                    buffUpStrength = 650;
                    buffUpStamina = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_4:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�i���V���n�x�̍��󂪍��܂�Ă���B�́{�U�T�O�A�S�{�U�T�O";
                    buffUpStrength = 650;
                    buffUpMind = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_5:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�鐝�x�̍��󂪍��܂�Ă���B�Z�{�U�T�O�A�m�{�U�T�O";
                    buffUpAgility = 650;
                    buffUpIntelligence = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_6:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�E���{���X�x�̍��󂪍��܂�Ă���B�Z�{�U�T�O�A�́{�U�T�O";
                    buffUpAgility = 650;
                    buffUpStamina = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_7:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�i�C���e�C���x�̍��󂪍��܂�Ă���B�Z�{�U�T�O�A�S�{�U�T�O";
                    buffUpAgility = 650;
                    buffUpMind = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_8:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�x�q���X�x�̍��󂪍��܂�Ă���B�m�{�U�T�O�A�́{�U�T�O";
                    buffUpIntelligence = 650;
                    buffUpStamina = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_9:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w���x�̍��󂪍��܂�Ă���B�m�{�U�T�O�A�S�{�U�T�O";
                    buffUpIntelligence = 650;
                    buffUpMind = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLATINUM_RING_10:
                    description = "�����̑f�ނŌ`�����ꂽ�r�ցB�w�����x�̍��󂪍��܂�Ă���B�́{�U�T�O�A�S�{�U�T�O";
                    buffUpStamina = 650;
                    buffUpMind = 650;
                    cost = 1600000;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "���i�̖�i�X"
                #region "�_���W�����P�K"
                // ����
                case Database.POOR_SMALL_GREEN_POTION:
                    description = "�����߂ɍ��ꂽ�X�L���񕜗p�̖�B�񕜗ʂT�`�P�O";
                    minValue = 5;
                    maxValue = 10;
                    cost = 150;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Poor;
                    break;
                case Database.POOR_POTION_CURE_POISON:
                    description = "���ʂ̓ł��u���ɏ򉻂����B���ʁy�ғŁz�������B";
                    minValue = 100;
                    maxValue = 100;
                    cost = 200;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Poor;
                    break;

                case Database.COMMON_REVIVE_POTION_MINI:
                    description = "���S�����p�[�e�B�����o�[�𕜊��������B���C�t�P�ŕ�������B��x�g���Ɩ����Ȃ�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 2000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_POTION_NATURALIZE:
                    description = "���R�f�ނ̗ΐF�f�𒲍������򉻖�B���ʁy�ғŁz�y�݉��z�������B�y�퓬����p�z";
                    minValue = 100;
                    maxValue = 100;
                    cost = 700;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    break;
                // ����
                case Database.COMMON_POTION_MAGIC_SEAL:
                    description = "�Ԃ��E�q�����疂�@������E�o���A�����ɐ����B���@�U���T���t�o�B�y�퓬����p�z";
                    cost = 2500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_POTION_ATTACK_SEAL:
                    description = "�A�����E�l�̉ԕ�����ؗ͂��ꎞ�����������B�����U���T���t�o�B�y�퓬����p�z";
                    cost = 2500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.COMMON_POTION_CURE_BLIND:
                    description = "�ő����̃}���h���S���ɋP���ӕ���D���������B���ʁy�ÈŁz�������B�y�퓬����p�z";
                    cost = 2500;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    break;
                case Database.RARE_POTION_MOSSGREEN_DREAM:
                    description = "���X�̗L���ȃG�L�X��E�o���A�h���[���p�E�_�[�Ő��򉻂�����B���ʁy�ғŁz�y�݉��z�y�ÈŁz�������B�y�퓬����p�z";
                    cost = 6000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    break;
                #endregion
                #region "�_���W�����Q�K"
                // ����
                // ��Ғ����|�[�V�����B�O�҂̐ݒ�ƃo�����X������Ȃ��̂ŐV�K�쐬����B
                case Database.COMMON_NORMAL_RED_POTION:
                    description = "�W���I�ȑ傫���ō��ꂽ���C�t�񕜗p�̖�B�񕜗ʂP�O�Q�O�`�P�Q�W�O";
                    minValue = 1020;
                    maxValue = 1280;
                    cost = 2000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_NORMAL_BLUE_POTION:
                    description = "�W���I�ȑ傫���ō��ꂽ�}�i�񕜗p�̖�B�񕜗ʂS�R�O�`�V�V�O";
                    minValue = 430;
                    maxValue = 770;
                    cost = 2000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_NORMAL_GREEN_POTION:
                    description = "�W���I�ȑ傫���ō��ꂽ�X�L���񕜗p�̖�B�񕜗ʂP�O�`�Q�O";
                    minValue = 10;
                    maxValue = 20;
                    cost = 2000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                // ����
                case Database.COMMON_RESIST_POISON:
                    description = "�ғŐ����𒊏o���A���w���̍Ĕz��ɐ����B�y�ғŁz�������y�ғŁz�ϐ���t�^�B�y�퓬����p�z";
                    cost = 12000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_POTION_OVER_GROWTH:
                    description = "�ُ퐬������������A�������i�G�L�X��E�o���A����������B�ő僉�C�t1000�t�o�y�퓬����p�z";
                    cost = 14000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_POTION_RAINBOW_IMPACT:
                    description = "�퓬�\�͒ቺ�ɑ΂����_�������s�����F��i�B�y�����U���z�y���@�U���z�ቺ�������y�퓬����p�z";
                    cost = 15000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_POTION_BLACK_GAST:
                    description = "���͂Ȑg�̊����Ɩ��͊����G�L�X�𒲍�������i�B���@�U��/�����U���V���t�o�y�퓬����p�z";
                    cost = 25000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�_���W�����R�K"
                case Database.COMMON_LARGE_RED_POTION:
                    description = "���Ȃ�傫�߂ɍ��ꂽ���C�t�񕜗p�̖�B�񕜗ʂR�Q�T�O�`�S�R�W�O";
                    minValue = 3250;
                    maxValue = 4380;
                    cost = 15000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_LARGE_BLUE_POTION:
                    description = "���Ȃ�傫�߂ɍ��ꂽ�}�i�񕜗p�̖�B�񕜗ʂP�S�X�O�`�Q�O�S�O";
                    minValue = 1490;
                    maxValue = 2040;
                    cost = 15000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_LARGE_GREEN_POTION:
                    description = "���Ȃ�傫�߂ɍ��ꂽ�X�L���񕜗p�̖�B�񕜗ʂQ�O�`�R�O";
                    minValue = 20;
                    maxValue = 30;
                    cost = 15000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FAIRY_BREATH:
                    description = "�t�F�A���[�̑����ɂ́A���_�����߂鐬�����܂܂�Ă���B�y���فz���������y���فz�ϐ���t�^�B�y�퓬����p�z";
                    cost = 40000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_HEART_ACCELERATION:
                    description = "�S���̏�Ԃ��ō��̃R���f�B�V�����ɂ��A�g�̖̂����𐶂ݏo���B�y��Ⴡz���������y��Ⴡz�ϐ���t�^�B�y�퓬����p�z";
                    cost = 40000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SAGE_POTION_MINI:
                    description = "���ҒB�̌������ʂ̈ꕔ��q�؂������B�S������ʂ��������A�S�ϐ���t�^�B�Ώێ҂͎��S���A�����ł��Ȃ��Ȃ�B�y�퓬����p�z";
                    cost = 150000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�_���W�����S�K"
                case Database.COMMON_HUGE_RED_POTION:
                    description = "���r�b�O�T�C�Y�ŁA�h��Ȍ��򂪎{����Ă��郉�C�t�񕜗p�̖�B�񕜗ʂQ�T�O�O�O�`�R�O�O�O�O";
                    minValue = 25000;
                    maxValue = 30000;
                    cost = 200000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_HUGE_BLUE_POTION:
                    description = "���r�b�O�T�C�Y�ŁA�h��Ȍ��򂪎{����Ă���}�i�񕜗p�̖�B�񕜗ʂP�T�O�O�O�`�P�W�O�O�O";
                    minValue = 15000;
                    maxValue = 18000;
                    cost = 200000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_HUGE_GREEN_POTION:
                    description = "���r�b�O�T�C�Y�ŁA�h��Ȍ��򂪎{����Ă���X�L���񕜗p�̖�B�񕜗ʂR�O�`�S�O";
                    minValue = 30;
                    maxValue = 40;
                    cost = 200000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_POWER_SURGE:
                    description = "�����̌�����p���[�̍����������o����B�́{�U�O�O�A�́{�S�O�O�A���U���{�Q�O����t�^����B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 500000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_GENSEI_MAGIC_BOTTLE:
                    description = "���_�̌�����m�b�̌����������o����B�m�{�U�O�O�A�S�{�S�O�O�A���U���{�Q�O����t�^����B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 500000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MIND_ILLUSION:
                    description = "��Z���̌�����C���[�W�̑����������o����B�́{�P�O�O�A�Z�{�P�O�O�A�m�{�P�O�O�A�́{�P�O�O�A�S�{�U�O�O�A���͗��{�Q�O����t�^����B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 500000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ZETTAI_STAMINAUP:
                    description = "���̌�����I�[���̑��݂������o����B�́{�Q�O�O�A�m�{�Q�O�O�A�́{�U�O�O�A���h���{�P�O���A���h���{�P�O����t�^����B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 500000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ZEPHER_BREATH:
                    description = "�V���̌���������̐S�������o����B�Z�{�U�O�O�A�m�{�S�O�O�A�푬���{�Q�O����t�^����B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 500000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ELEMENTAL_SEAL:
                    description = "�l�X�ȃ}�e���A���𕪐͂��A�ϐ��n���F�f�𒊏o���A��̃V�[���Ɏd���Ă���i�B";
                    description += "\r\n�Ώێ҂̓ŁA���فA�X�^���A��ჁA�����A�U�f�A�݉��A�ÈŁA�X���b�v����������B�y�퓬����p�z";
                    description += "\r\n�őϐ��A���ّϐ��A�X�^���ϐ��A��ბϐ��A�����ϐ��A�U�f�ϐ��A�݉��ϐ��A�Èőϐ��A�X���b�v�ϐ��y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 350000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_GENSEI_TAIMA_KUSURI:
                    description = "�w�����𑢁x�ƘA�g���A���Ɩ�����܂����������ޖ��̔��B";
                    description += "\r\n�����𔺂��A�N�V�������s��ꂽ�ꍇ�A�������������B���̌��ʂ͈�x�����K�p�����B�y�퓬����p�z";
                    description += "\r\n���C�t���O�ɂȂ����ꍇ�A���C�t�𔼕��ɂ܂ŉ񕜂���B���̌��ʂ͈�x�����K�p�����B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 700000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SHINING_AETHER:
                    description = "�_�X�������P���G�[�e���܁B���Ă��邾���ł��A�E�C���N���Ă���B";
                    description += "\r\n���̃^�[���܂ŁA�y���j�z�X�L������x���������\�ƂȂ�B�y�퓬����p�z";
                    description += "\r\n���̃^�[���܂ŁA�S�_���[�W����ؖ����ƂȂ�B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 750000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BLACK_ELIXIR:
                    description = "�̓��ɏh�鈫�����͂������ȗ͂ւƕϊ������B";
                    description += "\r\n�ő僉�C�t���T�O������������B���̑��������������A���C�t�񕜂���B�y�퓬����p�z";
                    description += "\r\n���C�t��������������ʁi���C�t�������A���C�t�����A���C�t�P�ϊ��j�������ꍇ�A������������B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 750000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_COLORESS_ANTIDOTE:
                    description = "�����I�A�܂��͐��_�I�Ȉ��z�𕥐@�����邽�߂ɊJ�����ꂽ������B";
                    description += "\r\n�����U���A�����h��A���@�U���A���@�h��A�퓬���x�A�퓬�����A���ݔ\��DOWN����������B�y�퓬����p�z";
                    description += "\r\n�����U���A�����h��A���@�U���A���@�h��A�퓬���x�A�퓬�����A���ݔ\��DOWN�ɑ΂���ϐ��𓾂�B�y�퓬����p�z";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1000000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�_���W�����T�K"
                case Database.COMMON_GORGEOUS_RED_POTION:
                    description = "���r�b�O�T�C�Y�ŁA�h��Ȍ��򂪎{����Ă��郉�C�t�񕜗p�̖�B�񕜗ʂR�T�O�O�O�`�T�O�O�O�O";
                    minValue = 35000;
                    maxValue = 50000;
                    cost = 450000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GORGEOUS_BLUE_POTION:
                    description = "���r�b�O�T�C�Y�ŁA�h��Ȍ��򂪎{����Ă���}�i�񕜗p�̖�B�񕜗ʂQ�Q�O�O�O�`�Q�U�O�O�O";
                    minValue = 22000;
                    maxValue = 26000;
                    cost = 450000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GORGEOUS_GREEN_POTION:
                    description = "���r�b�O�T�C�Y�ŁA�h��Ȍ��򂪎{����Ă���X�L���񕜗p�̖�B�񕜗ʂS�T�`�U�O";
                    minValue = 45;
                    maxValue = 60;
                    cost = 450000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                #endregion

                #endregion
                #region "Duel���Z��̓G��p����"
                case Database.COMMON_ZELKIS_SWORD: // �[���L�X��p����
                    description = "�[���L�X���p�̌��B���肵���З͂Ƌ���������B�U���͂P�Q�`�P�U";
                    minValue = 12;
                    maxValue = 16;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ZELKIS_ARMOR: // �[���L�X��p�Z
                    description = "�[���L�X���p�̊Z�B�͂��ȃR�[�e�B���O���{����Ă���B�h��͂T�`�V";
                    minValue = 5;
                    maxValue = 7;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_WHITE_ROD: // �G�I�l�E�t���l�A�������Ă����
                    description = "���F�̏�B���͂��኱�h���Ă���B���͂R�`�U";
                    MagicMinValue = 3;
                    MagicMaxValue = 6;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_ROBE: // �G�I�l�E�t���l�A�����Ă���h��
                    description = "�F�̃��[�u�B�͂������A���@�h�䂪�オ��B�h��͂P�`�Q�A���ϐ��P�O";
                    MinValue = 1;
                    MaxValue = 2;
                    ResistIce = 10;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FROZEN_BALL: // �Z�����C�E���E��p����
                    description = "�Ώۂ̑�����Q�^�[���������������邱�Ƃ��o����B��x�g���Ɩ����Ȃ�B";
                    minValue = 1;
                    maxValue = 1;
                    cost = 1200;
                    AdditionalDescription(ItemType.Use_Any);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_DEVIL_EYE_ROD:
                    description = "�J�[�����݂����q��b���邽�߂ɍ쐬������B���͂P�T�O�`�Q�Q�O";
                    MagicMinValue = 150;
                    MagicMaxValue = 220;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Epic;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_DARKMAGIC_DEVIL_EYE:
                    description = "���ڊ�ɑ���������ł̋[��B�V�j�L�A�E�J�[���n���c���g���쐬�����{�l��p�̕���B�m�{�T�S�U�W�A���͂R�U�Q�T�`�R�V�X�O";
                    MagicMinValue = 3625;
                    MagicMaxValue = 3790;
                    BuffUpIntelligence = 5468;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Legendary;
                    equipablePerson = Equipable.Kahl;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.LEGENDARY_DARKMAGIC_DEVIL_EYE_REPLICA:
                    description = "���ڊ�ɑ���������ł̋[��̃��v���J�B�V�j�L�A�E�J�[���n���c���g�쐬�ɂ�镐��B�m�{�T�S�U�A���͂U�Q�T�`�V�X�O";
                    MagicMinValue = 625;
                    MagicMaxValue = 790;
                    BuffUpIntelligence = 546;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Legendary;
                    equipablePerson = Equipable.Kahl;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_DARKMAGIC_DEVIL_EYE_2:
                    description = "���ڊ�ɑ���������ł̋[��B�V�j�L�A�E�J�[���n���c���g���쐬�����{�l��p�̕���B�m�{�W�W�X�A���͂P�R�R�Q�`�P�W�U�Q";
                    MagicMinValue = 1332;
                    MagicMaxValue = 1862;
                    BuffUpIntelligence = 889;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Epic;
                    equipablePerson = Equipable.Kahl;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_YAMITUYUKUSA_MOON_ROBE:
                    description = "�ł��琶�܂ꂽ�I���̗t�͖��͂̍������h���Ă���ƌ����Ă���B���@�h��S�Q�O�`�T�T�O�A���ّϐ��A�U�f�ϐ��A�݉��ϐ��A�őϐ��A���U���{�P�Q���A�őϐ��{3000�A�Αϐ��{3000";
                    MagicMinValue = 420;
                    MagicMaxValue = 550;
                    ResistSilence = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistPoison = true;
                    AmplifyMagicAttack = 1.2F;
                    ResistShadow = 3000;
                    ResistFire = 3000;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_YAMITUYUKUSA_MOON_ROBE_2:
                    description = "�ł��琶�܂ꂽ�I���̗t�͖��͂̍������h���Ă���ƌ����Ă���B���@�h��P�U�Q�T�`�P�X�V�O�A���ّϐ��A�U�f�ϐ��A�݉��ϐ��A�őϐ��A���U���{�Q�O���A�őϐ��{50000�A�Αϐ��{50000";
                    MagicMinValue = 1625;
                    MagicMaxValue = 1970;
                    ResistSilence = true;
                    ResistTemptation = true;
                    ResistSlow = true;
                    ResistPoison = true;
                    AmplifyMagicAttack = 1.20F;
                    ResistShadow = 50000;
                    ResistFire = 50000;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING:
                    description = "�Ñ㌫�҃c���F���h�[�[���N����ɕt���Ă��������i�B�m�{�R�T�O�A�́{�Q�O�O�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�Α����̖��@�U�����q�b�g���閈�ɁA�ǉ����ʁy�΁z�_���[�W��^����B";
                    BuffUpIntelligence = 350;
                    BuffUpStamina = 200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING_2:
                    description = "�Ñ㌫�҃c���F���h�[�[���N����ɕt���Ă��������i�B�m�{�W�O�O�A�́{�T�O�O�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�Α����̖��@�U�����q�b�g���閈�ɁA�ǉ����ʁy�΁z�_���[�W��^����B";
                    BuffUpIntelligence = 800;
                    BuffUpStamina = 500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING:
                    description = "�Ñ㌫�҃c���F���h�[�[�̍ȃA�i�X�e���T���t���Ă��������i�B�m�{�R�T�O�A�́{�Q�O�O�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�Α����̖��@�U�����q�b�g���閈�ɁA���C�t���񕜂���B";
                    BuffUpIntelligence = 350;
                    BuffUpStamina = 200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2:
                    description = "�Ñ㌫�҃c���F���h�[�[�̍ȃA�i�X�e���T���t���Ă��������i�B�m�{�S�T�O�A�́{�W�T�O�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�Α����̖��@�U�����q�b�g���閈�ɁA���C�t���񕜂���B";
                    BuffUpIntelligence = 450;
                    BuffUpStamina = 850;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "��҃_���W�����P�K"

                case Database.POOR_HARD_SHOES:
                    description = "�d��ގ��ō��ꂽ�C�B�d���̂ŕ����h���E�E�E�B�̗́{�Q�A�S�{�P";
                    BuffUpStamina = 2;
                    BuffUpMind = 1;
                    cost = 280;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Poor;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SIMPLE_BRACELET:
                    description = "�P���ɍ��ꂽ�u���X���b�g�����A�C�͕͂����ė���B�́{�Q�A�S�{�R";
                    BuffUpStrength = 2;
                    BuffUpMind = 3;
                    cost = 500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SEAL_OF_POSION:
                    description = "�ł̌����f�[�^�����ߍ���ł���V�[���B�̗́{�Q�A�y�ғŁz�ϐ�";
                    BuffUpStamina = 2;
                    ResistPoison = true;
                    cost = 530;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_EGG_KAIGARA:
                    description = "�ΐF�̗�����́A�L�x�ȗ{�����̎悳���B";
                    cost = 420;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_HAYATE_ORB:
                    description = "�����̑��������߂��Ă�����B�퓬���g�p�ŁA��u���x���オ��B";
                    cost = 880;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_VIKING_SWORD:
                    description = "�W���I�ȑ匕�B�����d�������o�����X�͗ǂ����B�U���͂S�`�Q�Q";
                    minValue = 4;
                    maxValue = 22;
                    cost = 850;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_NEBARIITO_KUMO:
                    description = "�y�w偂��\�z���Ă����w偂̎��̂�����B���Ȃ�S�������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 640;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SUN_PRISM:
                    description = "���z�̃G�b�Z���X����������Ă�v���Y���B�́{�U�A�́{�U";
                    BuffUpStrength = 6;
                    BuffUpStamina = 6;
                    cost = 1200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_POISON_EKISU:
                    description = "��ŊJ���͂����ł̗v�f���������邱�Ƃ���s����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 860;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SOLID_CLAW:
                    description = "�s����蓖����₷�����d�������݊�̂悤�Ȓ܁B�U���͂P�P�`�P�Q";
                    minValue = 11;
                    maxValue = 12;
                    cost = 1100;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_LEEF_CHARM:
                    description = "�Ηt�̑@�ۂ��ގ��Ƃ��č��ꂽ�������B�m�{�W�A�S�{�S�A���͂Q�`�S";
                    BuffUpIntelligence = 8;
                    BuffUpMind = 4;
                    MagicMinValue = 2;
                    MagicMaxValue = 4;
                    cost = 1400;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_WARRIOR_MEDAL:
                    description = "��m�̑��������Ă��郁�_���B�́{�P�O�A�S�{�P�O";
                    BuffUpStrength = 10;
                    BuffUpMind = 10;
                    cost = 1800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PALADIN_MEDAL:
                    description = "�p���f�B���̑��������Ă��郁�_���B�́{�P�O�A�m�{�P�O";
                    BuffUpStrength = 10;
                    BuffUpIntelligence = 10;
                    cost = 1800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KASHI_ROD:
                    description = "�~�̃p���[���h���Ă����B���͂V�`�X";
                    MagicMinValue = 7;
                    MagicMaxValue = 9;
                    cost = 550;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_TOTAL_HIYAKU_KASSEI:
                    description = "�́E�Z�E�m�̂����A��Ԕ\�͂̍���������������������B��x�g���Ɩ����Ȃ�B�퓬����p�B";
                    cost = 6000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_PURE_WATER:
                    description = "�񑩂��ꂽ�񕜖�B�����P�x�������C�t��100%�񕜁B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 25000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_DREAM_POWDER:
                    description = "�l�X�ȉ\�����߂Ă���p�E�_�[�B�������A�����҂̘r����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1500;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_COPPER:
                    description = "���x�̍������̐΁B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1800;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ORANGE_MATERIAL:
                    description = "�I�����W�F�̃}�e���A���B����قǒ������͖����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1560;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ZEPHER_FETHER:
                    description = "�[�t�B�[���̉H�B�������ꂽ�����Ɛ��m������������B�Z�{�R�O";
                    buffUpAgility = 30;
                    cost = 3800;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_LIFE_SWORD:
                    description = "�����̑�����ꕔ�؂���A�g�ݍ��܂ꂽ���B�y����\�́F�L�z�U���͂P�U�`�Q�Q";
                    useSpecialAbility = true;
                    minValue = 16;
                    maxValue = 22;
                    cost = 1850;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_PURE_GREEN_SILK_ROBE:
                    description = "���x�̍����V���N�f�ނɗΐF�f�𒍓����Ă���B�h��͂V�`�P�O�B���@�h��͂Q�O�`�R�O";
                    minValue = 7;
                    maxValue = 10;
                    MagicMinValue = 20;
                    MagicMaxValue = 30;
                    cost = 2500;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                // �K���c���ŕ���F�P�K
                case Database.COMMON_BRONZE_SWORD:
                    description = "�����̌��B���ɈЗ͂Ɋ��҂͏o���Ȃ����A�ЂƂ܂��g���镐��ł���B�U���͂S�`�U";
                    minValue = 4;
                    maxValue = 6;
                    cost = 300;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_LIGHT_SHIELD:
                    description = "�N�ł����Ă�قǂ̌y�����B���Ƃ��čŒ���̔\�͂����������킹�ĂȂ��B�h��͂Q�`�R";
                    minValue = 2;
                    maxValue = 3;
                    cost = 350;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FINE_SWORD_1:
                    description = "���Ȃ��g���錕���K���c�����������B�U���͂T(+3)�`�W(+3)";
                    minValue = 8;
                    maxValue = 11;
                    cost = 900;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FINE_ARMOR_1:
                    description = "���Ȃ��g����Z���K���c�����������B�h��͂R(+2)�`�U(+2)";
                    minValue = 5;
                    maxValue = 8;
                    cost = 900;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FINE_SHIELD_1:
                    description = "���Ȃ��g���鏂���K���c�����������B�h��͂R(+1)�`�S(+2)";
                    minValue = 4;
                    maxValue = 5;
                    cost = 750;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_LIGHT_CLAW_1:
                    description = "���ʂ̌������ō쐬���ꂽ�܂��K���c�����������B�U���͂T(+3)�`�V(+3)";
                    minValue = 8;
                    maxValue = 10;
                    cost = 950;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                
                case Database.COMMON_KASHI_ROD_1:
                    description = "�~�̃p���[���h���Ă����B���͂V(+3)�`�X(+3)";
                    MagicMinValue = 10;
                    MagicMaxValue = 12;
                    cost = 1000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_LETHER_CLOTHING_1:
                    description = "�W���I�ȃT�C�Y�ō쐬���ꂽ���U�[���̈߂��K���c�����������B�h��͂S(+2)�`�V(+2)";
                    minValue = 6;
                    maxValue = 9;
                    cost = 980;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BASTARD_SWORD_1:
                    description = "���茕��p�B������x�̗͂��K�v�����A�K���c������ɂ��̈З͂����������B�U���͂V(+3)�`�S�O(+5)";
                    minValue = 10;
                    maxValue = 45;
                    cost = 1350;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_IRON_SWORD:
                    description = "�S���̌��B�K���c�Ӑg�̋Z�ŋ������Ǎς݁B�U���͂Q�O(+6�j�`�R�O(+6)";
                    minValue = 26;
                    maxValue = 36;
                    cost = 2300;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_FIT_ARMOR:
                    description = "�����҂̑̎��ɍ����悤�ɍ��ꂽ�Z�B�����₷�����A����قǖh��͂Ɋ��҂͂ł��Ȃ��B�h��͂Q�`�T";
                    minValue = 2;
                    maxValue = 5;
                    cost = 450;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_KUSARI_KATABIRA:
                    description = "����҂ݍ��킹�č쐬���ꂽ�Z�B�K���c���`�̋Z�ŋ������Ǎς݁B�h��͂P�S(+3)�`�P�W(+3)";
                    minValue = 17;
                    maxValue = 21;
                    cost = 2600;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                
                case Database.RARE_FLOWER_WAND:
                    description = "�Ԍ^�̎���肪�t���Ă��郏���h�B�K���c�����i�̃C���[�W�ɍ��킹���Ƃ̎��B�y����\�́F�L�z���͂Q�O�`�Q�S";
                    description += "\r\n�y����\�́z�@MP���񕜂���B";
                    UseSpecialAbility = true;
                    MagicMinValue = 20;
                    MagicMaxValue = 24;
                    cost = 3000;
                    AdditionalDescription(ItemType.Weapon_Rod);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SURVIVAL_CLAW:
                    description = "�C���̍r���l�i�H�j�ł������g����悤�ɉ��ǂ��{����Ă���܁B�U���͂P�U�`�P�X";
                    minValue = 16;
                    maxValue = 19;
                    cost = 1600;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SUPERIOR_CROSS:
                    description = "���U�[���̈߂̒��ł��㎿�ȑf�ނ�I�肵�āA���ꂽ�����߁B�h��͂W�`�P�O";
                    minValue = 8;
                    maxValue = 10;
                    cost = 1200;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SILK_ROBE:
                    description = "���ʂ̃V���N�f�ނ������������@�̃��[�u�B�h��͂S�`�W�B���@�h��͂P�O�`�P�T";
                    minValue = 4;
                    maxValue = 8;
                    MagicMinValue = 10;
                    MagicMaxValue = 15;
                    cost = 1300;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BLACER_OF_SYOJIN:
                    description = "�A�C���ւ̐��i�̔O�����߂č쐬���ꂽ�u���X���b�g�B�́{�P�O�A�́{�Q�O�A�S�{�P�O";
                    buffUpStrength = 10;
                    buffUpAgility = 20;
                    buffUpMind = 10;
                    cost = 4000;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Ein;
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ZIAI_PENDANT:
                    description = "���i�ւ̎����̔O�����߂č쐬���ꂽ�y���_���g�B�m�{�P�O�A�́{�P�O�A�S�{�Q�O";
                    buffUpIntelligence = 10;
                    buffUpStamina = 10;
                    buffUpMind = 20;
                    cost = 4000;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Lana;
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �h��(Common1)
                case Database.COMMON_SMART_CLOTHING:
                    description = "���S�n���ǂ��A�����₷�������Q�̕����߁B�h��͂Q�T�`�Q�W�B";
                    minValue = 25;
                    maxValue = 28;
                    cost = 4600;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_ROBE:
                    description = "�X�����Ƃ����f�U�C����ǋ������퓬�������[�u�B�h��͂P�O�`�P�Q�B���@�h��Q�O�`�Q�Q";
                    minValue = 10;
                    maxValue = 12;
                    MagicMinValue = 20;
                    MagicMaxValue = 22;
                    cost = 5500;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SMART_PLATE:
                    description = "�K�b�`�������Z�ɂ��ւ�炸�A�ς킵���������B�h��͂R�O�`�R�T";
                    minValue = 30;
                    maxValue = 35;
                    cost = 5200;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �h��iRare1�j
                case Database.RARE_DIRGE_ROBE:
                    description = "�d�X������w�������A�m���Ȗ��@�ϐ�����������B�h��͂Q�O�`�Q�O�A���@�h��S�O�`�S�O�A���ϐ��P�O�O�A�őϐ��P�O�O";
                    minValue = 20;
                    maxValue = 20;
                    MagicMinValue = 40;
                    MagicMaxValue = 40;
                    ResistLight = 100;
                    ResistShadow = 100;
                    cost = 12000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DUNSID_PLATE:
                    description = "�J�ő�l�C�������A�[���E�_���V�b�h���������Ă����Z�B�h��͂T�P�`�T�W�A�Αϐ��P�O�O�A���ϐ��P�O�O";
                    minValue = 51;
                    maxValue = 58;
                    ResistFire = 100;
                    ResistIce = 100;
                    cost = 12000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �h��iCommon2�j
                case Database.COMMON_SERPENT_ARMOR:
                    description = "�T�[�y���g�����悭���p���Ă���Z�B�h��͂S�Q�`�S�X";
                    minValue = 42;
                    maxValue = 49;
                    cost = 7000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SWIFT_CROSS:
                    description = "���ɏu���͂��o���₷�������߁B�h��͂R�W�`�S�Q";
                    minValue = 38;
                    maxValue = 32;
                    cost = 6500;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_CHIFFON_ROBE:
                    description = "�t���t���Ƃ������G�Ŗ��@�ϐ����������Ă���߁B�h��͂P�T�`�P�W�A���@�h��Q�T�`�R�O";
                    minValue = 15;
                    maxValue = 18;
                    MagicMinValue = 25;
                    MagicMaxValue = 30;
                    cost = 7000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �h��iRare2�j
                case Database.RARE_SHARKSKIN_ARMOR:
                    description = "�L�̗؂��V�����`�󉻂��A�Z�`��ƂȂ����B�h��͂U�U�`�V�T�A���ϐ��Q�T�O�A�Αϐ��Q�T�O";
                    minValue = 66;
                    maxValue = 75;
                    ResistLight = 250;
                    ResistFire = 250;
                    cost = 13000;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BLACK_MAGICIAN_CROSS:
                    description = "���@�̎g����O�q�Ő키����O��Ƃ��Đ��ݏo���ꂽ�����߁B�h��͂T�Q�`�T�W�A���@�h��T�Q�`�T�W�A�őϐ��Q�T�O�A�Αϐ��Q�T�O";
                    minValue = 52;
                    maxValue = 58;
                    MagicMinValue = 52;
                    MagicMaxValue = 58;
                    ResistShadow = 250;
                    ResistFire = 250;
                    cost = 12000;
                    AdditionalDescription(ItemType.Armor_Middle);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_RED_THUNDER_ROBE:
                    description = "�Ԃ����̕��l���`����Ă��郍�[�u�B�h��͂Q�T�`�R�O�A���@�h��U�O�`�V�T�A���ϐ��Q�T�O�A�Αϐ��Q�T�O";
                    minValue = 46;
                    maxValue = 51;
                    ResistShadow = 250;
                    ResistFire = 250;
                    cost = 13500;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �h��i�K���c�����j2�K
                case Database.COMMON_BERSERKER_PLATE:
                    description = "����m�o�[�T�[�J�[�̋C�����`����Ă���Z�B�h��͂V�P�`�W�Q�A���@�h��U�O�`�V�T�A���ϐ��Q�T�O�A�Αϐ��Q�T�O";
                    minValue = 71;
                    maxValue = 82;
                    cost = 14500;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRIGHTNESS_ROBE:
                    description = "���̋P�����h�点���������ꂽ���[�u�B�h��͂R�U�`�S�Q�A���@�h��W�O�`�X�O";
                    minValue = 36;
                    maxValue = 42;
                    MagicMinValue = 80;
                    MagicMaxValue = 90;
                    cost = 15000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // �I���E�����f�B�X��������
                case Database.COMMON_AURA_ARMOR:
                    description = "�I�[�����܂Ƃ��Ă���Z�B�����f�B�X�{�l�̉e���ɂ����́B�h��͂U�W�`�V�U�A���ϐ��Q�O�O";
                    minValue = 68;
                    maxValue = 76;
                    ResistLight = 200;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    equipablePerson = Equipable.Ol;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_AURA_ARMOR_OMEGA:
                    description = "�i���Ȃ�I�[����тт��Z�B�����f�B�X�{�l�̐��_�g�����`��葱���Ă���B�h��͂V�Q�O�`�W�U�O�A���ϐ��{16000�A�Αϐ��{16000";
                    minValue = 720;
                    maxValue = 860;
                    ResistLight = 16000;
                    ResistFire = 16000;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Epic;
                    equipablePerson = Equipable.Ol;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                // ���iCommon1)
                case Database.COMMON_SMART_SHIELD:
                    description = "�����₷���A�������X�b�ƕς����鏂�B�h��͂P�Q�`�P�T";
                    minValue = 12;
                    maxValue = 15;
                    cost = 3500;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    break;
                // ���iCommon2)
                case Database.COMMON_PURE_BRONZE_SHIELD:
                    description = "���x�̂�����Ő��ݏo���ꂽ���B�h��͂P�W�`�Q�S";
                    minValue = 18;
                    maxValue = 24;
                    cost = 4800;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Common;
                    break;
                // ���iRare1)
                case Database.RARE_BLUE_SKY_SHIELD:
                    description = "���L��ȊC���`����Ă��鏂�B�h��͂Q�T�`�R�R�A���ϐ��R�O�O�A���ϐ��R�O�O";
                    minValue = 25;
                    maxValue = 33;
                    ResistLight = 300;
                    ResistIce = 300;
                    cost = 9500;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    break;
                // ���i�K���c�����j�Q�K
                case Database.RARE_STRONG_SERPENT_SHIELD: // �������̐ΊD�A�L�̗�
                    description = "���łȐL�̗؂��X�ɍ����������A�ቷ�x���Ōł߂����B�h��͂R�W�`�S�O";
                    minValue = 38;
                    maxValue = 40;
                    cost = 11000;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Rare;
                    break;
                #endregion
                #region "���̑��A���g�p�A�Ȃɂ��Ȃ��Aetc..."
                case "": // ��������
                    description = "";
                    minValue = 0;
                    maxValue = 0;
                    buffUpStrength = 0;
                    buffUpAgility = 0;
                    buffUpIntelligence = 0;
                    buffUpStamina = 0;
                    buffUpMind = 0;
                    amplifyPhysicalAttack = 0.0f;
                    amplifyPhysicalDefense = 0.0f;
                    amplifyMagicAttack = 0.0f;
                    amplifyMagicDefense = 0.0f;
                    amplifyBattleSpeed = 0.0f;
                    amplifyBattleResponse = 0.0f;
                    amplifyPotential = 0.0f;
                    ResistFire = 0;
                    ResistIce = 0;
                    ResistLight = 0;
                    ResistShadow = 0;
                    ResistForce = 0;
                    ResistWill = 0;
                    cost = 0;
                    //AdditionalDescription(ItemType.Useless);
                    rareLevel = RareLevel.Poor;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;

                case "�A�J�V�W�A�̎�":
                    description = "���낵���s�������A�H�ׂ�΋C�t�����ʂ�����B�퓬����p�B�u��Łv�u�X�^���v������";
                    minValue = 100;
                    maxValue = 100;
                    cost = 150;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = USING_ITEM_STACK_SIZE;
                    break;

                #endregion                   
                #region "�Œ�h���b�v�A�C�e��"
                #region "�P�K"
                case Database.POOR_BLACK_MATERIAL: // �h���b�v�A�C�e���i�P�K�C�Ӂj
                    description = "�����F�̗����́B�g�p�ς݃}�e���A���̂��߁A�g�����͂Ȃ��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 20;
                    AdditionalDescription(ItemType.Useless);
                    rareLevel = RareLevel.Poor;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_YELLOW_MATERIAL:
                    description = "�����F�̗����́B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 6000;
                    AdditionalDescription(ItemType.None);
                    rareLevel = RareLevel.Common;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WARM_NO_KOUKAKU: // �h���b�v�A�C�e���i�b�k���[���j
                    description = "���[�ƂȂ������[���̍b�k�̌��ЁB";
                    minValue = 0;
                    maxValue = 0;
                    cost = 60;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BEATLE_TOGATTA_TUNO: // �h���b�v�A�C�e���i�Ў�ȃr�[�g���j
                    description = "���[�ƂȂ����r�[�g���̊p�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 72;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREEN_SIKISO: // �h���b�v�A�C�e���i�O���[���E�`���C���h�j
                    description = "���؂̕\�ʂɂق�̂�c���Ă����ΐF�f�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 80;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_MANDORAGORA_ROOT: // �h���b�v�A�C�e���i�}���h���S���j
                    description = "�}���h���S�����ɍۂ̍��ɂ́A���͂��h�錾���Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 250;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SUN_LEAF: // �h���b�v�A�C�e���i�T���E�t�����[�j
                    description = "�y����f�ށz���z�̉��b���󂯂��ɐl�H�I�Ȍ��ň�����t�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 90;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_INAGO: // �h���b�v�A�C�e���i���b�h�z�b�p�[�j
                    description = "��Ȍ`���������[�B�ώςɂ���Ə�肢�������x�����K�v�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 110;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SPIDER_SILK: // �h���b�v�A�C�e���i�A�[�X�p�C�_�[�j
                    description = "�w偂��U���̍ۂɎT���U�炵���ǎ��Ȍ`��̎��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 120;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ALRAUNE_KAHUN: // �h���b�v�A�C�e���i�A�����E�l�j
                    description = "�A�����E�l����̎悳���ԕ��͛Z��̌��ƂȂ�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 300;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MARY_KISS: // �h���b�v�A�C�e���i�|�C�Y���E�}���[�j
                    description = "�Ō�ɓ�����ꂽ�E�q�B�L�X�}�[�N�̌`�����Ă�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1060;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RABBIT_KEGAWA: // �h���b�v�A�C�e���i�G�H�E�T�M�j
                    description = "�_��̂���E�T�M�̖є�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 150;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RABBIT_MEAT: // �h���b�v�A�C�e���i�G�H�E�T�M�j
                    description = "�G�H�ň�����E�T�M�̓��B�ςĂ���Ă��ĐH�ׂ�Ɣ��������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 160;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_TAKA_FETHER: // �h���b�v�A�C�e���i�r�q�ȑ�j
                    description = "��̉H�ɂ́A��̐��_���h��Ƃ����Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 172;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PLANTNOID_SEED: // �h���b�v�A�C�e���i�����_�[�V�[�h�j
                    description = "�̓����莞�ɕ��ꗎ���Ă����v�����g�m�C�h��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 350;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_TOGE_HAETA_SYOKUSYU: // �h���b�v�A�C�e���i�t�����V�X�i�C�g�j
                    description = "�U���p�̐G��Ƃ��Ĉُ픭�B�����G��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 370;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_HYUI_SEED: // �h���b�v�A�C�e���i�V���b�g�K���E�q���[�C�j
                    description = "�΂�T���ꂽ��e�ۂɕ���Ă�����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1220;
                    AdditionalDescription(ItemType.Material_Potion); description = description.Insert(0, Database.DESCRIPTION_POTION_MATERIAL);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_OOKAMI_FANG: // �h���b�v�A�C�e���i�r�q�ȑ�j
                    description = "�ԘT�̉�́A���ɂ��H���������Ă��������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 210;
                    AdditionalDescription(ItemType.Material_Equip); description = description.Insert(0, Database.DESCRIPTION_EQUIP_MATERIAL);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BRILLIANT_RINPUN: // �h���b�v�A�C�e���i�u�����A���g�E�o�^�t���C�j
                    description = "�o�^�t���C���̒���A��ۋP���������̗ӕ����̎�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 222;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RED_HOUSI: // �h���b�v�A�C�e���i�u���b�h���X�j
                    description = "�E�q�U���̍ۂɕ��o���ꂽ�E�q�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 450;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MOSSGREEN_EKISU: // �h���b�v�A�C�e���i���X�O���[���E�_�f�B�j
                    description = "���̓��L�G�L�X�͓���ȑϐ���^����ƌ����Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1310;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_ORB_GROW_GREEN: // �h���b�v�A�C�e���i��K�̎��ҁF���݂��t�����V�X�j
                    description = "�V�΂̑�����������ꂽ���B�ő�X�L���{�Q�O�A�ړ����X�L���񕜁A�퓬���X�L���񕜁{�R�B";
                    effectValue1 = 20;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                #endregion
                #region "�Q�K"
                case Database.POOR_BLACK_MATERIAL2: // �h���b�v�A�C�e���i�Q�K�C�Ӂj
                    description = "�����F�̗����́B�኱�̉��ǂ����݂��オ���邪�A�g�����͂Ȃ��B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 900;
                    AdditionalDescription(ItemType.Useless);
                    rareLevel = RareLevel.Poor;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_DAGGERFISH_UROKO: // �h���b�v�A�C�e���i�_�K�[�t�B�b�V���j
                    description = "�勛�̗؂́A���������������̂���d�����E���̈�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 242;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SIPPUU_HIRE: // �h���b�v�A�C�e���i�����E�t���C���O�t�B�b�V���j
                    description = "�����̃q���́A�_�炩���ƍ��΂������E���̈�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 254;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_WHITE_MAGATAMA: // �h���b�v�A�C�e���i�I�[�u�E�V�F���t�B�b�V���j
                    description = "���̔����́A���f�ł͂��邪�A�i�i�̂���`�����Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 264;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_MAGATAMA: // �h���b�v�A�C�e���i�I�[�u�E�V�F���t�B�b�V���j
                    description = "���̐��́A�ڗ����Ȃ����A���M�ȕ��͋C���o���Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 264;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KURIONE_ZOUMOTU: // �h���b�v�A�C�e���i�X�v���b�V���E�N���I�l�j
                    description = "�����̒��ł����ɑN�x�̍���������؂�o���Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 512;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_BLUEWHITE_SHARP_TOGE: // �h���b�v�A�C�e���i���\�ȃV�[�E�A�[�`���j
                    description = "�퓬���ɃA�[�`������΂��Ă����A�s�����̐j�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 310;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_RENEW_AKAMI: // �h���b�v�A�C�e���i���[�����O�E�}�O���j
                    description = "�����̗ǂ��}�O���̐Ԑg�B�����O���ł͔����No.1�@���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 334;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SEA_WASI_KUTIBASI: // �h���b�v�A�C�e���i�C�h�j
                    description = "�ُ퐬�������C�h�̂����΂��A�����ŏĂ��ƍ��΂�����������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 366;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_WASI_BLUE_FEATHER: // �h���b�v�A�C�e���i�C�h�j
                    description = "�ُ퐬�������C�h�̐��H�B�K�^���ĂԂƌ����Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 370;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GANGAME_EGG: // �h���b�v�A�C�e���i��T�j
                    description = "�ُ�ȑ傫���̗��B���̂܂܂ł͐H�ׂ��Ȃ��B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 724;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_JOE_TONGUE: // �h���b�v�A�C�e���i�r�b�O�}�E�X�E�W���[�j
                    description = "�����B�d���B�S�c���B�����̘r�������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 2628;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_JOE_ARM: // �h���b�v�A�C�e���i�r�b�O�}�E�X�E�W���[�j
                    description = "�^�Ƃ͎v���Ȃ����炢�̑傫���r�B���i���͕���f�ނƂ��Ďg�������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 2722;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_JOE_LEG: // �h���b�v�A�C�e���i�r�b�O�}�E�X�E�W���[�j
                    description = "���߂Č���҂́A���ꂪ�^�̑����Ƃ͎v�킸�A�����������ɐH�ׂ�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 2812;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SOFT_BIG_HIRE: // �h���b�v�A�C�e���i���[�O���E�}���^�j
                    description = "�ɔ��̃q���B�R���R���������G�ŁA���������\���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 522;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PURE_WHITE_BIGEYE: // �h���b�v�A�C�e���i���V����S�[���h�t�B�b�V���j
                    description = "�����̖ڋʂ̂��߁A�t�ɐH���̍ۂ͋��낵����ۂ��󂯂�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 588;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_HALF_TRANSPARENT_ROCK_ASH: // �h���b�v�A�C�e���i�o�j�b�V���O�E�R�[�����j
                    description = "�ΊD�͖{������̐F���t���Ă邪�A����͕s�����ł��菃�x���Ⴂ�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 622;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GOTUGOTU_KARA: // �h���b�v�A�C�e���i��q���E�n�[�~�b�g�N���u�j
                    description = "������Ƃ₻���Ƃ̃p���`�E�L�b�N�ł͉��Ȃ��k�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1250;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_SEKIKASSYOKU_HASAMI: // �h���b�v�A�C�e���i�L���V�[�E�U�E�L�����T�[�j
                    description = "�L���V�[�̃n�T�~�́A�ʏ�̃n�T�~�Ɣ�ׂČ`�󂪈ُ픭�B���Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 4200;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_KOUSITUKA_MATERIAL: // �h���b�v�A�C�e���i�u���b�N�E�X�^�[�t�B�b�V���j
                    description = "�u���b�N�E�X�^�[�t�B�b�V���͎��S��A�d�����������������ω�����B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 820;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_NANAIRO_SYOKUSYU: // �h���b�v�A�C�e���i���C���{�[�E�A�l���l�j
                    description = "�J���t���ȐG��̂��߁A�������̋�������㩂Ɉ���������ƌ����Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 890;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_AOSAME_KENSHI: // �h���b�v�A�C�e���i�G�b�W�h�E�n�C�E�V���[�N�j
                    description = "���x�������A�`����Y��Ȍ����B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1700;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_AOSAME_UROKO: // �h���b�v�A�C�e���i�G�b�W�h�E�n�C�E�V���[�N�j
                    description = "���ʂɐG��Ə_�炩�����A�ΏՌ����ɗD��Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 1800;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_EIGHTEIGHT_KUROSUMI: // �h���b�v�A�C�e���i�G�C�g�E�G�C�g�j
                    description = "�����F�̖n�B�����S�����������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 5100;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_EIGHTEIGHT_KYUUBAN: // �h���b�v�A�C�e���i�G�C�g�E�G�C�g�j
                    description = "�l�X�Ȍ`��������z�ՁB�ׂ�������ŏĂ��Ƒ�ϔ��������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 5200;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_ORB_GROUNDSEA_STAR: // �h���b�v�A�C�e���i��K�̎��ҁF��C�փ����B�A�T���j
                    description = "�����B�A�T�����j�̍ہA���Ƃ��ꂽ���B�H�ɃX�y���r�����Q�񔭐�����B���h���{�P�O���A���ϐ�2500�A���ϐ�2500�A���ّϐ��A�����ϐ�";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    amplifyMagicDefense = 1.1f;
                    ResistLight = 2500;
                    ResistIce = 2500;
                    ResistSilence = true;
                    ResistFrozen = true;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�R�K"
                case Database.POOR_BLACK_MATERIAL3: // �h���b�v�A�C�e���i�R�K�C�Ӂj
                    description = "�����F�̗����́B�}�e���A�����k�����݂����A�c�[�̂܂܂ł���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 9500;
                    AdditionalDescription(ItemType.Useless);
                    rareLevel = RareLevel.Poor;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ORC_MOMONIKU: // �h���b�v�A�C�e���i�ːi�I�[�N�j
                    description = "���񂪂�Ă��グ���������A��]�̂��閡�킢�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 10500;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SNOW_CAT_KEGAWA: // �h���b�v�A�C�e���i�X�m�[�L���b�g�j
                    description = "�㎿�Ȑ�L�̖є�B��������邪���p���͕���E�l�ɘr����B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 11200;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BIG_HIZUME: // �h���b�v�A�C�e���i�E�H�[�E�}�����X�j
                    description = "�}�����X�̑��Ղ����āA���̒���H���ƍl����l�͐����Ȃ��B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 12600;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FAIRY_POWDER: // �h���b�v�A�C�e���i�E�B���O�h�E�R�[���h�t�F�A���[�j
                    description = "�d������̎悳���p�E�_�[�́A�m�͊����̌��ƂȂ�B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 28000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_GOTUGOTU_KONBOU: // �h���b�v�A�C�e���i�u���[�^���E�I�[�K�j
                    description = "�ł������Ă��̂܂܂ł͎g�����ɂȂ�Ȃ��A�f�ގ��̂͏�v�ȕ��B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 14100;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_LIZARD_UROKO: // �h���b�v�A�C�e���i�n�C�h���[�E���U�[�h�j
                    description = "���̎��R�F�Ō`�������؂́A���ʂɂ��g����ꍇ������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 15400;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_EMBLEM_OF_PENGUIN: // �h���b�v�A�C�e���i�y���M���X�^�[�j
                    description = "�y���M���ŋ�����w���҂��e�n�̃y���M���ɔz�z���Ă�炵���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 16600;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SHARPNESS_TIGER_TOOTH: // �h���b�v�A�C�e���i�����Ձj
                    description = "�����Ղ̉傩��͋��x���G�L�X���E�o�\�ł���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 36000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BEAR_CLAW_KAKERA: // �h���b�v�A�C�e���i�t�F���V�A�X�E���C�W�x�A�j
                    description = "���{���������x�A���������܂����܁B�؂���͐Ԃ����Ő��܂��Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 110000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_TOUMEI_SNOW_CRYSTAL: // �h���b�v�A�C�e���i�E�B���^�[�E�I�[�u�j
                    description = "�ጋ���Ƃ��Č`�����ꂽ�E�B���^�[�E�I�[�u�̌��ЁB���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 21000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_WHITE_AZARASHI_MEAT: // �h���b�v�A�C�e���i�Ǐ]�����A�U���V�j
                    description = "�Ɋ��̒n�Ŏ�ꂽ���͐g�����܂��Ă���A��ώ�������������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 23600;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ARGONIAN_PURPLE_UROKO: // �h���b�v�A�C�e���i�m�I�ȃA���S�j�A���j
                    description = "�A���S�j�A�����甍��������؂͌���̂��鎇�F�����Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 45500;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BLUE_DANGAN_KAKERA: // �h���b�v�A�C�e���i�����e�ۂ̌��Ёj
                    description = "���@�������X�Ɍ����������Ă����e�ۂ̌��ЁB���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 68000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_PURE_CRYSTAL: // �h���b�v�A�C�e���i�s���A�E�u���U�[�h�E�N���X�^���j
                    description = "�����̃N���X�^���A�󏭉��l�������A�g���[�h�ޗ��Ɏg����B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 175000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_WOLF_KEGAWA: // �h���b�v�A�C�e���i���ځE�E�F�A�E���t�j
                    description = "���킲��Ƃ����E���t�̖є�B�����Ƃ��Ƃ����ĂĐG��ƒɂ��B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 26000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_FROZEN_HEART: // �h���b�v�A�C�e���i�t���X�g�E�n�[�g�j
                    description = "���@�������������������̐S�����B�ۓ����������Ă��������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 28200;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_ESSENCE_OF_WIND: // �h���b�v�A�C�e���i�E�B���h�u���C�J�[�j
                    description = "���̃}�e���A�������f�ށB����E�l�̗͗ʂ������B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 59000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_TUNDRA_DEER_HORN: // �h���b�v�A�C�e���i�c���h���E�����O�z�[���E�f�B�A�j
                    description = "�_�̎g���Ə̂���鎭�̈̑�Ȃ�p�A�c��Ȗ��͂����߂��Ă���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 210000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_ORB_SILENT_COLD_ICE: // �h���b�v�A�C�e���i�O�K�̎��ҁF����n�E�����O�V�[�U�[�j
                    // �U���q�b�g���A�H�ɑΏۂ𓀌������A�{���ʂ�BUFF��S�ď����B�����U���P�O��UP�A���@�U���P�O��UP�y�펞�z
                    description = "�n�E�����O�V�[�U�[���j�̍ہA���Ƃ��ꂽ���B�����@�R�X�g�R�O���y���A�����@�R�O������";
                    description += "\r\n�y����\�́z�@�U���q�b�g���A�H�ɑΏۂ𓀌������A�{���ʂ�BUFF��S�ď����B";
                    minValue = 0;
                    maxValue = 0;
                    manaCostReductionIce = 0.3f;
                    amplifyIce = 1.30f;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�S�K"
                case Database.POOR_BLACK_MATERIAL4: // �h���b�v�A�C�e���i�S�K�C�Ӂj
                    description = "�����F�̗����́B�f���ω������݂����A�s�ς̂܂܂ł���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 78000;
                    AdditionalDescription(ItemType.Useless);
                    rareLevel = RareLevel.Poor;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_HUNTER_SEVEN_TOOL: // �h���b�v�A�C�e���i���Ãn���^�[�j
                    description = "�n���^�[�B�����N���p���Ă����A�C�e���ނ́A�틵��L���ȏ󋵂ւƓ����Ă����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 127000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_BEAST_KEGAWA: // �h���b�v�A�C�e���i�r�[�X�g�}�X�^�[�j
                    description = "�r�[�X�g�}�X�^�[�����L���Ă����є�B�e�͂ƍ����������˔����Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 131000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BLOOD_DAGGER_KAKERA: // �h���b�v�A�C�e���i�G���_�[�A�T�V���j
                    description = "�_�K�[�ɕt�����Ă��錌�t�́A�ǂ̏b�̌������ɂ킩��Ȃ��B���t�������L�b�`���Ӓ肷��΁A�����Ɏg���������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 138000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SABI_BUGU: // �h���b�v�A�C�e���i�t�H�[�����V�[�J�[�j
                    description = "�����鋁���҂́A����̃����e�i���X��S���s���ĂȂ��B�b�艮�������e�i���X����Ό��̏�Ԃɖ߂��������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 188000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_ESSENCE_OF_DARK: // �h���b�v�A�C�e���i�}�X�^�[���[�h�j
                    description = "�ł̃}�e���A�������f�ށB����E�l�̗͗ʂ������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 179000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_EXECUTIONER_ROBE: // �h���b�v�A�C�e���i�G�O�[�L���[�W���i�[�j
                    description = "���s�l�̃��[�u�ɂ͎􂢂̔O�����߂��Ă���A�ʏ�̐l�Ԃɂ͈����Ȃ��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 191000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_SEEKER_HEAD: // �h���b�v�A�C�e���i�ł��ő��j
                    description = "�����҂̖����p�B�ł��ő��͋����҂̐��ݔ\�͂��z�����A�����ƂƂ��ė͂𓾂Ă����B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 216000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MASTERBLADE_KAKERA: // �h���b�v�A�C�e���i�����}�X�^�[�u���C�h�j
                    description = "�}�X�^�[�u���C�h�̈З͂͏h��F�ɂ��ω�����ƌ����Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 263000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_MASTERBLADE_FIRE: // �h���b�v�A�C�e���i�����}�X�^�[�u���C�h�j
                    description = "�}�X�^�[�u���C�h�ɏh�点��΁B�ЁX�����͏��������Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 273000;
                    AdditionalDescription(ItemType.Material_Food);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_GREAT_JEWELCROWN: // �h���b�v�A�C�e���i�V���E�U�E�_�[�N�G���t�j
                    description = "�h��ȑ����ɂ�����т₩�Ɍ����Ă���B��Αf�ނ̂ݎg��������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 450000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ESSENCE_OF_SHINE: // �h���b�v�A�C�e���i�T���E�X�g���C�_�[�j
                    description = "���̃}�e���A�������f�ށB�|�[�V���������X�L���̗͗ʂ������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 360000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DEMON_HORN: // �h���b�v�A�C�e���i�A�[�N�f�[�����j
                    description = "�����̊p�ɂ͙�����߂�\�͂����߂��Ă���B���̔\�͂͊p�̑@�ۂ̉��[���ɉB����Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 370000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KUMITATE_TENBIN: // �h���b�v�A�C�e���i�V�����i��ҁj
                    description = "���@�����̓V����蓾��ꂽ�f�ށB���̂܂܂ł͎g���Ȃ����E�E�E";
                    minValue = 0;
                    maxValue = 0;
                    cost = 380000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KUMITATE_TENBIN_DOU: // �h���b�v�A�C�e���i�V�����i��ҁj
                    description = "���@�����̓V����蓾��ꂽ�f�ށB���̂܂܂ł͎g���Ȃ����E�E�E";
                    minValue = 0;
                    maxValue = 0;
                    cost = 380000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_KUMITATE_TENBIN_BOU: // �h���b�v�A�C�e���i�V�����i��ҁj
                    description = "���@�����̓V����蓾��ꂽ�f�ށB���̂܂܂ł͎g���Ȃ����E�E�E";
                    minValue = 0;
                    maxValue = 0;
                    cost = 380000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ESSENCE_OF_FLAME: // �h���b�v�A�C�e���i�ƁE�t���C���X���b�V���[�j
                    description = "�΂̃}�e���A�������f�ށB����E�l�̗͗ʂ������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 385000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_BLACK_SEAL_IMPRESSION: // �h���b�v�A�C�e���i�f�r���E�`���h�����j
                    description = "���F�Ƃ͕�����Ȃ��قǁA�ЁX�����܂łɃh�X������ӁB�����t�̂̓G�L�X�Ƃ��Ďg���������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 520000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ONRYOU_HAKO: // �h���b�v�A�C�e���i�n�E�����O�z���[�j
                    description = "������яo�Ă��邩������Ȃ��E�E�E������͋���ȋ��|���`����Ă��邽�߁A�̂̂�������b��E�l�ɂ����J�����Ƃ͏o���Ȃ��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 475000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ANGEL_SILK: // �h���b�v�A�C�e���i�y�C���G���W�F���j
                    description = "�����������ȃV���N�f�ށB�G���Ă��銴�G��������Ȃ��قǌy���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 490000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Common;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_CHAOS_SIZUKU: // �h���b�v�A�C�e���i�J�I�X���[�f��)
                    description = "�ǂ�قǂ̐l�i�҂ł������Ƃ��Ă��A����ɐG�ꂽ�r�[�A�J�I�X�����ɗ��Ƃ����ރG�L�X�������ɋÌŉ����ē����Ă���B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 560000;
                    AdditionalDescription(ItemType.Material_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DOOMBRINGER_TUKA: // �h���b�v�A�C�e���i�h�D�[���u�����K�[�j
                    description = "�ł�ł���h�D�[���u�����K�[�̕��B�s�v�c�ƈ���ꂽ�Ղ��Ȃ��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 666666;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_DOOMBRINGER_KAKERA: // �h���b�v�A�C�e���i�h�D�[���u�����K�[�j
                    description = "�ł�ł���h�D�[���u�����K�[�̌��ЁB�s�v�c�ƌ��t�͕t�����ĂȂ��B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 666666;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_JOUKA_TANZOU: // ��
                    description = "�b���̒��́A�����F�Ɍ���������i�ɑ��݂��Ă���B������Ă��s�v�c�ƑS���M���͂Ȃ��B";
                    cost = 650000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_ESSENCE_OF_ADAMANTINE: // ��
                    description = "�Y��ȋ���̍d�f�ށB�J�X������t���Ă��炸�A���̔��˂����Ă���Ɩ������Ă��܂��������B";
                    cost = 750000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_ORB_DESTRUCT_FIRE: // �h���b�v�A�C�e���i�ŉ����M�B���A�[�[�j
                    description = "���M�B���A�[�[���j�̍ہA���Ƃ��ꂽ���B�N���e�B�J���_���[�W�ʂ��X�ɋ�������B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�T�K"
                case Database.POOR_BLACK_MATERIAL5: // �h���b�v�A�C�e��
                    description = "�����F�̗����́B���f�ւ̊Ҍ������݂����A�Ҍ��͂���Ȃ��܂܁B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 8400;
                    AdditionalDescription(ItemType.Useless);
                    rareLevel = RareLevel.Poor;
                    limitValue = OTHER_ITEM_STACK_SIZE;
                    break;

                case "�n�[�g�E�I�u�E�t�F�j�b�N�X": // �h���b�v�A�C�e���iPhoenix�j
                    description = "�`���̐���Phoenix�̐S���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 110000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case "�n�[�g�E�I�u�E�h���S��": // �h���b�v�A�C�e���iEmerald Dragon�j
                    description = "�`���̐���Emerald Dragon�̐S���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 120000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case "�n�[�g�E�I�u�E�����X�^�[": // �h���b�v�A�C�e���iNine Tail�j
                    description = "�`���̐���Nine Tail�̐S���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 130000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                case "�n�[�g�E�I�u�E�W���b�W": // �h���b�v�A�C�e���iJudgement�j
                    description = "�`���̐���Judgement�̐S���B���p��p�i�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 140000;
                    AdditionalDescription(ItemType.Material_Equip);
                    rareLevel = RareLevel.Rare;
                    limitValue = MATERIAL_ITEM_STACK_SIZE;
                    break;
                #endregion
                #endregion
                #region "�K�w�ʔ�ˑ��h���b�v�A�@�������L�b�h�V���[�Y"
                case Database.GROWTH_LIQUID_STRENGTH:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̓p�����^���P�`�R�t�o����B";
                    minValue = 1;
                    maxValue = 3;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID_AGILITY:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�Z�p�����^���P�`�R�t�o����B";
                    minValue = 1;
                    maxValue = 3;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID_INTELLIGENCE:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�m�p�����^���P�`�R�t�o����B";
                    minValue = 1;
                    maxValue = 3;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID_STAMINA:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̃p�����^���P�`�R�t�o����B";
                    minValue = 1;
                    maxValue = 3;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID_MIND:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�S�p�����^���P�`�R�t�o����B";
                    minValue = 1;
                    maxValue = 3;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.GROWTH_LIQUID2_STRENGTH:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̓p�����^���T�`�P�O�t�o����B";
                    minValue = 5;
                    maxValue = 10;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID2_AGILITY:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�Z�p�����^���T�`�P�O�t�o����B";
                    minValue = 5;
                    maxValue = 10;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID2_INTELLIGENCE:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�m�p�����^���T�`�P�O�t�o����B";
                    minValue = 5;
                    maxValue = 10;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID2_STAMINA:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̃p�����^���T�`�P�O�t�o����B";
                    minValue = 5;
                    maxValue = 10;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID2_MIND:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�S�p�����^���T�`�P�O�t�o����B";
                    minValue = 5;
                    maxValue = 10;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.GROWTH_LIQUID3_STRENGTH:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̓p�����^���Q�O�`�R�O�t�o����B";
                    minValue = 20;
                    maxValue = 30;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID3_AGILITY:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�Z�p�����^���Q�O�`�R�O�t�o����B";
                    minValue = 20;
                    maxValue = 30;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID3_INTELLIGENCE:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�m�p�����^���Q�O�`�R�O�t�o����B";
                    minValue = 20;
                    maxValue = 30;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID3_STAMINA:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̃p�����^���Q�O�`�R�O�t�o����B";
                    minValue = 20;
                    maxValue = 30;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID3_MIND:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�S�p�����^���Q�O�`�R�O�t�o����B";
                    minValue = 20;
                    maxValue = 30;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.GROWTH_LIQUID4_STRENGTH:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̓p�����^���S�T�`�U�O�t�o����B";
                    minValue = 45;
                    maxValue = 60;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID4_AGILITY:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�Z�p�����^���S�T�`�U�O�t�o����B";
                    minValue = 45;
                    maxValue = 60;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID4_INTELLIGENCE:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�m�p�����^���S�T�`�U�O�t�o����B";
                    minValue = 45;
                    maxValue = 60;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID4_STAMINA:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̃p�����^���S�T�`�U�O�t�o����B";
                    minValue = 45;
                    maxValue = 60;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID4_MIND:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�S�p�����^���S�T�`�U�O�t�o����B";
                    minValue = 45;
                    maxValue = 60;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.GROWTH_LIQUID5_STRENGTH:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̓p�����^���W�O�`�P�O�O�t�o����B";
                    minValue = 80;
                    maxValue = 100;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID5_AGILITY:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�Z�p�����^���W�O�`�P�O�O�t�o����B";
                    minValue = 80;
                    maxValue = 100;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID5_INTELLIGENCE:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�m�p�����^���W�O�`�P�O�O�t�o����B";
                    minValue = 80;
                    maxValue = 100;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID5_STAMINA:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�̃p�����^���W�O�`�P�O�O�t�o����B";
                    minValue = 80;
                    maxValue = 100;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.GROWTH_LIQUID5_MIND:
                    description = "�\�͂̈ꕔ�𐬒����i�������B�S�p�����^���W�O�`�P�O�O�t�o����B";
                    minValue = 80;
                    maxValue = 100;
                    cost = 0;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "���x���͈͂ɉ������h���b�v�A�C�e��(Epic Only)"
                case Database.EPIC_RING_OF_OSCURETE:
                    description = "�Ñ㌫�҃I�X�L�����[�e���c������ɕt���Ă��������O�B�́{�P�T�A�Z�{�V�A�m�{�R�O�A�̗́{�S�A�S�{�W�A���͂S�T�`�U�Q";
                    MagicMinValue = 45;
                    MagicMaxValue = 62;
                    BuffUpStrength = 15;
                    BuffUpAgility = 7;
                    BuffUpIntelligence = 30;
                    BuffUpStamina = 4;
                    BuffUpMind = 8;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_MERGIZD_SOL_BLADE:
                    description = "�Ñ㌫�҃����M�Y�h���c������ɕt���Ă����u���[�h�B�́{�R�O�A�m�{�P�U�A�U���͂S�T�`�V�W�A���͂R�U�`�S�Q";
                    minValue = 45;
                    maxValue = 78;
                    MagicMinValue = 36;
                    MagicMaxValue = 42;
                    BuffUpStrength = 30;
                    BuffUpIntelligence = 16;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_GARVANDI_ADILORB:
                    description = "�Ñ㌫�҃G�[�f�B�����c������ɕt���Ă��������B�Z�{�U�T�A�m�{�P�P�O�A�́{�U�R�A���͂Q�O�P�`�R�S�S";
                    MagicMinValue = 201;
                    MagicMaxValue = 344;
                    buffUpAgility = 65;
                    buffUpIntelligence = 110;
                    buffUpStamina = 63;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_MAXCARN_X_BUSTER:
                    description = "�Ñ㌫�҃}�N�X�J�[�����c������ɕt���Ă������茕�B�́{�V�T�A�Z�{�U�R�A�S�{�P�O�O�A�U���͂Q�Q�O�`�U�O�O";
                    minValue = 220;
                    maxValue = 600;
                    BuffUpStrength = 75;
                    BuffUpAgility = 63;
                    BuffUpMind = 100;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_JUZA_ARESTINE_SLICER:
                    description = "�Ñ㌫�҃W���U���c������ɕt���Ă����X���C�T�[�܁B�́{�P�O�U�A�Z�{�P�R�Q�A�U���͂R�Q�Q�`�R�W�V";
                    minValue = 322;
                    maxValue = 387;
                    BuffUpStrength = 106;
                    BuffUpAgility = 132;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_SHEZL_MYSTIC_FORTUNE:
                    description = "�Ñ㌫�҃V�F�Y�����c������ɕt���Ă����߁B�y����\�́F�L�z���@�h��S�U�O�`�T�X�W�A���ّϐ��A�X�^���ϐ��A��ბϐ��A�m�{�T�U�O�A���U���{�P�O���A���h���{�P�O���A���ϐ�2000�A���ϐ�2000";
                    description += "\r\n�y����\�́z�@���^�[���A�}�i���񕜂���B";
                    MagicMinValue = 460;
                    MagicMaxValue = 598;
                    ResistSilence = true;
                    ResistStun = true;
                    ResistParalyze = true;
                    amplifyMagicAttack = 1.1f;
                    amplifyMagicDefense = 1.1f;
                    buffUpIntelligence = 560;
                    ResistLight = 2000;
                    ResistForce = 2000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE:
                    description = "�Ñ㌫�҃c���F���h�[�[���c������ɕt���Ă��������i�B�y����\�́F�L�z�����h��O�`�P�O�T�O�A���@�h��O�`�P�O�T�O�A�S�{�T�O�O�A���ϐ�3500�A�őϐ�3500";
                    description += "\r\n�y����\�́z�@�퓬�J�n���A���[�h�E�I�u�E���C�t�ƃ��C�Y�E�I�u�E�C���[�W���������g�ɂ�����B";
                    minValue = 0;
                    maxValue = 1050;
                    MagicMinValue = 0;
                    MagicMaxValue = 1050;
                    amplifyPotential = 1.2f;
                    buffUpMind = 500;
                    ResistShadow = 3500;
                    ResistLight = 3500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_MERGIZD_DAV_AGITATED_BLADE:
                    description = "�Ñ㌫�҃����M�Y�h���N����Ɉ��p���Ă����u���[�h�B�U���͂T�S�T�`�U�P�Q�A���͂S�Q�R�`�S�U�T�A�́{�Q�W�T�A�m�{�P�U�W�A��U���{�P�T���A���U���{�P�T��";
                    minValue = 545;
                    maxValue = 612;
                    MagicMinValue = 423;
                    MagicMaxValue = 465;
                    buffUpStrength = 285;
                    buffUpIntelligence = 168;
                    amplifyPhysicalAttack = 1.15f;
                    amplifyMagicAttack = 1.15f;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;                   
                #endregion
                #region "�t�F���g�D�[�V���i�KUP"
                case Database.POOR_PRACTICE_SWORD_ZERO:
                    description = "�K���c�������񂩂�����ꂽ���B�ǂ��݂Ă����K�p�����E�E�E�B�U���͂P�`�R";
                    minValue = 1;
                    maxValue = 3;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Poor;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_PRACTICE_SWORD_1:
                    description = "�K���c�������񂩂�����ꂽ���BLv1�ɐ������Ă���B�U���͂P�`�S�Q";
                    minValue = 1;
                    maxValue = 42;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Poor;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.POOR_PRACTICE_SWORD_2:
                    description = "�K���c�������񂩂�����ꂽ���BLv2�ɐ������Ă���B�U���͂P�`�X�T";
                    minValue = 1;
                    maxValue = 95;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Poor;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PRACTICE_SWORD_3:
                    description = "�K���c�������񂩂�����ꂽ���BLv3�ɐ������Ă���B�U���͂P�`�Q�P�P";
                    minValue = 1;
                    maxValue = 211;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.COMMON_PRACTICE_SWORD_4:
                    description = "�K���c�������񂩂�����ꂽ���BLv4�ɐ������Ă���B�U���͂P�`�S�R�X";
                    minValue = 1;
                    maxValue = 439;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Common;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_PRACTICE_SWORD_5:
                    description = "�K���c�������񂩂�����ꂽ���BLv5�ɐ������Ă���B�U���͂P�`�P�O�P�Q";
                    minValue = 1;
                    maxValue = 1012;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.RARE_PRACTICE_SWORD_6:
                    description = "�K���c�������񂩂�����ꂽ���BLv6�ɐ������Ă���B�U���͂P�`�Q�R�O�W";
                    minValue = 1;
                    maxValue = 2308;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_PRACTICE_SWORD_7:
                    description = "�K���c�������񂩂�����ꂽ���BLv7�ɐ������Ă���B�U���͂P�`�S�T�R�V";
                    minValue = 1;
                    maxValue = 4537;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Epic;
                    equipablePerson = Equipable.Ein;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                #endregion
                #region "�����A�C�e��"
                #region "�����P�K"
                case Database.COMMON_KOUKAKU_ARMOR: // ���[���̍b�k
                    description = "�b�k�����q�����킹���Z�ɁA���@�ϐ����኱�t�^��������i�B�h��͂P�P�`�P�T�B�Αϐ��Q�O";
                    minValue = 11;
                    maxValue = 15;
                    this.ResistFire = 20;
                    cost = 1800;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    break;

                case Database.COMMON_SISSO_TUKEHANE: // ��̔��H�A���z�̗t
                    description = "�є�Ɋ���̔��H�𖄂ߍ��񂾃A�N�Z�T���B�́{�R�A�Z�{�R�A�S�{�R";
                    minValue = 0;
                    maxValue = 0;
                    BuffUpStrength = 3;
                    BuffUpAgility = 3;
                    BuffUpMind = 3;
                    cost = 2500;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;

                case Database.RARE_WAR_WOLF_BLADE: // �h�̐������G��A�T�̉�
                    description = "�T�̉����f�ނƂ��A�h�t���G������H��������B�U���͂R�Q�`�S�S";
                    minValue = 32;
                    maxValue = 44;
                    cost = 3600;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Rare;
                    break;

                case Database.COMMON_BLUE_COPPER_ARMOR_KAI:
                    description = "���̍ގ����x�𗎂Ƃ����Ɏd�グ��ꂽ�Z�B�h��͂Q�O�`�Q�T�B";
                    minValue = 20;
                    maxValue = 25;
                    cost = 3200;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Common;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_RABBIT_SHOES: // �E�T�M�̖є�A�X�p�C�_�[�V���N
                    description = "�E�T�M�̖є�Ǝ��̗ǂ��X�p�C�_�[�V���N�����������o�����V���[�Y�B�Z�{�P�Q�A�̗́{�P�O";
                    minValue = 0;
                    maxValue = 0;
                    BuffUpAgility = 12;
                    BuffUpStamina = 10;
                    cost = 3200;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Common;
                    break;

                #endregion
                #region "�T�K"
                case "�l�_���ɐۗ�": // �n�[�g�E�I�u�E�t�F�j�b�N�X,�n�[�g�E�I�u�E�h���S��,�n�[�g�E�I�u�E�����X�^�[,�n�[�g�E�I�u�E�W���b�W
                    description = "�`�������̐S�������W�������A�N�Z�T���B���@�ƃX�L���𓯎����p�\�ɂȂ�B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    break;
                #endregion
                #endregion
                #region "Duelist�B�̃A�C�e��"
                case Database.RARE_PURE_GREEN_WATER:
                    description = "�񑩂��ꂽ�񕜖�B�����P�x�����X�L���|�C���g��100%�񕜁B";
                    minValue = 0;
                    maxValue = 0;
                    cost = 25000;
                    AdditionalDescription(ItemType.Use_Potion);
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_FAZIL_ORB_1:
                    description = "�t�@�[�W�����ƁA�\�Z����̈�y�����z�@�́{�R�T�A�Z�{�R�T�A�m�{�R�T�A�́{�R�T�A�S�{�R�T";
                    buffUpStrength = 35;
                    buffUpAgility = 35;
                    buffUpIntelligence = 35;
                    buffUpStamina = 35;
                    buffUpMind = 35;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_FAZIL_ORB_2:
                    description = "�t�@�[�W�����ƁA�\�Z����̈�y�n���z�@�́{�T�O�A�Z�{�T�O�A�m�{�T�O�A�́{�T�O�A�S�{�T�O";
                    buffUpStrength = 50;
                    buffUpAgility = 50;
                    buffUpIntelligence = 50;
                    buffUpStamina = 50;
                    buffUpMind = 50;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_SHUVALTZ_FLORE_SWORD:
                    description = "�V�����@���c�F���p�̌��B��z���������ɔ����З͂𔭊�����B�y����\�́F�L�z�@�U���͂S�W�W�`�T�V�P  �́{�R�U�W";
                    description += "\r\n�y����\�́z�@�^�[���o�ߖ��ɁA�X�L���|�C���g���T�񕜂���B";
                    minValue = 488;
                    maxValue = 571;
                    buffUpStrength = 368;
                    AdditionalDescription(ItemType.Weapon_Heavy);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_SHUVALTZ_FLORE_SHIELD:
                    description = "�V�����@���c�F���p�̌��B���`��ƂȂ��Ă��邪�A�h����s�����߂ɍ쐬���ꂽ���B�y����\�́F�L�z�h��͂P�X�T�`�Q�R�X  ���h���{�P�Q��";
                    description += "\r\n�y����\�́z�@�^�[���o�ߖ��ɁA�ő僉�C�t�̂T���������C�t�񕜂���B";
                    minValue = 195;
                    maxValue = 239;
                    amplifyPhysicalDefense = 1.2f;
                    AdditionalDescription(ItemType.Shield);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_SHUVALTZ_FLORE_ARMOR:
                    description = "�V�����@���c�F���p�̈߁B���[�u�n���ł���Ȃ���A���̖h��͂͌v��m��Ȃ��B�y����\�́F�L�z�h��͂R�U�T�`�S�Q�Q�@���@�h��S�P�T�`�T�S�U�A���ّϐ��A�m�{�S�Q�O�A���h���{�P�Q���A�őϐ�2000�A���ϐ�2000";
                    description += "\r\n�y����\�́z�@�^�[���o�ߖ��ɁA�ő�}�i�̂T�������}�i�񕜂���B";
                    minValue = 365;
                    maxValue = 422;
                    MagicMinValue = 415;
                    MagicMaxValue = 546;
                    ResistSilence = true;
                    amplifyMagicDefense = 1.2f;
                    buffUpIntelligence = 420;
                    ResistShadow = 2000;
                    ResistIce = 2000;
                    AdditionalDescription(ItemType.Armor_Light);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1:
                    description = "�t�@�[�W�����ƁA�\�Z����̈�y������z�@�́{�P�O�O�A�Z�{�P�O�O�A�m�{�P�O�O�A�́{�P�O�O�A�S�{�P�O�O�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�퓬�J�n���Ƀ��C�Y�E�I�u�E�C���[�W�������Ҏ��g�ɕt�^�����B���̌��ʂ��f�B�X�y�����ꂽ�ꍇ�A���̃^�[�����ēx���ʂ𔭊�����B";
                    buffUpStrength = 100;
                    buffUpAgility = 100;
                    buffUpIntelligence = 100;
                    buffUpStamina = 100;
                    buffUpMind = 100;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2:
                    description = "�t�@�[�W�����ƁA�\�Z����̈�y�i�z�z�@�́{�P�T�O�A�Z�{�P�T�O�A�m�{�P�T�O�A�́{�P�T�O�A�S�{�P�T�O�y����\�́F�L�z";
                    description += "\r\n�y����\�́z�@�퓬�J�n�Ƀ��[�h�E�I�u�E���C�t�������Ҏ��g�ɕt�^�����B���̌��ʂ��f�B�X�y�����ꂽ�ꍇ�A���̃^�[�����ēx���ʂ𔭊�����B";
                    buffUpStrength = 150;
                    buffUpAgility = 150;
                    buffUpIntelligence = 150;
                    buffUpStamina = 150;
                    buffUpMind = 150;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_SWORD_OF_RVEL: // ���x���E�[���L�X��p����
                    description = "���x���E�[���L�X���p�̑匕�B������͕����E���@�̗����̃I�[������������B�U���͂Q�W�O�`�V�U�O�A���͂R�Q�O�`�R�X�O";
                    minValue = 280;
                    maxValue = 760;
                    MagicMinValue = 320;
                    MagicMaxValue = 390;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_TwoHand);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.COMMON_ARMOR_OF_RVEL: // ���x���E�[���L�X��p����
                    description = "���x���E�[���L�X���p�̊Z�B�Z�͖��@�̌����ттĂ���A���S�ȏ��A�z������B�h��͂P�S�O�`�P�W�O�A���@�h��P�V�O�`�Q�Q�O";
                    minValue = 140;
                    maxValue = 180;
                    MagicMinValue = 170;
                    MagicMaxValue = 220;
                    cost = 0;
                    AdditionalDescription(ItemType.Armor_Heavy);
                    rareLevel = RareLevel.Rare;
                    limitValue = EQUIP_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_LADA_ACHROMATIC_ORB:
                    description = "���_���p�̎����̃I�[�u�B�I�[�u�Ɍ����ǂ��Ƃ炳��Ă������ċP�����͂Ȃ��B���͂P�R�Q�O�`�P�U�S�S";
                    MagicMinValue = 1320;
                    MagicMaxValue = 1644;
                    cost = 0;
                    AdditionalDescription(ItemType.Weapon_Light);
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_TAU_WHITE_SILVER_SWORD_0:
                    description = "�_�̈�Y�̈�B����F�̌������Ȕ������铧���Ȍ��B�U���͂P�O�O�`�P�O�P";
                    minValue = 100;
                    maxValue = 101;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Legendary;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR_0:
                    description = "�_�̈�Y�̈�B�W���̌������Ȕ������Ă���A�^��g�����Z�ɕt�^����Ă���B�h��͂P�`�Q";
                    minValue = 1;
                    maxValue = 2;
                    AdditionalDescription(ItemType.Armor_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Legendary;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING_0:
                    description = "�_�̈�Y�̈�B���z�������Ȕ������Ă���A�����͓̂����B�Z�{�P";
                    buffUpAgility = 1;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Legendary;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_WHITE_SILVER_SWORD_REPLICA:
                    description = "�_�̈�Y��͕킵�����B���F�̌����d�q�o�H�Ŏ{����Ă���B�U���͂S�Q�V�`�S�W�P";
                    minValue = 427;
                    maxValue = 481;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_WHITE_SILVER_SWORD_REPLICA:
                    description = "�_�̈�Y��͕킵�����B���F�̌����d�q�o�H�Ŏ{����Ă���B�U���͂Q�O�Q�U�`�Q�R�W�T";
                    minValue = 2026;
                    maxValue = 2385;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_TAU_WHITE_SILVER_SWORD:
                    description = "�_�̈�Y�̈�B����F�̌������Ȕ������铧���Ȍ��B�U���͂T�O�P�Q�`�U�O�Q�Q";
                    minValue = 5012;
                    maxValue = 6022;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Legendary;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                case Database.EPIC_COLORESS_ETERNAL_BREAKER:
                    description = "�����̃t�F�C�Y�����A�̉���Ŕ������ꂽ���B���̌��͌����ė򉻂��Ȃ��B�U���͂S�W�V�P�`�T�Q�R�X";
                    minValue = 4871;
                    maxValue = 5239;
                    AdditionalDescription(ItemType.Weapon_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_BLACK_AERIAL_ARMOR_REPLICA:
                    description = "�_�̈�Y��͕킵���Z�B�������锭�s�F���{���Ă���B�h��͂W�X�`�P�O�P";
                    minValue = 89;
                    maxValue = 101;
                    AdditionalDescription(ItemType.Armor_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_BLACK_AERIAL_ARMOR_REPLICA:
                    description = "�_�̈�Y��͕킵���Z�B�������锭�s�F���{���Ă���B�����h��͂P�P�U�T�`�P�R�S�O�A���@�h��͂P�O�X�Q�`�P�P�W�R";
                    minValue = 1165;
                    maxValue = 1340;
                    MagicMinValue = 1092;
                    MagicMaxValue = 1183;
                    AdditionalDescription(ItemType.Armor_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                
                case Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR:
                    description = "�_�̈�Y�̈�B�W���̌������Ȕ������Ă���A�^��g�����Z�ɕt�^����Ă���B�����h��͂Q�V�W�Q�`�Q�X�V�X�A���@�h��͂Q�U�V�S�`�R�O�P�Q";
                    minValue = 2782;
                    maxValue = 2979;
                    MagicMinValue = 2674;
                    MagicMaxValue = 3012;
                    ResistBlind = true;
                    ResistFrozen = true;
                    ResistNoResurrection = true;
                    ResistParalyze = true;
                    ResistPoison = true;
                    ResistSilence = true;
                    ResistSlip = true;
                    ResistSlow = true;
                    ResistStun = true;
                    ResistTemptation = true;
                    AdditionalDescription(ItemType.Armor_Middle);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Legendary;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_SEFINE_HYMNUS_RING:
                    description = "���͖S���Z�t�B�[�l�̌`���B���F���[�͎����̍����ׂ���܂ł��̘r�ւ𗣂����͂Ȃ��B";
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.RARE_HEAVENLY_SKY_WING_REPLICA:
                    description = "�_�̈�Y��͕킵�����B���z�F�̔������{����Ă���B�Z�{�R�P�Q";
                    buffUpAgility = 312;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Rare;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.EPIC_HEAVENLY_SKY_WING_REPLICA:
                    description = "�_�̈�Y��͕킵�����B���z�F�̔������{����Ă���B�Z�{�P�W�X�U";
                    buffUpAgility = 1896;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Epic;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;

                case Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING:
                    description = "�_�̈�Y�̈�B���z�������Ȕ������Ă���A�����͓̂����B�Z�{�T�U�X�P";
                    buffUpAgility = 5691;
                    cost = 0;
                    AdditionalDescription(ItemType.Accessory);
                    equipablePerson = Equipable.Verze;
                    rareLevel = RareLevel.Legendary;
                    limitValue = RARE_EPIC_ITEM_STACK_SIZE;
                    break;
                #endregion
            }
        }

        public int CompareTo(Object obj)
        {
            return this.name.CompareTo(((ItemBackPack)obj).name);
        }

        public int CompareToWithType(Object obj, Object obj2)
        {
            return ((ItemBackPack)obj2).type.CompareTo(((ItemBackPack)obj).type);
        }

        public static IComparer SortItemBackPackUsed()
        {
            return (IComparer)new ItemBackPackSortUsed();
        }
        public static IComparer SortItemBackPackAccessory()
        {
            return (IComparer)new ItemBackPackSortAccessory();
        }
        public static IComparer SortItemBackPackWeapon()
        {
            return (IComparer)new ItemBackPackSortWeapon();
        }
        public static IComparer SortItemBackPackArmor()
        {
            return (IComparer)new ItemBackPackSortArmor();
        }
        public static IComparer SortItemBackPackName()
        {
            return (IComparer)new ItemBackPackSortName();
        }
        public static IComparer SortItemBackPackRare()
        {
            return (IComparer)new ItemBackPackSortRare();
        }

        public void CleanUpStatus()
        {
            effectStatus = false;
        }
    }

    // �\�[�g�͎w�肳�ꂽType���g�b�v�ցB
    // ����ȊO��Used,Accessory�AWeapon�AArmor�̏����ŕ��ׂ�B
    // ���O�\�[�g�̏ꍇ�A��L���[���𖳎����Ė��O���Ƃ���B
    // Rare�\�[�g�̏ꍇ�ARare���ŁA����Rare�̏ꍇ���O���Ƃ���B
    public class ItemBackPackSortUsed : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortAccessory : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortWeapon : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortArmor : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Type == ItemBackPack.ItemType.Armor_Heavy || c1.Type == ItemBackPack.ItemType.Armor_Light || c1.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Armor_Heavy || c2.Type == ItemBackPack.ItemType.Armor_Light || c2.Type == ItemBackPack.ItemType.Armor_Middle)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Material_Equip || c1.Type == ItemBackPack.ItemType.Material_Food || c1.Type == ItemBackPack.ItemType.Material_Potion || c1.Type == ItemBackPack.ItemType.None)
            {
                if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if ((c2.Type == ItemBackPack.ItemType.Material_Equip || c2.Type == ItemBackPack.ItemType.Material_Food || c2.Type == ItemBackPack.ItemType.Material_Potion || c2.Type == ItemBackPack.ItemType.None))
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Accessory)
            {
                if (c2.Type == ItemBackPack.ItemType.Accessory)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Accessory)
            {
                return 1;
            }

            if (c1.Type == ItemBackPack.ItemType.Weapon_Heavy || c1.Type == ItemBackPack.ItemType.Weapon_Light || c1.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
                {
                    return c1.Name.CompareTo(c2.Name);
                }
                else
                {
                    return -1;
                }
            }
            if (c2.Type == ItemBackPack.ItemType.Weapon_Heavy || c2.Type == ItemBackPack.ItemType.Weapon_Light || c2.Type == ItemBackPack.ItemType.Weapon_Middle)
            {
                return 1;
            }

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortName : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            return c1.Name.CompareTo(c2.Name);
        }
    }

    public class ItemBackPackSortRare : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            ItemBackPack c1 = (ItemBackPack)a;
            ItemBackPack c2 = (ItemBackPack)b;

            if (c1.Rare.CompareTo(c2.Rare) == 0)
            {
                return c1.Name.CompareTo(c2.Name);
            }
            else
            {
                return c2.Rare.CompareTo(c1.Rare);
            }
        }
    }
}