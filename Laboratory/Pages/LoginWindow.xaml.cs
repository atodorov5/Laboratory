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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var conn = DBConfig.Connection)
            {
                String salt = Password_salt.CreateSalt(10);
                String hashedpass = Password_salt.GenerateSHA256Hash("az", salt);

                conn.Open();

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO user(name, surname,lastname,username,hashedPassWrd,salt,ClinicBranch_idClinicBranch,Roles_idRoles) VALUES(@name, @surname,@lastname,@username,@hashedPassWrd,@salt,@ClinicBranch_idClinicBranch,@Roles_idRoles)";
                comm.Parameters.AddWithValue("@name", "Angel");
                comm.Parameters.AddWithValue("@surname", "Angelov");
                comm.Parameters.AddWithValue("@lastname", "Mihailov");
                comm.Parameters.AddWithValue("@username", "az");
                comm.Parameters.AddWithValue("@hashedPassWrd", hashedpass);
                comm.Parameters.AddWithValue("@salt", salt);
                comm.Parameters.AddWithValue("@ClinicBranch_idClinicBranch", 1);
                comm.Parameters.AddWithValue("@Roles_idRoles", 1);
                comm.ExecuteNonQuery();
                MessageBox.Show("Успешно добавен лаборант!");
            }
        }

            private void login2(object sender, RoutedEventArgs e)
        {


            string salt, hashedpass;
            int? roleID, userid , labID;


            if (usernameTB.Text != "")
            {

                Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
                retrieve_UsersTableAdapter.login_procedure(usernameTB.Text,out hashedpass, out salt,out roleID, out userid, out labID);


                if (Password_salt.GenerateSHA256Hash(passwordTB.Password.ToString(), salt) == hashedpass)
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
