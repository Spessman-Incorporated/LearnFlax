using System;
using System.Collections.Generic;
using FlaxEngine;
using Game.Networking.Demo;

namespace Game.Networking.Core {
    /// <summary>
    /// Game service with players list including local player.
    /// </summary>
    public class GameSession : GamePlugin
    {
        public delegate void OnPlayerAddedHandler(Player player);

        public event OnPlayerAddedHandler OnPlayerAdded;

        public delegate void OnPlayerRemovedHandler(Player player);

        public event OnPlayerRemovedHandler OnPlayerRemoved;

        public readonly List<Player> Players = new List<Player>();
        public readonly List<ChatMessage> ChatMessages = new List<ChatMessage>();

        public Player LocalPlayer;

        public override void Initialize()
        {
            base.Initialize();
            Players.Clear();
            LocalPlayer = new Player();
        }

        public override void Deinitialize()
        {
            if (_instance == this)
            {
                _instance = null;
            }

            base.Deinitialize();
        }

        public Player AddPlayer()
        {
            Player p = new Player
            {
                Id = Guid.NewGuid()
            };
            AddPlayer(p);
            return p;
        }

        public Player AddPlayer(ref Guid guid, string name)
        {
            Player p = new Player()
            {
                Id = guid, Name = name
            };
            AddPlayer(p);
            return p;
        }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
            OnPlayerAdded?.Invoke(player);
        }

        public bool RemovePlayer(ref Guid id)
        {
            for (int i = Players.Count - 1; i >= 0; i--)
            {
                if (Players[i].Id != id)
                {
                    continue;
                }

                Player p = Players[i];
                Players.RemoveAt(i);
                OnPlayerRemoved?.Invoke(p);
                return true;
            }

            return false;
        }

        public void AddChatMessage(Guid sender, string message)
        {
            ChatMessages.Add(new ChatMessage() {Sender = sender, Message = message});
        }

        public Player GetPlayer(Guid guid)
        {
            if (LocalPlayer.Id == guid)
            {
                return LocalPlayer;
            }

            foreach (Player t in Players)
            {
                if (t.Id == guid)
                {
                    return t;
                }
            }

            return null;
        }

        private static GameSession _instance;

        public static GameSession Instance => _instance ?? (_instance = PluginManager.GetPlugin<GameSession>());
    }
}