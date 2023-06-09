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
    /// Interaction logic for BlockIntro.xaml
    /// </summary>
    public partial class BlockIntro : Page, IPagePreviewKey
    {
        public BlockIntro()
        {
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            // hard-coded text values
            switch (App.Stage)
            {
                case Models.Stages.First:
                    DescriptionTextBlock.Text =
                        "Get ready to start the first block.\n\n" +
                        "This block will go for " + ToDurationString(App.IntroDuration) + ".\n" +
                        "There will only be rectangles for this block.\n" +
                        "Press \"R\", \"G\", \"B\", \"Y\" in response to the color of the rectangles.";
                    break;
                case Models.Stages.Second:
                    DescriptionTextBlock.Text =
                        "Get ready to start the second block.\n\n" +
                        "This block will go for " + ToDurationString(App.BlockDuration) + ".\n" +
                        "There will only be texts for this block.\n" +
                        "Press \"R\", \"G\", \"B\", \"Y\" in response to the color of the texts.";
                    break;
                case Models.Stages.Third:
                    DescriptionTextBlock.Text =
                        "Get ready to start the third block.\n\n" +
                        "This block will go for " + ToDurationString(App.BlockDuration) + ".\n" +
                        "There will only be texts for this block.\n" +
                        "Press \"R\", \"G\", \"B\", \"Y\" in response to the color of the texts.";
                    break;
            }

            TitleLabel.Content = "Stroop Task";
            NextLabel.Content = "Press Spacebar when you are ready to start the block";
        }

        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                NavigationService.Navigate(new BlockView());
            }
        }

        private static string ToDurationString(int seconds)
        {
            StringBuilder @string = new();
            if (seconds <= 60)
            {
                @string.Append(seconds.ToString() + " seconds");
            }
            else
            {
                int minutes = seconds / 60;
                int trailingSeconds = seconds % 60;

                @string.Append(minutes.ToString() + " minutes");

                if (trailingSeconds > 0)
                {
                    @string.Append(" and ");
                    @string.Append(trailingSeconds.ToString() + " seconds");
                }
            }

            return @string.ToString();
        }
    }
}
