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
            string action = request.Split(',')[0];
            string response = "ERROR";
            switch (state)
            {
                case State.WAIT:
                    //Checks the state
                    if (action.Equals("ADD_USER"))
                    {
                        //Right state checks for right format
                        if (Regex.Match(request, @"^ADD_USER,[A-Za-z]+$").Success)
                        {
                            string name = request.Split(',')[1];
                            //Checks if user exists
                            if (canAddUser(name))
                            {
                                //Adds user, changes respose and state
                                addUser(name);
                                response = "DONE";
                                state = State.ADD_USERS;
                            }
                                //user already there
                            else
                            {
                                response = "ERROR_USER_EXISTS";
                            }
                        }
                    }
                        //Checks if score
                    else if (action.Equals("ADD_SCORE"))
                    {
                        //Checks right format
                        if (Regex.Match(request, @"^ADD_SCORE,[A-Za-z]+,[0-9]+$").Success)
                        {
                            string name = request.Split(',')[1];
                            int movecount = Int32.Parse(request.Split(',')[2]);
                            //Sees if it can update the score
                            if (updateScore(name, movecount))
                            {
                                //Can update changes state
                                response = "DONE";
                                state = State.ADD_SCORES;
                            }
                            else
                            {
                                response = "ERROR_UNKNOWN_USER";
                            }
                        }
                    }
                    break;
                case State.ADD_USERS:
                    //Checks the state for add user
                    if (action.Equals("ADD_USER"))
                    {
                        //Right state checks for right format
                        if (Regex.Match(request, @"^ADD_USER,[A-Za-z]+$").Success)
                        {
                            string name = request.Split(',')[1];
                            //Checks if user exists
                            if (canAddUser(name))
                            {
                                addUser(name);
                                response = "DONE";
                            }
                            //user already there
                            else
                            {
                                response = "ERROR_USER_EXISTS";
                            }
                        }
                    }
                        //Checks if end add users
                    else if(action.Equals("END_ADD_USERS"))
                    {
                        response = "OKAY";
                        state = State.WAIT;
                    }
                    break;
                case State.ADD_SCORES:
                    //checks if it can add score
                    if (action.Equals("ADD_SCORE"))
                    {
                        //Right state checks for right format
                        if (Regex.Match(request, @"^ADD_SCORE,[A-Za-z]+,[0-9]+$").Success)
                        {
                            string name = request.Split(',')[1];
                            int movecount = Int32.Parse(request.Split(',')[2]);
                            //checks if it can update
                            if (updateScore(name, movecount))
                            {
                                response = "DONE";
                                state = State.ADD_SCORES;
                            }
                            else
                            {
                                response = "ERROR_UNKNOWN_USER";
                            }
                        }
                    }
                        //Ending adding score
                    else if(action.Equals("END_ADD_SCORES"))
                    {
                        response = "OKAY";
                        state = State.WAIT;
                    }
                    break;
            }
            return response;
        }
        private void addUser(string name)
        {
            User newUser = new User();
            newUser.name = name;
            newUser.scores = new List<Score>();
            users.Add(newUser);
        }

        private bool updateScore(string name, int movecount)
        {
            bool exists = false;
            for (int i = 0; i < users.Count; ++i)
            {
                if (users[i].name.Equals(name))
                {
                    Score score = new Score();
                    score.moveCount = movecount;
                    users[i].scores.Add(score);
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        private bool canAddUser(string name)
        {
            bool exists = true;
            //Loops through all users to see if name exists 
            foreach (User user in users)
            {
                if (user.name.ToLower().Equals(name))
                {
                    exists = false;
                    break;
                }
            }
            return exists;
        }
    }
}
