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
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;

namespace SketchTime
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int time = 5220;
        private DispatcherTimer Timer;
        
        public double canvasHeightPass;
        public double canvasWidthPass;

        public SetParametrs paramWin;
        public MainWindow()
        {
            InitializeComponent();
            paramWin = new SetParametrs();
            paramWin.Show();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();

            canvasHeightPass = MainCanvas.Height;
            canvasWidthPass = MainCanvas.Width;          
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(time>540)
            {
                time--;
                TBCountDown.Content = string.Format("00:{0}:{1}", time / 60, time % 60);
            }
            else if (time > 0)
            {
                time--;
                TBCountDown.Content = string.Format("00:0{0}:{1}", time / 60, time % 60);
            }
            else
            {
                Timer.Stop();
                MessageBox.Show("Время вышло!");
            }
        }

        private void MFile_MouseEnter(object sender, MouseEventArgs e)
        {
            fileMenu.Visibility = 0;
            MFile.Foreground = new SolidColorBrush(Color.FromArgb(255, 0xa3, 0xf7, 0xb7));
            MEdit_MouseLeave(sender, e);
        }

        private void MFile_MouseLeave(object sender, MouseEventArgs e)
        {
            fileMenu.Visibility = (Visibility)2;
            MFile.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x29, 0xa1, 0x9c));

        }

        private void MEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            editMenu.Visibility = 0;
            MEdit.Foreground = new SolidColorBrush(Color.FromArgb(255, 0xa3, 0xf7, 0xb7));
            this.MFile_MouseLeave(sender,e);
        }

        private void MEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            editMenu.Visibility = (Visibility)2;
            MEdit.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x29, 0xa1, 0x9c));
        }

        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.EditingMode=(InkCanvasEditingMode)1; //ink
            Eraser.IsChecked = false;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.EditingMode = (InkCanvasEditingMode)5; //eraeByPoint
            Brush.IsChecked = false;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Galery_Click(object sender, RoutedEventArgs e)
        {
            WinGalery winGalery = new WinGalery();
            winGalery.Show();
        }

        private void ResizeCanvas_Click(object sender, RoutedEventArgs e)
        {
            WinResizeCanvas resize = new WinResizeCanvas(canvasHeightPass,canvasWidthPass);
            bool? resizeRez=resize.ShowDialog();
            if(resizeRez.HasValue && resize.NewHieght!=0 && resize.NewWidth!=0)
            {
                MainCanvas.Height = resize.NewHieght;
                MainCanvas.Width = resize.NewWidth;
            }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if(Stop.IsChecked==true)
            {
                Stop.Content = "ᐅ";
                Timer.Stop();
            }
            if (Stop.IsChecked == false)
            {
                Stop.Content = "| |";
                Timer.Start();
            }
            
        }

        private void ColorPic_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }


        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            paramWin.Show();
        }



        /*private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }*/
    }
}
