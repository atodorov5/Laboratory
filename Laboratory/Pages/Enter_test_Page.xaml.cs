﻿using PasswordGenerator;
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

namespace Laboratory.Pages
{
    /// <summary>
    /// Interaction logic for Enter_test_Page.xaml
    /// </summary>
    public partial class Enter_test_Page : Page
    {
        public Enter_test_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));

            Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter testTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.testtype1TableAdapter();
            testTypeTableAdapter.Fill(laboratoryDataSet.testtype1);
            System.Windows.Data.CollectionViewSource testtype1ViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("testtype1ViewSource")));
            testtype1ViewSource.View.MoveCurrentToFirst();

            Laboratory.laboratorydbDataSetTableAdapters.bloodtypeTableAdapter bloodTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.bloodtypeTableAdapter();
            bloodTypeTableAdapter.Fill(laboratoryDataSet.bloodtype);
            System.Windows.Data.CollectionViewSource bloodtypeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("bloodtypeViewSource")));
            bloodtypeViewSource.View.MoveCurrentToFirst();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_ClearSelection(object sender, RoutedEventArgs e)
        {
            testtype1ListView.UnselectAll();
        }

        private void Button_CheckUser(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            
            Laboratory.laboratorydbDataSetTableAdapters.check_patientTableAdapter testTypeTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.check_patientTableAdapter();
            object gender ;
            int? lab = 0;
            string name, surname, lastname, address;
            var res = testTypeTableAdapter.GetData(user_pinTB.Text, out name, out surname, out lastname, out gender, out address, out lab);
            if (name != null)
            {
                user_nameTB.Text = name;
                user_surnameTB.Text = surname;
                user_lastnameTB.Text = lastname;
                user_addressTB.Text = address;

                if (Convert.ToBoolean(gender))
                    femaleRB.IsChecked = true;
                else
                    maleRB.IsChecked = true;

                bloodtypeComboBox.SelectedIndex = (int)lab;
                bloodtypeComboBox.IsEnabled = false;
            }
            else
                MessageBox.Show("Пациентът не е намерен!");


        }

        private void Button_save_test(object sender, RoutedEventArgs e)
        {
            //Laboratory.laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));

            Laboratory.laboratorydbDataSetTableAdapters.retrieve_testTableAdapter testTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_testTableAdapter();
            var pwd = new Password().LengthRequired(4).IncludeLowercase().IncludeNumeric();
            var result = pwd.Next();
            int? testid;
            testTableAdapter.Insert(user_nameTB.Text, user_surnameTB.Text, user_lastnameTB.Text, user_pinTB.Text, 1, user_addressTB.Text, bloodtypeComboBox.SelectedIndex, Properties.Settings.Default.userID, ref_numberTB.Text, result, out testid);
            

            Laboratory.laboratorydbDataSetTableAdapters.QueriesTableAdapter queryTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.QueriesTableAdapter();

            foreach (DataRowView item in testtype1ListView.SelectedItems)
            {
                queryTableAdapter.insertResult(testid, (int)item["idTestType"]);
            }
                  

        }
    }


}
