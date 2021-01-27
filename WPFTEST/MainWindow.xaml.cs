using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace WPFTEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Определим от кого и кому будем отправлять почту
            // Тут надо подставить рабочие адреса электронной почты
            var mail_sender = new MailAddress("lekcsandr@mail.ru", "Sender");
            var mail_recipient = new MailAddress("lekcsandr@mail.ru");

            // Создадим объект сообщения указав отправителя и адресата
            using var message = new MailMessage(mail_sender, mail_recipient)
            {
                // а также заполним свойства заголовка и тела сообщения
                Subject = "Тестовое сообщение",
                Body = $"Текст письма: {DateTime.Now}",

            };

            // Создадим клиента для общения с почтовым сервером по протоколу SMTP
            // В конструкторе надо указать адрес сервера и порт (по умолчанию 25)
            using (var client = new SmtpClient("smtp.mail.ru", 466))
            {
                const string login = "lekcsandr@mail.ru";
                const string password = "password";
                // Обычно сервер не разрешает анонимам отправлять почту
                // Нам необходимо добавить информацию с нашими учётными данными
                client.Credentials = new NetworkCredential(login, password);
                client.EnableSsl = true;
                // После чего можем попросить клиента открыть канал с сервером
                // и отправить наше сообщение
                client.Send(message);
            }
            // Если всё прошло нормально и сервер принял наше сообщение
            // то мы увидим на консоли соответствующую надпись.
            // При указании неверного адреса сервера/порта,
            // получим ошибку таймаута
            // Если мы ввели неверные учётные данные, или забыли их указать
            // то получим ошибку протокола подключения к серверу
            Console.WriteLine("Почта отправлена");
            Console.ReadLine();
        }
    }
}
