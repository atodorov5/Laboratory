using MySql.Data.MySqlClient;
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
    /// Interaction logic for Check_test_Page.xaml
    /// </summary>
    public partial class Check_test_Page : Page
    {
        public Check_test_Page()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Laboratory.laboratorydbDataSet laboratoryDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));

            Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter bloodlabDataSetclinicbranchTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.clinicbranchTableAdapter();
            bloodlabDataSetclinicbranchTableAdapter.FillBy(laboratoryDataSet.clinicbranch,1);
            System.Windows.Data.CollectionViewSource clinicbranchViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("clinicViewSource")));
            clinicbranchViewSource.View.MoveCurrentToFirst();
        }

        private void Button_Check_Test(object sender, RoutedEventArgs e)
        {
            int res=0;

            if (test_idTB.Text != "")
            {
                Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
                // Load data into the table clinicbranch. You can modify this code as needed.
                Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter resultTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter();
                res = resultTableAdapter.Fill(laboratorydbDataSet.select_result_byTestID, Int32.Parse(test_idTB.Text), 1);
                System.Windows.Data.CollectionViewSource resultViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("select_result_byTestIDViewSource")));
                resultViewSource.View.MoveCurrentToFirst();
            }
           
                if (res > 0)
                {
                    var dt_n = select_result_byTestIDDataGrid.Items.Count;

                    for (int i = 0; i < dt_n; i++)
                    {
                        DataRowView rowView = select_result_byTestIDDataGrid.Items[i] as DataRowView; //Get RowView
                        rowView.BeginEdit();
                        if (Convert.ToDouble(rowView[7]) == 0 & Convert.ToDouble(rowView[8]) == 0)
                        {
                            if (Convert.ToDouble(rowView[0]) == 1)
                                rowView[12] = "ПОЛОЖИТЕЛЕН";
                            else if (Convert.ToDouble(rowView[0]) == 0)
                                rowView[12] = "ОТРИЦАТЕЛЕН";
                        }
                        else
                        {
                            if (Convert.ToDouble(rowView[0]) > Convert.ToDouble(rowView[8]))
                                rowView[12] = "H";
                            else if (Convert.ToDouble(rowView[0]) < Convert.ToDouble(rowView[7]))
                                rowView[12] = "L";
                            rowView.EndEdit();
                            select_result_byTestIDDataGrid.Items.Refresh();
                        }
                    }
                }
                else
                    MessageBox.Show("Тестът не е оброботен или не съществува!", "Известие", MessageBoxButton.OK, MessageBoxImage.Asterisk);
          

            

        }

        private void Button_Print(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();            
            printDlg.PrintVisual(this.printDG, "Резултати от изследване");

        }

    }
}
