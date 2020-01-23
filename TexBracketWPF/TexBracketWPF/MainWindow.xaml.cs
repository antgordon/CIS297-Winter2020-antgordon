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

namespace TexBracketWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GuiState _state;
        public MainWindow()
        {
            _state = new GuiState();
            InitializeComponent();

            _state.Loaded = true;
            publishState();

   
        }


        private void pubIncome() {
            if (_state.GrossIncome > 0.0)
            {
                grossIncomeLabel.Content = $"Gross Income ${_state.GrossIncome}";
            }
            else
            {
                grossIncomeLabel.Content = "Gross Income";
            }

        }


        private void pubDeduct() {


            if (deductionLabel2 == null)
                return;

            if (_state.UsingStandardDeduction)
            {
                deductionLabel2.Content 
                    = $"Deductions ${GuiState.STANDARD_DEDUCTION}";
                deductItemTextBox.Visibility = Visibility.Hidden;
            }
            else
            {
                deductItemTextBox.Visibility = Visibility.Visible;
                if (_state.Deductions > 0.0)
                {
                    deductionLabel2.Content = $"Deductions ${_state.Deductions}";
                }
                else
                {
                    deductionLabel2.Content = "Deductions";
                }
            }
        }
        private void publishState() {


            if (!_state.Loaded)
                return;


            pubIncome();

            pubDeduct();

            publishBracketBreakdown();
        }


        private void publishBracketBreakdown() {

            double[] taxesOwed = _state.TaxResults.TaxesOwed;

            tax37.Content = $"37% ${taxesOwed[(int)TaxCalculator.Backets.PER_37]:F2}";
            tax35.Content = $"35% ${taxesOwed[(int)TaxCalculator.Backets.PER_35]:F2}";
            tax32.Content = $"32% ${taxesOwed[(int)TaxCalculator.Backets.PER_32]:F2}";
            tax24.Content = $"24% ${taxesOwed[(int)TaxCalculator.Backets.PER_24]:F2}";
            tax22.Content = $"22% ${taxesOwed[(int)TaxCalculator.Backets.PER_22]:F2}";
            tax12.Content = $"12% ${taxesOwed[(int)TaxCalculator.Backets.PER_12]:F2}";
            tax10.Content = $"10% ${taxesOwed[(int)TaxCalculator.Backets.PER_10]:F2}";

            taxTotal.Content = $"Total ${_state.TaxResults.TotalTaxesOwed:F2}";
            taxPercentGross.Content = $"Percent of Gross Income {_state.TaxResults.TaxesPecentOfGrossIncome * 100.0 :F2}%";
            taxPercentAdjust.Content = $"Percent of Adjusted Income {_state.TaxResults.TaxesPecentOfAdjustedIncome * 100.0:F2}%";

        }

        private void deductStandRadio_Checked(object sender, RoutedEventArgs e)
        {
           
            _state.UsingStandardDeduction = true;
            publishState();
        }

        private void deductItemRadio_Checked(object sender, RoutedEventArgs e)
        {
            _state.UsingStandardDeduction = false;
            publishState();
        }

        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            double deduct;

            if (_state.UsingStandardDeduction)
            {
                deduct = GuiState.STANDARD_DEDUCTION;
            }
            else {
                deduct = _state.Deductions;
            }

            _state.TaxResults = new TaxCalculator(_state.GrossIncome, deduct);
            publishState();
        }

        private void clearAllButton_Click(object sender, RoutedEventArgs e)
        {
            _state = new GuiState();
            _state.Loaded = true;
            publishState();
        }

        private void grossIncomeTextbox_KeyDown(object sender, KeyEventArgs e)
        {
            Key sel = e.Key;

            if (e.Key == Key.Return) {

                int value = 0;


                if (int.TryParse(grossIncomeTextbox.Text, out value))
                {

                    if (value > 0)
                    {
                        grossIncomeTextbox.Text = String.Empty;
                        _state.GrossIncome += value;
                        publishState();
                    }

                }
                else { 
                
                }
            }
        }

        private void deductItemTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {

                int value = 0;


                if (int.TryParse(deductItemTextBox.Text, out value))
                {

                    if (value > 0)
                    {
                        deductItemTextBox.Text = String.Empty;
                        _state.Deductions += value;
                        publishState();
                    }

                }
                else
                {

                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /*private void deductItemRadio_Checked_1(object sender, RoutedEventArgs e)
        {

        }*/
    }
}
