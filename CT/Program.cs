using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CT
{
    class Program
    {
        static void Main(string[] args)
        {

            // Определим от кого и кому будем отправлять почту
            // Тут надо подставить рабочие адреса электронной почты
            var sender = new MailAddress("sender_user@yandex.com", "Sender");
            var recipient = new MailAddress("recipient_user@yandex.ru");

            // Создадим объект сообщения указав отправителя и адресата
            using var message = new MailMessage(sender, recipient)
            {
                // а также заполним свойства заголовка и тела сообщения
                Subject = "Тестовое сообщение",
                Body = $"Текст письма: {DateTime.Now}",

            };

            // Создадим клиента для общения с почтовым сервером по протоколу SMTP
            // В конструкторе надо указать адрес сервера и порт (по умолчанию 25)
            using (var client = new SmtpClient("smtp.yandex.ru", 587))
            {
                const string login = "sender_user@yandex.com";
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
