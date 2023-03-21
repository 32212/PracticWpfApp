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

        public static string login; //поля для записи логина и пароля, хотя зачем они нкжны? пусть пользователь новый введет и все
        public static string password;

        public AutorizationPage()
        {
            InitializeComponent();
        }

        public AutorizationPage(int a)
        {
            InitializeComponent();
            spTm.Visibility = Visibility.Visible;
            btnAutorization.IsEnabled = false;

            timer.Interval = new TimeSpan(0, 0, 1); //устанавливаем интервал в 1 секунду
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
                MessageBox.Show("Повторите ввод данных");
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e) //авторизация в качестве гостя
        {
            MessageBox.Show("Добро пожаловать, дорогой гость");
            FrameClass.MainFrame.Navigate(new ProductListPage());
        }

        private void btnAutorization_Click(object sender, RoutedEventArgs e) //событие при авторизации
        {
            if (tbLogin.Text == "" || tbLogin.Text == " " && tbPassword.Text == "" || tbPassword.Text == " ")
            {
                attempts++;
                MessageBox.Show("Ошибка ввода");
                //return;
            }

            User user = BaseClass.EM.User.FirstOrDefault(x => x.UserLogin == tbLogin.Text && x.UserPassword == tbPassword.Text);

            if (user == null)
            {
                attempts++;
                MessageBox.Show("Проверьте введенные данные", "Пользователь не найден!", MessageBoxButton.OK); 
                //return;
            }

            if (attempts == 2) //запуск капчи
            {
                attempts = 0;
                FrameClass.MainFrame.Navigate(new CAPTCHAPage());              
            }

            if (user != null) 
            {
                attempts = 0;

                switch (user.RoleID) //проверка на роль, если пользоваетль найден
                {
                    case 1: //клиент
                        MessageBox.Show("Добро пожаловать, клиент");
                        FrameClass.MainFrame.Navigate(new ProductListPage(user));
                        break;
                    case 2: //админ
                        MessageBox.Show("Добро пожаловать, администратор");
                        FrameClass.MainFrame.Navigate(new ProductListPage(user));
                        break;
                    case 3: //менеджер
                        MessageBox.Show("Добро пожаловать, менеджер");
                        FrameClass.MainFrame.Navigate(new ProductListPage(user));
                        break;
                }
            }      
        }
    }
}
