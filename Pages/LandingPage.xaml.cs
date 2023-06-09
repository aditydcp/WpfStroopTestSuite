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

namespace WpfStroopTestSuite.Pages
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page, IPagePreviewKey
    {
        // Local counter for LandingPage progress
        private int step = 0;

        public LandingPage()
        {
            InitializeComponent();
            LoadInitialContent();
        }

        private void LoadInitialContent()
        {
            // hard-coded text values
            TitleLabel.Content = "Stroop Task";
            DescriptionTextBlock.Text =
                "This task will be divided into 3 blocks.\n\n" +
                "You will be shown shapes and texts in a particular \"print\" color.\n\n" +
                "Your goal is to respond to the print color and press the corresponding button.\n\n" +
                "There will be 4 colors and their corresponding button used in this task:\n" +
                "\"R\" for Red\n" +
                "\"G\" for Green\n" +
                "\"B\" for Blue\n" +
                "\"Y\" for Yellow";
            NextLabel.Content = "Press the Spacebar to continue...";
            Step1InstructionContainer.Visibility = Visibility.Collapsed;
            Step2InstructionContainer.Visibility = Visibility.Collapsed;
        }

        private void SetContent()
        {
            switch (step)
            {
                case 1:
                    Step1InstructionContainer.Visibility = Visibility.Visible;
                    Step2InstructionContainer.Visibility= Visibility.Collapsed;
                    DescriptionTextBlock.Text =
                        "In the case of shapes, you will see a rectangle for each trial.\n" +
                        "As an example, please refer to the area below.";
                    NextLabel.Content = "Press the Spacebar to continue...";
                    break;
                case 2:
                    Step1InstructionContainer.Visibility = Visibility.Collapsed;
                    Step2InstructionContainer.Visibility = Visibility.Visible;
                    DescriptionTextBlock.Text =
                        "In the case of texts, you will see color name (red, green, blue, yellow) for each trial.\n" +
                        "As an example, please refer to the area below.";
                    NextLabel.Content = "Press the Spacebar to prepare for block 1...";
                    break;
                default: break;
            }
        }

        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                step++;
                if (step > 2)
                    NavigationService.Navigate(new BlockIntro());
                SetContent();
            }
        }
    }
}
