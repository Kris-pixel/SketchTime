using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SketchTime
{
    /// <summary>
    /// Логика взаимодействия для WinResizeCanvas.xaml
    /// </summary>
    public partial class WinResizeCanvas : Window
    {
        public double NewHieght {get; set;}
        public double NewWidth { get; set; }
        public WinResizeCanvas(double curHeight, double curWidth)
        {
            InitializeComponent();
            Heighttxb.Text = Convert.ToString(curHeight);
            Widthtxb.Text = Convert.ToString(curWidth);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            NewHieght = Convert.ToDouble(Heighttxb.Text);
            NewWidth = Convert.ToDouble(Widthtxb.Text);
            this.Close();
        }
    }
}
