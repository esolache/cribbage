using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Util;
namespace Models {
    class CribbageHand : CribbageDeck {

        public PokerCard? Pull {get; set;}
        private int points;
        public int Points {get {
            this.points = 0;
            
            List<PokerCard> temp = new List<PokerCard>();

            if (this.Pull != null) {
                temp.Add(this.Pull);
            }
            
            foreach (PokerCard card in this.Cards) {
                temp.Add(card);

                // Count Nobs
                if (this.Pull != null 
                    && card.CardValue == CardValue.Jack 
                    && card.Suit == this.Pull.Suit) {
                    this.points++;
                }
            }

            temp = temp.OrderBy(o=>o.CardValue).ToList();;

            List<List<PokerCard>> allCardCombinations = CribbageHand.GetAllCardCombinations(temp);
            allCardCombinations = allCardCombinations.OrderByDescending(o=>o.Count).ToList();;

            bool isFlushOfFive = false;
            bool noFlushOfFive = true;

            bool isFlushOfFour = false;

            bool isRunOfFive = false;
            bool noRunOfFive = true;

            bool isRunOfFour = false;
            bool noRunOfFour = true;
            
            bool isRunOfThree = false;


            foreach (List<PokerCard> currCombo in allCardCombinations) {
                bool isDouble = false;
                bool isFifteen = CribbagePoints.IsFifteen(currCombo);

                if (currCombo.Count == 5) {
                    isFlushOfFive = CribbagePoints.IsRunOfFive(currCombo);
                    isRunOfFive = CribbagePoints.IsRunOfFive(currCombo);
                }
                if (currCombo.Count == 4) {
                    isRunOfFour = CribbagePoints.IsRunOfFour(currCombo);
                    isFlushOfFour = CribbagePoints.IsFlushOfFour(currCombo);
                }
                if (currCombo.Count == 3) {
                    isRunOfThree = CribbagePoints.IsRunOfThree(currCombo);
                }
                if (currCombo.Count == 2) {
                    isDouble = CribbagePoints.IsDouble(currCombo);
                }

                if (isFlushOfFive) {
                    this.points += 5;
                    noFlushOfFive = false;
                }
                else if (isFlushOfFour && noFlushOfFive) {
                    this.points += 4;
                }

                if (isRunOfFive) {
                    this.points += 5;
                    noRunOfFive = false;
                }
                else if (isRunOfFour && noRunOfFive) {
                    this.points +=4;
                    noRunOfFour = false;
                }
                else if (isRunOfThree && noRunOfFour) {
                    this.points += 3;
                }

                if (isDouble) {
                    this.points += 2;
                }
                if (isFifteen) {
                    this.points += 2;
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

        public List<PokerCard> GetCopyCardsInHand()
        {
            List<PokerCard> result = new List<PokerCard>();

            foreach(PokerCard card in this.Cards) {
                result.Add(card);
            }

            return result;
        }

        public static List<List<PokerCard>> GetAllCardCombinations(List<PokerCard> temp) { 
            List<List<PokerCard>> result = new List<List<PokerCard>>();

            if (temp.Count < 2) {
                result.Add(temp);
                return result;
            }

            int count = (int) Math.Pow(2, temp.Count);
            for (int i = 1; i <= count - 1; i++)
            {
                List<PokerCard> currList = new List<PokerCard>();
                string str = Convert.ToString(i, 2).PadLeft(temp.Count, '0');
                for (int j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        currList.Add(temp[j]);
                    }
                }

                if (currList.Count > 1) {
                    result.Add(currList);
                }
            }

            return result;
        }

        // what if we check first the combination of 5
        // if combination of 5 is run or flush
        // we negate any run or flush combos of 4
        // 15s are still ok to check

        // then we check all combinations of 4
        // if combination of 4 is run
        // then we negate any run combos of 3
        // 15s are still ok to check

        // then we check all the combinations of 3
        // check for 15s and runs

        // independently check all the combinations of 2
        // for 15s and doubles

    }
}