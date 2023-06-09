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
using WpfStroopTestSuite.Models;

namespace WpfStroopTestSuite.Pages
{
    /// <summary>
    /// Interaction logic for BlockView.xaml
    /// </summary>
    public partial class BlockView : Page, IPagePreviewKey
    {
        // debug only
        List<UIElement> list;
        int index = 0;

        public BlockView()
        {
            InitializeComponent();
            //RedRed.Visibility = Visibility.Visible;
            list = new List<UIElement>()
            {
                RedRed, RedGreen, RedBlue, RedYellow,
                GreenRed, GreenGreen, GreenBlue, GreenYellow,
                BlueRed, BlueGreen, BlueBlue, BlueYellow,
                YellowRed, YellowGreen, YellowBlue, YellowYellow,
                RedSquare, GreenSquare, BlueSquare, YellowSquare
            };
            list[index].Visibility = Visibility.Visible;
        }

        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            // debug only
            if (e.Key == Key.Enter || e.Key == Key.Space)
            {
                index++;
                if (index >= list.Count) { index = 0; }
                if (index == 0)
                {
                    list.Last().Visibility = Visibility.Collapsed;
                }
                else list[index - 1].Visibility = Visibility.Collapsed;
                list[index].Visibility = Visibility.Visible;

                //if (RedRed.Visibility == Visibility.Visible)
                //{
                //    RedRed.Visibility = Visibility.Collapsed;
                //    GreenSquare.Visibility = Visibility.Visible;
                //}
                //else if (GreenSquare.Visibility == Visibility.Visible)
                //{
                //    RedRed.Visibility = Visibility.Visible;
                //    GreenSquare.Visibility = Visibility.Collapsed;
                //}
            }
        }

        internal void FinishBlock()
        {
            throw new NotImplementedException();
        }

        internal void SetBlockTimerText(string v)
        {
            throw new NotImplementedException();
        }

        internal void SetTimerText(string v)
        {
            throw new NotImplementedException();
        }

        internal void SetTrialLabel(Trial trial)
        {
            throw new NotImplementedException();
        }

        internal void ShowFeedback(bool v)
        {
            throw new NotImplementedException();
        }

        internal void ShowTrial(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
