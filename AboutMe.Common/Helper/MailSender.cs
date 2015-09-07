using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail;
using System.Net;

namespace AboutMe.Common.Helper
{
    public class MailSender
    {
        public string sender;
        public string receiver;
        public string subject;
        public string body;

        public int err_no; // 0:성공  , 1+ :에러코드
        public string err_msg;  //에러시 메세지

        private string sender_pw;
        private string sender_smtp_server;
        private int sender_smtp_port;
        private int timeout;

        //public async Task MailSendAction(string _receiver = "", string _subject = "[ABOUT ME]No Subject", string _body = "")
        public void MailSendAction(string _sender = "", string _sender_pw = "", string _sender_smtp_server = "", string _sender_smtp_port = "587", string _timeout="20000"
            , string _receiver = "", string _subject = "[ABOUT ME]No Subject", string _body = "")
        {
            this.err_no = 0; //일단 에러없음
            this.err_msg = ""; //일단 에러없음

            this.sender = _sender; // "noreply@cstone.co.kr"; //발송자 계정 -상수로 변경 필요
            this.sender_pw = _sender_pw; //"cstonedev12"; //발송자 암호
            this.sender_smtp_server = _sender_smtp_server; // "smtp.gmail.com";  //발송 SMTP서버
            this.sender_smtp_port =int.Parse(_sender_smtp_port); //587;  //465 or 587   //발송 SMTP서버 포트
            this.timeout = int.Parse(_timeout);  // 20000;  //타임아웃 20초

            this.receiver = _receiver;
            this.subject = _subject;
            this.body = _body;

            //파람 확인
            if (sender == "" || receiver == "" || body == "")
            {
                this.err_no = 1;
                this.err_msg = " 수신자, 내용은 필수값입니다.";
                //return this.err_no;
            }

            //메세지 준비
            MailAddress fromAddress = new MailAddress(this.sender,"ABOUT ME");
            MailAddress toAddress = new MailAddress(this.receiver);

            MailMessage mailMessage = new MailMessage(fromAddress.Address, toAddress.Address);

            mailMessage.Subject = this.subject;
            mailMessage.Body = this.body;

            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            mailMessage.IsBodyHtml = true;

            //smtp 준비
            SmtpClient client = new SmtpClient();
            //client.EnableSsl = true;
            client.EnableSsl = false;
            client.Host = this.sender_smtp_server;
            client.Port = this.sender_smtp_port; // = 587;  //465 or 587
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            // client.UseDefaultCredentials = false; //<<<<<문제??
            client.Timeout = this.timeout;
            client.Credentials = new NetworkCredential(this.sender, this.sender_pw);

            /*
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("noreply@cstone.co.kr", "cstonedev12"),
                EnableSsl = true,
                Timeout = 20000
                //UseDefaultCredentials = false  //문제???
            };
            */



            //client.Send(mailMessage);
            try
            {


                client.Send(mailMessage); //동기 발송
                //client.SendMailAsync(mailMessage);  //비동기 발송


            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //throw new Exception(ex.Message);
                this.err_no = 99; //TimeoutException out
                this.err_msg = ex.Message;
            }

            client.Dispose();

            // return this.err_no;

        }
    }
}
