using Laboratory.Data;
using log4net;
using MySql.Data.MySqlClient;
using PasswordGenerator;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Laboratory.Windows
{
    /// <summary>
    /// Interaction logic for Add_user_Window.xaml
    /// </summary>
    public partial class Add_user_Window : Window
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Add_user_Window()
        {
            InitializeComponent();
            labCB.SelectedIndex = 2;
            log4net.Config.XmlConfigurator.Configure();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            laboratorydbDataSet laboratorydbDataSet = (laboratorydbDataSet)this.FindResource("laboratorydbDataSet");
            // Load data into the table clinicbranch. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter laboratorydbDataSetclinicbranchTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter();
            laboratorydbDataSetclinicbranchTableAdapter.Fill(laboratorydbDataSet.clinicbranch);
            CollectionViewSource clinicbranchViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clinicbranchViewSource")));
            clinicbranchViewSource.View.MoveCurrentToFirst();

            // Load data into the table roles. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.rolesTableAdapter laboratorydbDataSetrolesTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.rolesTableAdapter();
            laboratorydbDataSetrolesTableAdapter.Fill(laboratorydbDataSet.roles);
            System.Windows.Data.CollectionViewSource rolesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("rolesViewSource")));
            rolesViewSource.View.MoveCurrentToFirst();
        }

        private void add_user_btn(object sender, RoutedEventArgs e)
        {
            Laboratory.laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            string salt = Password_salt.CreateSalt(10);

            var pwd = new Password().LengthRequired(8).IncludeLowercase().IncludeNumeric().IncludeUppercase();
            var result = pwd.Next();
            MessageBox.Show("Потребителско име: "+ usernameTB.Text + " Парола: " + result,"Данни", MessageBoxButton.OK);

            Password_salt pass_gen = new Password_salt();
            string hashedpass = pass_gen.GenerateSHA256Hash(result, salt);
            retrieve_UsersTableAdapter.add_lab_user(firstnameTB.Text, surnameTB.Text, lastnameTB.Text, usernameTB.Text, pinTB.Text, addressTB.Text, phoneTB.Text, hashedpass, salt, (int)labCB.SelectedValue, (int)roleCB.SelectedValue);
            log.Info("Потребител-" + Properties.Settings.Default.user + " добави потребител " + usernameTB.Text);
            this.Close();



        }
    }

   
}
