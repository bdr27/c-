using System;
using System.Collections.Generic;
using System.Linq;
using MiniCheckers.Utility;

namespace MiniCheckers
{
    class Program
    {
        static DBInteraction dbInteraction = new DBInteraction();
        static int failedTestsAmount = 0;
        static int numberOfTest = 1;
        static List<int> ListFailedTests = new List<int>();

        static void Main(string[] args)
        {
            ValidLoginTest("jack", "12345");
            InvalidLoginTest("jack", "12345");
            ValidLoginTest("jill", "54321");
            ValidLoginTest("douglas", "42");
            InvalidLoginTest("jill", "54321");
            InvalidLoginTest("douglas", "42");
            CheckDictionary(dbInteraction.GetPlayers());
            AddNewHighScore(1, "jack", 5);
            dbInteraction.LoadHighScores();
            CheckHighScores(dbInteraction.GetHighScores());

            Console.WriteLine("\nNumber of failed tests: " + failedTestsAmount);
            foreach (var failedTest in ListFailedTests)
            {
                Console.WriteLine("Test Failed : " + failedTest);
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }

        private static void AddNewHighScore(int playerID, string username, int score)
        {
            dbInteraction.insertHighScore(playerID, new Score(username, score));
        }

        private static void CheckHighScores(List<Score> highScores)
        {
            foreach (var highScore in highScores)
            {
                Console.WriteLine(numberOfTest++ + ". " + highScore.ToString());
            }
        }

        private static void CheckDictionary(Dictionary<int, string> players)
        {
            foreach (var player in players)
            {
                Console.WriteLine(numberOfTest++ + ". Key: " + player.Key + " Username: " + player.Value);
            }
        }

        private static void ValidLoginTest(string username, string password)
        {
            var result = dbInteraction.isValidLogin(username, password);
            if (result == false)
            {
                Console.Write("\t");
                failedTestsAmount++;
                ListFailedTests.Add(numberOfTest);
            }
            Console.WriteLine(String.Format("{0}. Attempt to log in [{1}, {2}] (True) : {3}", numberOfTest++, username, password, result));
        }

        private static void InvalidLoginTest(string username, string password)
        {
            var result = dbInteraction.isValidLogin(username, password);
            if (result == true)
            {
                Console.Write("\t");
                failedTestsAmount++;
                ListFailedTests.Add(numberOfTest);
            }
            Console.WriteLine(string.Format("{0}. Try to log {1} in again (False): {2}", numberOfTest++, username, result));
        }
    }
}
