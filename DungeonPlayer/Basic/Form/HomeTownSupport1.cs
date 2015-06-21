using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class HomeTown : MotherForm
    {
        private string MessageFormatForLana(int num)
        {
            switch (num)
            {
                case 1001:
                    if (!we.AvailableSecondCharacter)
                    {
                        return sc.GetCharacterSentence(num);
                    }
                    else
                    {
                        return sc.GetCharacterSentence(1003);
                    }

                case 1002:
                    if (!we.AvailableSecondCharacter)
                    {
                        return sc.GetCharacterSentence(num);
                    }
                    else
                    {
                        return sc.GetCharacterSentence(1004);
                    }
                default:
                    return sc.GetCharacterSentence(num);
            }
        }
    }
}
