using System;
using FlaxEngine.Networking;
using Game.Networking.Core;
using Game.Networking.Packets;

namespace Game.Networking.Chat {
    public class ChatMessagePacket : NetworkPacket
    {
        public string Message = string.Empty;
        public Guid SenderId = Guid.Empty;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteString(Message);
            bool hasSender = SenderId != Guid.Empty;
            msg.WriteBoolean(hasSender);
            if (hasSender)
            {
                msg.WriteGuid(SenderId);
            }
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            Message = msg.ReadString();
            SenderId = msg.ReadBoolean() ? msg.ReadGuid() : Guid.Empty;
        }

        public override void ServerHandler(ref NetworkConnection sender)
        {
            SenderId = NetworkSession.Instance.GuidByConn(ref sender);
            GameSession.Instance.AddChatMessage(SenderId, Message);
            NetworkSession.Instance.SendAll(this, NetworkChannelType.ReliableOrdered);
        }

        public override void ClientHandler()
        {
            if (GameSession.Instance.LocalPlayer.Id == SenderId)
                return;
            GameSession.Instance.AddChatMessage(SenderId, Message);
        }
    }

    class ChatMessagePacketImpl : ChatMessagePacket { }
}