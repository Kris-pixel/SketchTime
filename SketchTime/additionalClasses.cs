using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace SketchTime
{
    static public class SelectionParanerts
    {
        static public int marker = 0;
        //1 human
        //2 parts
        //3 animal
        //4 things

        static public int time = 300;
        static public class SelectHuman
        {
            static public string sex = "Любой";
            static public string clothes = "Любой";
            static public string posture = "Любая";
            static public string config = "Любая";
            static public string view = "Любой";
        }
        static public class SelectedAnimal
        {
            static public string specias = "Любой";
            static public string posture = "Любой";
            static public string config = "Любая";
            static public string view = "Любой";
        }
        static public class SelectedPart
        {
            static public string config = "Любая";
            static public string view = "Любой";
        }
        static public class SelectedThing
        {
            static public string surfase = "Любая";
            static public string category = "Любая";
            static public string view = "Любой";
        }
        static public class DelObj
        {
            static public string delSection;
            static public int delNumber;
        }
    }



    public partial class MainWindow : Window
    {
        public void MakeSelection(int marker)
        {
            imgCounter = 0;
            if(forDisplay!=null)
            {
                forDisplay.Clear();
             displaySection.Clear();
            displayNumbers.Clear();
             displayId.Clear();
            }
             
            switch (marker)
            {
                case 1:
                    using (SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities())
                    {
                            var joinedTAblesH = db.IMG_FILES.Join(db.PEOPLE,
                              i => i.ID,
                              p => p.P_ID,
                              (i, p) => new
                              {
                                  display = i.DISPLAY_SATUS,
                                  idImg = i.ID,
                                  Path = i.IMG_FILEDATA,
                                  Section = p.SECTION,
                                  Number = p.NUMBER,
                                  Sex = p.SEX,
                                  Posture = p.POSTURE,
                                  View = p.POINT_V,
                                  Congig = p.CONFIG,
                                  Clothes = p.CLOTHES
                              });

                            joinedTAblesH = joinedTAblesH.Where(x => x.Section == "Человек");
                            if (!SelectionParanerts.SelectHuman.sex.Contains("л"))
                            {
                                joinedTAblesH = joinedTAblesH.Where(x => x.Sex == SelectionParanerts.SelectHuman.sex);
                            }
                            if (!SelectionParanerts.SelectHuman.posture.Contains("Люб"))
                            {
                                var temp = from p in db.POSTURES
                                           where p.P_NAME == SelectionParanerts.SelectHuman.posture
                                           select p.P_KEY;
                                joinedTAblesH = joinedTAblesH.Where(x => x.Posture == temp.FirstOrDefault());
                            }
                            if (!SelectionParanerts.SelectHuman.view.Contains("Люб"))
                            {
                                var temp = from v in db.POINTS_OF_VIEW
                                           where v.V_NAME == SelectionParanerts.SelectHuman.view
                                           select v.V_KEY;
                                joinedTAblesH = joinedTAblesH.Where(x => x.View == temp.FirstOrDefault());
                            }
                            if (!SelectionParanerts.SelectHuman.config.Contains("Люб"))
                            {
                                var temp = from c in db.CONFIGURATION
                                           where c.C_NAME == SelectionParanerts.SelectHuman.config
                                           select c.C_KEY;
                                joinedTAblesH = joinedTAblesH.Where(x => x.Congig == temp.FirstOrDefault());
                            }
                            if (!SelectionParanerts.SelectHuman.clothes.Contains("Люб"))
                            {
                                joinedTAblesH = joinedTAblesH.Where(x => x.Clothes == SelectionParanerts.SelectHuman.clothes);
                            }
                            joinedTAblesH = joinedTAblesH.OrderBy(p => p.display);
                            displaySection = joinedTAblesH.Select(p => p.Section).ToList();
                            displayNumbers = joinedTAblesH.Select(p => p.Number).ToList();
                            displayId = joinedTAblesH.Select(p => p.idImg).ToList();
                            forDisplay = joinedTAblesH.Select(p => p.Path).ToList();


                            if (forDisplay.Count() == 0)
                        {
                            MessageBox.Show("К сожалению подходящих изображений не нашлось:(\n Поменяйте параметры.");
                        }
                    }
                    break;
                case 2:
                    using (SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities())
                    {
                        var joinedTAblesPr = db.IMG_FILES.Join(db.PARTS_OF_THE_BODY,
                         i => i.ID,
                         p => p.PR_ID,
                         (i, p) => new
                         {
                             display = i.DISPLAY_SATUS,
                             idImg = i.ID,
                             Path = i.IMG_FILEDATA,
                             Section = p.SECTION,
                             Number = p.NUMBER,
                             View = p.POINT_V,
                             Congig = p.CONFIG
                         });

                        joinedTAblesPr = joinedTAblesPr.Where(x => x.Section == "Часть тела");
                        if (!SelectionParanerts.SelectedPart.view.Contains("Люб"))
                        {
                            var temp = from v in db.POINTS_OF_VIEW
                                       where v.V_NAME == SelectionParanerts.SelectedPart.view
                                       select v.V_KEY;
                            joinedTAblesPr = joinedTAblesPr.Where(x => x.View == temp.FirstOrDefault());
                        }
                        if (!SelectionParanerts.SelectedPart.config.Contains("Люб"))
                        {
                            var temp = from c in db.CONFIGURATION
                                       where c.C_NAME == SelectionParanerts.SelectedPart.config
                                       select c.C_KEY;
                            joinedTAblesPr = joinedTAblesPr.Where(x => x.Congig == temp.FirstOrDefault());
                        }
                        joinedTAblesPr = joinedTAblesPr.OrderBy(p => p.display);
                        displaySection = joinedTAblesPr.Select(p => p.Section).ToList();
                        displayNumbers = joinedTAblesPr.Select(p => p.Number).ToList();
                        displayId = joinedTAblesPr.Select(p => p.idImg).ToList();
                        forDisplay = joinedTAblesPr.Select(p => p.Path).ToList();
                        if (forDisplay.Count() == 0)
                        {
                            MessageBox.Show("К сожалению подходящих изображений не нашлось:(\n Поменяйте параметры.");
                        }
                    }
                    break;
                case 3:
                    using (SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities())
                    {
                        var joinedTAblesA = db.IMG_FILES.Join(db.ANIMALS,
                         i => i.ID,
                         p => p.A_ID,
                         (i, p) => new
                         {
                             display = i.DISPLAY_SATUS,
                             idImg = i.ID,
                             Path = i.IMG_FILEDATA,
                             Section = p.SECTION,
                             Number = p.NUMBER,
                             Species = p.SPECIES,
                             Posture = p.POSTURE,
                             View = p.POINT_V,
                             Congig = p.CONFIG
                         });

                        joinedTAblesA = joinedTAblesA.Where(x => x.Section == "Животные");
                        if (!SelectionParanerts.SelectedAnimal.specias.Contains("Люб"))
                        {
                            joinedTAblesA = joinedTAblesA.Where(x => x.Species == SelectionParanerts.SelectedAnimal.specias);
                        }
                        if (!SelectionParanerts.SelectedAnimal.posture.Contains("Люб"))
                        {
                            var temp = from p in db.POSTURES
                                       where p.P_NAME == SelectionParanerts.SelectedAnimal.posture
                                       select p.P_KEY;
                            joinedTAblesA = joinedTAblesA.Where(x => x.Posture == temp.FirstOrDefault());
                        }
                        if (!SelectionParanerts.SelectedAnimal.view.Contains("Люб"))
                        {
                            var temp = from v in db.POINTS_OF_VIEW
                                       where v.V_NAME == SelectionParanerts.SelectedAnimal.view
                                       select v.V_KEY;
                            joinedTAblesA = joinedTAblesA.Where(x => x.View == temp.FirstOrDefault());
                        }
                        if (!SelectionParanerts.SelectedAnimal.config.Contains("Люб"))
                        {
                            var temp = from c in db.CONFIGURATION
                                       where c.C_NAME == SelectionParanerts.SelectedAnimal.config
                                       select c.C_KEY;
                            joinedTAblesA = joinedTAblesA.Where(x => x.Congig == temp.FirstOrDefault());
                        }
                        joinedTAblesA = joinedTAblesA.OrderBy(p => p.display);
                        displaySection = joinedTAblesA.Select(p => p.Section).ToList();
                        displayNumbers = joinedTAblesA.Select(p => p.Number).ToList();
                        displayId = joinedTAblesA.Select(p => p.idImg).ToList();
                        forDisplay = joinedTAblesA.Select(p => p.Path).ToList();
                        if (forDisplay.Count() == 0)
                        {
                            MessageBox.Show("К сожалению подходящих изображений не нашлось:(\n Поменяйте параметры.");
                        }
                    }
                    break;
                case 4:
                    using (SKETCH_TTIMEEntities db = new SKETCH_TTIMEEntities())
                    {
                        var joinedTAblesT = db.IMG_FILES.Join(db.THINGS,
                         i => i.ID,
                         p => p.T_ID,
                         (i, p) => new
                         {
                             display = i.DISPLAY_SATUS,
                             idImg = i.ID,
                             Path = i.IMG_FILEDATA,
                             Section = p.SECTION,
                             Number = p.NUMBER,
                             Surface = p.SURFACE,
                             Category = p.CATEGORY,
                             View = p.POINT_V
                         });

                        joinedTAblesT = joinedTAblesT.Where(x => x.Section == "Предметы");
                        if (!SelectionParanerts.SelectedThing.category.Contains("Люб"))
                        {
                            joinedTAblesT = joinedTAblesT.Where(x => x.Category == SelectionParanerts.SelectedThing.category);
                        }
                        if (!SelectionParanerts.SelectedThing.view.Contains("Люб"))
                        {
                            var temp = from v in db.POINTS_OF_VIEW
                                       where v.V_NAME == SelectionParanerts.SelectedThing.view
                                       select v.V_KEY;
                            joinedTAblesT = joinedTAblesT.Where(x => x.View == temp.FirstOrDefault());
                        }
                        if (!SelectionParanerts.SelectedThing.surfase.Contains("Люб"))
                        {
                            joinedTAblesT = joinedTAblesT.Where(x => x.Surface == SelectionParanerts.SelectedThing.surfase);
                        }
                        joinedTAblesT = joinedTAblesT.OrderBy(p => p.display);
                        displaySection = joinedTAblesT.Select(p => p.Section).ToList();
                        displayNumbers = joinedTAblesT.Select(p => p.Number).ToList();
                        displayId = joinedTAblesT.Select(p => p.idImg).ToList();
                        forDisplay = joinedTAblesT.Select(p => p.Path).ToList();
                        if (forDisplay.Count() == 0)
                        {
                            MessageBox.Show("К сожалению подходящих изображений не нашлось:(\n Поменяйте параметры.");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        public void DisplayImage(int imgCounter)
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(curDir + forDisplay[imgCounter]);
            image.EndInit();

                CurrentImg.Source = image;
                using (SKETCH_TTIMEEntities context = new SKETCH_TTIMEEntities())
                {
                    var display = context.IMG_FILES.Find(displayId[imgCounter]);

                    context.SaveChanges();

                    time = SelectionParanerts.time;
                    if (forDisplay.Count() == context.IMG_FILES.Count())
                    {
                        SectionLable.Content = "Все ";
                        NunInSectLable.Content = "--";
                        spoilerLabel.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        SectionLable.Content = displaySection[imgCounter];
                        NunInSectLable.Content = displayNumbers[imgCounter];
                        spoilerLabel.Visibility = Visibility.Visible;
                    }
                }
           
        }

    }
}
