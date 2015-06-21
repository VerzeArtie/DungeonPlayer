using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DungeonPlayer
{
    public partial class TruthHomeTown : MotherForm
    {
        private void SecondCommunicationStart()
        {
            we.Truth_CommunicationSecondHomeTown = true;
            we.AlreadyRest = true;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            this.BackColor = Color.WhiteSmoke;
            this.Update();
            CallRestInn();
        }

        private void ThirdCommunicationStart()
        {
            we.Truth_CommunicationThirdHomeTown = true;
            we.AlreadyRest = true;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            this.BackColor = Color.WhiteSmoke;
            this.Update();
            CallRestInn();
        }

        private void FourthCommunicationStart()
        {
            we.Truth_CommunicationFourthHomeTown = true;
            we.AlreadyRest = true;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            this.BackColor = Color.WhiteSmoke;
            this.Update();
            CallRestInn();
        }

        private void FifthCommunicationStart()
        {
            we.Truth_CommunicationFifthHomeTown = true;
            we.AlreadyRest = true;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            this.BackColor = Color.WhiteSmoke;
            this.Update();
            CallRestInn();
        }
    }
}
