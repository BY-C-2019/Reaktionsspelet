using System;

namespace Reaktionsspelet
{
    class Program
    {
        static void Main(string[] args)
        {
            Game myGame = new Game();
			myGame.StartGame();
			Console.WriteLine("Spelet tog slut, resultatet var " + myGame.Results);
        }
    }
}