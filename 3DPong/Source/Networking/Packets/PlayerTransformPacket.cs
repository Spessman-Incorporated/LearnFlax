﻿using System;
using FlaxEngine;
using FlaxEngine.Networking;
using Game.Networking.Core;
using Game.Networking.Demo;

namespace Game.Networking.Packets
{
    public class PlayerTransformPacket : NetworkPacket
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteVector3(Position);
            msg.WriteQuaternion(Rotation);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            Position = msg.ReadVector3();
            Rotation = msg.ReadQuaternion();
        }

        public override void ServerHandler(ref NetworkConnection sender)
        {
            Guid guid = NetworkSession.Instance.GuidByConn(ref sender);
            Player player = GameSession.Instance.GetPlayer(guid);
            player.Position = Position;
            player.Rotation = Rotation;
        }
    }
}
