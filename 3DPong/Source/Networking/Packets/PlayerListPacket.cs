using System.Collections.Generic;
using FlaxEngine.Networking;
using Game.Networking.Core;
using Game.Networking.Demo;

namespace Game.Networking.Packets
{
    public class PlayerListPacket : NetworkPacket
    {
        public List<Player> Players = new List<Player>();

        public override void Serialize(ref NetworkMessage msg)
        {
            msg.WriteInt32(Players.Count + 1);
            foreach (Player t in Players)
            {
                msg.WriteString(t.Name);
                msg.WriteGuid(t.Id);
            }

            msg.WriteString(GameSession.Instance.LocalPlayer.Name);
            msg.WriteGuid(GameSession.Instance.LocalPlayer.Id);
        }

        public override void Deserialize(ref NetworkMessage msg)
        {
            int count = msg.ReadInt32();
            Players = new List<Player>();
            for (int i = 0; i < count; i++)
            {
                Player p = new Player
                {
                    Name = msg.ReadString(),
                    Id = msg.ReadGuid()
                };
                Players.Add(p);
            }
        }

        public override void ClientHandler()
        {
            foreach (Player t in Players)
            {
                if (GameSession.Instance.GetPlayer(t.Id) == null &&
                    t.Id != GameSession.Instance.LocalPlayer.Id)
                {
                    GameSession.Instance.AddPlayer(t);
                }
            }
        }
    }
}
