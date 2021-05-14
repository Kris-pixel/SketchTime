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
using System.Drawing.Imaging;
using System.Windows.Ink;

namespace SketchTime
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        static FileInfo ff = new FileInfo("MainWindow.xaml");
        string curDir=ff.DirectoryName+@"\";
        List<string> forDisplay;
        List<string> displaySection;
        List<int> displayNumbers;
        List<int> displayId;
        StrokeCollection redoStrokes;
        
        int imgCounter = 0;

        private int time;
        private DispatcherTimer Timer;
        
        public double canvasHeightPass;
        public double canvasWidthPass;

        public SetParametrs paramWin;
        public MainWindow()
        {
           
            InitializeComponent();
            redoStrokes = new StrokeCollection();
            redoStrokes = MainCanvas.Strokes.Clone();
            using (SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities())
            {
                paramWin = new SetParametrs();
                bool? paramRez = paramWin.ShowDialog();
                if (paramRez.HasValue && paramRez.Value)
                {
                    try
                    {
                        if (forDisplay != null)
                        {
                            forDisplay.Clear();
                        }

                        MakeSelection(SelectionParanerts.marker);
                        if(forDisplay.Count()!=0)
                        {
                            DisplayImage(imgCounter);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.Source);
                    }
                }
                else
                {

                    try
                    {
                        imgCounter = 0;
                        if (forDisplay != null)
                        {
                            forDisplay.Clear();
                            displaySection.Clear();
                            displayNumbers.Clear();
                            displayId.Clear();
                        }


                        forDisplay = db.IMG_FILES.OrderBy(p => p.DISPLAY_SATUS).Select(p => p.IMG_FILEDATA).ToList();
                        displayId = db.IMG_FILES.Select(p => p.ID).ToList();
                        if (imgCounter == 0)
                            Prev.IsEnabled = false;
                        else
                            Prev.IsEnabled = true;

                        if (imgCounter == forDisplay.Count())
                            Next.IsEnabled = false;
                        else
                            Next.IsEnabled = true;

                        DisplayImage(imgCounter);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + ex.Source);
                    }
                }
            }
            Timer = new DispatcherTimer();
                Timer.Interval = new TimeSpan(0, 0, 0, 1);
                Timer.Tick += Timer_Tick;
                Timer.Start();


            canvasHeightPass = MainCanvas.Height;
            canvasWidthPass = MainCanvas.Width;

        }

        /*technical events*/
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Timer.Stop();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            if((string)Stop.Content  == "| |")
            {
                Timer.Start();
            }
           
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 1800)
            {
                Timer.Stop();
                TBCountDown.Content = string.Format("00:00:00");
            }
            else
            {
                if (time > 540)
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
                    if (imgCounter < forDisplay.Count() - 1)
                    {
                        DisplayImage(imgCounter);
                        MainCanvas.Strokes.Clear();
                        MainCanvas.Strokes.RemoveAt(0);
                    }
                    else
                    {
                        MessageBox.Show("Поздравляем! Вы отрисовали все изображения в текущей выборке");
                        CurrentImg.Source = BitmapFrame.Create(new Uri(curDir + @"congrats.jpg"));
                    }

                }
            }
        }




        /* visual evants*/
        private void MFile_MouseEnter(object sender, MouseEventArgs e)
        {
            fileMenu.Visibility = Visibility.Visible;
            MFile.Foreground = new SolidColorBrush(Color.FromArgb(255, 0xa3, 0xf7, 0xb7));
            MEdit_MouseLeave(sender, e);
        }

        private void MFile_MouseLeave(object sender, MouseEventArgs e)
        {
            fileMenu.Visibility = Visibility.Collapsed;
            MFile.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x29, 0xa1, 0x9c));

        }

        private void MEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            editMenu.Visibility = Visibility.Visible;
            MEdit.Foreground = new SolidColorBrush(Color.FromArgb(255, 0xa3, 0xf7, 0xb7));
            this.MFile_MouseLeave(sender,e);
        }

        private void MEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            editMenu.Visibility = Visibility.Collapsed;
            MEdit.Foreground = new SolidColorBrush(Color.FromArgb(255, 0x29, 0xa1, 0x9c));
        }
        private void Img_info_MouseEnter(object sender, MouseEventArgs e)
        {
            InfoBorder.Visibility = Visibility.Visible;
        }

        private void Img_info_MouseLeave(object sender, MouseEventArgs e)
        {
            InfoBorder.Visibility = Visibility.Collapsed;
        }






        /*menu events*/

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Galery_Click(object sender, RoutedEventArgs e)
        {
            WinGalery winGalery = new WinGalery(curDir);
            winGalery.Show();
        }

        private void ResizeCanvas_Click(object sender, RoutedEventArgs e)
        {
            WinResizeCanvas resize = new WinResizeCanvas(canvasHeightPass,canvasWidthPass);
            bool? resizeRez=resize.ShowDialog();
            if(resizeRez.HasValue && resizeRez.Value)
            {
                MainCanvas.Height = resize.NewHieght;
                MainCanvas.Width = resize.NewWidth;
            }
        }

        private void ButtonChange_Click(object sender, RoutedEventArgs e)
        {
            paramWin = new SetParametrs();
            bool? paramRez = paramWin.ShowDialog();
            if (paramRez.HasValue && paramRez.Value)
            {
                try
                {
                    if (forDisplay != null)
                    {
                        forDisplay.Clear();
                    }

                    MakeSelection(SelectionParanerts.marker);
                    if(forDisplay.Count()!=0)
                    {
                        DisplayImage(imgCounter);
                    }
                    MainCanvas.Strokes.Clear();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.Source);
                }
            }
        }
        private void NewItem_Click(object sender, RoutedEventArgs e)
        {
            AddWin addWin = new AddWin();
            bool? addRez = addWin.ShowDialog();
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities())
                {
                    if (db.IMG_FILES.Count() > 10)
                    {
                        DelWin delWin = new DelWin();
                        bool? delRez = delWin.ShowDialog();
                        if (delRez.HasValue && delRez.Value)
                        {

                            switch (SelectionParanerts.DelObj.delSection)
                            {
                                case "Человек":
                                    using (SKETCH_TTIMEEntities context = new SKETCH_TTIMEEntities())
                                    {
                                        PEOPLE delHum = context.PEOPLE.FirstOrDefault(p => p.NUMBER == SelectionParanerts.DelObj.delNumber);
                                        if (Convert.ToInt32(NunInSectLable.Content.ToString()) == SelectionParanerts.DelObj.delNumber)
                                        {
                                            Next_Click(sender, e);
                                        }

                                        if (delHum != null)
                                        {
                                            int delId = delHum.P_ID;
                                            IMG_FILES delImg = context.IMG_FILES.FirstOrDefault(p => p.ID == delId);

                                            if (File.Exists(curDir + delImg.IMG_FILEDATA))
                                            {
                                          
                                                File.Delete(curDir + delImg.IMG_FILEDATA);
                                                context.PEOPLE.Remove(delHum);
                                                context.IMG_FILES.Remove(delImg);
                                                MessageBox.Show("Изображение успешно удалено.");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Изображение не найдено.\n Возможно оно уже было удалено.");
                                        }


                                        context.SaveChanges();
                                    }
                                    break;

                                case "Часть тела":

                                    using (SKETCH_TTIMEEntities context = new SKETCH_TTIMEEntities())
                                    {
                                        PARTS_OF_THE_BODY delPart = context.PARTS_OF_THE_BODY.FirstOrDefault(p => p.NUMBER == SelectionParanerts.DelObj.delNumber);
                                        if (Convert.ToInt32(NunInSectLable.Content.ToString()) == SelectionParanerts.DelObj.delNumber)
                                        {
                                            Next_Click(sender, e);
                                        }

                                        if (delPart != null)
                                        {
                                            int delId = delPart.PR_ID;
                                            IMG_FILES delImg = context.IMG_FILES.FirstOrDefault(p => p.ID == delId);

                                            if (File.Exists(curDir + delImg.IMG_FILEDATA))
                                            {
                                                File.Delete(curDir + delImg.IMG_FILEDATA);
                                                context.PARTS_OF_THE_BODY.Remove(delPart);
                                                context.IMG_FILES.Remove(delImg);
                                                MessageBox.Show("Изображение успешно удалено.");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Изображение не найдено.\n Возможно оно уже было удалено.");
                                        }


                                        context.SaveChanges();
                                    }

                                    break;
                                case "Животные":

                                    using (SKETCH_TTIMEEntities context = new SKETCH_TTIMEEntities())
                                    {
                                        ANIMALS delA = context.ANIMALS.FirstOrDefault(p => p.NUMBER == SelectionParanerts.DelObj.delNumber);
                                        if (Convert.ToInt32(NunInSectLable.Content.ToString()) == SelectionParanerts.DelObj.delNumber)
                                        {
                                            Next_Click(sender, e);
                                        }

                                        if (delA != null)
                                        {
                                            int delId = delA.A_ID;
                                            IMG_FILES delImg = context.IMG_FILES.FirstOrDefault(p => p.ID == delId);

                                            if (File.Exists(curDir + delImg.IMG_FILEDATA))
                                            {
                                                File.Delete(curDir + delImg.IMG_FILEDATA);
                                                context.ANIMALS.Remove(delA);
                                                context.IMG_FILES.Remove(delImg);
                                                MessageBox.Show("Изображение успешно удалено.");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Изображение не найдено.\n Возможно оно уже было удалено.");
                                        }


                                        context.SaveChanges();
                                    }

                                    break;
                                case "Предметы":

                                    using (SKETCH_TTIMEEntities context = new SKETCH_TTIMEEntities())
                                    {
                                        THINGS delT = context.THINGS.FirstOrDefault(p => p.NUMBER == SelectionParanerts.DelObj.delNumber);
                                        if (Convert.ToInt32(NunInSectLable.Content.ToString()) == SelectionParanerts.DelObj.delNumber)
                                        {
                                            Next_Click(sender, e);
                                        }

                                        if (delT != null)
                                        {
                                            int delId = delT.T_ID;
                                            IMG_FILES delImg = context.IMG_FILES.FirstOrDefault(p => p.ID == delId);

                                            if (File.Exists(curDir + delImg.IMG_FILEDATA))
                                            {
                                                File.Delete(curDir + delImg.IMG_FILEDATA);
                                                context.THINGS.Remove(delT);
                                                context.IMG_FILES.Remove(delImg);
                                                MessageBox.Show("Изображение успешно удалено.");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Изображение не найдено.\n Возможно оно уже было удалено.");
                                        }


                                        context.SaveChanges();
                                    }

                                    break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("В тренажере слишком мало изображений");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source);
            }

        }

        private void UndoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MainCanvas.Strokes.Count() != 0)
            {
                redoStrokes.Add(MainCanvas.Strokes[MainCanvas.Strokes.Count() - 1]);
                MainCanvas.Strokes.RemoveAt(MainCanvas.Strokes.Count() - 1);
            }
        }
        private void RedoCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (redoStrokes.Count() != 0)
            {
                MainCanvas.Strokes.Add(redoStrokes[redoStrokes.Count() - 1]);
                redoStrokes.RemoveAt(redoStrokes.Count() - 1);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.Title = "Сохранить в файл...";
            dlg.OverwritePrompt = true;
            dlg.CheckPathExists = true;
            dlg.Filter = "Файлы JPEG (*.jpg)|*.jpg";
            ImageFormat[] ff = { ImageFormat.Jpeg };

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    RenderTargetBitmap rtb = new RenderTargetBitmap((int)MainCanvas.Width, (int)MainCanvas.Height, 72d, 72d, PixelFormats.Default);
                    rtb.Render(MainCanvas);
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(rtb));


                    using (FileStream fs = File.Open(dlg.FileName, FileMode.Create))
                    {
                        encoder.Save(fs);
                    }
                    MessageBox.Show("Изображение успешно сохранено");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n" + ex.Source);
                }
            }
        }


        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Разработчик: Васильева Кристина Юрьевна\n Версия: 55.37.09\n 2021 год ");
        }




        /*Image part events*/
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

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgCounter++;
                if (imgCounter == 0)
                    Prev.IsEnabled = false;
                else
                    Prev.IsEnabled = true;
                if (imgCounter == forDisplay.Count()-1)
                    Next.IsEnabled = false;
                else
                    Next.IsEnabled = true;

                DisplayImage(imgCounter);
                MainCanvas.Strokes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source);
            }
        }

        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgCounter--;
                if (imgCounter == 0)
                    Prev.IsEnabled = false;
                else
                    Prev.IsEnabled = true;
                if (imgCounter == forDisplay.Count())
                    Next.IsEnabled = false;
                else
                    Next.IsEnabled = true;

                DisplayImage(imgCounter);
                MainCanvas.Strokes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source);
            }
        }



       


        /*Painting part events*/
        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.EditingMode = (InkCanvasEditingMode)1; //ink
            Eraser.IsChecked = false;
        }

        private void Eraser_Click(object sender, RoutedEventArgs e)
        {
            MainCanvas.EditingMode = (InkCanvasEditingMode)5; //eraeByPoint
            Brush.IsChecked = false;
        }

        private void ColorPic_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (MainCanvas != null)
            {
                MainCanvas.DefaultDrawingAttributes.Color = (Color)ColorPic.SelectedColor;
            }
        }



    }
}
