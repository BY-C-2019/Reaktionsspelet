using System;
using System.Diagnostics;
using System.Threading;

namespace reaktionsspelet
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to the reaction-speed game!");
            long lastTime = -1;
            
            do {                
                lastTime = Game();
                if (lastTime == -1) {
                    Console.WriteLine("CHEATER!");
                }
                else {
                    string comment = (lastTime > 1000) ? "A bit slow!" : "Well done!";
                    Console.WriteLine("Your time was {0} milliseconds. {1}", lastTime, comment);
                }
                Console.Write("Play again? Y/N: ");
            } while (Console.ReadLine().ToUpper() != "N");
        }

        static long Game() {
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
            Console.ReadKey();
            s.Stop();
            return s.ElapsedMilliseconds;
        }
    }    
}
