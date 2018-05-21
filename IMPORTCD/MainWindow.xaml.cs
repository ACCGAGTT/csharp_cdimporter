using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
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

namespace CDROM
{
    public class Commands
    {
        [DllImport("winmm.dll")]
        static extern Int32 mciSendString(string command, string buffer, int bufferSize, IntPtr hwndCallback);

        public static void Eject()
        {
            string rt = "";
            mciSendString("set CDAudio door open", rt, 127, IntPtr.Zero);
        }

        public static void Close()
        {
            string rt = "";
            mciSendString("set CDAudio door closed", rt, 127, IntPtr.Zero);
        }
    }
}

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
            System.Media.SoundPlayer click = new System.Media.SoundPlayer(@"C:\Windows\media\Windows Message Nudge.wav");
            click.Play();
            var dicom = EvilDICOM.Core.DICOMObject.Read(@"\\thebeast-laptop\d$\DEV\test_data\test2\DICOMDIR");
            MessageBox.Show(dicom.FindFirst("00100010").DData.ToString());           

        }

        private void myButton_Quit(object sender, RoutedEventArgs e)
        {
            System.Media.SoundPlayer click = new System.Media.SoundPlayer(@"C:\Windows\media\Windows Shutdown.wav");
            click.Play();
            System.Threading.Thread.Sleep(1000);            
            this.Close();
        }

        private void myButton_Eject(object sender, RoutedEventArgs e)
        {

            System.Media.SoundPlayer click = new System.Media.SoundPlayer(@"C:\Windows\media\Windows Hardware Remove.wav");
            click.Play();         
            CDROM.Commands.Eject();
        }

        private void patient_name(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}

