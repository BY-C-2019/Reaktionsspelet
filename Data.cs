using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;

namespace reaktionsspelet
{
    public static class Data
    {
        public static void ViewHighscore() {
            SqlConnection cnn = DatabaseConnect();
            string sql = "SELECT playerName, playerHighscore FROM players WHERE playerHighscore <> -1;";
            SqlDataReader reader = new SqlCommand(sql, cnn).ExecuteReader();
            List<Player> players = new List<Player>();

            while (reader.Read()) {
                Player p = new Player((string)reader.GetValue(0), (int)reader.GetValue(1));
                players.Add(p);
            }

            players = players.OrderBy(o=>o.Highscore).ToList();

            int loopCondition = (players.Count < 10) ? players.Count : 10;

            for (int i = 0; i < loopCondition; i++) {
                Console.WriteLine("{0}: {1} with a score of {2} milliseconds", 
                                    i + 1, players[i].Name, players[i].Highscore);
            }

        }
        public static void UpdateHighscore(string playerName, long newScore) {
            SqlConnection cnn = DatabaseConnect();
            string sql = "UPDATE players SET playerHighscore = "+newScore+" WHERE playerName = '"+playerName+"';";
            new SqlCommand(sql, cnn).ExecuteNonQuery();

            cnn.Close();
        }

        public static Player UserLogin() {
            SqlConnection cnn = DatabaseConnect();
            string name, passcode;
            while (true) {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                Console.Write("Enter passcode: ");
                passcode = Console.ReadLine();

                string sql = "SELECT * FROM players WHERE playerName = '"+ name +"' AND playerPasscode = "+passcode+";";
                SqlCommand cmd = new SqlCommand(sql, cnn);
                SqlDataReader reader = cmd.ExecuteReader();
                Player p = new Player("foo", -2);

                while (reader.Read()) {
                    p = new Player((string)reader.GetValue(1), (int)reader.GetValue(3));
                }   

                reader.Close();

                if (p.Highscore != -2) {
                    cnn.Close();
                    return p;
                } else {
                    Console.WriteLine("Couldn't find entered player. Try again.");
                }
            }
        }

        public static Player CreateUser() {
            SqlConnection cnn = DatabaseConnect();
            string sql = "SELECT * FROM players;";
            bool nameExists = false;
            string name, passcode;
            do {
                Console.Write("Enter name: ");
                name = Console.ReadLine();
                Console.Write("Enter passcode: ");

                passcode = Console.ReadLine();
                SqlDataReader reader = new SqlCommand(sql, cnn).ExecuteReader();

                while (reader.Read()) {
                    if ((string)reader.GetValue(1) == name) {
                        nameExists = true;
                        Console.WriteLine("That name already exists!");
                    }
                }
                reader.Close();
            } while (nameExists);

            sql = "INSERT INTO players (playerName, playerPasscode, playerHighscore)"+ 
                "VALUES ('"+name+"', "+passcode+", -1);";
            new SqlCommand(sql, cnn).ExecuteNonQuery();            
            
            cnn.Close();

            return new Player(name, -1);
        }

        static SqlConnection DatabaseConnect() {
            string connectionString = @"Data Source=40.85.84.155;"+
            "Initial Catalog=Student14;User ID=Student14;Password=YH-student@2019";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();
            return cnn;
        }
    }
}