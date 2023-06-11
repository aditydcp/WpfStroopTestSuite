using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfStroopTestSuite.Pages
{
    /// <summary>
    /// Interaction logic for BlockResult.xaml
    /// </summary>
    public partial class BlockResult : Page, IPagePreviewKey
    {
        public BlockResult()
        {
            InitializeComponent();
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            LoadContent();
        }

        private void LoadContent()
        {
            // hard-coded text values
            switch (App.Stage)
            {
                case Models.Stages.First:
                    TitleTextBlock.Text = "You have finished the first block!";
                    break;
                case Models.Stages.Second:
                    TitleTextBlock.Text = "You have finished the second block!";
                    break;
                case Models.Stages.Third:
                    TitleTextBlock.Text = "You have finished the third block!";
                    break;
            }
            DescriptionTextBlock.Text =
                        "You attempted a total of " + App.BlockData.TrialsCount + " trials\n\n" +
                        "Accuracy (percentage of trials answered correctly) : " + ToStringWithNaN(App.BlockData.Accuracy) + "%\n\n" +
                        "Mean reaction time of correct responses (in ms) : " + ToStringWithNaN(App.BlockData.MeanReactionTimeOnCorrectTrials / 10000.00) + " ms";
            NextLabel.Content = "Press Spacebar when you are ready to start the block";
        }

        private void NavigateToNextSection()
        {
            if (App.Stage == Models.Stages.Third)
            {
                NavigationService.Navigate(new ClosingPage());
                return;
            }
            App.Stage++;
            App.ResetBlockData();
            NavigationService.Navigate(new BlockIntro());
        }

        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                NavigateToNextSection();
            }
        }

        private static string ToStringWithNaN(double number)
        {
            if (double.IsNaN(number)) return "~";
            else return number.ToString("F", CultureInfo.InvariantCulture);
        }
    }
}
