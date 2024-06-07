using System.Runtime.InteropServices;
using Models;
namespace Util {
    static class CribbagePoints {

        public static bool IsFifteen(List<PokerCard> temp, bool print = false) {
            bool result = false;
            int sum = 0;

            foreach (PokerCard card in temp) {
                sum += card.PointValue;
            }
            
            result = sum == 15;

            if (print && result) {
                Console.WriteLine("\n15 for 2!\n");
            }
            
            return result;
        }
        public static bool IsDouble(List<PokerCard> temp, bool print = false) {
            bool result = temp[0].CardValue == temp[1].CardValue;

            if (print && result) {
                Console.WriteLine("\nDouble\n");
            }
            
            return result;
        }
        public static bool IsTriple(List<PokerCard> temp, bool print = false) {
            bool result = temp[0].CardValue == temp[1].CardValue
                        && temp[1].CardValue == temp[2].CardValue;

            if (print && result) {
                Console.WriteLine("\nTriple\n");
            }
            
            return result;
        }

        public static bool IsQuadruple(List<PokerCard> temp, bool print = false) {
            bool result = temp[0].CardValue == temp[1].CardValue
                        && temp[1].CardValue == temp[2].CardValue
                        && temp[2].CardValue == temp[3].CardValue;

            if (print && result) {
                Console.WriteLine("\nQuadruple\n");
            }
            
            return result;
        }

        public static bool IsRunOfFive(List<PokerCard> temp, bool print = false) {
            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            bool result =  (int) temp[0].CardValue == (int) temp[1].CardValue - 1
                        && (int) temp[1].CardValue == (int) temp[2].CardValue - 1
                        && (int) temp[2].CardValue == (int) temp[3].CardValue - 1
                        && (int) temp[4].CardValue == (int) temp[5].CardValue - 1;


            if (print && result) {
                Console.WriteLine("\nRun of Five\n");
            }

            return result;
        }

        public static bool IsRunOfFour(List<PokerCard> temp, bool print = false) {
            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            bool result = (int) temp[0].CardValue == (int) temp[1].CardValue - 1
            && (int) temp[1].CardValue == (int) temp[2].CardValue - 1
            && (int) temp[2].CardValue == (int) temp[3].CardValue - 1;

            
            if (print && result) {
                Console.WriteLine("\nRun of Four\n");
            }
            


            return result;
        }

        public static bool IsRunOfThree(List<PokerCard> temp, bool print = false) {
            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            bool result = (int) temp[0].CardValue == (int) temp[1].CardValue - 1
            && (int) temp[1].CardValue == (int) temp[2].CardValue - 1;

            
            
            if (print && result) {
                Console.WriteLine("\nRun of Three\n");
            }

            return result;
        }

        public static bool IsFlushOfFive(List<PokerCard> temp, bool print = false) {
            bool result = temp[0].Suit == temp[1].Suit
            && temp[1].Suit == temp[2].Suit
            && temp[2].Suit == temp[3].Suit
            && temp[3].Suit == temp[4].Suit;

            if (print && result) {
                Console.WriteLine("\nFlush of Five\n");
            }


            return result;
        }

        public static bool IsFlushOfFour(List<PokerCard> temp, bool print = false) {
            bool result = temp[0].Suit == temp[1].Suit
            && temp[1].Suit == temp[2].Suit
            && temp[2].Suit == temp[3].Suit;

            if (print && result) {
                Console.WriteLine("\nFlush of Four\n");
            }
            

            return result;
        }
        
    }
}