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

        }

        private void Button_Check_Test(object sender, RoutedEventArgs e)
        {
            
             Laboratory.laboratorydbDataSet laboratorydbDataSet = ((Laboratory.laboratorydbDataSet)(this.FindResource("laboratorydbDataSet")));
             // Load data into the table clinicbranch. You can modify this code as needed.
             Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter resultTableAdapter = new Laboratory.laboratorydbDataSetTableAdapters.select_result_byTestIDTableAdapter();
             resultTableAdapter.Fill(laboratorydbDataSet.select_result_byTestID, Int32.Parse(test_idTB.Text));
             System.Windows.Data.CollectionViewSource resultViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("select_result_byTestIDViewSource")));
             resultViewSource.View.MoveCurrentToFirst();
        
            var dt_n = select_result_byTestIDDataGrid.Items.Count;

            for (int i = 0; i < dt_n; i++)
            {
                DataRowView rowView = (select_result_byTestIDDataGrid.Items[0] as DataRowView); //Get RowView
                rowView.BeginEdit();
                if(Convert.ToDouble(rowView[0])==1)
                    rowView[12] = "ПОЛОЖИТЕЛЕН";
                else if (Convert.ToDouble(rowView[0]) == 0)
                    rowView[12] = "ОТРИЦАТЕЛЕН";
                else if(Convert.ToDouble( rowView[0])> Convert.ToDouble(rowView[8]))
                    rowView[12] = "H";
                else if (Convert.ToDouble(rowView[0]) < Convert.ToDouble(rowView[7]))
                    rowView[12] = "L";
                rowView.EndEdit();
                select_result_byTestIDDataGrid.Items.Refresh();
            }
            
        }
    }
}
