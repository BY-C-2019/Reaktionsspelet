using System;
using System.Diagnostics;
using System.Threading;

namespace reaktionsspelet
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Välkommen till Reaktionsspelet!");
            long lastTime = -1;
            
            do {                
                lastTime = Game();
                if (lastTime == -1) {
                    Console.WriteLine("CHEATER!");
                }
                else {
                    string comment = (lastTime > 1000) ? "Lite långsamt!" : "Bra jobbat!";
                    Console.WriteLine("Din tid var {0} millisekunder. {1}", lastTime, comment);
                }
                Console.Write("Vill du köra igen? Y/N: ");
            } while (Console.ReadLine().ToUpper() != "N");
        }

        static long Game() {
            Stopwatch s = new Stopwatch();
            Stopwatch antiCheat = new Stopwatch();
            Console.WriteLine("När du ser texten [NU!], klicka så snabbt du kan på mellanslag.");
            antiCheat.Start();
            int timer = new Random().Next(3, 11) * 1000;
            do {
                if (Console.KeyAvailable) {                    
                    return -1;
                }

            } while (antiCheat.ElapsedMilliseconds < timer);
            // Thread.Sleep(new Random().Next(3, 11) * 1000);
            antiCheat.Stop();
            Console.WriteLine("NU!");
            s.Start();
            Console.ReadKey();
            s.Stop();
            return s.ElapsedMilliseconds;
        }
    }    
}
