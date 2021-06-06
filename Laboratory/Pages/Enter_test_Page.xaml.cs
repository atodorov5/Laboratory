using System;
using System.Collections.Generic;
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
        }

    }


}
