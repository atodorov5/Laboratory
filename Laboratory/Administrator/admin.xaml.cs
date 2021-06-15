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
            retrieve_usersDataGrid.UnselectAll();
            testtypeDataGrid.UnselectAll();

            
        }

        

        private void updateViewTestType()
        {
            Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter bloodlabDataSetselect_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter();
            testtypeDataGrid.ItemsSource = null;
            testtypeDataGrid.ItemsSource = bloodlabDataSetselect_typeTableAdapter.GetData();
        }

        private void updateViewUsers()
        {
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter bloodlabDataSetselect_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_usersDataGrid.ItemsSource = null;
            retrieve_usersDataGrid.ItemsSource = bloodlabDataSetselect_typeTableAdapter.GetData();
        }

        private void updateViewGroups()
        {
            Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter group_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
            type_groupDataGrid.ItemsSource = null;
            type_groupDataGrid.ItemsSource = group_typeTableAdapter.GetData();
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
                try
                {
                    Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter testTypeTA = new Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter();
                    DataRowView row = (DataRowView)testtypeDataGrid.SelectedItems[0];
                    testTypeTA.delete_testtype((int)row[0]);

                    updateViewTestType();
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
            else
                MessageBox.Show("Изберете тест");

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
            Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter testTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter();
            testTypeTableAdapter.Fill(laboratoryDataSet.testtype);
            System.Windows.Data.CollectionViewSource testtypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testtypeViewSource")));
            testtypeViewSource.View.MoveCurrentToFirst();

            // Load data into the table bloodtype. You can modify this code as needed
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_UsersTableAdapter.Fill(laboratoryDataSet.retrieve_users);
            System.Windows.Data.CollectionViewSource retrieve_usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("retrieve_usersViewSource")));
            retrieve_usersViewSource.View.MoveCurrentToFirst();

            Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter type_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
            type_groupTableAdapter.Fill(laboratoryDataSet.type_group);
            System.Windows.Data.CollectionViewSource type_groupreViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("type_groupViewSource")));
            type_groupreViewSource.View.MoveCurrentToFirst();
        }

        private void edit_user(object sender, RoutedEventArgs e)
        {

            DataRowView rowview = retrieve_usersDataGrid.SelectedItem as DataRowView;
            if (rowview != null)
            {
                var edit_User = new Edit_user_Window(rowview);
                edit_User.ShowDialog();
                updateViewUsers();
            }

        }

        private void add_group(object sender, RoutedEventArgs e)
        {

            TestGroupWindow testGroupWindow = new TestGroupWindow();
            testGroupWindow.ShowDialog();

            updateViewGroups();
        }

        private void edit_group(object sender, RoutedEventArgs e)
        {


            DataRowView rowview = type_groupDataGrid.SelectedItem as DataRowView;
            if (rowview != null)
            {

                TestGroupWindow testGroupWindow = new TestGroupWindow(rowview);
                testGroupWindow.ShowDialog();

                updateViewGroups();
            }



        }
    }
    }
