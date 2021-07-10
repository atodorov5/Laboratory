using Laboratory.Data;
using Laboratory.Windows;
using log4net;
using MySql.Data.MySqlClient;
using Notifications.Wpf;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        Notification notification = new Notification();
        public admin()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
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
            /*Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter bloodlabDataSetselect_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_usersDataGrid.ItemsSource = null;
            retrieve_usersDataGrid.ItemsSource = bloodlabDataSetselect_typeTableAdapter.GetData(Properties.Settings.Default.labID);*/
            laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_UsersTableAdapter.Fill(laboratoryDataSet.retrieve_users, Properties.Settings.Default.labID);
            System.Windows.Data.CollectionViewSource retrieve_usersViewSource = (System.Windows.Data.CollectionViewSource)(this.FindResource("retrieve_usersViewSource"));
            retrieve_usersViewSource.View.MoveCurrentToFirst();
        }

        private void updateViewGroups()
        {
            Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter group_typeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
            type_groupDataGrid.ItemsSource = null;
            type_groupDataGrid.ItemsSource = group_typeTableAdapter.GetData();
        }

        private void updateViewRefValues(int id)
        {
            laboratorydbDataSetTableAdapters.ref_valueTableAdapter refval_TableAdapter = new laboratorydbDataSetTableAdapters.ref_valueTableAdapter();
            ref_valueDataGrid.ItemsSource = null;
            ref_valueDataGrid.ItemsSource = refval_TableAdapter.GetDataByTypeID(id);
            ref_valueDataGrid.Items.Refresh();
            System.Windows.Data.CollectionViewSource ref_valViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("ref_valueViewSource");
            ref_valViewSource.View.MoveCurrentToFirst();
            ref_valViewSource.View.Refresh();
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
                    laboratorydbDataSetTableAdapters.testtypeTableAdapter testTypeTA = new laboratorydbDataSetTableAdapters.testtypeTableAdapter();
                    DataRowView row = (DataRowView)testtypeDataGrid.SelectedItems[0];
                    int res = testTypeTA.delete_testtype((int)row[0]);
                    
                    if (res <= 0)                   
                        notification.show("Изследването не може да бъде изтрито!", 'e');
                    
                    updateViewTestType();
                }
                catch(MySqlException ex)
                {
                    log.Error(ex.Message);
                    notification.show("Изследването не може да бъде изтрито!", 'e');
                    
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

            laboratorydbDataSet laboratoryDataSet = (laboratorydbDataSet)FindResource("laboratorydbDataSet");
            
            // Load data into the table bloodtype. You can modify this code as needed
            Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter testTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtypeTableAdapter();
            testTypeTableAdapter.Fill(laboratoryDataSet.testtype);
            System.Windows.Data.CollectionViewSource testtypeViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("testtypeViewSource");
            testtypeViewSource.View.MoveCurrentToFirst();

            // Load data into the table bloodtype. You can modify this code as needed
            Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter retrieve_UsersTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_usersTableAdapter();
            retrieve_UsersTableAdapter.Fill(laboratoryDataSet.retrieve_users,Properties.Settings.Default.labID);
            System.Windows.Data.CollectionViewSource retrieve_usersViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("retrieve_usersViewSource");
            retrieve_usersViewSource.View.MoveCurrentToFirst();
            
            Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter type_groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
            type_groupTableAdapter.Fill(laboratoryDataSet.type_group);
            System.Windows.Data.CollectionViewSource type_groupreViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("type_groupViewSource");
            type_groupreViewSource.View.MoveCurrentToFirst();
            /*
            Laboratory.laboratorydbDataSetTableAdapters.ref_valueTableAdapter ref_valTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.ref_valueTableAdapter();
            ref_valTableAdapter.FillByTypeID(laboratoryDataSet.ref_value, 2);
            System.Windows.Data.CollectionViewSource ref_valViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("ref_valueViewSource");
            ref_valViewSource.View.MoveCurrentToFirst();*/
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
            testGroupWindow.groupBtn.Content = "Добави";
            testGroupWindow.ShowDialog();

            updateViewGroups();
        }

        private void edit_group(object sender, RoutedEventArgs e)
        {


            DataRowView rowview = type_groupDataGrid.SelectedItem as DataRowView;
            if (rowview != null)
            {

                TestGroupWindow testGroupWindow = new TestGroupWindow(rowview);
                testGroupWindow.groupBtn.Content = "Запази";
                testGroupWindow.ShowDialog();

                updateViewGroups();
            }
            else
                MessageBox.Show("Изберете група");


        }
        private void delete_group(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = type_groupDataGrid.SelectedItem as DataRowView;

            if (rowview != null)
            {
                try
                {
                    if (MessageBox.Show( "Сигурни ли сте че желаете да изтриете " + rowview[1] + " ?", "Изтриване на група", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        laboratorydbDataSetTableAdapters.type_groupTableAdapter testGroupTA = new laboratorydbDataSetTableAdapters.type_groupTableAdapter();
                        DataRowView row = (DataRowView)type_groupDataGrid.SelectedItems[0];
                        int res = testGroupTA.delete_type_group((int)row[0]);

                        if (res <= 0)
                        {
                            MessageBox.Show("Групата не може да бъде изтрита!");
                        }

                        updateViewGroups();
                    }
                    else
                    {
                       
                    }


                  
                }
                catch (MySqlException ex)
                {
                    log.Error(ex.Message);
                    MessageBox.Show("Групата не може да бъде изтрита!");
                }

            }
            else
                MessageBox.Show("Изберете група");

        }
        string title;
        private void load_ref_val(object sender, RoutedEventArgs e)
        {
            laboratorydbDataSet laboratoryDataSet = (laboratorydbDataSet)this.FindResource("laboratorydbDataSet");

            DataRowView row = (DataRowView)testtypeDataGrid.SelectedItems[0];

            laboratorydbDataSetTableAdapters.ref_valueTableAdapter ref_valTableAdapter = new laboratorydbDataSetTableAdapters.ref_valueTableAdapter();

            ref_valTableAdapter.FillByTypeID(laboratoryDataSet.ref_value, (int)row[0]);

            System.Windows.Data.CollectionViewSource ref_valViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("ref_valueViewSource");
            ref_valViewSource.View.MoveCurrentToFirst();
            ref_valViewSource.View.Refresh();

            title = "Референтни стойности за " + row[1];
            ref_val_lb.Content = title;
        }


            private void ref_valueDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
         {
            load_ref_val(sender,e);
           
        }

        private void edit_refVal(object sender, RoutedEventArgs e)
        {
            if (ref_valueDataGrid.SelectedItems.Count<1)
            {
                notification.show("Изберете стойност!", 'w');
            }
            else
            {
                DataRowView row = (DataRowView)ref_valueDataGrid.SelectedItems[0];
                
                Ref_Values_Window ref_Values_Window = new Ref_Values_Window(row,title);
                ref_Values_Window.ShowDialog();


                if (ref_Values_Window.DialogResult.HasValue && ref_Values_Window.DialogResult.Value)
                {
               

                    laboratorydbDataSet laboratoryDataSet = (laboratorydbDataSet)this.FindResource("laboratorydbDataSet");

                    DataRowView row2 = (DataRowView)testtypeDataGrid.SelectedItems[0];

                    laboratorydbDataSetTableAdapters.ref_valueTableAdapter ref_valTableAdapter = new laboratorydbDataSetTableAdapters.ref_valueTableAdapter();

                    ref_valTableAdapter.FillByTypeID(laboratoryDataSet.ref_value, (int)row2[0]);

                    System.Windows.Data.CollectionViewSource ref_valViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("ref_valueViewSource");
                    ref_valViewSource.View.MoveCurrentToFirst();
                    ref_valViewSource.View.Refresh();
                }

                
            }

        }
    }
}
