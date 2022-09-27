using System;
using System.Collections.Generic;
using System.Linq;
using FlaxEngine.Networking;
using Game.Networking.Demo;

namespace Game.Networking.Core
{
    public class ConnectionRegistry
    {
        private readonly Dictionary<NetworkConnection, Guid> _idByCon = new Dictionary<NetworkConnection, Guid>();
        private readonly Dictionary<Guid, NetworkConnection> _conById = new Dictionary<Guid, NetworkConnection>();

        public void Add(ref NetworkConnection conn, Player player)
        {
            _idByCon.Add(conn, player.Id);
            _conById.Add(player.Id, conn);
        }

        public void Remove(ref NetworkConnection conn)
        {
            Guid guid = _idByCon[conn];
            _conById.Remove(guid);
            _idByCon.Remove(conn);
        }

        public void Remove(ref Guid guid)
        {
            NetworkConnection conn = _conById[guid];
            _idByCon.Remove(conn);
            _conById.Remove(guid);
        }

        public void Clear()
        {
            _idByCon.Clear();
            _conById.Clear();
        }


        public Guid GuidByConn(ref NetworkConnection conn)
        {
            return _idByCon.ContainsKey(conn) ? _idByCon[conn] : default;
        }

        public NetworkConnection ConnByGuid(ref Guid guid)
        {
            return _conById.ContainsKey(guid) ? _conById[guid] : default;
        }

        public List<NetworkConnection> ToList()
        {
            return _conById.Values.ToList();
        }

        public NetworkConnection[] ToArray()
        {
            return _conById.Values.ToArray();
        }
    }
}
