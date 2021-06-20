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
using Laboratory.Data;

namespace Laboratory.Pages
{
    /// <summary>
    /// Interaction logic for Edit_profile_Page.xaml
    /// </summary>
    public partial class Edit_profile_Page : Page
    {
        public Edit_profile_Page()
        {
            InitializeComponent();
        }

        private void Button_SavePass(object sender, RoutedEventArgs e)
        {
            laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            string salt;
            retrieve_UsersTableAdapter.get_userSalt(Properties.Settings.Default.userID,out salt);


            string hashed_old = Password_salt.GenerateSHA256Hash(old_pass.Password, salt);


            
            if (new_pass.Password.Equals(new_pass2.Password))
            {
                string new_salt = Password_salt.CreateSalt(10);
                string hashed_new = Password_salt.GenerateSHA256Hash(new_pass.Password, new_salt);
                int res = retrieve_UsersTableAdapter.change_password(Properties.Settings.Default.userID, hashed_old, new_salt, hashed_new);
                if (res < 1)
                    MessageBox.Show("Грешна парола!");
                else
                {
                    MessageBox.Show("Успешно сменена парола!");
                    old_pass.Clear();
                    new_pass.Clear();
                    new_pass2.Clear();
                }
            }
            else
                MessageBox.Show("Нова парола не съвпада!");
        }
    }
}
