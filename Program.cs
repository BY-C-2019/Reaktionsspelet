using System;
using System.Diagnostics;
using System.Threading;
using ClassLibrary;

namespace reaktionsspelet
{
    class Program
    {
        public static Player currentPlayer;
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the reaction-speed game!");
            currentPlayer = new Player(Menu.YesOrNo("Do you have an account?"));     

            while (true) {
                string highscore = (currentPlayer.Highscore != -1) ? Convert.ToString(currentPlayer.Highscore) : "You don't have a score yet!";
                switch (Menu.DisplayMenu(new string[]{"Play!", "View Highscorelist", "Exit"}, 
                                        currentPlayer.Name + ": " + highscore)) {
                    case 0:
                        Game();
                        break;
                    case 1:
                        Data.ViewHighscore();
                        Console.ReadLine();
                        break;
                    case 2:
                        return;
                }
            }
            
        }

        static void Game() {
            long lastTime = -1;                        
            do {                
                lastTime = Play();
                if (lastTime == -1) {
                    Console.WriteLine("CHEATER!");
                }
                else {
                    string comment = (lastTime > 1000) ? "A bit slow!" : "Well done!";
                    Console.WriteLine("Your time was {0} milliseconds. {1}", lastTime, comment);
                    if (lastTime < currentPlayer.Highscore) {
                        if (currentPlayer.Highscore != -1) {
                            Console.WriteLine("You beat your record with {0} milliseconds!", currentPlayer.Highscore - lastTime);
                            currentPlayer.Highscore = lastTime;
                        }
                        else {
                            Console.WriteLine("You got the new highscore {0} milliseconds!", lastTime);
                            currentPlayer.Highscore = lastTime;
                        }
                    }
                }
                Console.Write("Play again? Y/N: ");
            } while (Console.ReadLine().ToUpper() != "N");
        }

        static long Play() {
            Stopwatch s = new Stopwatch();
            Stopwatch antiCheat = new Stopwatch();
            Console.WriteLine("When you see [NOW], press a key as fast as you can!");
            antiCheat.Start();
            int timer = new Random().Next(3, 11) * 1000;
            do {
                if (Console.KeyAvailable) {                    
                    return -1;
                }

            } while (antiCheat.ElapsedMilliseconds < timer);
            // Thread.Sleep(new Random().Next(3, 11) * 1000);
            antiCheat.Stop();
            Console.WriteLine("NOW!");
            s.Start();
            Console.ReadKey(true);
            s.Stop();
            return s.ElapsedMilliseconds;
        }
    }    
}
