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
    /// Логика взаимодействия для SetParametrs.xaml
    /// </summary>
    public partial class SetParametrs : Window
    {
        Human hu = new Human();
        Parts part = new Parts();
        Animals ani = new Animals();
        Things thing = new Things();
        int marker = 1;
        public SetParametrs()
        {
            InitializeComponent();
            framPlace.Content = hu;
            SelectionParanerts.marker = 1;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            SelectionParanerts.marker = 1;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            string pattern = @"System.Windows.Controls.ComboBoxItem: ";
            Regex regex = new Regex(pattern);
            switch (marker)
            {
                case 1:
                    
                    SelectionParanerts.SelectHuman.sex = regex.Replace(hu.SexCB.SelectedItem.ToString(),"").Substring(0,1).ToLower();
                    SelectionParanerts.SelectHuman.clothes = regex.Replace(hu.ClothesCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectHuman.posture = regex.Replace(hu.PostureCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectHuman.config = regex.Replace(hu.ConfigCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectHuman.view = regex.Replace(hu.ViewCB.SelectedItem.ToString(),"");
                    try
                    {
                        SelectionParanerts.time = Convert.ToInt32(regex.Replace(hu.TimeCB.SelectedItem.ToString(), "").Substring(0, 2).Trim()) * 60;
                    }
                    catch
                    {
                        SelectionParanerts.time = 2000;
                    }
                    SelectionParanerts.marker = marker;
                    break;
                case 2:
                    SelectionParanerts.SelectedPart.config = regex.Replace(part.ConfigCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectedPart.view = regex.Replace(part.ViewCB.SelectedItem.ToString(),"");
                    SelectionParanerts.time = Convert.ToInt32(regex.Replace(part.TimeCB.SelectedItem.ToString(),"").Substring(0, 2).Trim()) * 60;
                    SelectionParanerts.marker = marker;
                    break;
                case 3:
                    SelectionParanerts.SelectedAnimal.specias = regex.Replace( ani.SpeciasCB.SelectedItem.ToString(), "");
                    SelectionParanerts.SelectedAnimal.posture = regex.Replace(ani.PostureCB.SelectedItem.ToString(), "");
                    SelectionParanerts.SelectedAnimal.config = regex.Replace(ani.ConfigCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectedAnimal.view = regex.Replace(ani.ViewCB.SelectedItem.ToString(),"");
                    SelectionParanerts.time = Convert.ToInt32(regex.Replace(ani.TimeCB.SelectedItem.ToString(), "").Substring(0, 2).Trim()) * 60;
                    SelectionParanerts.marker = marker;
                    break;
                case 4:
                    SelectionParanerts.SelectedThing.category = regex.Replace(thing.CategoryCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectedThing.surfase = regex.Replace(thing.SurfaseCB.SelectedItem.ToString(),"");
                    SelectionParanerts.SelectedAnimal.view = regex.Replace(thing.ViewCB.SelectedItem.ToString(),"");
                    SelectionParanerts.time = Convert.ToInt32(regex.Replace(thing.TimeCB.SelectedItem.ToString(),"").Substring(0, 2).Trim()) * 60;
                    SelectionParanerts.marker = marker;
                    break;
                default:
                    break;
            }
            this.DialogResult = true;
        }

        private void RadioButtonHuman_Click(object sender, RoutedEventArgs e)
        {
            
             framPlace.Content = hu;
            marker = 1;
             
        }

        private void RadioButtonParts_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = part;
            marker = 2;
        }

        private void RadioButtonAnimals_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = ani;
            marker = 3;
        }

        private void RadioButtonThings_Checked(object sender, RoutedEventArgs e)
        {
            framPlace.Content = thing;
            marker = 4;
        }
    }
}
