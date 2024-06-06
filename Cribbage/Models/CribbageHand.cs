using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Models {
    class CribbageHand : CribbageDeck {

        public PokerCard? Pull {get; set;}

        private int sum;
        public int Sum {get {
            this.sum = 0;
            foreach (PokerCard card in this.Cards) {
                this.sum += card.PointValue;
            }
            return this.sum;
        }}  
        private int points;
        public int Points {get {
            this.points = 0;
            
            List<PokerCard> temp = new List<PokerCard>();

            if (this.Pull != null) {
                temp.Add(this.Pull);
            }
            
            foreach (PokerCard card in this.Cards) {
                temp.Add(card);
            }

            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            for (int i = 0; i < temp.Count; i++) {
                bool isFlushOfFive = false;
                bool isFlushOfFour = false;

                bool isRunOfFive = false;
                bool isRunOfFour = false;
                bool isRunOfThree = false;


                    bool isNobs = this.Pull != null
                            && temp[i].CardValue == CardValue.Jack 
                            && temp[i].Suit == this.Pull.Suit;
                
                // Count Nobs
                if (isNobs) {
                    this.points +=1;
                }

                for (int j = i+1; j < temp.Count; j++ ) {
                    bool isDouble = temp[i].CardValue == temp[j].CardValue;
                    bool isFifteenDouble = temp[i].PointValue + temp[j].PointValue == 15;
                    
                    // Count Sets of 2 == 15s
                    if (isFifteenDouble) {
                        this.points += 2;
                    }

                    // Count Doubles
                    if (isDouble) {
                        this.points += 2;
                    }


                    for (int k = j+1; k < temp.Count; k++) {
                    
                    isRunOfThree = CribbageHand.IsRunOfThree(temp,i,j,k);
                        

                    bool isFifteenTriple = temp[i].PointValue 
                                            + temp[j].PointValue 
                                            + temp[k].PointValue == 15;

                        if (isFifteenTriple) {
                            points += 2;
                        }

                        for (int l = k+1; l < temp.Count; l++) {
                            isFlushOfFour = CribbageHand.IsFlushOfFour(temp, i,j,k,l);

                            isRunOfFour = CribbageHand.IsRunOfFour(temp, i,j,k,l);


                            bool isFifteenQuadruple = temp[i].PointValue 
                                            + temp[j].PointValue 
                                            + temp[k].PointValue 
                                            + temp[l].PointValue == 15;
                                            
                            if (isFifteenQuadruple) {
                                points += 2;
                            }
                            for (int m = l+1; m < temp.Count; m++) {
                                isRunOfFive = CribbageHand.IsRunOfFive(temp, i,j,k,l,m);
                                
                                isFlushOfFive = CribbageHand.IsFlushOfFive(temp, i,j,k,l,m);

                                bool isFifteenQuintuple = temp[i].PointValue 
                                                + temp[j].PointValue 
                                                + temp[k].PointValue 
                                                + temp[l].PointValue == 15;
                                                
                                if (isFifteenQuintuple) {
                                    this.points += 2;
                                }

                                
                                
                            }
                            if (isRunOfFive) {
                                    this.points += 5;
                                    isRunOfFive = false;
                                    isRunOfFour = false;
                                    isRunOfThree = false;
                                }

                            if (isFlushOfFive) {
                                this.points += 5;
                                isFlushOfFive = false;
                                isFlushOfFour = false;
                            }

                            

                        }
                        if (isRunOfFour) {
                                this.points += 4;
                                isRunOfFour = false;
                                isRunOfThree = false;
                            }

                            if (isFlushOfFour) {
                                isFlushOfFour = false;
                                this.points += 4;
                            }
                        
                    }
                    if (isRunOfThree) {
                            isRunOfThree = false;
                            this.points += 3;
                        }
                }
            }

            return this.points;
        }}
        

        public CribbageHand() {
            this.Pull = null;
            this.Cards = new List<PokerCard>();

            return;
        }

        public void AddPull(PokerCard card) {
            this.Pull = card;
        }

        public override string ToString()
        {
            if (this.Pull == null) {
                return
                    "Points: " + this.Points + "\n" + 
                    "Hand:\n" + base.ToString();
            }

            return
                "Points: " + this.Points + "\n" + 
                "Pull: " + this.Pull.ToString() + "\n"+
                "Hand:\n" + base.ToString();
            
        }
        public string HandToString()
        {
            return 
            "Hand:\n" + base.ToString();
        }

        public static bool IsRunOfFive(List<PokerCard> temp, int i, int j, int k, int l, int m) {
            bool result =  (int) temp[i].CardValue == (int) temp[j].CardValue - 1
                        && (int) temp[j].CardValue == (int) temp[k].CardValue - 1
                        && (int) temp[k].CardValue == (int) temp[l].CardValue - 1
                        && (int) temp[l].CardValue == (int) temp[m].CardValue - 1;


            if (result) {
                // Console.WriteLine("\nRun of Five\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", m, temp[m].ToString()));
            }




            return result;
        }

        public static bool IsRunOfFour(List<PokerCard> temp, int i, int j, int k, int l) {
            bool result = (int) temp[i].CardValue == (int) temp[j].CardValue - 1
            && (int) temp[j].CardValue == (int) temp[k].CardValue - 1
            && (int) temp[k].CardValue == (int) temp[l].CardValue - 1;

            
            if (result) {
                // Console.WriteLine("\nRun of Four\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));
            }
            


            return result;
        }

        public static bool IsRunOfThree(List<PokerCard> temp, int i, int j, int k) {
            bool result = (int) temp[i].CardValue == (int) temp[j].CardValue - 1
            && (int) temp[j].CardValue == (int) temp[k].CardValue - 1;

            
            
            if (result) {
                // Console.WriteLine("\nRun of Three\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));

            }

            return result;
        }

        public static bool IsFlushOfFive(List<PokerCard> temp, int i, int j, int k, int l, int m) {
            bool result = temp[i].Suit == temp[j].Suit
            && temp[j].Suit == temp[k].Suit
            && temp[k].Suit == temp[l].Suit
            && temp[l].Suit == temp[m].Suit;

            if (result) {
                // Console.WriteLine("\nFlush of Five\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", m, temp[m].ToString()));

            }


            return result;
        }

        public static bool IsFlushOfFour(List<PokerCard> temp, int i, int j, int k, int l) {
            bool result = temp[i].Suit == temp[j].Suit
            && temp[j].Suit == temp[k].Suit
            && temp[k].Suit == temp[l].Suit;

            if (result) {
                // Console.WriteLine("\nFlush of Four\n");

                // Console.WriteLine(String.Format("[{0}]: {1}", i, temp[i].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", j, temp[j].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", k, temp[k].ToString()));
                // Console.WriteLine(String.Format("[{0}]: {1}", l, temp[l].ToString()));

            }
            

            return result;
        }

        public List<PokerCard> GetCopyCardsInHand()
        {
            List<PokerCard> result = new List<PokerCard>();

            foreach(PokerCard card in this.Cards) {
                result.Add(card);
            }

            return result;
        }

    }
}