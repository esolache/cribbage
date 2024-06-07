using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.SignalR;
using Models;

namespace ProgramNamespace
{
    class Cribbage
    {
        public static void MainLoop()
        {
            CribbagePlayer player1 = new CribbagePlayer("Edwin", false);
            CribbagePlayer player2 = new CribbagePlayer("Al", true);
            player1.NextPlayer = player2;
            player2.NextPlayer = player1;

            List<CribbagePlayer> players = new List<CribbagePlayer>();
            players.Add(player1);
            players.Add(player2);


            CribbageGame game = new CribbageGame(players);
            
            while (game.Players[0].Points < 120 || game.Players[1].Points < 120) {
                Cribbage.SetUp(ref game);
                Console.WriteLine(game.PlayerPointsToString());

                // Print Hand
                Console.WriteLine((game.Players[0].Hand.HandToString()));


                // Select Cards to Discard to Crib
                Cribbage.UserSelectCardForCrib(ref game);
                Cribbage.UserSelectCardForCrib(ref game);
                
                // Computer Discard To Crib
                Cribbage.ComputerSelectCardForCrib(ref game);
                
                if (game.PullCard().CardValue == CardValue.Jack) {
                    game.Dealer.Points += 2;
                }

                Cribbage.PlayLoop(ref game);

                Console.WriteLine(game.ToString());

                Cribbage.TallyPoints(ref game);

                game.Dealer = game.Dealer.NextPlayer;
                game.CurrPlayer = game.Dealer.NextPlayer;
            }




        }
        public static void SetUp(ref CribbageGame game) {
            game.NewRound();
            game.Deck.Shuffle();
            game.Deal();
            foreach (CribbagePlayer player in game.Players) {
                player.Hand.SortCards();
            }
            return;
        }

        public static bool CardsActive(CribbageGame game) {
            foreach (CribbagePlayer player in game.Players) {
                if (player.Hand.Cards.Count > 0) {
                    return true;
                }
            }

            return false;
        }

        public static bool PlayersCanPlay(CribbageGame game) {
            foreach (CribbagePlayer player in game.Players) {
                if (Cribbage.CanPlay(player.Hand, game.GetActivePlay())) {
                    return true;
                }
            }

            return false;
        }

        public static void PlayLoop(ref CribbageGame game) {
            
            
            Dictionary<CribbagePlayer, List<PokerCard>> playerHands = new Dictionary<CribbagePlayer, List<PokerCard>>();
            foreach (CribbagePlayer player in game.Players) {
                playerHands.Add(player, player.Hand.GetCopyCardsInHand());
            }

            CribbagePlayer currPlayer = game.CurrPlayer;
            
            while (Cribbage.CardsActive(game)) {
                while (game.GetActivePlay().Sum < 31 && PlayersCanPlay(game)) {
                    bool currPlayerCanPlay = Cribbage.CanPlay(currPlayer.Hand, game.GetActivePlay());
                    
                    if (currPlayerCanPlay) 
                    {
                        // computer turn
                        if (currPlayer.IsComputer) {
                            Cribbage.ComputerSelectCardToPlay(ref game, ref currPlayer);
                        }
                        // user turn
                        else {
                            Cribbage.UserSelectCardToPlay(ref game, ref currPlayer);
                        }

                        if (!Cribbage.CardsActive(game)) {
                            Console.WriteLine("Last Card! " + currPlayer.Name);
                            currPlayer.Points++;
                        }
                    }
                    else {
                        Console.WriteLine("Go! " + currPlayer.Name);
                    }

                    currPlayer = currPlayer.NextPlayer;

                    Console.WriteLine(game.PlayerPointsToString());
                    Console.WriteLine(game.GetActivePlay().ToString());
                }
                game.Plays.Add(new CribbagePlay());
            }

            // return cards to each players hand for counting
            foreach (CribbagePlayer player in game.Players) {
                player.Hand.Cards = playerHands[player];
            }
            //game.Players[0].Hand.Cards = userHand;
            //game.Players[1].Hand.Cards = computerHand;

            return;
        }

        public static void TallyPoints(ref CribbageGame game) {
            foreach (CribbagePlayer player in game.Players) {
                Console.WriteLine("{0} Hand Worth {1} Points", player.Name, player.Hand.Points);
                player.Points += player.Hand.Points;
            }

            Console.WriteLine("Crib Worth {0} Points", game.Crib.Points);
            game.Dealer.Points += game.Crib.Points;
            
            return;
        }

        public static bool CanPlay(CribbageHand hand, CribbagePlay cribbagePlay) {
            bool result = false;
            foreach (PokerCard card in hand.Cards) {
                if (card.PointValue + cribbagePlay.Sum <= 31) {
                    result = true;
                }
            }

            return result;
        }

        public static void ComputerSelectCardToPlay(ref CribbageGame game, ref CribbagePlayer player) {
            for (int i = 0; i < player.Hand.Cards.Count; i++) {
                try {
                    PokerCard card = player.Hand.Cards[i];
                    game.AddToPlay(card);
                    player.Points += game.GetActivePlay().GetPointsForLastCardPlayed();
                    return;
                }
                catch (Exception) {
                }
            }

            throw new Exception("Computer Could Not Play");
        }

        public static void UserSelectCardToPlay(ref CribbageGame game, ref CribbagePlayer player) {
            PokerCard result;

            bool isValidPlay = false;
            Console.WriteLine(player.Hand.HandToString());
            Console.Write("Select Card to Play: ");


            int userInParsed = -1;
            
            do {
                try {
                    var userIn = Console.ReadLine();
                    Int32.TryParse(userIn, out userInParsed);

                    result = player.Hand.Cards[userInParsed];

                    game.AddToPlay(result);
                    player.Points += game.GetActivePlay().GetPointsForLastCardPlayed();

                    isValidPlay = true;
                }
                catch (Exception) {
                    Console.WriteLine("Can't Play that Card.");
                    Console.WriteLine("Current Play" + game.Players[game.Plays.Count - 1].ToString());
                    Console.WriteLine("Hand: " + player.Hand.HandToString());
                    Console.Write("Select Card To Play: ");
                    
                }

            } while (!isValidPlay);

            return;
        }

        public static void ComputerSelectCardForCrib(ref CribbageGame game) {
            game.AddCardToCrib(game.Players[1].Hand.Cards[0]);
            game.AddCardToCrib(game.Players[1].Hand.Cards[0]);
        }

        public static void UserSelectCardForCrib(ref CribbageGame game) {
            PokerCard result;

            Console.WriteLine("Hand: " + game.Players[0].Hand.HandToString());
            Console.Write("Select Cards 0-{0} To Discard: ", game.Players[0].Hand.Cards.Count - 1);


            int userInParsed = -1;
            
            do {
                try {
                    var userIn = Console.ReadLine();
                    Int32.TryParse(userIn, out userInParsed);

                    result = game.Players[0].Hand.Cards[userInParsed];

                    game.AddCardToCrib(result);

                }
                catch (Exception) {
                    Console.WriteLine("Please enter a valid number.");
                    Console.WriteLine("Hand: " + game.Players[0].Hand.HandToString());
                    Console.Write("Select Cards 0-{0} To Discard: ", game.Players[0].Hand.Cards.Count - 1);

                }

            } while (userInParsed < 0 || userInParsed > 6);

            return;
        }
    }
}