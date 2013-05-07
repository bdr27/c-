using Microsoft.Win32;
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

namespace Viewer
{
    public partial class MainWindow : Window
    {
        private string filename;
        private int[] fieldSizes;
        private string[] columnNames;
        private int lineSize;
        private long filelen;
        private long listSize;
        private bool activated;
        private FileStream fileStream;
        public MainWindow()
        {
            InitializeComponent();
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
            GridView.Columns.Clear();
            foreach (var col in columnNames) {
                var item = new GridViewColumn();
                item.Header = col;
                item.DisplayMemberBinding = new Binding("[" + col + "]");
                item.ClearValue(GridViewColumn.WidthProperty);
                GridView.Columns.Add(item);
            }
        }

        private void setupRows(long offset)
        {
            if (fileStream == null) return;
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
                foreach (var col in columnNames) {
                    var field = line.Substring(startIndex, fieldSizes[j]);
                    row.Add(col, field.Trim());
                    startIndex += fieldSizes[j++];
                }

                ListView.Items.Add(row);
            }
        }
        public void AddWindowClosing(System.EventHandler handler)
        {
           this.Deactivated += handler;
            //Window_Closing += handler;
        }
        public void ResetGridColumns()
        {
            GridView.Columns.Clear();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var filePositionIndex = (long)e.NewValue;
            var offset = filePositionIndex * lineSize;
            System.Diagnostics.Debug.WriteLine("file position: " + filePositionIndex);

            setupRows(offset);
        }

      public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (fileStream == null) return;
            fileStream.Close();
        } 

        private void Window_Activated(object sender, EventArgs e)
        {
      /*      if (activated) return;
            activated = true;

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
                System.Diagnostics.Debug.WriteLine("name: " + filename);

                calculateFieldSizes();

                var fileInfo = new FileInfo(filename);
                filelen = fileInfo.Length;
                listSize = filelen / lineSize;
                Slider.Maximum = listSize - 1; // out by one
                System.Diagnostics.Debug.WriteLine("file size: " + filelen);
                System.Diagnostics.Debug.WriteLine("list view length: " + listSize);

                setupHeaders();

                fileStream = new FileStream(filename, FileMode.Open);

                setupRows(0);
            }*/
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            setupRows((long)Slider.Value * lineSize);
        }

        public void AddColumns(List<string> headers)
        {
            foreach (var header in headers)
            {
                var item = new GridViewColumn();
                item.Header = header;
                item.DisplayMemberBinding = new Binding("[" + header + "]");
                item.ClearValue(GridViewColumn.WidthProperty);
                GridView.Columns.Add(item);
            }
        }

        public void AddSliderMove(System.EventHandler handler)
        {
            this.Slider.ValueChanged += Slider_ValueChanged;
        }
    }
}
