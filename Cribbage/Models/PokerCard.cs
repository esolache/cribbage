namespace Models {

    enum Suit{
        Heart,
        Club,
        Diamond,
        Spades
    }

    enum CardValue {
        Ace = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13

    }

    class PokerCard {

        public Suit Suit {get;}
        public CardValue CardValue {get;}
        public int PointValue {get;}
        
        public PokerCard(Suit suit, CardValue cardValue) {
            this.Suit = suit;
            this.CardValue = cardValue;
            this.PointValue = (int) cardValue;

            return;
        }
        
        public PokerCard(Suit suit, CardValue cardValue, int pointValue) {
            this.Suit = suit;
            this.CardValue = cardValue;
            this.PointValue = pointValue;

            return;
        }

        public override string ToString()
        {
            return String.Format("{0} of {1}", this.CardValue, this.Suit);
        }
    }
}

