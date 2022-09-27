using FlaxEngine.Networking;

namespace Game.Networking.Packets
{
    public abstract class NetworkPacket
    {
        public NetworkConnection Sender;

        public abstract void Serialize(ref NetworkMessage msg);

        public abstract void Deserialize(ref NetworkMessage msg);

        public virtual void ServerHandler(ref NetworkConnection sender) { }

        public virtual void ClientHandler() { }
    }
}
