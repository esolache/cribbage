using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using Util;

namespace Models {
    class CribbagePlay : CribbageDeck {

        private int sum;
        public int Sum {get {
            this.sum = 0;
            foreach (PokerCard card in this.Cards) {
                this.sum += card.PointValue;
            }
            return this.sum;
        }}
        
        public CribbagePlay() {
            this.Cards = new List<PokerCard>();

            return;
        }

        public int GetPointsForLastCardPlayed() {
            int result = 0;
            int numCards = this.Cards.Count;
            bool isFlushOfFive = false;
            bool isRunOfFive = false;
            bool isRunOfFour = false;
            bool isFlushOfFour = false;
            bool isRunOfThree = false;
            bool isQuadruple = false;
            bool isTriple = false;
            bool isDouble = false;

            if (this.Sum == 15) {
                Console.WriteLine("15 for 2!");
                result += 2;
            }
            
            if (this.Sum == 31) {
                Console.WriteLine("31 for 2!");
                result += 2;
            }

            if (numCards > 4) {
                isFlushOfFive = CribbagePoints.IsRunOfFive(this.Cards[(numCards-5)..(numCards)], true);
                isRunOfFive = CribbagePoints.IsRunOfFive(this.Cards[(numCards-5)..(numCards)], true);
            }
            if (numCards > 3) {
                isRunOfFour = CribbagePoints.IsRunOfFour(this.Cards[(numCards-4)..(numCards)], true);
                isFlushOfFour = CribbagePoints.IsFlushOfFour(this.Cards[(numCards-4)..(numCards)], true);
                isQuadruple = CribbagePoints.IsQuadruple(this.Cards[(numCards-4)..(numCards)], true);
            }
            if (numCards > 2) {
                isRunOfThree = CribbagePoints.IsRunOfThree(this.Cards[(numCards-3)..(numCards)], true);
                isTriple = CribbagePoints.IsTriple(this.Cards[(numCards-3)..(numCards)], true);
            }
            if (numCards > 1) {
                isDouble = CribbagePoints.IsDouble(this.Cards[(numCards-2)..(numCards)], true);
            }

            if (isFlushOfFive) {
                result += 5;
            }
            else if (isFlushOfFour) {
                result += 4;
            }

            if (isRunOfFive) {
                result += 5;
            }
            else if (isRunOfFour) {
                result +=4;
            }
            else if (isRunOfThree) {
                result += 3;
            }
            
            if (isQuadruple) {
                result += 12;
            }
            else if (isTriple) {
                result += 6;
            }
            else if (isDouble) {
                result += 2;
            }

            return result;
        }

        public override string ToString()
        {

            return
                "Sum: " + this.Sum + "\n" + 
                "Play:\n" + base.ToString();
            
        }
    }
}