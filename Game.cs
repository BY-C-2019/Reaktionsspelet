using System;

namespace Reaktionsspelet
{
	// Ett reaktionsspel
	class Game
	{
		// X - tiden i millisekunder innan NOW skrivs ut.
		private int sleepTime;
		// Tickcount när vi börjar vänta.
		private int sleepTick;
		// Y - tickcount när NOW skrivs ut.
		private int startTick;
		// Z - tickcount när användaren trycker på skärmen.
		private int endTick;

		// Resultatet av spelet
		private int results;

		public int Results {
			get {
				return results;
			}
		}

		public Game() {
			Random random = new Random();
			sleepTime = 1000 * random.Next(3, 10);
		}

		public void StartGame() {
			Console.WriteLine("OK, get ready...");
			sleepTick = System.Environment.TickCount;
			int endTick = sleepTick + sleepTime;

			// Under väntetiden...
			while (System.Environment.TickCount < endTick) {
				if (Console.KeyAvailable) {
					return EndGame(true);
				}
			}

			// Kör nu!
			Console.WriteLine("NOW!!!!!");
			startTick = System.Environment.TickCount;
			while (true) {
				if (Console.KeyAvailable) {
					return EndGame(false);
				}
			}
		}

		private void EndGame(bool didCheat) {
			if (didCheat) {
				results = 1000000;
				Console.WriteLine("Du fuskade! skäms!");
			}
			else {
				endTick = System.Environment.TickCount;
				results = endTick - startTick;
				Console.WriteLine("Reaktionstiden i millisekunder var: " + results);
			}
		}
	}
}