using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;

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

        /// <summary>
        /// Wires up the closing window and slider movement for the window.
        /// </summary>
        /// <param name="window"></param>
        private void WireHandlers(MainWindow window)
        {
            window.AddWindowClosing(HandlerWindowClosing);
            window.AddSliderMove(HandlerSliderMovement);
        }

        /// <summary>
        /// When the window closes it closes the fileHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlerWindowClosing(object sender, EventArgs e)
        {
            fileHandler.CloseFile();
            Debug.WriteLine("I closed");
        }

        /// <summary>
        /// When the slider is moved it works out the new files to display
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlerSliderMovement(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //Gets the new position
            var filePositionIndex = (long)e.NewValue;
            //Calculates the file offset
            var offset = filePositionIndex * fileHandler.GetLineSize();
            System.Diagnostics.Debug.WriteLine("file position: " + filePositionIndex);

            //Displays the Rows that need to be displayed
            DisplayRows(offset);
            Debug.WriteLine("I slide");
            Debug.WriteLine("e: " + e.NewValue);
            Debug.WriteLine("sender: " + sender.ToString());
        }

        /// <summary>
        /// Displays the headers on the file
        /// </summary>
        private void DisplayHeaders()
        {
            List<string> headers = fileHandler.GetHeaders();
            viewer.AddColumns(headers);
        }

        /// <summary>
        /// Updates the rows based on the current offset
        /// </summary>
        /// <param name="offset"></param>
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
