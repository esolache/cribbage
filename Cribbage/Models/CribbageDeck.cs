namespace Models
{
    class CribbageDeck : AbstractDeck {
        
        public CribbageDeck() : base() {
            
            foreach (Suit suit in Enum.GetValues(typeof(Suit))) {
                foreach( CardValue cardValue in Enum.GetValues(typeof(CardValue))) {
                    if ( cardValue > CardValue.Ten) {
                        this.AddCard(new PokerCard(suit, cardValue, 10));
                    }
                    else {
                        this.AddCard(new PokerCard(suit, cardValue, (int) cardValue));
                    }
                    
                }
            }
        }
        public CribbageDeck(List<PokerCard> cards) {
            this.Cards = cards;
        }

        public static PokerCard CompareCardCardValue(PokerCard a, PokerCard b)
        {
            if (a.CardValue == b.CardValue)
            {
                return a.Suit > b.Suit ? a : b;
            }
            else
            {
                return a.CardValue > b.CardValue ? a : b;
            }
        }

        public static PokerCard CompareCardPointValue(PokerCard a, PokerCard b)
        {
            if (a.PointValue == b.PointValue)
            {
                return a.Suit > b.Suit ? a : b;
            }
            else
            {
                return a.PointValue > b.PointValue ? a : b;
            }
        }
    }
}