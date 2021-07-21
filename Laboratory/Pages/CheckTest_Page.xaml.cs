using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Laboratory.Pages
{
    /// <summary>
    /// Interaction logic for CheckTest_Page.xaml
    /// </summary>
    public partial class CheckTest_Page : Page
    {
      
        public CheckTest_Page()
        {
            InitializeComponent();
            DataContext = new Test();
        }

        private void Button_Check(object sender, RoutedEventArgs e)
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;

            if (testID.Text == "")
            {
                MessageBox.Show("Въведете номер на изледване!");
            }
            else
            {
                int test_id = Convert.ToInt32(testID.Text);

                this.reportViewer.Reset();
                this.reportViewer.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource1 = new ReportDataSource();
                laboratorydbDataSet dataset = new laboratorydbDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "laboratorydbDataSet"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.select_result_byTestID;
                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "Laboratory.Reports.CheckTestReport.rdlc";

                dataset.EndInit();

                //fill data into DataSet
                laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter resultTableAdapter = new laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter();
                resultTableAdapter.ClearBeforeFill = true;
                int res = resultTableAdapter.Fill(dataset.select_result_byTestID, test_id, 1);

                if (res < 1)
                {
                    MessageBox.Show("Няма данни за въведеното изследване!");
                    this.reportViewer.Reset();
                }
                reportViewer.RefreshReport();

            }
        }

        
        
        private void testID2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
             Regex regex = new Regex("[^0-9]+");


            if (regex.IsMatch(e.Text))
            {
                warningLB.Visibility = Visibility.Visible;
            }
            else
            {

                warningLB.Visibility = Visibility.Hidden;
            }         


        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            

            
        }

        private void Button_CheckByPIn(object sender, RoutedEventArgs e)
        {

            laboratorydbDataSet laboratoryDataSet = (laboratorydbDataSet)this.FindResource("laboratorydbDataSet");

            // Load data into the table bloodtype. You can modify this code as needed
            laboratorydbDataSetTableAdapters.retrieve_testTableAdapter retrieve_UsersTableAdapter = new laboratorydbDataSetTableAdapters.retrieve_testTableAdapter();
            retrieve_UsersTableAdapter.FillBy2(laboratoryDataSet.retrieve_test, userPin.Text);
            CollectionViewSource retrieve_usersViewSource = (CollectionViewSource)this.FindResource("retrieve_testViewSource");
            retrieve_usersViewSource.View.MoveCurrentToFirst();
        }

        private void retrieve_testDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;

            if (userPin.Text == "")
            {
                MessageBox.Show("Въведете номер на изледване!");
            }
            else
            {
                DataRowView row = sender as DataRowView;
                DataRowView row2 = (DataRowView)retrieve_testDataGrid.SelectedItems[0];
                int test_id = (int)row2["idTest"];
                
                this.reportViewer.Reset();
                this.reportViewer.LocalReport.DataSources.Clear();

                ReportDataSource reportDataSource1 = new ReportDataSource();
                laboratorydbDataSet dataset = new laboratorydbDataSet();

                dataset.BeginInit();

                reportDataSource1.Name = "laboratorydbDataSet"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = dataset.select_result_byTestID;
                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "Laboratory.Reports.CheckTestReport.rdlc";

                dataset.EndInit();

                //fill data into DataSet
                laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter resultTableAdapter = new laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter();
                resultTableAdapter.ClearBeforeFill = true;
                int res = resultTableAdapter.Fill(dataset.select_result_byTestID, test_id, 1);

                if (res < 1)
                {
                    MessageBox.Show("Няма данни за въведеното изследване!");
                    this.reportViewer.Reset();
                }
                reportViewer.RefreshReport();

            }
        }
    }
}
