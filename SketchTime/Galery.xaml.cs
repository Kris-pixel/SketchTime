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
using System.IO;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SketchTime
{
    /// <summary>
    /// Логика взаимодействия для Galery.xaml
    /// </summary>
    public partial class Galery : Page
    {
        public Galery(string curDir)
        {
            InitializeComponent();
            using (var context = new SKETCH_TTIMEEntities())
            {
                var imgs = context.IMG_FILES.ToList();
                foreach (var i in imgs)
                {
                    Grid grid = new Grid();
                    grid.Height = 170;
                    grid.Width = 170;
                    grid.Margin = new Thickness(5, 10, 5, 5);
                    Label labelSect = new Label();
                    labelSect.Margin = new Thickness(10, 5, 0, 0);
                    Label labelNum = new Label();
                    labelNum.Margin = new Thickness(0, 5, 10, 0);

                    labelSect.Foreground= new SolidColorBrush(Color.FromArgb(255, 0x29, 0xa1, 0x9c));
                    labelNum.Foreground= new SolidColorBrush(Color.FromArgb(255, 0x29, 0xa1, 0x9c));
                    labelSect.HorizontalAlignment = HorizontalAlignment.Left;
                    labelNum.HorizontalAlignment = HorizontalAlignment.Right;

                    if (i.PEOPLE!=null)
                    {
                        labelNum.Content = i.PEOPLE.NUMBER;
                        labelSect.Content = i.PEOPLE.SECTION;
                    }
                    else if (i.PARTS_OF_THE_BODY != null)
                    {
                        labelNum.Content = i.PARTS_OF_THE_BODY.NUMBER;
                        labelSect.Content = i.PARTS_OF_THE_BODY.SECTION;
                    }
                    else if (i.ANIMALS != null)
                    {
                        labelNum.Content = i.ANIMALS.NUMBER;
                        labelSect.Content = i.ANIMALS.SECTION;
                    }
                    else if (i.THINGS != null)
                    {
                        labelNum.Content = i.THINGS.NUMBER;
                        labelSect.Content = i.THINGS.SECTION;
                    }
                    else
                    {
                        labelNum.Content = "--";
                        labelSect.Content = "--";
                    }
                    Image img = new Image();
                    img.Height = 130;
                    img.VerticalAlignment = VerticalAlignment.Bottom;

                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = new Uri(curDir + i.IMG_FILEDATA);
                    image.EndInit();

                    img.Source = image;

                    grid.Children.Add(img);
                    grid.Children.Add(labelSect);
                    grid.Children.Add(labelNum);
                    AllImg.Children.Add(grid);
                }
            }     
        }
    }
}
