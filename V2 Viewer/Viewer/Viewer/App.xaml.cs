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
            wireHandlers(viewer);
            viewer.Show();   
            
            openFileLocationDialog();

                   
        }

        private void displayHeaders()
        {
            List<string> headers = fileHandler.getHeaders();
            viewer.AddColumns(headers);
        }

        private void displayRows(long offset)
        {
            viewer.ListView.Items.Clear();
            fileHandler.setupRows(offset, (int)viewer.ListView.ActualHeight, (int) viewer.ListView.FontSize);
            foreach (var row in fileHandler.getRows())
            {
                viewer.ListView.Items.Add(row);
            }
        }

        private void openFileLocationDialog()
        {
            var dialog = new OpenFileDialog();
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string pathDownload = System.IO.Path.Combine(pathUser, "Downloads");
            dialog.InitialDirectory = pathDownload;
            dialog.Filter = "data files (*.txt)|*.txt";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            bool? ok = dialog.ShowDialog();

            if (ok.HasValue && ok.Value == true)
            {
                filename = dialog.FileName;

                fileHandler.loadFile(filename);
                fileHandler.calculateFieldSizes();
                fileHandler.calculateImportantInfo();
                fileHandler.setupHeaders();                
                displayHeaders();
                displayRows(0);
            }
        }

        private void wireHandlers(MainWindow window)
        {
            window.AddWindowClosing(HandlerWindowClosing);
            window.AddSliderMove(HandlerSliderMovement);
        }

        private void HandlerWindowClosing(object sender, EventArgs e)
        {
            fileHandler.closeFile();
            Debug.WriteLine("I closed");
        }

        private void HandlerSliderMovement(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var filePositionIndex = (long)e.NewValue;
            var offset = filePositionIndex * fileHandler.getLineSize();
            System.Diagnostics.Debug.WriteLine("file position: " + filePositionIndex);

            displayRows(offset);
            Debug.WriteLine("I slide");
            Debug.WriteLine("e: " + e.NewValue);
            Debug.WriteLine("sender: " + sender.ToString());
        }
    }
}
