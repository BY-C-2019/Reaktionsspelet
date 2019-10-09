using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace ClassLibrary {

    public static class Menu {
        
        public static int DisplayMenu(string[] menuOptions, string header) {
            int currentIndex = 0;
            ConsoleKey keyPress;
            string selectionArrow = "-> ";
            while (true) {
                Console.Clear();
                Console.WriteLine(header);
                foreach (string s in menuOptions) {
                    string finalOption = "";
                    if (Array.IndexOf(menuOptions, s) == currentIndex) {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        finalOption += selectionArrow;
                    } else {
                        finalOption += "   ";
                    }
                    finalOption += s;
                    Console.WriteLine(finalOption);
                    Console.ForegroundColor = ConsoleColor.White;
                }

                keyPress = Console.ReadKey(true).Key;

                if (keyPress == ConsoleKey.UpArrow) {
                    currentIndex = (currentIndex == 0) ? menuOptions.Length - 1 : currentIndex - 1;
                }
                else if (keyPress == ConsoleKey.DownArrow) {
                    currentIndex = (currentIndex == menuOptions.Length - 1) ? 0 : currentIndex + 1;
                }
                else if (keyPress == ConsoleKey.Enter) {
                    return currentIndex;
                }
            }
        }    
        
        public static bool YesOrNo(string header) {
            string[] m = {"Yes", "No"};
            return (DisplayMenu(m, header) == 0);
        }

        public static double MenuForAmount(string header, double max, bool smallScale) {
            double amount = 1;
            double increment = 0.5;
            ConsoleKey keyPress;
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (true) {
                Console.Clear();
                Console.Write(header + amount + "\n");
                keyPress = Console.ReadKey(true).Key;

                if (stopWatch.ElapsedMilliseconds >= 75) {
                    if (smallScale) {
                        increment = 0.1;
                    }
                    else {
                        increment = (amount >= 2) ? 1 : 0.5;
                        increment = (amount >= 10) ? 5 : increment;
                    }


                    if (keyPress == ConsoleKey.DownArrow) {
                        amount = (amount - increment <= increment) ? increment : amount - increment;
                        stopWatch.Restart();
                    }
                    else if (keyPress == ConsoleKey.UpArrow) {
                        if (max == -1) {
                            amount += increment; 
                        }
                        else {
                            amount = (amount + increment >= max) ? max : amount + increment;
                        }
                        stopWatch.Restart();
                    }
                    else if (keyPress == ConsoleKey.Enter) {
                        stopWatch.Stop();
                        return amount;
                    }
                }
            }
        }
    }    
}