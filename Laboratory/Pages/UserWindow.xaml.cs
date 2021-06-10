using Laboratory.Administrator;
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
using Laboratory.Pages;

namespace Laboratory
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            if(Properties.Settings.Default.roleID > 1)
            {
                adminItem.Visibility = Visibility.Hidden;
            }
        }

        private void MenuItem_Admin(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.userID==1)
            userMainFrame.Content = new admin();
            else
                MessageBox.Show("Нямате права за достъп");
            
            
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Enter_test_Page();
        }

        private void MenuItem_NewTest(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Enter_test_Page();
        }

        private void MenuItem_pending(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Fill_test_Page();
        }

        private void MenuItem_checkTest(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Check_test_Page();
        }
    }
}
