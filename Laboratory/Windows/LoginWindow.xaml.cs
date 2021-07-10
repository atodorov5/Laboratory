using Laboratory.Data;
using MySql.Data.MySqlClient;
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
using log4net;
using PasswordGenerator;

namespace Laboratory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public LoginWindow()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            
        }

       
            private void login(object sender, RoutedEventArgs e)
        {


            string salt, hashedpass;
            int? roleID, userid , labID;


            if (usernameTB.Text != "")
            {

                Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
                retrieve_UsersTableAdapter.login_procedure(usernameTB.Text,out hashedpass, out salt,out roleID, out userid, out labID);

                Password_salt pass_gen = new Password_salt();
                if (pass_gen.GenerateSHA256Hash(passwordTB.Password.ToString(), salt) == hashedpass)
                {
                    
                    Properties.Settings.Default.user = usernameTB.Text;
                    Properties.Settings.Default.roleID = (int)roleID;
                    Properties.Settings.Default.userID = (int)userid;
                    Properties.Settings.Default.labID = (int)labID;
                    Properties.Settings.Default.Save();
                    log.Info("User " + usernameTB.Text + " has logged.");
                    UserWindow userW = new UserWindow();
                    userW.Show();
                    this.Close();

                }
                else
                {
                    loginError.Content = "Грешно потребителско име или парола!";
                }

            }
            else
                loginError.Content = "Грешно потребителско име или парола!";

        }
        
    }
}
