namespace Models {
    class CribbagePlayer {

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

        public override string ToString()
        {
            return "Player: " + this.Name + "\n" + 
            "Points: " + this.Points + "\n" + 
            "Hand:\n" + this.Hand.ToString();
        }


    }
}