using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSendMail
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        public void SendMail()
        {
            try
            {
                MailMessage mail = new MailMessage();

                /* 3個參數分別是發件人的地址（可以隨便寫），發件人姓名，編碼*/
                mail.From = new MailAddress("XXX@gmail.com", "小魚", System.Text.Encoding.UTF8);

                mail.To.Add("blinda12@ms4.hinet.net");
                //msg.To.Add("b@b.com");可以發送給多人
                //msg.CC.Add("c@c.com");
                //msg.CC.Add("c@c.com");可以抄送副本給多人 

                mail.Bcc.Add("密件副本的收件者Mail");//加入密件副本的Mail

                //mail.Priority = MailPriority.High;//郵件優先級 

                mail.Subject = "測試標題";//郵件標題
                mail.SubjectEncoding = Encoding.UTF8;//郵件標題編碼

                //插入圖片至信件內文
                LinkedResource res = new LinkedResource("netcore3.png"); //建立連結資源
                res.ContentId = Guid.NewGuid().ToString();
                //使用<img src="/img/loading.svg" data-src="cid:..."方式引用內嵌圖片
                string htmlBody = $@"<div>.NET Core 3 架構圖如下：</div>
                                  <div><img src='cid:{res.ContentId}'/></div>";
                //建立AlternativeView
                var altView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
                
                //將圖檔資源加入AlternativeView
                altView.LinkedResources.Add(res);

                //將AlternativeView加入MailMessage
                mail.AlternateViews.Add(altView);
                // todo 不確定上下兩者是否可以並存
                mail.Body = "測試一下"; //郵件內容
                mail.BodyEncoding = Encoding.UTF8;//郵件內容編碼 
           
                mail.IsBodyHtml = true;//是否是HTML郵件 
                mail.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件 可多個

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("XXX@gmail.com", "****"); //這裡要填正確的帳號跟密碼
                client.Host = "smtp.gmail.com"; //設定smtp Server
                client.Port = 25; //設定Port
                client.EnableSsl = true; //gmail預設開啟驗證 Gmail的smtp 必須要使用SSL
                client.Send(mail); //寄出信件
                client.Dispose();
                mail.Dispose();
                Console.WriteLine("郵件寄送成功！");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
