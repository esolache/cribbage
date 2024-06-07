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
            CribbagePlayer player2 = new CribbagePlayer("Al", false);
            CribbagePlayer player3 = new CribbagePlayer("Macey", false);
            player1.NextPlayer = player2;
            player2.NextPlayer = player3;
            player3.NextPlayer = player1;

            List<CribbagePlayer> players = new List<CribbagePlayer>();
            players.Add(player1);
            players.Add(player2);
            players.Add(player3);


            CribbageGame game = new CribbageGame(players);
            
            while (game.Winner() == null) {
                Cribbage.SetUp(ref game);
                Console.WriteLine(game.PlayerPointsToString());

                Cribbage.CreateCrib(ref game);
                
                Console.WriteLine(game.Crib.CardsToString());

                if (game.PullCard().CardValue == CardValue.Jack) {
                    game.Dealer.Points += 2;
                }

                Console.WriteLine(game.Crib.CardsToString());


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
            
            // Store Original Player Hand
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
                        // BUG: Go is kind of messed upd
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

            return;
        }

        public static void TallyPoints(ref CribbageGame game) {
            int points = 0;
            foreach (CribbagePlayer player in game.Players) {
                points = player.Hand.Points;
                Console.WriteLine("{0} Hand Worth {1} Points", player.Name, points);
                player.Points += points;
            }

            points = game.Crib.Points;
            Console.WriteLine("Crib Worth {0} Points", points);
            game.Dealer.Points += points;
            
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
            Console.WriteLine(player.Hand.CardsToString());
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
                    Console.WriteLine("Current Play" + game.GetActivePlay().ToString());
                    Console.WriteLine("Hand: " + player.Hand.CardsToString());
                    Console.Write("Select Card To Play: ");
                    
                }

            } while (!isValidPlay);

            return;
        }

        public static void CreateCrib(ref CribbageGame game) {
            foreach (CribbagePlayer player in game.Players) {
                    Cribbage.SelectCardForCrib(ref game, player);

                    if (game.NumPlayers == 2) {
                        Cribbage.SelectCardForCrib(ref game, player);
                    }
                }

                if (game.NumPlayers == 3) {
                    game.AddCardToCrib(game.Deck.Cards[0]);
                }
            return;
        }

        public static void SelectCardForCrib(ref CribbageGame game, CribbagePlayer player) {
            if (player.IsComputer) {
                ComputerSelectCardForCrib(ref game, ref player);
            }
            else {
                UserSelectCardForCrib(ref game, ref player);
            }
        }

        public static void ComputerSelectCardForCrib(ref CribbageGame game, ref CribbagePlayer player) {
            game.AddCardToCrib(player.Hand.Cards[0]);
        }

        public static void UserSelectCardForCrib(ref CribbageGame game, ref CribbagePlayer player) {
            PokerCard result;

            Console.WriteLine("Hand: " + player.Hand.CardsToString());
            Console.Write("Select Cards 0-{0} To Discard: ", player.Hand.Cards.Count - 1);


            int userInParsed = -1;
            
            do {
                try {
                    var userIn = Console.ReadLine();
                    Int32.TryParse(userIn, out userInParsed);

                    result = player.Hand.Cards[userInParsed];

                    game.AddCardToCrib(result);

                }
                catch (Exception) {
                    Console.WriteLine("Please enter a valid number.");
                    Console.WriteLine("Hand: " + player.Hand.CardsToString());
                    Console.Write("Select Cards 0-{0} To Discard: ", player.Hand.Cards.Count - 1);

                }

            } while (userInParsed < 0 || userInParsed > 6);

            return;
        }
    }
}