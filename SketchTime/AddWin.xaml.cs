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
using System.IO;
using Microsoft.Win32;

namespace SketchTime
{
    /// <summary>
    /// Логика взаимодействия для AddWin.xaml
    /// </summary>
    public partial class AddWin : Window
    {
        SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities();
        static FileInfo ff = new FileInfo("MainWindow.xaml");
        string curDir = ff.DirectoryName + @"\";
        Human hu = new Human();
        Parts part = new Parts();
        Animals ani = new Animals();
        Things thing = new Things();
        int marker = 1;
        string aimName;

        string aimPath;

        public AddWin()
        {
            InitializeComponent();
            OKbut.IsEnabled = false;
            hu.grid.Children[10].Visibility = Visibility.Collapsed;
            hu.grid.Children[11].Visibility = Visibility.Collapsed;
            hu.grid.Margin = new Thickness(0,0,0,-80);
            hu.Sl.IsSelected = false;
            hu.Sl.Visibility = Visibility.Collapsed;
            hu.Cl.IsSelected = false;
            hu.Cl.Visibility = Visibility.Collapsed;
            hu.Pl.IsSelected = false;
            hu.Pl.Visibility = Visibility.Collapsed;
            hu.Configll.IsSelected = false;
            hu.Configll.Visibility = Visibility.Collapsed;
            hu.Vl.IsSelected = false;
            hu.Vl.Visibility = Visibility.Collapsed;
            framPlace.Content=hu;
        }

        

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rez = MessageBox.Show("Вы уверены, что хотите\n добавить это изабражение?",
               "Добавить", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (rez == MessageBoxResult.OK)
            {
                try
                {
                    AimImg.Source = null;
                    string pattern = @"System.Windows.Controls.ComboBoxItem: ";
                    Regex regex = new Regex(pattern);
                    File.Copy(aimPath, curDir + @"imgs\" + aimName);
                    switch (marker)
                    {
                        case 1:

                            IMG_FILES newImg = new IMG_FILES
                            {
                                IMG_FILEDATA = @"\imgs\" + aimName,
                            };

                            PEOPLE p = new PEOPLE
                            {
                                SECTION= "Человек",
                                SEX = regex.Replace(hu.SexCB.SelectedItem.ToString(), "").Substring(0, 1).ToLower(),
                                CLOTHES = regex.Replace(hu.ClothesCB.SelectedItem.ToString(), ""),
                                POSTURE = GetKey(regex.Replace(hu.PostureCB.SelectedItem.ToString(), "")),
                                CONFIG = GetKey(regex.Replace(hu.ConfigCB.SelectedItem.ToString(), "")),
                                POINT_V = GetKey(regex.Replace(hu.ViewCB.SelectedItem.ToString(), ""))
                            };

                            db.IMG_FILES.Add(newImg);
                            db.PEOPLE.Add(p);
                            db.SaveChanges();
                            break;
                        case 2:

                            IMG_FILES newImgP = new IMG_FILES
                            {
                                IMG_FILEDATA = @"\imgs\" + aimName,
                            };

                            PARTS_OF_THE_BODY pr = new PARTS_OF_THE_BODY
                            {
                                SECTION= "Часть тела",
                                CONFIG = GetKey(regex.Replace(part.ConfigCB.SelectedItem.ToString(), "")),
                                POINT_V = GetKey(regex.Replace(part.ViewCB.SelectedItem.ToString(), ""))
                            };

                            db.IMG_FILES.Add(newImgP);
                            db.PARTS_OF_THE_BODY.Add(pr);
                            db.SaveChanges();

                            break;
                        case 3:

                            IMG_FILES newImgA = new IMG_FILES
                            {
                                IMG_FILEDATA = @"\imgs\" + aimName,
                            };

                            ANIMALS a = new ANIMALS
                            {
                                SECTION= "Животные",
                                SPECIES = regex.Replace(ani.SpeciasCB.SelectedItem.ToString(), ""),
                                POSTURE = GetKey(regex.Replace(ani.PostureCB.SelectedItem.ToString(), "")),
                                CONFIG = GetKey(regex.Replace(ani.ConfigCB.SelectedItem.ToString(), "")),
                                POINT_V = GetKey(regex.Replace(ani.ViewCB.SelectedItem.ToString(), ""))
                            };

                            db.IMG_FILES.Add(newImgA);
                            db.ANIMALS.Add(a);
                            db.SaveChanges();

                            break;
                        case 4:

                            IMG_FILES newImgT = new IMG_FILES
                            {
                                IMG_FILEDATA = @"\imgs\" + aimName,
                            };

                            THINGS t = new THINGS
                            {
                                SECTION= "Предметы",
                                CATEGORY = regex.Replace(thing.CategoryCB.SelectedItem.ToString(), ""),
                                SURFACE = regex.Replace(thing.SurfaseCB.SelectedItem.ToString(), ""),
                                POINT_V = GetKey(regex.Replace(thing.ViewCB.SelectedItem.ToString(), ""))
                            };

                            db.IMG_FILES.Add(newImgT);
                            db.THINGS.Add(t);
                            db.SaveChanges();

                            break;
                        default:
                            break;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.Source);
                }
                this.DialogResult = true;
            }
            else
            {

            }
        }

        private void RadioButtonHuman_Click(object sender, RoutedEventArgs e)
        {
            framPlace.Content = hu;
            marker = 1;

        }

        private void RadioButtonParts_Checked(object sender, RoutedEventArgs e)
        {
            part.grid.Children[4].Visibility = Visibility.Collapsed;
            part.grid.Children[5].Visibility = Visibility.Collapsed;
            part.Configl.IsSelected = false;
            part.Configl.Visibility = Visibility.Collapsed;
            part.Vl.IsSelected = false;
            part.Vl.Visibility = Visibility.Collapsed;
            part.grid.Margin = new Thickness(0,0,0,-80);
            framPlace.Content = part;
            marker = 2;
        }

        private void RadioButtonAnimals_Checked(object sender, RoutedEventArgs e)
        {
            ani.grid.Children[8].Visibility = Visibility.Collapsed;
            ani.grid.Children[9].Visibility = Visibility.Collapsed;
            ani.Sl.IsSelected = false;
            ani.Sl.Visibility = Visibility.Collapsed;
            ani.Pl.IsSelected = false;
            ani.Pl.Visibility = Visibility.Collapsed;
            ani.Configl.IsSelected = false;
            ani.Configl.Visibility = Visibility.Collapsed;
            ani.Vl.IsSelected = false;
            ani.Vl.Visibility = Visibility.Collapsed;
            ani.grid.Margin = new Thickness(0, 0, 0, -80);
            framPlace.Content = ani;
            marker = 3;
        }

        private void RadioButtonThings_Checked(object sender, RoutedEventArgs e)
        {
            thing.grid.Children[6].Visibility = Visibility.Collapsed;
            thing.grid.Children[7].Visibility = Visibility.Collapsed;
            thing.grid.Margin = new Thickness(0, 0, 0, -80);
            thing.Cl.IsSelected = false;
            thing.Cl.Visibility = Visibility.Collapsed;
            thing.Sl.IsSelected = false;
            thing.Sl.Visibility = Visibility.Collapsed;
            thing.Vl.IsSelected = false;
            thing.Vl.Visibility = Visibility.Collapsed;
            framPlace.Content = thing;
            marker = 4;
        }

        private void Addbut_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                aimPath = dlg.FileName;
                aimName = dlg.SafeFileName;
                BitmapImage aim = new BitmapImage(new Uri(aimPath));
                AimImg.Source = aim;
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (marker == 1 && (hu.SexCB.SelectedItem != null && hu.SexCB.SelectedItem != null &&
                hu.PostureCB.SelectedItem != null && hu.ConfigCB.SelectedItem != null &&
                hu.ViewCB.SelectedItem != null && aimPath != null))
            {
                OKbut.IsEnabled = true;
            }
            if (marker == 2 && (part.ConfigCB.SelectedItem != null && part.ViewCB.SelectedItem != null &&
                aimPath != null))
            {
                OKbut.IsEnabled = true;
            }
            if (marker == 3 && (ani.SpeciasCB.SelectedItem != null && ani.PostureCB.SelectedItem != null &&
                ani.ConfigCB.SelectedItem != null && ani.ViewCB.SelectedItem != null &&
                aimPath != null))
            {
                OKbut.IsEnabled = true;
            }
            if (marker == 4 && (thing.CategoryCB.SelectedItem != null && thing.SurfaseCB.SelectedItem != null &&
                thing.ViewCB.SelectedItem != null && aimPath != null))
            {

                OKbut.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }


        public string GetKey(string findKey)
        {
            if (findKey.Contains("Ста") || findKey.Contains("Кожа") ||findKey.Contains("Сбоку"))
            {
                return "S";
            }
            if(findKey.Contains("Гипс"))
            {
                return "G";
            }
            if (findKey.Contains("Кости") || findKey.Contains("Сзади"))
            {
                return "B";
            }
            if (findKey.Contains("Мышцы"))
            {
                return "M";
            }
            if (findKey.Contains("Спереди"))
            {
                return "F";
            }
            if (findKey.Contains("Снизу"))
            {
                return "L";
            }
            if (findKey.Contains("Сверху"))
            {
                return "U";
            }
            else
            {
                return null;
            }
        }

    }
}
