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
    /// Логика взаимодействия для DelWin.xaml
    /// </summary>
    public partial class DelWin : Window
    {
        public DelWin()
        {
            InitializeComponent();
            OKbut.IsEnabled = false;
        }

        private void Numtxb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string pattern = @"^\d\d?\d?$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(Numtxb.Text))
            {
                Numtxb.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                if(cmb.SelectedValue!=null)
                {
                    OKbut.IsEnabled = true; 
                }
            }
            else
            {
                Numtxb.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
                OKbut.IsEnabled = false;
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rez = MessageBox.Show("Вы уверены, что хотите\n удалить это изабражение?",
                "Удалить", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(rez==MessageBoxResult.OK)
            {
                string pattern2 = @"System.Windows.Controls.ComboBoxItem: ";
                Regex regex2 = new Regex(pattern2);
                SelectionParanerts.DelObj.delSection = regex2.Replace(cmb.SelectedItem.ToString(), "");
                SelectionParanerts.DelObj.delNumber = Convert.ToInt32(Numtxb.Text);
                this.DialogResult = true;
            }
            
        }
    }
}
