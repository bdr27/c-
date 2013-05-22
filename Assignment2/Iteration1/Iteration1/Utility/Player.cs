using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniCheckers.Utility
{
    public class Player
    {
        private int ID;
        private string username;

        public Player(int ID, string username)
        {
            this.ID = ID;
            this.username = username;
        }

        public int GetID()
        {
            return ID;
        }

        public string GetUsername()
        {
            return username;
        }

        public override string ToString()
        {
            return "ID: " + ID + " username : " + username;
        }
    }
}
