using System;
using FlaxEngine.Networking;
using Game.Networking.Core;

namespace Game.Networking.Packets
{
    public class PlayerDisconnectedPacket : NetworkPacket
    {
        public Guid Id;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteGuid(Id);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            Id = msg.ReadGuid();
        }

        public override void ClientHandler()
        {
            if (Id == GameSession.Instance.LocalPlayer.Id)
                return;
            NetworkSession.Instance.RemovePlayer(ref Id);
            GameSession.Instance.RemovePlayer(ref Id);
        }
    }
}
