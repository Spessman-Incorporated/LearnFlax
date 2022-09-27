﻿using System;
using FlaxEngine.Networking;
using Game.Networking.Core;

namespace Game.Networking.Packets
{
    public class PlayerDisconnectedPacket : NetworkPacket
    {
        public Guid ID;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteGuid(ID);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            ID = msg.ReadGuid();
        }

        public override void ClientHandler()
        {
            if (ID == GameSession.Instance.LocalPlayer.Id)
                return;
            NetworkSession.Instance.RemovePlayer(ref ID);
            GameSession.Instance.RemovePlayer(ref ID);
        }
    }
}
