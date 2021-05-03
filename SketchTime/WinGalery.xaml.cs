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
    /// Логика взаимодействия для WinGalery.xaml
    /// </summary>
    public partial class WinGalery : Window
    {
        Galery gal = new Galery();
        Statistics stat = new Statistics();
        public WinGalery()
        {
            InitializeComponent();
            framPlace.Content = gal;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void RadioButtonGalery_Click(object sender, RoutedEventArgs e)
        {
            framPlace.Content = gal;
        }

        private void RadioButtonStatistic_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = stat;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            framPlace.Content = gal;
        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            framPlace.Content = stat;
        }
    }
}
