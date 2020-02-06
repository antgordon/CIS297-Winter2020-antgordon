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
using WPFYahtzee.model;

namespace WPFYahtzee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private YahtzeeGame yahtzeeGame;
        public MainWindow()
        {
            yahtzeeGame = new YahtzeeGame();
            InitializeComponent();
            updateDisplay();
        }


        public void updateDisplay() {
            updateDice();
            updateThrowButton();
            updateScoreCard();
        }

        private void updateThrowButton()
        {

            if (yahtzeeGame.IsGameOver())
            {
                rollDiceButton.IsEnabled = false;
                rollDiceButton.Content = $"Game Over";
            }
            else
            {
                if (yahtzeeGame.Player1State.CanThrow())
                {
                    rollDiceButton.IsEnabled = true;
                    rollDiceButton.Content = $"Roll Dice Throw {yahtzeeGame.Player1State.Throws}";
                }

                else
                {
                    rollDiceButton.IsEnabled = false;
                    rollDiceButton.Content = $"Chose a Score!";
                }
            }
        
        }


        private void updateScoreCard()
        {

            ScoreCard card = yahtzeeGame.Player1State.ScoreCard;
            PlayerState player = yahtzeeGame.Player1State;
            updateScoreSlot("Ones", ScoreOnesLabel, ScoreOnesButton, card.SLOT_ONES,player);
            updateScoreSlot("Twos", ScoreTwosLabel, ScoreTwosButton, card.SLOT_TWOS, player);
            updateScoreSlot("Threes", ScoreThreesLabel, ScoreThreesButton, card.SLOT_THREES, player);
            updateScoreSlot("Fours", ScoreFoursLabel, ScoreFoursButton, card.SLOT_FOURS, player);
            updateScoreSlot("Fives", ScoreFivesLabel, ScoreFivesButton, card.SLOT_FIVES, player);
            updateScoreSlot("Sixes", ScoreSixesLabel, ScoreSixesButton, card.SLOT_SIXES, player);

            updateScoreSlot("Three Of Kind", ScoreThreeKindLabel, ScoreThreeKindButton, card.SLOT_THREE_OF_KIND, player);
            updateScoreSlot("Four of Kind", ScoreFourKindLabel, ScoreFourKindButton, card.SLOT_FOUR_OF_KIND, player);
            updateScoreSlot("Full House", ScoreFullHouseLabel, ScoreFullHouseButton, card.SLOT_FULL_HOUSE, player);
            updateScoreSlot("Small Straight", ScoreSmallStrLabel, ScoreSmallStrButton, card.SLOT_SMALL_STRAIGHT, player);
            updateScoreSlot("Large Straigt", ScoreLargeStrLabel, ScoreLargeStrButton, card.SLOT_LARGE_STRAIGHT, player);
            updateScoreSlot("Chance", ScoreChanceLabel, ScoreChanceButton, card.SLOT_CHANCE, player);
            updateScoreSlot("Yahtzee", ScoreYahtzeeLabel, ScoreYahtzeeButton, card.SLOT_YAHTZEE, player);

            ScoreSumLabel.Content = $"Sum: {card.GetUpperSum()}";
            ScoreBonusLabel.Content = $"Bonus: {card.GetBonus()}";
            ScoreTotalLabel.Content = $"Total: {card.GetTotalScore()}";

        }


        private void updateScoreSlot(String labelPrefix, Label scoreLabel, Button scoreButton, ScoreSlot slot, PlayerState state)
        {

            if (slot.Score.HasValue)
            {
                scoreLabel.Content = labelPrefix + ": " + slot.Score.Value;
                scoreButton.Visibility = Visibility.Hidden;

            }
            else {

                scoreLabel.Content = labelPrefix;
                if (state.CanScore())
                {
                    int potential = 0;
                    scoreButton.Visibility = Visibility.Visible;

                    if (slot.Qualifier(state.DiceSet))
                    {
                        
                        potential = slot.ScorePotential(state.DiceSet);

                    } else
                    {
                        potential = 0;
                    }

               
                    scoreButton.Content = $"+{potential}";

                }
                else
                {
                    scoreButton.Visibility = Visibility.Hidden;
                }
            }
            
        }

        private void updateDice()
        {
            DiceSet set = yahtzeeGame.Player1State.DiceSet;
            updateGameDie(set[0], Dice1NButton, Dice1HButton, yahtzeeGame.Player1State);
            updateGameDie(set[1], Dice2NButton, Dice2HButton, yahtzeeGame.Player1State);
            updateGameDie(set[2], Dice3NButton, Dice3HButton, yahtzeeGame.Player1State);
            updateGameDie(set[3], Dice4NButton, Dice4HButton, yahtzeeGame.Player1State);
            updateGameDie(set[4], Dice5NButton, Dice5HButton, yahtzeeGame.Player1State);


        }

        private void updateGameDie(GameDie die, Button freeButton, Button heldButton, PlayerState state) 
        {

            bool visible = state.CanHoldDice();
            String dieText = $" {die.Number} ";
            if (die.IsHeld)
            {
         
                freeButton.IsEnabled = false;
                freeButton.Visibility = Visibility.Hidden;

                heldButton.IsEnabled = visible;
                heldButton.Visibility = Visibility.Visible;
                heldButton.Content = dieText;
            }
            else {

                heldButton.IsEnabled = false;
                heldButton.Visibility = Visibility.Hidden;

                freeButton.IsEnabled = visible;
                freeButton.Visibility = Visibility.Visible;
                freeButton.Content = dieText;
            }
         
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void FreeDie_Click(object sender, RoutedEventArgs e) {

            DiceSet set = yahtzeeGame.DiceSet;
            if (Dice1NButton == sender)
            {
                set[0].IsHeld = true;

            }
            else if (Dice2NButton == sender)
            {

                set[1].IsHeld = true;
            }
            else if (Dice3NButton == sender)
            {
                set[2].IsHeld = true;

            }
            else if (Dice4NButton == sender)
            {

                set[3].IsHeld = true;
            }
            else if (Dice5NButton == sender)
            {
                set[4].IsHeld = true;

            }

            updateDisplay();
        }

        private void HeldDie_Click(object sender, RoutedEventArgs e)
        {
            DiceSet set = yahtzeeGame.DiceSet;
            if (Dice1HButton == sender)
            {
                set[0].IsHeld = false;

            }
            else if (Dice2HButton == sender)
            {

                set[1].IsHeld = false;
            }
            else if(Dice3HButton == sender)
            {
                set[2].IsHeld = false;

            }
            else if(Dice4HButton == sender)
            {

                set[3].IsHeld = false;
            }
            else if(Dice5HButton == sender)
            {
                set[4].IsHeld = false;

            }

            updateDisplay();
        
        }

        private void ThrowDie_Click(object sender, RoutedEventArgs e)
        {

            PlayerState player = yahtzeeGame.Player1State;

            if (player.CanThrow()) {
                player.ThrowDice();
                updateDisplay();
            }
        }

        private void ScoreSlot_Click(object sender, RoutedEventArgs e)
        {
            PlayerState playerState = yahtzeeGame.Player1State;
            ScoreCard scoreCard = yahtzeeGame.Player1State.ScoreCard;

            ScoreSlot slot = null;

            if (ScoreOnesButton == sender) {
                slot = scoreCard.SLOT_ONES;
            }
            else if (ScoreTwosButton == sender)
            {
                slot = scoreCard.SLOT_TWOS;
            }
            else if (ScoreThreesButton == sender)
            {
                slot = scoreCard.SLOT_THREES;
            }
            else if (ScoreFoursButton == sender)
            {
                slot = scoreCard.SLOT_FOURS;

            }
            else if (ScoreFivesButton == sender)
            {
                slot = scoreCard.SLOT_FIVES;
            }
            else if (ScoreSixesButton == sender)
            {
                slot = scoreCard.SLOT_SIXES;
            }
            else if (ScoreThreeKindButton == sender)
            {
                slot = scoreCard.SLOT_THREE_OF_KIND;
            }
            else if (ScoreFourKindButton == sender)
            {
                slot = scoreCard.SLOT_FOUR_OF_KIND;
            }
            else if (ScoreFullHouseButton == sender)
            {
                slot = scoreCard.SLOT_FULL_HOUSE;
            }
            else  if (ScoreSmallStrButton == sender)
            {
                slot = scoreCard.SLOT_SMALL_STRAIGHT;
            }
            else if (ScoreLargeStrButton == sender)
            {
                slot = scoreCard.SLOT_LARGE_STRAIGHT;
            }
            else if (ScoreChanceButton == sender)
            {
                slot = scoreCard.SLOT_CHANCE;
            }
            else if (ScoreYahtzeeButton == sender)
            {
                slot = scoreCard.SLOT_YAHTZEE;
            }

           
            playerState.UseScore(slot, playerState.DiceSet);
            updateDisplay();



        }
    }
}
