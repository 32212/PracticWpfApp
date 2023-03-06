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

namespace PracticWpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для CAPTCHAPage.xaml
    /// </summary>
    public partial class CAPTCHAPage : Page
    {

        string code = "";
        int numsymbs; //сгенеррированное число символов
        int count; //число для входа

        public CAPTCHAPage()
        {
            InitializeComponent();

            Random rnd = new Random(); //для линий

            Random rnd1 = new Random(); //для чисел

            //линии
            Line line1 = new Line()
            {
                X1 = rnd.Next(0, 100),
                Y1 = rnd.Next(0, 100),
                X2 = rnd.Next(0, 300),
                Y2 = rnd.Next(0, 200),
                Stroke = Brushes.Black,
            };

            Line line2 = new Line()
            {
                X1 = rnd.Next(0, 100),
                Y1 = rnd.Next(0, 100),
                X2 = rnd.Next(0, 300),
                Y2 = rnd.Next(0, 200),
                Stroke = Brushes.Black,
            };

            Line line3 = new Line()
            {
                X1 = rnd.Next(0, 100),
                Y1 = rnd.Next(0, 100),
                X2 = rnd.Next(0, 300),
                Y2 = rnd.Next(0, 200),
                Stroke = Brushes.Blue,
            };

            Line line4 = new Line()
            {
                X1 = rnd.Next(0, 100),
                Y1 = rnd.Next(0, 100),
                X2 = rnd.Next(0, 300),
                Y2 = rnd.Next(0, 200),
                Stroke = Brushes.Black,
            };

            Line line5 = new Line()
            {
                X1 = rnd.Next(0, 100),
                Y1 = rnd.Next(0, 100),
                X2 = rnd.Next(0, 300),
                Y2 = rnd.Next(0, 200),
                Stroke = Brushes.Green,
            };

            Line line6 = new Line()
            {
                X1 = rnd.Next(0, 100),
                Y1 = rnd.Next(0, 100),
                X2 = rnd.Next(0, 300),
                Y2 = rnd.Next(0, 200),
                Stroke = Brushes.DarkOliveGreen,
            };

            //добавляем линии в контейнер
            Container.Children.Add(line1);
            Container.Children.Add(line2);
            Container.Children.Add(line3);
            Container.Children.Add(line4);
            Container.Children.Add(line5);
            Container.Children.Add(line6);


            char[] wordsymbs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            numsymbs = rnd1.Next(7, 10); //от 7 до 10 символов

            int wordnum; //число или цифра 
            int rndnum; //случайное число

            for (int j = 1; j <= numsymbs; j++) //генерируем цифру или символ
            {
                wordnum = rnd1.Next(1, 2);
                if (wordnum == 1)
                {
                    int wordsymbs_num = rnd1.Next(0, wordsymbs.Length - 1); //генерируем символ
                    code += wordsymbs[wordsymbs_num];
                }

                else
                {
                    rndnum = rnd1.Next(0, 9); //генерируем цифру
                    code += rndnum.ToString();
                }
            }

            char[] randstyle = code.ToCharArray(); //рандомный стиль для каждого символа


            //тестовый блок
            int width; //ширина
            int heigyh; //высота
            int maxWid; //максимальная ширина

            maxWid = (int)(250 / numsymbs); //вычисление максимальной ширины
            int first = 0;
            int second;


            for (int i = 0; i < numsymbs; i++)
            {
                if (i == 0)
                {
                    width = rnd1.Next(0, maxWid);
                    first = maxWid;
                }
                else
                {
                    second = first + maxWid;
                    width = rnd1.Next(first, second);
                    first = width;
                }

                heigyh = rnd1.Next(0, 100);

                int styletype = rnd1.Next(1, 3); //стиль для каждого символа

                switch (styletype)
                {
                    case 1:
                        TextBlock txb = new TextBlock() //курсив
                        {
                            Text = randstyle[i].ToString(),
                            Padding = new Thickness(width, heigyh, 0, 0),
                            FontSize = 26,
                            FontStyle = FontStyles.Italic
                        };
                        Container.Children.Add(txb);

                        break;

                    case 2:
                        TextBlock txb1 = new TextBlock() //жирный шрифт
                        {
                            Text = randstyle[i].ToString(),
                            Padding = new Thickness(width, heigyh, 0, 0),
                            FontSize = 26,
                            FontWeight = FontWeights.Bold,

                        };
                        Container.Children.Add(txb1);
                        break;

                    case 3:
                        TextBlock txb2 = new TextBlock() //курсив и жирный
                        {
                            Text = randstyle[i].ToString(),
                            Padding = new Thickness(width, heigyh, 0, 0),
                            FontSize = 26,
                            FontStyle = FontStyles.Italic
                        };
                        Container.Children.Add(txb2);
                        break;
                }
            } 
        }

        private void tbCap_TextChanged(object sender, TextChangedEventArgs e) //ввод кода
        {
            string word = tbCap.Text; //введенный текст
            int str = tbCap.Text.Length; //количество символов

            if (str == numsymbs)
            {
                if (code == word) //успешный входе
                {
                    MessageBox.Show("Вы авторизировались");
                    count = 0; //сбрасываем попытки
                }

                else if (count == 2)
                {
                    MessageBox.Show("Вы вводите неверный код, будте внимательнее при вводе и повторите попытку еще раз");
                    Content = null;
                    count = 0;
                }

                else
                {
                    MessageBox.Show("Повторите ввод");
                    Content = null; //сбрасываем
                    count += 1;
                }
            }
        }
    }
}