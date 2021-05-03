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
    /// Логика взаимодействия для SetParametrs.xaml
    /// </summary>
    public partial class SetParametrs : Window
    {
        Human hu = new Human();
        Parts part = new Parts();
        Animals ani = new Animals();
        Things thing = new Things();
        public SetParametrs()
        {
            InitializeComponent();
            framPlace.Content = hu;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void RadioButtonHuman_Click(object sender, RoutedEventArgs e)
        {
            
            //framPlace.Source = new Uri("Human.xaml", UriKind.RelativeOrAbsolute);
             framPlace.Content = hu;
            //framPlace.NavigationService.Navigate(new Human());
        }

        private void RadioButtonParts_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = part;
        }

        private void RadioButtonAnimals_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = ani;
        }

        private void RadioButtonThings_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = thing;
        }
    }
}
