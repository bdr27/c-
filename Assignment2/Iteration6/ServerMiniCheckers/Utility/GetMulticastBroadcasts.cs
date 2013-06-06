using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Utility
{
    public class GetMulticastBroadcasts
    {
        public static List<String> GetListOfPlayers(Dictionary<int, string> dictionary)
        {
            var players = new List<string>();
            foreach (var player in dictionary.Values)
            {
                players.Add(player);
            }
            return players;
        }

        public static string GetMulticastSendPlayersMessage(List<string> players)
        {
            var userMulticast = "USERS,";
            for (int i = 0; i < players.Count - 1; i++)
            {
                userMulticast = userMulticast + players[i] + ",";
            }
            userMulticast += players[players.Count - 1];
            return userMulticast;
        }
    }
}
