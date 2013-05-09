using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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

        /// <summary>
        /// Adds the headers to the view
        /// </summary>
        /// <param name="headers"></param>
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
