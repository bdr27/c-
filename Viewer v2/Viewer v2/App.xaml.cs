using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace Viewer_v2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow viewer;
        private string filename;
        private FileHandler fileHandler;
        private int[] fieldSizes;
        private string[] columnNames;
        private int lineSize;
        private long filelen;
        private long listSize;
        private long maxSliderSize;
        private bool activated;
        private FileStream fileStream;

        public App()
            : base()
        {
            fileHandler = new FileHandler();
            viewer = new MainWindow();
            //wire up handlers
            wireHandlers(viewer);
            
            openFileLocationDialog();
            viewer.Show();           
        }

        private void calculateFieldSizes()
        {
            using (var stream = new StreamReader(filename + ".hd"))
            {
                var names = stream.ReadLine();
                var widths = stream.ReadLine();
                columnNames = new string[names.Split(',').Length];
                fieldSizes = new int[widths.Split(',').Length];
                var i = 0;
                foreach (var name in names.Split(','))
                {
                    fieldSizes[i] = int.Parse(widths.Split(',')[i]);
                    columnNames[i++] = name;
                }

                lineSize = fieldSizes.Sum() + 1;
                System.Diagnostics.Debug.WriteLine("line size: " + lineSize);
            }
        }

        private void setupHeaders()
        {
            viewer.ResetGridColumns();
            List<string> headers = new List<string>();
            foreach (var col in columnNames)
            {
                headers.Add(col);
            }
            viewer.AddColumns(headers);
        }
        private void setupRows(long offset)
        {
           /* if (fileStream == null) return;
            ListView.Items.Clear();
            var buffer = new byte[lineSize];
            fileStream.Seek(offset, 0);

            for (var i = 0; i < (int)(ListView.ActualHeight / (ListView.FontSize + 10)); ++i)
            {
                fileStream.Read(buffer, 0, lineSize);
                var line = Encoding.UTF8.GetString(buffer);
                System.Diagnostics.Debug.WriteLine("line: " + line);

                var startIndex = 0;
                var j = 0;

                var row = new Dictionary<string, object>();
                foreach (var col in columnNames)
                {
                    var field = line.Substring(startIndex, fieldSizes[j]);
                    row.Add(col, field.Trim());
                    startIndex += fieldSizes[j++];
                }

                ListView.Items.Add(row);
            }*/
        }

        private void openFileLocationDialog()
        {
            var dialog = new OpenFileDialog();
            string pathUser = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string pathDownload = System.IO.Path.Combine(pathUser, "Downloads");
            dialog.InitialDirectory = pathDownload;
            dialog.Filter = "data files (*.txt)|*.txt";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            bool? ok = dialog.ShowDialog();

            if (ok.HasValue && ok.Value == true)
            {
                filename = dialog.FileName;
                System.Diagnostics.Debug.WriteLine("name: " + filename);

                calculateFieldSizes();

                var fileInfo = new FileInfo(filename);
                filelen = fileInfo.Length;
                listSize = filelen / lineSize;
                maxSliderSize = listSize - 1; // out by one
                System.Diagnostics.Debug.WriteLine("file size: " + filelen);
                System.Diagnostics.Debug.WriteLine("list view length: " + listSize);

                setupHeaders();

                fileStream = new FileStream(filename, FileMode.Open);

                setupRows(0);
            }
        }

        private void wireHandlers(MainWindow window)
        {
            window.AddWindowClosing(HandlerWindowClosing);
            window.AddSliderMove(HandlerSliderMovement);
        }

        private void HandlerWindowClosing(object sender, EventArgs e)
        {
            fileHandler.closeFile(fileStream);
            Debug.WriteLine("I closed");
        }

        private void HandlerSliderMovement(object sender, EventArgs e)
        {
            Debug.WriteLine("I slide");
        }
    }
}
