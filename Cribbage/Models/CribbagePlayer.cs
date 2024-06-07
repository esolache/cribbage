namespace Models {
    public class CribbagePlayer {

        public string Name {get; set;}
        public CribbageHand Hand {get; set;}
        public int Points {get; set;}
        public CribbagePlayer NextPlayer {get; set;}
        public bool IsComputer {get; set;}
        

        public CribbagePlayer(string name, bool IsComputer) {
            this.Name = name;
            this.Hand = new CribbageHand();
            this.Points = 0;
            this.NextPlayer = this;
            this.IsComputer = IsComputer;

            return;
        }

        public string PlayerCardsPointsToString() {
            return "Player: " + this.Name + "\n" + 
            "Hand:\n" + this.Hand.CardsPointsPullToString();
        }

        public string PlayerCardsToString()
        {
            return "Player: " + this.Name + "\n" + 
            "Hand:\n" + this.Hand.CardsToString();
        }


    }
}