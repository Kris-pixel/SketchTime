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
                    Image img = new Image();
                    img.Height = 170;
                    img.Width = 170;
                    img.Margin = new Thickness(5, 5, 5, 5);
                    img.Source = BitmapFrame.Create(new Uri(curDir + i.IMG_FILEDATA));
                    AllImg.Children.Add(img);
                }
            }     
        }
    }
}
