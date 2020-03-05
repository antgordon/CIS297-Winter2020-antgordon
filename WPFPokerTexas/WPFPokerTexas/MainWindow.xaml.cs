using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFPokerTexas.model;

namespace WPFPokerTexas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, PokerGameListener
    {

        private PokerGame game;

        public MainWindow()
        {
            InitializeComponent();
            game = new PokerGame(new DefaultCardDealer(), this);
            game.MoveStage();
        }

        private void writeCpuMoney(int money)
        {
            OpBalanceLabel.Content = $"CPU: ${money}";
        }

        private void writePlayerMoney(int money)
        {
            PlayerBalanceLabel.Content = $"Player: ${money}";
        }

        private void writePotMoney(int money)
        {
            PotBalanceLabel.Content = $"Pot: ${money}";
        }

        private void writeCpuHand(PokerHand.HandType? type)
        {
            OpHandLabel.Content = $"Hand: {nameOfHand(type)}";


        }

        private void writePlayerAction(PokerPlayer.PokerResponse resp) {
            writeActionLabel(PlayerActionLabel, resp);
        }
        private void writeCpuAction(PokerPlayer.PokerResponse resp)
        {
            writeActionLabel(OpActionLabel, resp);
        }


        private void hidePlayerAction()
        {
            writeActionLabel(PlayerActionLabel, null);
        }
        private void hideCpuAction()
        {
            writeActionLabel(OpActionLabel, null);
        }


        private void writeActionLabel(Label label, PokerPlayer.PokerResponse? response) {

            if (response.HasValue)
            {

                if (response == PokerPlayer.PokerResponse.FOLD)
                {
                    label.Content = "Fold";

                }
                else if (response == PokerPlayer.PokerResponse.RAISE)
                {
                    label.Content = "Call";
                }
                else {
                    label.Content = "???";
                }
            }
            else {
                label.Content = "";
            }
        
        }

        private void writeWinner(WinStatus? status)
        {

            if (status.HasValue)
            {
                switch (status.Value)
                {
                    case WinStatus.PLAYER_HUMAN:
                        GameWinnerLabel.Content = "Winner- Player ";
                        break;
                    case WinStatus.PLAYER_ROBO:
                        GameWinnerLabel.Content = "Winner - CPU";
                        break;
                    case WinStatus.TIE:
                        GameWinnerLabel.Content = "TIE - No one wins";
                        break;

                    default:
                        GameWinnerLabel.Content = "";
                        break;
                }
            }
            else
            {
                GameWinnerLabel.Content = "";
            }
        }

        private void writePlayerHand(PokerHand.HandType? type)
        {
            PlayerHandLabel.Content = $"Hand: {nameOfHand(type)}";
        }

        private void writeCard(Label label, PlayingCard card)
        {
            if (card == null)
            {
                label.Content = "Hidden";
            }
            else
            {
                label.Content = nameOfCard(card);
            }


        }


        private void hideCpuCards()
        {
            writeCard(OpCard1Label, null);
            writeCard(OpCard2Label, null);
        }

        private void revealCpuCards(OrderedCardSet set)
        {
            writeCard(OpCard1Label, set[0]);
            writeCard(OpCard2Label, set[1]);
        }

        private void revealPlayerCards(OrderedCardSet set)
        {
            writeCard(PlayerCard1Label, set[0]);
            writeCard(PlayerCard2Label, set[1]);
        }

        private void revealCommunityrCards(OrderedCardSet set)
        {
            writeCard(ComCard1Label, set[0]);
            writeCard(ComCard2Label, set[1]);
            writeCard(ComCard3Label, set[2]);
            writeCard(ComCard4Label, set[3]);
            writeCard(ComCard5Label, set[4]);
        }

        private void writeCpuChances(HoldemHand.HandPlayResult results)
        {
            double total = (double)results.Wins + (double)results.Losses + (double)results.Ties;
            double winChances = results.Wins / total;
            double tiesChances = results.Ties / total;
            double lossesChances = results.Losses / total;
            OpChancesLoseLabel.Content = $"CPU Lose: %{lossesChances * 100:F1}";
            OpChancesWinLabel.Content = $"CPU Win: %{winChances * 100:F1}";
            OpChancesTieLabel.Content = $"CPU Tie: %{tiesChances * 100:F1}";
            OpChancesLoseLabel.Visibility = Visibility.Visible;
            OpChancesWinLabel.Visibility = Visibility.Visible;
            OpChancesTieLabel.Visibility = Visibility.Visible;
        }
        private void hideeCpuChances()
        {
           
            OpChancesLoseLabel.Content = "CPU Lose: %???";
            OpChancesWinLabel.Content = "CPU Win: %???";
            OpChancesTieLabel.Content = "CPU Tie: %???";
            OpChancesLoseLabel.Visibility = Visibility.Hidden;
            OpChancesWinLabel.Visibility = Visibility.Hidden;
            OpChancesTieLabel.Visibility = Visibility.Hidden;
        }


        private void writePlayerChances(HoldemHand.HandPlayResult results)
        {
            double total = (double)results.Wins + (double)results.Losses + (double)results.Ties;
            double winChances = results.Wins / total;
            double tiesChances = results.Ties / total;
            double lossesChances = results.Losses / total;
            PlayerChancesLoseLabel.Content = $"Player Lose: %{lossesChances* 100 :F1}";
            PlayerChancesWinLabel.Content = $"Player Win: %{winChances * 100:F1}";
            PlayerChancesTieLabel.Content = $"Player Tie: %{tiesChances * 100:F1}";
        }


        private string nameOfCard(PlayingCard card)
        {

            return $"{nameOfRank(card.CardRank)} {nameOfSuit(card.CardSuit)}";
        }

        private string nameOfSuit(PlayingCard.Suit suit)
        {

            switch (suit)
            {
                case PlayingCard.Suit.CLUBS:
                    return "Clubs";

                case PlayingCard.Suit.DIAMONDS:
                    return "Diamonds";
                case PlayingCard.Suit.HEARTS:
                    return "Hearts";
                case PlayingCard.Suit.SPADES:
                    return "Spades";
                default:
                    return "SNone";
            }
        }

        private string nameOfRank(PlayingCard.Rank rank)
        {

            switch (rank)
            {
                case PlayingCard.Rank.ACE:
                    return "A";

                case PlayingCard.Rank.TWO:
                    return "2";


                case PlayingCard.Rank.THREE:
                    return "3";

                case PlayingCard.Rank.FOUR:
                    return "4";

                case PlayingCard.Rank.FIVE:
                    return "5";

                case PlayingCard.Rank.SIX:
                    return "6";

                case PlayingCard.Rank.SEVEN:
                    return "7";

                case PlayingCard.Rank.EIGHT:
                    return "8";


                case PlayingCard.Rank.NINE:
                    return "9";


                case PlayingCard.Rank.TEN:
                    return "10";


                case PlayingCard.Rank.JACK:
                    return "J";

                case PlayingCard.Rank.QUEEN:
                    return "Q";

                case PlayingCard.Rank.KING:
                    return "K";


                default:
                    return "RNone";
            }
        }

        private string nameOfHand(PokerHand.HandType? handType)
        {

            if (handType.HasValue)
            {
                switch (handType.Value)
                {
                    case PokerHand.HandType.STRAIGHT_FLUSH:
                        return "Striaght Flush";

                    case PokerHand.HandType.STRAIGHT:
                        return "Straight";


                    case PokerHand.HandType.FLUSH:
                        return "Flush";

                    case PokerHand.HandType.FULL_HOUSE:
                        return "Full House";

                    case PokerHand.HandType.FOUR_KIND:
                        return "Four of a Kind";


                    case PokerHand.HandType.THREE_KIND:
                        return "Three of a Kind";

                    case PokerHand.HandType.TWO_PAIR:
                        return "Two Pair";

                    case PokerHand.HandType.ONE_PAIR:
                        return "One Pair";


                    case PokerHand.HandType.HIGH_CARD:
                        return "High Card";

                    default:
                        return "????";
                }
            }
            else
            {
                return "None";
            }
        }


        private void hideCpuInfo() {
            hideCpuCards();
            hideeCpuChances();
            writeCpuHand(null);

        }

        private void showCpuInfo()
        {
            revealCpuCards(game.RoboPlayer.HandCards);
            writeCpuChances(game.RoboPlayer.HandChances);
            writeCpuHand(game.RoboPlayer.Hand.Type);

        }

        private void enablePlayerResponse() {
            PlayerCallButton.IsEnabled = true;
            PlayerFoldButton.IsEnabled = true;
        }

        private void disablePlayerResponse()
        {
            PlayerCallButton.IsEnabled = false;
            PlayerFoldButton.IsEnabled = false;
        }

        private void revealWinner() {
            writeWinner(game.Winner);
            continueButton.Visibility = Visibility.Visible;
            continueButton.IsEnabled = true;
        }

        private void hideWinner()
        {
            writeWinner(null);
            continueButton.Visibility = Visibility.Hidden;
            continueButton.IsEnabled = false;
        }

        private void mainUpdate() {
            writeCpuMoney(game.RoboPlayer.Money);
            writePlayerMoney(game.HumanPlayer.Money);
            writePotMoney(game.Pot);
        }


        public void PostGameSetup()

        {
            mainUpdate(); 
            writePlayerChances(game.HumanPlayer.HandChances);
            writePlayerHand(game.HumanPlayer.Hand.Type);
  
            revealPlayerCards(game.HumanPlayer.HandCards);
            writeWinner(null);
            revealCommunityrCards(game.GameDealer.CommunityCards);
            hideCpuInfo();
            hideWinner();
            hideCpuAction();
            hidePlayerAction();
            enablePlayerResponse();
        }

        public void OnPlayerResponse(PokerPlayer.PokerResponse response)
        {
            mainUpdate();
            disablePlayerResponse();
            writePlayerAction(response);
        }

        public void OnOpponentResponse(PokerPlayer.PokerResponse response)
        {
            mainUpdate();
            showCpuInfo();
            writeCpuAction(response);

        }

        public void PostDsitributePot()
        {
            mainUpdate();
            revealWinner();
            showCpuInfo();
        }

        public void OnGameEnd()
        {
            mainUpdate();
            revealWinner();
            showCpuInfo();
            continueButton.IsEnabled = false;
            continueButton.Content = "GAME OVER";

          
        }

        private void PlayerCallButton_Click(object sender, RoutedEventArgs e)
        {
            game.HumanPlayer.RaiseHand();

        }

        private void PlayerFoldButton_Click(object sender, RoutedEventArgs e)
        {
            game.HumanPlayer.FoldHand();

        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            game.MoveStage();
        }
    }
}
