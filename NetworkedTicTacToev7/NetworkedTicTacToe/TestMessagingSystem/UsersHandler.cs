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
            string response = "ERROR";
            
            switch (state)
            {
                case State.WAIT:
                    if (Regex.Match(request, @"^ADD_USER,[A-Za-z]+$").Success)
                    {
                        state = State.ADD_USERS;

                        string[] elements = request.Split(',');
                        User user = new User();
                        user.name = elements[1];
                        user.scores = new List<Score>();
                        users.Add(user);

                        response = "DONE";
                    }
                    else if (Regex.Match(request, @"^ADD_SCORE,[A-Za-z]+,[0-9]+$").Success)
                    {
                        string[] elements = request.Split(',');
                        string name = elements[1];

                        if (UserExists(name))
                        {
                            state = State.ADD_SCORES;

                            int moveCount = int.Parse(elements[2]);
                            Score score = new Score();
                            score.date = DateTime.Now;
                            score.moveCount = moveCount;

                            int i = findUserIndex(name);
                            users[i].scores.Add(score);
                            response = "DONE";
                        }
                        else
                        {
                            response = "ERROR_UNKNOWN_USER";
                        }
                    }
                    break;
                case State.ADD_USERS:
                    if (Regex.Match(request, @"^ADD_USER,[A-Za-z]+$").Success)
                    {
                        state = State.ADD_USERS;
                        string[] elements = request.Split(',');
                        User user = new User();
                        user.name = elements[1];
                        user.scores = new List<Score>();
                        users.Add(user);

                        response = "DONE";
                    }
                    else if (request == "END_ADD_USERS")
                    {
                        state = State.WAIT;
                        response = "OKAY";
                    }
                    break;
                case State.ADD_SCORES:
                    if (Regex.Match(request, @"^ADD_SCORE,[A-Za-z]+,[0-9]+$").Success)
                    {
                        string[] elements = request.Split(',');
                        string name = elements[1];

                        if (UserExists(name))
                        {
                            int moveCount = int.Parse(elements[2]);
                            Score score = new Score();
                            score.date = DateTime.Now;
                            score.moveCount = moveCount;

                            int i = findUserIndex(name);
                            users[i].scores.Add(score);

                            response = "DONE";
                        }
                        else
                        {
                            response = "ERROR_UNKNOWN_USER";
                        }
                    } else if (request == "END_ADD_SCORES") {
                        state = State.WAIT;
                        response = "OKAY";
                    }
                    break;
            }

            return response;
        }

        private int findUserIndex(string name)
        {
            for (var i = 0; i < users.Count; ++i)
            {
                if (users[i].name == name) return i;
            }
            return -1;
        }

        private bool UserExists(string name)
        {
            foreach (var user in users)
            {
                if (user.name == name) return true;
            }
            return false;
        }
    }
}

