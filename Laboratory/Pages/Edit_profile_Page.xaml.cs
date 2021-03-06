using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using Microsoft.Reporting.WinForms;
using Notifications.Wpf;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

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
            load_chart();
            
        }
        
        private void Button_SavePass(object sender, RoutedEventArgs e)
        {
            

            laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            string salt;
            retrieve_UsersTableAdapter.get_userSalt(Properties.Settings.Default.userID,out salt);
            Password_salt pass_gen = new Password_salt();

            string hashed_old = pass_gen.GenerateSHA256Hash(old_pass.Password, salt);


            if (new_pass.Password.Equals(new_pass2.Password))
            {
                if (passwordRequirements(new_pass.Password))
                {
                    string new_salt = Password_salt.CreateSalt(10);
                    string hashed_new = pass_gen.GenerateSHA256Hash(new_pass.Password, new_salt);
                    int res = retrieve_UsersTableAdapter.change_password(Properties.Settings.Default.userID, hashed_old, new_salt, hashed_new);
                    if (res < 1)
                        MessageBox.Show("Грешна парола!");
                    else
                    {
                        MessageBox.Show("Паролата е сменена успешно!");

                        old_pass.Clear();
                        new_pass.Clear();
                        new_pass2.Clear();

                    }
                }
                else
                {
            
                    MessageBox.Show("Новата парола трябва да съръдажа поне 1 главна буква и поне 1 число! Дължината на паролата трябва да е минимун 8 символа!");

                }


            }
            else
                MessageBox.Show("Нова парола не съвпада!");
           
  
        }

        private bool passwordRequirements(string password)
        {

            var rule = new Regex(@"^(?=.{8,16}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9]).*$");
            Match match = rule.Match(password);
            if (match.Success)
                return true;

            return false;
        }
      
        private void load_chart()
        {
            
            ReportDataSource reportDataSource1 = new ReportDataSource();
            laboratorydbDataSet dataset = new laboratorydbDataSet();

            dataset.BeginInit();

            reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
            reportDataSource1.Value = dataset.user_report;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "Laboratory.Reports.UserReport.rdlc";

            dataset.EndInit();

            //fill data into DataSet
            laboratorydbDataSetTableAdapters.user_reportTableAdapter userTableAdapter = new laboratorydbDataSetTableAdapters.user_reportTableAdapter();
            userTableAdapter.ClearBeforeFill = true;
            userTableAdapter.Fill(dataset.user_report,Properties.Settings.Default.userID);

            reportViewer2.RefreshReport();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            laboratorydbDataSet laboratoryDataSet = (laboratorydbDataSet)this.FindResource("laboratorydbDataSet");

            // Load data into the table bloodtype. You can modify this code as needed
            laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_UsersTableAdapter.FillByUserID(laboratoryDataSet.retrieve_users, Properties.Settings.Default.userID);
            CollectionViewSource retrieve_usersViewSource = (CollectionViewSource)this.FindResource("retrieve_usersViewSource");
            retrieve_usersViewSource.View.MoveCurrentToFirst();
        }
    }
}
