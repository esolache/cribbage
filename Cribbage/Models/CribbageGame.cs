namespace Models {
    public class CribbageGame {

        public List<CribbagePlayer> Players {get; set;}
        public int NumPlayers { get {
            return this.Players.Count;
        }}
        public CribbagePlayer CurrPlayer {get; set;}
        public CribbagePlayer Dealer {get; set;}
        public PokerCard? Pull {get; set;}
        public CribbageDeck Deck {get; set;}
        public CribbageHand Crib {get; set;} 
        public List<CribbagePlay> Plays {get;set;}
        

        public CribbageGame(List<CribbagePlayer> players) {
            this.Players = players;
            this.Deck = new CribbageDeck();
            this.Crib = new CribbageHand();
            this.Plays = new List<CribbagePlay>();
            this.CurrPlayer = players[0];
            this.Dealer = players[0];
            this.Plays.Add(new CribbagePlay());
            this.Pull = null;

            return;
        }

        public CribbagePlay GetActivePlay() {
            return this.Plays[this.Plays.Count - 1];
        }

        public void Deal() {
            if ( this.NumPlayers == 2 ) {
                foreach (CribbagePlayer currPlayer in this.Players) {
                    List<PokerCard> cards = this.Deck.Cards[0..6];
                    foreach (PokerCard card in cards) {
                        this.Deck.RemoveCard(card);
                        currPlayer.Hand.AddCard(card);
                    }
                }
            }
            else if (this.NumPlayers == 3) {
                foreach (CribbagePlayer currPlayer in this.Players) {
                    List<PokerCard> cards = this.Deck.Cards[0..5];
                    foreach (PokerCard card in cards) {
                        this.Deck.RemoveCard(card);
                        currPlayer.Hand.AddCard(card);
                    }
                }
            }
            
        }

        public void NewRound() {
            this.Deck = new CribbageDeck();
            this.Crib = new CribbageHand();
            this.Pull = null;
            foreach (CribbagePlayer currPlayer in this.Players) {
                currPlayer.Hand = new CribbageHand();
            }
        }

        public PokerCard PullCard() {
            this.Pull = this.Deck.Cards[0];
            this.Deck.RemoveCard(this.Pull);
            
            foreach (CribbagePlayer player in this.Players) {
                player.Hand.AddPull(this.Pull);
            }

            this.Crib.AddPull(this.Pull);

            return this.Pull;
        }

        public void AddCardToCrib(PokerCard card) {
            this.Crib.AddCard(card);
            this.Deck.RemoveCard(card);
            foreach(CribbagePlayer player in this.Players) {
                player.Hand.RemoveCard(card);
            }

            return;
        }

        public void AddToCrib(List<PokerCard> cards) {
            if (this.Pull == null) {
                return;
            }
            this.Crib.AddPull(this.Pull);
            foreach (PokerCard card in cards) {
                this.Crib.AddCard(card);
                foreach(CribbagePlayer player in this.Players) {
                    player.Hand.RemoveCard(card);
                }
            }
        }

        public void AddToPlay(PokerCard card) {
            if (this.Plays[this.Plays.Count - 1].Sum + card.PointValue > 31) {
                throw new Exception("Card Cannot Be Played");
            }

            this.GetActivePlay().AddCard(card);
            foreach(CribbagePlayer player in this.Players) {
                player.Hand.RemoveCard(card);
            }
        }
        
        // null if game is not over
        public CribbagePlayer? Winner() {
            foreach (CribbagePlayer player in this.Players) {
                if (player.Points > 120) {
                    return player;
                }
            }

            return null;
        }

        public override string ToString()
        {
            string result = "";
            if (this.Pull != null) {
                result += "Pull: " + this.Pull.ToString() + "\n";
            }
            foreach (CribbagePlayer player in Players) {
                result += player.PlayerCardsPointsToString() + "\n";
            }

            result += "Crib:\n" + this.Crib.CardsPointsPullToString();

            return result;
        }

        public string PlayerPointsToString() {
            string result = "";
            foreach (CribbagePlayer player in this.Players) {
                result += String.Format("Name: {0}\n Points: {1}\n",player.Name, player.Points);
            }
            
            return result;
        }
    }
}