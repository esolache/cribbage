namespace Models
{
    abstract class AbstractDeck {
        public List<PokerCard> Cards { get; set; }

        public AbstractDeck() {
            this.Cards = new List<PokerCard>();
        }
        public void Shuffle() {
            Random rand = new Random();
            for (int i = 0; i < this.Cards.Count; i++) {
                int randIndex = rand.Next(this.Cards.Count);
                PokerCard temp = this.Cards[randIndex];
                this.Cards[randIndex] = this.Cards[i];
                this.Cards[i] = temp;
            }
        }

        public List<PokerCard> Deal(int numCards) {
            if (numCards > this.Cards.Count) {
                return new List<PokerCard>();
            }

            List<PokerCard> result = this.Cards[0..numCards];

            foreach(PokerCard card in result) {
                this.RemoveCard(card);
            }

            return result;
        }

        public PokerCard? RemoveCard(PokerCard remove) {
            try {
                this.Cards.Remove(this.Cards.Single(card => card.Suit == remove.Suit && card.CardValue == remove.CardValue ));
                return remove;
                }
            catch (Exception) {
                return null;
            }
        }
        public void AddCard(PokerCard add) {
            this.Cards.Add(add);
            return;
        }

        public void SortCards() {
            this.Cards = this.Cards.OrderBy(o=>o.CardValue).ToList();;
        }

        public override string ToString() {
            string result = "";
            //List<PokerCard> temp = this.Cards.OrderBy(o=>o.CardValue).ToList();;

            int index = 0;
            foreach (PokerCard card in this.Cards) {
                result += index + ": [" + card.ToString() + "]\n";
                index++;
            }

            return result;
        }

    }

}