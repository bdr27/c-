using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            string addUsersRegex = @"^ADD_USER,[A-Za-z]+,[0-9]+$";
            string response = "ERROR";
            switch (state)
            {
                case State.WAIT:
                    if (Regex.Match(request, addUsersRegex).Success)
                    {
                        state = State.ADD_USERS;
                        response = "SUCCESS";
                        
                        System.Diagnostics.Debug.WriteLine("State: ADD_USERS");
                        //System.Diagnostics.Debug.WriteLine("user: " + user);

                    }
                    break;
                case State.ADD_USERS:
                    if (Regex.Match(request, addUsersRegex).Success)
                    {
                        response = "SUCCESS";
                        string user = request.Split(',')[1];
                        System.Diagnostics.Debug.WriteLine("State: ADD_USERS");
                        System.Diagnostics.Debug.WriteLine("user: " + user);

                    }
                    break;
                case State.ADD_SCORES:
                    break;
            }
            return response;
        }
    }
}
