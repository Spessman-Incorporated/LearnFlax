using FlaxEngine.Networking;
using Game.Networking.Core;

namespace Game.Networking.Packets
{
    public class ConnectionRequestPacket : NetworkPacket
    {
        public string Username = string.Empty;

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteString(Username);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            Username = msg.ReadString();
        }

        public override void ServerHandler(ref NetworkConnection sender)
        {
            ConnectionResponsePacket cr = new ConnectionResponsePacket
            {
                Id = NetworkSession.Instance.GuidByConn(ref sender)
            };
            NetworkSession.Instance.Send(cr, NetworkChannelType.ReliableOrdered, ref sender);

            GameSession.Instance.GetPlayer(cr.Id).Name = Username;

            PlayerListPacket plp = new PlayerListPacket
            {
                Players = GameSession.Instance.Players
            };
            NetworkSession.Instance.Send(plp, NetworkChannelType.Reliable, ref sender);

            PlayerConnectedPacket pcp = new PlayerConnectedPacket
            {
                Id = cr.Id,
                Username = Username
            };
            NetworkSession.Instance.SendAll(pcp, NetworkChannelType.Reliable);
        }
    }
}
