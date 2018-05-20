using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EvilDICOM.Core;

namespace IMPORTCD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void myButton_PACS(object sender, RoutedEventArgs e)
        {
            foreach (var drive in DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.CDRom))
                MessageBox.Show(drive.Name + " " + drive.IsReady.ToString());
        }

        private void myButton_Quit(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}
