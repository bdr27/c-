using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMessagingSystem
{
    class UsersHandler
    {
        private State state;
        private List<User> users;

        public UsersHandler()
        {
            state = State.WAIT;
            users = new List<User>();
        }

        public string handleRequest(string request)
        {
            return "ERROR";
        }
    }
}
