using Laboratory.Data;
using Laboratory.Windows;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Laboratory.Administrator
{
    /// <summary>
    /// Interaction logic for admin.xaml
    /// </summary>
    public partial class admin : Page
    {
        public admin()
        {
            InitializeComponent();
            clinicbranchDataGrid.UnselectAll();
            retrieve_usersDataGrid.UnselectAll();
            testtypeDataGrid.UnselectAll();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var conn = DBConfig.Connection)
            {

                String salt = Password_salt.CreateSalt(10);
                String hashedpass = Password_salt.GenerateSHA256Hash("ax", salt);

                conn.Open();

                MySqlCommand comm = conn.CreateCommand();
                comm.CommandText = "INSERT INTO user(name, surname,lastname,username,hashedPassWrd,salt,ClinicBranch_idClinicBranch,Roles_idRoles) VALUES(@name, @surname,@lastname,@username,@hashedPassWrd,@salt,@ClinicBranch_idClinicBranch,@Roles_idRoles)";
                comm.Parameters.AddWithValue("@name", "Milen");
                comm.Parameters.AddWithValue("@surname", "Angelov");
                comm.Parameters.AddWithValue("@lastname", "Mihailov");
                comm.Parameters.AddWithValue("@username", "ax");
                comm.Parameters.AddWithValue("@hashedPassWrd", hashedpass);
                comm.Parameters.AddWithValue("@salt", salt);
                comm.Parameters.AddWithValue("@ClinicBranch_idClinicBranch", 1);
                comm.Parameters.AddWithValue("@Roles_idRoles", 2);
                //comm.ExecuteNonQuery();
                MessageBox.Show("Успешно добавен лаборант!");
            }

        }

        private void updateViewTestType()
        {
            Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter bloodlabDataSetselect_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter();
            testtypeDataGrid.ItemsSource = null;
            testtypeDataGrid.ItemsSource = bloodlabDataSetselect_typeTableAdapter.GetData();
        }

        private void updateViewClinics()
        {
            Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter bloodlabDataSetselect_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter();
            clinicbranchDataGrid.ItemsSource = null;
            clinicbranchDataGrid.ItemsSource = bloodlabDataSetselect_typeTableAdapter.GetData();
        }

        private void updateViewUsers()
        {
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter bloodlabDataSetselect_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_usersDataGrid.ItemsSource = null;
            retrieve_usersDataGrid.ItemsSource = bloodlabDataSetselect_typeTableAdapter.GetData();
        }

        private void add_testtype(object sender, RoutedEventArgs e)
        {
            var test = new Type_Window();
            test.ShowDialog();
            if (test.DialogResult.HasValue && test.DialogResult.Value)
                updateViewTestType();
        }

        private void edit_testtype(object sender, RoutedEventArgs e)
        {
            DataRowView row = testtypeDataGrid.SelectedItem as DataRowView;
            if (row != null)
            {
                Edittype_Window edittype_Window = new Edittype_Window(row);
                edittype_Window.ShowDialog();

                if (edittype_Window.DialogResult.HasValue && edittype_Window.DialogResult.Value)
                    updateViewTestType();

            }
            else
                MessageBox.Show("Изберете тест!");
        }

        private void delete_testtype(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = testtypeDataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {
                Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter testTypeTA = new Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter();
                DataRowView row = (DataRowView)testtypeDataGrid.SelectedItems[0];
                testTypeTA.delete_testtype((int)row[0]);

                updateViewTestType();
            }
            else
                MessageBox.Show("Изберете тест");

        }

        private void add_laboratory(object sender, RoutedEventArgs e)
        {
            var window = new Add_clinic_Window();
            window.ShowDialog();
            clinicbranchDataGrid.Items.Refresh();
            updateViewClinics();

        }

        private void edit_clinic(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = clinicbranchDataGrid.SelectedItem as DataRowView;
            if (rowview != null)
            {
                var window = new Edit_clinic_Window(rowview);
                window.ShowDialog();
                updateViewClinics();

            }
        }

        private void delete_clinic(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = clinicbranchDataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {
                Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter clinicTA = new Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter();
                
                    DataRowView row = (DataRowView)clinicbranchDataGrid.SelectedItems[0];

                    clinicTA.delete_clinic((int)row[0]);
                
                updateViewClinics();
                
            }
            else
                MessageBox.Show("Изберете клиника");
        }

        private void delete_user(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = retrieve_usersDataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {
                using (var conn = DBConfig.Connection)
                {
                    try
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("deactivate_user", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new MySqlParameter("p_userID", MySqlDbType.Int32));
                        cmd.Parameters[0].Direction = System.Data.ParameterDirection.Input;
                        cmd.Parameters[0].Value = (int)rowview.Row[0];
                        if (cmd.ExecuteNonQuery() > 0)
                            MessageBox.Show("Успешно деактивиран");
                        else
                            MessageBox.Show("Профилът не може да бъде дективиран");

                        updateViewUsers();

                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Грешка " + ex);
                    }

                   
                }

                Page_Loaded(sender, e);
            }
            else
                MessageBox.Show("Изберете потребител");
        }

        private void add_user(object sender, RoutedEventArgs e)
        {
            var window = new Add_user_Window();
            window.ShowDialog();
            updateViewUsers();

           



    }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));


            // Load data into the table bloodtype. You can modify this code as needed
            Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter bloodlabDataSetclinicbranchTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter();
            bloodlabDataSetclinicbranchTableAdapter.Fill(laboratoryDataSet.clinicbranch);
            System.Windows.Data.CollectionViewSource clinicbranchViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clinicbranchViewSource")));
            clinicbranchViewSource.View.MoveCurrentToFirst();

            // Load data into the table bloodtype. You can modify this code as needed
            Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter testTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter();
            testTypeTableAdapter.Fill(laboratoryDataSet.testtype1);
            System.Windows.Data.CollectionViewSource testtype1ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testtype1ViewSource")));
            testtype1ViewSource.View.MoveCurrentToFirst();

            // Load data into the table bloodtype. You can modify this code as needed
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_UsersTableAdapter.Fill(laboratoryDataSet.retrieve_users);
            System.Windows.Data.CollectionViewSource retrieve_usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("retrieve_usersViewSource")));
            retrieve_usersViewSource.View.MoveCurrentToFirst();
        }

        private void edit_user(object sender, RoutedEventArgs e)
        {

        }
    }
    }
