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
using System.Windows.Shapes;

namespace WpfStroopTestSuite
{
    /// <summary>
    /// Interaction logic for SubjectForm.xaml
    /// </summary>
    public partial class SubjectForm : Window
    {
        public SubjectForm()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void WindowOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OnStartButtonClicked(this, new RoutedEventArgs());
            }
        }

        private void OnStartButtonClicked(object sender, RoutedEventArgs e)
        {
            App.Subject.SetSubjectCredentials(SubjectNameInput.Text, SubjectIdInput.Text, GroupIdInput.Text);
            Window window = new MainWindow();
            window.Show();
            Close();
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        // debug only
        private void WindowOnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(SubjectNameInput.Text);
            Console.WriteLine(SubjectIdInput.Text);
            Console.WriteLine(GroupIdInput.Text);
        }
    }
}
