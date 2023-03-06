using PracticWpfApp.Classes;
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
using System.Windows.Threading;

namespace PracticWpfApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutorizationPage.xaml
    /// </summary>
    public partial class AutorizationPage : Page
    {
        DispatcherTimer timer = new DispatcherTimer();

        public int attempts = 0; //считаем попытки входа
        public int sec = 10; //задаем требуемое для ожидания время

        public AutorizationPage()
        {
            InitializeComponent();
        }

        public AutorizationPage(int a)
        {
            InitializeComponent();
            spTm.Visibility = Visibility.Visible;
            btnAutorization.IsEnabled = false;

            timer.Interval = new TimeSpan(0, 0, 0, 0); //устанавливаем интервал в 1 секунду
            timer.Start(); //запускаем таймер
            timer.Tick += new EventHandler(unsuccessInp); //по окончанию таймера, метод будет запускаться, обновляя число
        }

        public void unsuccessInp(object sender, EventArgs e) //неудачная попытка авторизироваться при вводе captcha
        {
            if (sec != 0)
            {
                tblSec.Text = sec.ToString();
                sec--;
            }

            else
            {
                timer.Stop(); //останавливаем работу таймера
                FrameClass.MainFrame.Navigate(new AutorizationPage()); //обновляем страницу без таймера
                MessageBox.Show("Отсчет завершен", "Повторите ввод", MessageBoxButton.OK);
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e) //авторизация в качестве гостя
        {
            FrameClass.MainFrame.Navigate(new ProductListPage()); //потом тут добавить параметр для гостя
        }

        private void btnAutorization_Click(object sender, RoutedEventArgs e) //событие при авторизации
        {
            string login = "Иван"; //добавить сюда возможность ввода существующих данных, исправить и условие проверки
            string password = "1234";

            if (login == tbLogin.Text && password == tbPassword.Text) //Условие на проверку введенного значения
            {
                attempts = 0; //сбрасываем попытки при успешном входе 
                FrameClass.MainFrame.Navigate(new ProductListPage());
            }

            else if (attempts >= 1)
            {
                FrameClass.MainFrame.Navigate(new CAPTCHAPage()); //требуем пользователся пройти капчу
            }

            else
            {
                MessageBox.Show("Неверный логин или пароль, повторите ввод", "Ошибка ввода", MessageBoxButton.OK);
            }

        }
    }
}
