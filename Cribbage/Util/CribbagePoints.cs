using Models;
namespace Util {
    static class CribbagePoints {
        public static bool IsDouble(List<PokerCard> temp) {
            bool result = temp[0].CardValue == temp[1].CardValue;
            if (result) {
                Console.WriteLine("\nDouble\n");
            }
            
            return result;
        }
        public static bool IsTriple(List<PokerCard> temp) {
            bool result = temp[0].CardValue == temp[1].CardValue
                        && temp[1].CardValue == temp[2].CardValue;

            if (result) {
                Console.WriteLine("\nTriple\n");
            }
            
            return result;
        }

        public static bool IsQuadruple(List<PokerCard> temp) {
            bool result = temp[0].CardValue == temp[1].CardValue
                        && temp[1].CardValue == temp[2].CardValue
                        && temp[2].CardValue == temp[3].CardValue;

            if (result) {
                Console.WriteLine("\nQuadruple\n");
            }
            
            return result;
        }

        public static bool IsRunOfFive(List<PokerCard> temp) {
            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            bool result =  (int) temp[0].CardValue == (int) temp[1].CardValue - 1
                        && (int) temp[1].CardValue == (int) temp[2].CardValue - 1
                        && (int) temp[2].CardValue == (int) temp[3].CardValue - 1
                        && (int) temp[4].CardValue == (int) temp[5].CardValue - 1;


            if (result) {
                Console.WriteLine("\nRun of Five\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", m, temp[m].ToString()));
            }




            return result;
        }

        public static bool IsRunOfFour(List<PokerCard> temp) {
            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            bool result = (int) temp[0].CardValue == (int) temp[1].CardValue - 1
            && (int) temp[1].CardValue == (int) temp[2].CardValue - 1
            && (int) temp[2].CardValue == (int) temp[3].CardValue - 1;

            
            if (result) {
                Console.WriteLine("\nRun of Four\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));
            }
            


            return result;
        }

        public static bool IsRunOfThree(List<PokerCard> temp) {
            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            bool result = (int) temp[0].CardValue == (int) temp[1].CardValue - 1
            && (int) temp[1].CardValue == (int) temp[2].CardValue - 1;

            
            
            if (result) {
                Console.WriteLine("\nRun of Three\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));

            }

            return result;
        }

        public static bool IsFlushOfFive(List<PokerCard> temp) {
            bool result = temp[0].Suit == temp[1].Suit
            && temp[1].Suit == temp[2].Suit
            && temp[2].Suit == temp[3].Suit
            && temp[3].Suit == temp[4].Suit;

            if (result) {
                Console.WriteLine("\nFlush of Five\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", m, temp[m].ToString()));

            }


            return result;
        }

        public static bool IsFlushOfFour(List<PokerCard> temp) {
            bool result = temp[0].Suit == temp[1].Suit
            && temp[1].Suit == temp[2].Suit
            && temp[2].Suit == temp[3].Suit;

            if (result) {
                Console.WriteLine("\nFlush of Four\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));

            }
            

            return result;
        }
        
    }
}