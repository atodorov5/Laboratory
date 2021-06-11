using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Test_values_Page.xaml
    /// </summary>
    public partial class Test_values_Page : Page
    {
        DataRowView row;
        TabControl tabctrl;
        ListView list;
        public Test_values_Page(DataRowView row,TabControl tabctrl, ListView ls)
        {
            InitializeComponent();
            this.row = row;
            name_label.Content = row["p_name"].ToString() + " " + row["p_surname"].ToString() + " " + row["p_lastname"].ToString();
            pin_label.Content = row["pin"].ToString();
            select_result_byTestIDListView.UnselectAll();
            ref_numbTV.Content = row["refNumber"].ToString();
            this.tabctrl = tabctrl;
            this.list = ls;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
            // Load data into the table clinicbranch. You can modify this code as needed.
            Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter resultTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter();
            resultTableAdapter.Fill(laboratorydbDataSet.select_result_byTestID, (int)row["idTest"]);
            System.Windows.Data.CollectionViewSource resultViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("select_result_byTestIDViewSource")));
            resultViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Save_Results(object sender, RoutedEventArgs e)
        {
            int count = select_result_byTestIDListView.Items.Count;

            Laboratory.laboratorydbDataSetTableAdapters.QueriesTableAdapter queryTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.QueriesTableAdapter();
            

            for (int i = 0; i < count; i++)
            {
                DataRowView item = (DataRowView)select_result_byTestIDListView.Items[i];

                queryTableAdapter.enter_test_result(item[0].ToString(), (int)item[10],(int)item[11]);
            }

            queryTableAdapter.set_test_status((int)row["idTest"]);



            if (MessageBox.Show("Резултатите са запазени", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                tabctrl.Items.Remove(tabctrl.SelectedItem);
                tabctrl.Items.Refresh();

                Laboratory.laboratorydbDataSetTableAdapters.retrieve_testTableAdapter testTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.retrieve_testTableAdapter();
                list.ItemsSource =  testTableAdapter.GetPendingTests();

            }
            else
            {
                
            }
            
        }
    }
}
