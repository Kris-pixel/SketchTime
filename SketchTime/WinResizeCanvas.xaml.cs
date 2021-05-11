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
using System.Text.RegularExpressions;

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


        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;         
               
        }

        private void Widthtxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pattern = @"^\d{2}\d?\d?$";
            Regex regex = new Regex(pattern);
           
            if (regex.IsMatch(Widthtxb.Text))
            {
                Widthtxb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                Widthtxb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                OKbut.IsEnabled = false;
            }

            if (regex.IsMatch(Heighttxb.Text))
            {
                Heighttxb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            }
            else
            {
                OKbut.IsEnabled = false;
                Heighttxb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            }

            if (regex.IsMatch(Heighttxb.Text) && regex.IsMatch(Widthtxb.Text))
            {
                NewHieght = Convert.ToDouble(Heighttxb.Text);
                NewWidth = Convert.ToDouble(Widthtxb.Text);
                OKbut.IsEnabled = true;
            }
            else
            {
                OKbut.IsEnabled = false;
            }
        }

    }
}
