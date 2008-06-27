﻿using System;
using System.Collections.Generic;
using System.Text;


namespace FlashTowerDefense.Shared
{
    public partial class SharedClass1
    {
        // members defined over here can be used on client and on server
        // x

        public string Hello;

        /// <summary>
        /// this interface is to be used in a generator
        /// </summary>
        partial interface IMessages
        {
            void TeleportTo(int x, int y);
            void WalkTo(int x, int y);
        }

        #region generated from IMessages
        public enum Messages
        {
            None = 100,
            
            Ping,
            Pong,

            UserJoined,
            ToUserJoinedReply,  // client->server
            UserJoinedReply,    // server->client

            UserLeft,

            UserEnterMachineGun,
            UserExitMachineGun,

            UserStartMachineGun,
            UserStopMachineGun,

            UserTeleportTo,
            UserWalkTo,
            UserFiredShotgun,

            EnterMachineGun,
            ExitMachineGun,

            StartMachineGun,
            StopMachineGun,

            TeleportTo,
            WalkTo,
            FiredShotgun,

            ServerMessage,
            ServerRandomNumbers,

            ReadyForServerRandomNumbers,
            CancelServerRandomNumbers,

            AddDamageFromDirection,
            UserAddDamageFromDirection,

            
            // for others
            TakeBox,
            UserTakeBox,


            ShowBulletsFlying,
            UserShowBulletsFlying,
        }

        
        public partial class RemoteEvents
        {
            public event Action<int, int> TeleportTo;


        }

        public partial class RemoteMessages : IMessages
        {

            #region IMessages Members

            public void TeleportTo(int x, int y)
            {
                throw new NotImplementedException();
            }

            public void WalkTo(int x, int y)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion

    }
}
