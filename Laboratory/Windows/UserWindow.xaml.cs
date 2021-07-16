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
using System.Diagnostics;

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
            usernameLabel.Content = "Потребител: " + Properties.Settings.Default.user;
            if (Properties.Settings.Default.roleID != 1)
            {
                adminItem.Visibility = Visibility.Collapsed;
            }
        }

        private void MenuItem_Admin(object sender, RoutedEventArgs e)
        {
            if(Properties.Settings.Default.roleID==1)
            userMainFrame.Content = new admin();
            else
                MessageBox.Show("Нямате права за достъп");
            
            
        }

        private void MenuItem_Close(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }

        Enter_test_Page enter_Test_Page;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if(enter_Test_Page==null)
                enter_Test_Page = new Enter_test_Page();
            userMainFrame.Content = enter_Test_Page;
        }

        private void MenuItem_NewTest(object sender, RoutedEventArgs e)
        {
            userMainFrame.Content = new Enter_test_Page();
        }
        Fill_test_Page fill_Test_Page;
        private void MenuItem_pending(object sender, RoutedEventArgs e)
        {
            
            if(fill_Test_Page==null)
                fill_Test_Page = new Fill_test_Page();            
            userMainFrame.Content = fill_Test_Page;
            //fill_Test_Page.load_pending();
            TestCollection col3 = new TestCollection();
            fill_Test_Page.retrieve_testListView.ItemsSource = col3.fillColection();
        }

        private void MenuItem_checkTest(object sender, RoutedEventArgs e)
        {
            //userMainFrame.Content = new Check_test_Page();
            userMainFrame.Content = new CheckTest_Page();
        }

        Edit_profile_Page edit_Profile_Page;
        private void MenuItem_Edit_Pwrd(object sender, RoutedEventArgs e)
        {
            if(edit_Profile_Page==null)
                edit_Profile_Page= new Edit_profile_Page();
            userMainFrame.Content = edit_Profile_Page;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
