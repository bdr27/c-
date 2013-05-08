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
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddSliderMove(System.Windows.RoutedPropertyChangedEventHandler<double> handler)
        {
            this.Slider.ValueChanged += handler;
        }

        public void AddWindowClosing(System.EventHandler handler)
        {
            this.Closed += handler;
        }

        public void ResetGridColumns()
        {
            GridView.Columns.Clear();
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
    }
}
