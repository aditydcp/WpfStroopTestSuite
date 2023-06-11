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
using WpfStroopTestSuite.Controllers;
using WpfStroopTestSuite.Models;
using Color = WpfStroopTestSuite.Models.Color;
using Type = WpfStroopTestSuite.Models.Type;

namespace WpfStroopTestSuite.Pages
{
    /// <summary>
    /// Interaction logic for BlockView.xaml
    /// </summary>
    public partial class BlockView : Page, IPagePreviewKey
    {
        private BlockViewController? controller;
        private MainWindow? window;

        //// debug only
        //List<UIElement> list;
        //int index = 0;

        public BlockView()
        {
            InitializeComponent();

            // debug only
            //RedRed.Visibility = Visibility.Visible;
            //list = new List<UIElement>()
            //{
            //    RedRed, RedGreen, RedBlue, RedYellow,
            //    GreenRed, GreenGreen, GreenBlue, GreenYellow,
            //    BlueRed, BlueGreen, BlueBlue, BlueYellow,
            //    YellowRed, YellowGreen, YellowBlue, YellowYellow,
            //    RedSquare, GreenSquare, BlueSquare, YellowSquare
            //};
            //list[index].Visibility = Visibility.Visible;
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            controller = new BlockViewController(this);
            controller.ControlView();
            controller.StartBlock();

            // debug purpose
            window = App.FindParentOfType<MainWindow>(this) as MainWindow;
            window?.SetConsoleText();
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            controller!.StopAllTimer();
            controller = null;
        }

        public void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (controller!.IsReady())
            {
                // only register aplhanumeric keys
                if ((int)e.Key <= 69 && (int)e.Key >= 34)
                controller!.SubmitAnswer(e.Key);
            }

            //// debug only
            //if (e.Key == Key.Enter || e.Key == Key.Space)
            //{
            //    index++;
            //    if (index >= list.Count) { index = 0; }
            //    if (index == 0)
            //    {
            //        list.Last().Visibility = Visibility.Collapsed;
            //    }
            //    else list[index - 1].Visibility = Visibility.Collapsed;
            //    list[index].Visibility = Visibility.Visible;
            //}
        }

        internal void FinishBlock()
        {
            NavigationService.Navigate(new BlockResult());
        }

        #region UI Setters
        public void SetTimerText(string timeString) { TimerLabel.Text = timeString; }
        public void ShowTimer(bool show)
        {
            if (show) { TimerLabel.Visibility = Visibility.Visible; }
            else { TimerLabel.Visibility = Visibility.Collapsed; }
        }

        public void SetFeedbackLabel(Result result)
        {
            if (result == Result.Correct) { FeedbackLabel.Text = result.ToString().ToUpper(); }
            else { FeedbackLabel.Text = Result.Incorrect.ToString().ToUpper(); }
        }

        public void ShowFeedback(bool show)
        {
            if (show) { FeedbackLabel.Visibility = Visibility.Visible; }
            else { FeedbackLabel.Visibility = Visibility.Collapsed; }
        }

        public void SetTrialLabel(Color color, Type type)
        {
            if (type == Type.Square)
            {
                switch (color)
                {
                    case Color.Red:
                        RedSquare.Visibility = Visibility.Visible;
                        break;
                    case Color.Green:
                        GreenSquare.Visibility = Visibility.Visible;
                        break;
                    case Color.Blue:
                        BlueSquare.Visibility = Visibility.Visible;
                        break;
                    case Color.Yellow:
                        YellowSquare.Visibility = Visibility.Visible;
                        break;
                }
            }
            else
            {
                WordTrialLabel.Visibility = Visibility.Visible;
                WordTrialLabel.Content = type.ToString().ToUpper();
                WordTrialLabel.Foreground = ColorToBrushes(color);
            }
        }

        public void ShowTrial(bool show)
        {
            if (show) { TrialContainer.Visibility = Visibility.Visible; }
            else 
            { 
                WordTrialLabel.Visibility = Visibility.Collapsed;

                RedSquare.Visibility = Visibility.Collapsed;
                GreenSquare.Visibility = Visibility.Collapsed;
                BlueSquare.Visibility = Visibility.Collapsed;
                YellowSquare.Visibility = Visibility.Collapsed;

                TrialContainer.Visibility = Visibility.Collapsed;
            }
        }

        // debug only
        public void SetBlockTimerText(string timeString) { BlockTimerLabel.Text = timeString; }
        #endregion

        private static Brush ColorToBrushes(Color color)
        {
            return color switch
            {
                Color.Red => (Brush) Application.Current.FindResource("ColorRed"),
                Color.Green => (Brush) Application.Current.FindResource("ColorGreen"),
                Color.Blue => (Brush) Application.Current.FindResource("ColorBlue"),
                Color.Yellow => (Brush) Application.Current.FindResource("ColorYellow"),
                _ => (Brush) Application.Current.FindResource("TextColorPrimary"),
            };
        }
    }
}
