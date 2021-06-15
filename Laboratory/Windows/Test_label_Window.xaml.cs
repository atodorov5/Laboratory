using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Shapes;

namespace Laboratory.Windows
{
    /// <summary>
    /// Interaction logic for Test_label_Window.xaml
    /// </summary>
    public partial class Test_label_Window : Window
    {
       
        public Test_label_Window(int testID,double price)
        {
            InitializeComponent();
            testIDLabel.Content = testID;
            priceLabel.Content = price;

            testId_barcode.Text = testID.ToString();

        }


 
    }
}
