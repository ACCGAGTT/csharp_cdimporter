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
            try
            {
                System.Media.SoundPlayer click = new System.Media.SoundPlayer(@"C:\Windows\media\Windows Message Nudge.wav");
                click.Play();
                DriveInfo[] lecteurCD = DriveInfo.GetDrives().Where(drive => drive.DriveType == DriveType.CDRom).ToArray();
                    
                    if (lecteurCD[0].IsReady)
                    {
                        var cd_rom = lecteurCD[0].Name.ToString();

                        var files = new DirectoryInfo(cd_rom);
                        FileInfo[] fileInfo = files.GetFiles("DICOMDIR", SearchOption.AllDirectories);
                        

                        if (fileInfo.Length != 0)
                        {
                            FileInfo dicomdir = new FileInfo(fileInfo[0].FullName.ToString());
                            var dicomInfo = dicomdir.FullName;
                            var dicomObject = EvilDICOM.Core.DICOMObject.Read(dicomInfo);

                            patient_Name.Content = dicomObject.FindFirst("00100010").DData.ToString();
                            patient_Dossier.Content = dicomObject.FindFirst("00100020").DData.ToString();
                           // List<object> studies  = new List<object>();
                            list_studies.ItemsSource = dicomObject.FindFirst("00041500").DData.ToString();
                            
                            
                        }
                        else
                        {
                            System.Media.SoundPlayer click_error = new System.Media.SoundPlayer(@"C:\Windows\media\Windows Critical Stop.wav");
                            click_error.Play();
                            patient_Name.Content = "CD NON-CONFORME";
                            patient_Dossier.Content = "VEUILLEZ CONTACTER LA FILMOTHEQUE";
                        }

                    }
                    else
                    {
                        System.Media.SoundPlayer click_error = new System.Media.SoundPlayer(@"C:\Windows\media\Windows Critical Stop.wav");
                        click_error.Play();
                        patient_Name.Content = "CD NON-CONFORME";
                        patient_Dossier.Content = "VEUILLEZ CONTACTER LA FILMOTHEQUE";
                        CDROM.Commands.Eject();
                    }       

            }
            catch
            {

            }
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

