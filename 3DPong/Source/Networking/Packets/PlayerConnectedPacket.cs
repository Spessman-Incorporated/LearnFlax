using System;
using FlaxEngine.Networking;
using Game.Networking.Core;

namespace Game.Networking.Packets
{
    public class PlayerConnectedPacket : NetworkPacket
    {
        public Guid Id;
        public string Username;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteGuid(Id);
            msg.WriteString(Username);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            Id = msg.ReadGuid();
            Username = msg.ReadString();
        }

        public override void ClientHandler()
        {
            if (Id == GameSession.Instance.LocalPlayer.Id)
                return;
            GameSession.Instance.AddPlayer(ref Id, Username);
        }
    }
}
