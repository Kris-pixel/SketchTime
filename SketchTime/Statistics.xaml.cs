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
    /// Логика взаимодействия для Statistics.xaml
    /// </summary>
    public partial class Statistics : Page
    {
        public Statistics()
        {
            InitializeComponent();
            using (SKETCH_TTIMEEntities context = new SKETCH_TTIMEEntities())
            {
                int temp = context.IMG_FILES.Count();
                All.Content += "\t"+temp.ToString();
                AllProgress.Value = (double)context.IMG_FILES.Where(p => p.DISPLAY_SATUS != 0).Count() / temp * 100;

                temp = context.PEOPLE.Count();
                AllHum.Content += "\t" + temp.ToString();
                var tempTable = context.IMG_FILES.Join(context.PEOPLE,
                    i => i.ID,
                    p => p.P_ID,
                    (i, p) => new
                    {
                        d = i.DISPLAY_SATUS
                    });
                HumProgress.Value = (double)tempTable.Where(d=>d.d !=0).Count() / temp * 100;
                m.Content =  context.PEOPLE.Where(p => p.SEX == "м").Count().ToString();
                w.Content=   context.PEOPLE.Where(p => p.SEX == "ж").Count().ToString();
                DynamicHum.Content =  context.PEOPLE.Where(p => p.POSTURE == "D").Count().ToString();
                StaticHum.Content =  context.PEOPLE.Where(p => p.POSTURE == "S").Count().ToString();
                SkinHum.Content =  context.PEOPLE.Where(p => p.CONFIGURATION.C_KEY == "S").Count().ToString();
                BonesHum.Content =  context.PEOPLE.Where(p => p.CONFIGURATION.C_KEY == "B").Count().ToString();
                MuscleHum.Content =  context.PEOPLE.Where(p => p.CONFIGURATION.C_KEY == "M").Count().ToString();
                GipsHum.Content =  context.PEOPLE.Where(p => p.CONFIGURATION.C_KEY == "G").Count().ToString();

                temp = context.ANIMALS.Count();
                AllA.Content += "\t" + temp.ToString();
                tempTable= context.IMG_FILES.Join(context.ANIMALS,
                    i => i.ID,
                    p => p.A_ID,
                    (i, p) => new
                    {
                        d = i.DISPLAY_SATUS
                    });
                AniProgress.Value = (double)tempTable.Where(d=>d.d!=0).Count() / temp * 100;
                m.Content = context.PEOPLE.Where(p => p.SEX == "м").Count().ToString();
                ml.Content = context.ANIMALS.Where(p => p.SPECIES == "Млекопитающие").Count().ToString();
                bird.Content = context.ANIMALS.Where(p => p.SPECIES == "Птицы").Count().ToString();
                sl.Content = context.ANIMALS.Where(p => p.SPECIES == "Ящерицы").Count().ToString();
                sea.Content = context.ANIMALS.Where(p => p.SPECIES == "Морские").Count().ToString();
                DynamicA.Content = context.ANIMALS.Where(p => p.POSTURE == "D").Count().ToString();
                StaticA.Content = context.ANIMALS.Where(p => p.POSTURE == "S").Count().ToString();
                SkinA.Content = context.ANIMALS.Where(p => p.CONFIGURATION.C_KEY == "S").Count().ToString();
                BonesA.Content = context.ANIMALS.Where(p => p.CONFIGURATION.C_KEY == "B").Count().ToString();
                MuscleA.Content = context.ANIMALS.Where(p => p.CONFIGURATION.C_KEY == "M").Count().ToString();
                GipsA.Content = context.ANIMALS.Where(p => p.CONFIGURATION.C_KEY == "G").Count().ToString();

                temp = context.PARTS_OF_THE_BODY.Count();
                AllPart.Content += "\t" +temp .ToString();
                tempTable = context.IMG_FILES.Join(context.PARTS_OF_THE_BODY,
                    i => i.ID,
                    p => p.PR_ID,
                    (i, p) => new
                    {
                        d = i.DISPLAY_SATUS
                    });
                PartProgress.Value = (double)tempTable.Where(d=>d.d != 0).Count() / temp * 100;
                m.Content = context.PEOPLE.Where(p => p.SEX == "м").Count().ToString();
                SkinP.Content = context.PARTS_OF_THE_BODY.Where(p => p.CONFIGURATION.C_KEY == "S").Count().ToString();
                BonesP.Content = context.PARTS_OF_THE_BODY.Where(p => p.CONFIGURATION.C_KEY == "B").Count().ToString();
                MuscleP.Content = context.PARTS_OF_THE_BODY.Where(p => p.CONFIGURATION.C_KEY == "M").Count().ToString();
                GipsP.Content = context.PARTS_OF_THE_BODY.Where(p => p.CONFIGURATION.C_KEY == "G").Count().ToString();

                temp = context.THINGS.Count();
                AllT.Content += "\t" +temp .ToString();
                tempTable = context.IMG_FILES.Join(context.THINGS,
                    i => i.ID,
                    p => p.T_ID,
                    (i, p) => new
                    {
                        d = i.DISPLAY_SATUS
                    });
                TProgress.Value = (double)tempTable.Where(d=>d.d!=0).Count() / temp * 100;
                m.Content = context.PEOPLE.Where(p => p.SEX == "м").Count().ToString();
                Stuff.Content = context.THINGS.Where(p => p.CATEGORY == "Предметы быта").Count().ToString();
                Geom.Content = context.THINGS.Where(p => p.CATEGORY == "Геометрия").Count().ToString();
                OtherT.Content = context.THINGS.Where(p => p.CATEGORY == "Другое").Count().ToString();
                Transp.Content = context.THINGS.Where(p => p.SURFACE == "Прозрачная").Count().ToString();
                Math.Content = context.THINGS.Where(p => p.SURFACE == "Матовая").Count().ToString();
                Gloss.Content = context.THINGS.Where(p => p.SURFACE == "Глянцевая").Count().ToString();
                OtherS.Content = context.THINGS.Where(p => p.SURFACE == "Другая").Count().ToString();

            }
        }
    }
}
