using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Viewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow viewer;
        private string filename;
        private FileHandler fileHandler;

        public App()
            : base()
        {
            fileHandler = new FileHandler();
            viewer = new MainWindow();

            //wire up handlers
            WireHandlers(viewer);

            //Show the window
            viewer.Show();   
            
            OpenFileLocationDialog();                 
        }

        private void WireHandlers(MainWindow window)
        {
            window.AddWindowClosing(HandlerWindowClosing);
            window.AddSliderMove(HandlerSliderMovement);
        }

        private void HandlerWindowClosing(object sender, EventArgs e)
        {
            fileHandler.CloseFile();
            Debug.WriteLine("I closed");
        }

        private void HandlerSliderMovement(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var filePositionIndex = (long)e.NewValue;
            var offset = filePositionIndex * fileHandler.GetLineSize();
            System.Diagnostics.Debug.WriteLine("file position: " + filePositionIndex);

            DisplayRows(offset);
            Debug.WriteLine("I slide");
            Debug.WriteLine("e: " + e.NewValue);
            Debug.WriteLine("sender: " + sender.ToString());
        }

        private void DisplayHeaders()
        {
            List<string> headers = fileHandler.GetHeaders();
            viewer.AddColumns(headers);
        }

        private void DisplayRows(long offset)
        {
            viewer.ListView.Items.Clear();
            fileHandler.SetupRows(offset, (int)viewer.ListView.ActualHeight, (int) viewer.ListView.FontSize);
            foreach (var row in fileHandler.GetRows())
            {
                viewer.ListView.Items.Add(row);
            }
        }

        private void OpenFileLocationDialog()
        {
            //Open the dialogue and show only .txt
            var dialog = new OpenFileDialog();
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = System.IO.Path.Combine(pathUser, "Downloads");
            dialog.InitialDirectory = pathDownload;
            dialog.Filter = "data files (*.txt)|*.txt";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            bool? ok = dialog.ShowDialog();

            //Okay
            if (ok.HasValue && ok.Value == true)
            {
                //Get the filename
                filename = dialog.FileName;

                //Sets up the file handler
                fileHandler.LoadFile(filename);
                fileHandler.CalculateFieldSizes();
                fileHandler.CalculateFileInfo();
                fileHandler.SetupHeaders();                
                DisplayHeaders();
                DisplayRows(0);
            }
        }       
    }
}
