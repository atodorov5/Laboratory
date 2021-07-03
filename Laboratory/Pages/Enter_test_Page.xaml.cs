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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Laboratory.Windows;
using log4net;
using System.Text.RegularExpressions;

namespace Laboratory.Pages
{
    /// <summary>
    /// Interaction logic for Enter_test_Page.xaml
    /// </summary>
    public partial class Enter_test_Page : Page
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Enter_test_Page()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            testtypeListView.UnselectAll();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            laboratorydbDataSet laboratoryDataSet = (laboratorydbDataSet)this.FindResource("laboratorydbDataSet");

            laboratorydbDataSetTableAdapters.testtypeTableAdapter testTypeTableAdapter = new laboratorydbDataSetTableAdapters.testtypeTableAdapter();
            testTypeTableAdapter.Fill(laboratoryDataSet.testtype);
            CollectionViewSource testtype1ViewSource = (CollectionViewSource)this.FindResource("testtypeViewSource");
            testtype1ViewSource.View.MoveCurrentToFirst();

            laboratorydbDataSetTableAdapters.bloodtypeTableAdapter bloodTypeTableAdapter = new laboratorydbDataSetTableAdapters.bloodtypeTableAdapter();
            bloodTypeTableAdapter.Fill(laboratoryDataSet.bloodtype);
            CollectionViewSource bloodtypeViewSource = (CollectionViewSource)FindResource("bloodtypeViewSource");
            bloodtypeViewSource.View.MoveCurrentToFirst();


           
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ClearSelection(object sender, RoutedEventArgs e)
        {
            testtypeListView.UnselectAll();
        }

        private void Button_CheckUser(object sender, RoutedEventArgs e)
        {
                        
            laboratorydbDataSetTableAdapters.check_patientTableAdapter testTypeTableAdapter = new laboratorydbDataSetTableAdapters.check_patientTableAdapter();
            object gender ;
            string name, surname, lastname, address, email;
            int? bloodType;
            var res = testTypeTableAdapter.GetData(user_pinTB.Text, out name, out surname, out lastname, out gender, out address, out email, out bloodType);
            if (name != null)
            {
                user_nameTB.Text = name;
                user_surnameTB.Text = surname;
                user_lastnameTB.Text = lastname;
                user_addressTB.Text = address;
                emailTB.Text = email;

                if (Convert.ToBoolean(gender))
                    femaleRB.IsChecked = true;
                else
                    maleRB.IsChecked = true;

                bloodtypeComboBox.SelectedIndex = (int)bloodType;
                
            }
            else
                MessageBox.Show("Пациентът не е намерен!");


        }

        private void clean_controls()
        {
            foreach (Control ctl in patient_infoDG.Children)
            {
               
                if (ctl.GetType() == typeof(TextBox))
                    ((TextBox)ctl).Text = String.Empty;
                if (ctl.GetType() == typeof(CheckBox))
                    ((CheckBox)ctl).IsChecked = false;
                if (ctl.GetType() == typeof(ComboBox))
                    ((ComboBox)ctl).SelectedIndex = -1;
            }
        }

        private void Button_save_test(object sender, RoutedEventArgs e)
        {
            if (testtypeListView.SelectedItems.Count > 0 && user_pinTB.Text!="" && user_nameTB.Text != "" && user_lastnameTB.Text != "")
            {

                laboratorydbDataSetTableAdapters.retrieve_testTableAdapter testTableAdapter = new laboratorydbDataSetTableAdapters.retrieve_testTableAdapter();
                var pwd = new Password().LengthRequired(4).IncludeLowercase().IncludeNumeric();
                var result = pwd.Next();
                int? testid;
                double price = 0;
                foreach (DataRowView item in testtypeListView.SelectedItems)
                {
                    price += (double)item["price"];
                }
                price = Math.Round(price, 2);

                var gender= false;
                if (maleRB.IsChecked==true)
                    gender = false;
              else if (femaleRB.IsChecked == true)
                    gender = true;


                testTableAdapter.Insert(user_nameTB.Text, user_surnameTB.Text, user_lastnameTB.Text, user_pinTB.Text, gender, user_addressTB.Text, emailTB.Text, bloodtypeComboBox.SelectedIndex, Properties.Settings.Default.userID, ref_numberTB.Text, result, price, out testid);


                laboratorydbDataSetTableAdapters.QueriesTableAdapter queryTableAdapter = new laboratorydbDataSetTableAdapters.QueriesTableAdapter();

                foreach (DataRowView item in testtypeListView.SelectedItems)
                {
                    queryTableAdapter.insertResult(testid, (int)item["idTestType"]);
                }

                Test_label_Window test_Label_Window = new Test_label_Window((int)testid, price);
                log.Info(Properties.Settings.Default.user + " въведе изследване -" + testid);
                test_Label_Window.Show();
                clean_controls();
            }
            else
            {
                MessageBox.Show("Изберете изследване и въведете данни!");
            }
        
        }

        private void Button_PKK(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            
            foreach (DataRowView item in testtypeListView.Items)
            {
                Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter groupTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.type_groupTableAdapter();
                int group_id = (int)groupTableAdapter.get_test_by_group(button.Content.ToString());

                if ((int)item[4] == group_id)
                {
                    testtypeListView.SelectedItems.Add(item);
                }
            }
        }

       
    }


}
