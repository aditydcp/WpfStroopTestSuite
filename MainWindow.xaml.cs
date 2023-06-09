using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfStroopTestSuite.Pages;
using Application = System.Windows.Application;
using WinForms = System.Windows.Forms;

namespace WpfStroopTestSuite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //try
            //{
            //    _hookID = InterceptKeys.SetHook(_proc);
            //}
            //catch
            //{
            //    DetachKeyboardHook();
            //}
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            //DetachKeyboardHook();
            base.OnClosing(e);
        }

        private void OnMainFrameLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LandingPage();
            ControlFontSize();
        }

        private void MainFrame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ControlFontSize();
        }

        private void WindowOnPreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                //Close();

                //if (MainFrame.Content.GetType() != typeof(LandingPage))
                //{
                //    App.ResetBlockData();
                //    MainFrame.Content = new BlockIntro();
                //}
            }
            if (e.Key == Key.F12)
            {
                if (DebugContainer.Visibility == Visibility.Visible) { DebugContainer.Visibility = Visibility.Collapsed; }
                else if (DebugContainer.Visibility == Visibility.Collapsed) { DebugContainer.Visibility = Visibility.Visible; }
            }
            
            // call page's preview key down method
            IPagePreviewKey? page = MainFrame.Content as IPagePreviewKey;
            page?.OnPreviewKeyDown(sender, e);
        }

        /// <summary>
        /// Get desired font size based on the Frame's height
        /// and set it to text font size variables
        /// </summary>
        private void ControlFontSize()
        {
            // Font size controller
            double sizeFactor = ((MainFrame.ActualHeight / 12) / 3 * 2) / 5;
            // size factor table
            // 1080 -> 12
            // 720 -> 8
            // 480 -> 5.333

            double titleFontSize = sizeFactor * 5.5;
            double bodyFontSize = sizeFactor * 2.5;
            double materialFontSize = sizeFactor * 3.5;
            double materialFocusFontSize = sizeFactor * 7.5;
            //double subBodyFontSize = sizeFactor * 2;

            Application.Current.Resources.Remove("TitleFontSize");
            Application.Current.Resources.Add("TitleFontSize", titleFontSize);
            Application.Current.Resources.Remove("BodyFontSize");
            Application.Current.Resources.Add("BodyFontSize", bodyFontSize);
            Application.Current.Resources.Remove("MaterialFontSize");
            Application.Current.Resources.Add("MaterialFontSize", materialFontSize);
            Application.Current.Resources.Remove("MaterialFocusFontSize");
            Application.Current.Resources.Add("MaterialFocusFontSize", materialFocusFontSize);
            //Application.Current.Resources.Remove("SubBodyFontSize");
            //Application.Current.Resources.Add("SubBodyFontSize", subBodyFontSize);
        }

        #region Keyboard Controller
        private static readonly InterceptKeys.LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        /// <summary>
        /// Detach the keyboard hook.
        /// Must be called during shutdown to prevent error
        /// </summary>
        private static void DetachKeyboardHook()
        {
            if (_hookID != IntPtr.Zero)
                InterceptKeys.UnhookWindowsHookEx(_hookID);
        }

        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                bool alt = (WinForms.Control.ModifierKeys & Keys.Alt) != 0;
                bool control = (WinForms.Control.ModifierKeys & Keys.Control) != 0;

                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                if (alt && key == Keys.F4)
                {
                    Application.Current.Shutdown();
                    return (IntPtr)1; // Handled.
                }

                if (!AllowKeyboardInput(alt, control, key))
                {
                    return (IntPtr)1; // Handled.
                }
            }

            return InterceptKeys.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        /// <summary>Determines whether the specified keyboard input should be allowed to be processed by the system.</summary>
        /// <remarks>Helps block unwanted keys and key combinations that could exit the app, make system changes, etc.</remarks>
        public static bool AllowKeyboardInput(bool alt, bool control, Keys key)
        {
            // Disallow various special keys.
            if (key <= Keys.Back || key == Keys.None ||
                key == Keys.Menu || key == Keys.Pause ||
                key == Keys.Help)
            {
                return false;
            }

            // Disallow ranges of special keys.
            // Currently leaves volume controls enabled; consider if this makes sense.
            // Disables non-existing Keys up to 65534, to err on the side of caution for future keyboard expansion.
            if ((key >= Keys.LWin && key <= Keys.Sleep) ||
                (key >= Keys.KanaMode && key <= Keys.HanjaMode) ||
                (key >= Keys.IMEConvert && key <= Keys.IMEModeChange) ||
                (key >= Keys.BrowserBack && key <= Keys.BrowserHome) ||
                (key >= Keys.MediaNextTrack && key <= Keys.LaunchApplication2) ||
                (key >= Keys.ProcessKey && key <= (Keys)65534))
            {
                return false;
            }

            // Disallow specific key combinations. (These component keys would be OK on their own.)
            if ((alt && key == Keys.Tab) ||
                (alt && key == Keys.Space) ||
                (control && key == Keys.Escape))
            {
                return false;
            }

            // Allow anything else (like letters, numbers, spacebar, braces, and so on).
            return true;
        }
        #endregion

        #region debug only
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (!App.BlockData.IsUnpopulated())
            //{
            SetConsoleText();
            //}
        }

        public void SetConsoleText()
        {
            ConsoleText.Text =
                App.Subject.ToConsoleString(false) + "\n" +
                "Block Data:\n" + App.BlockData.ToConsoleString();
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewportSizeLabel.Content = MainFrame.ActualHeight + "x" + MainFrame.ActualWidth;
        }
        #endregion
    }
}
