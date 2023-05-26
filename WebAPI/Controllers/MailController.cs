using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/mail")]
    [ApiController]
    public class MailController : BaseApiController
    {


        [HttpPost]
        public async Task<IActionResult> SendMailAsync(MailDto req)
        {

            string host = "smtp.yandex.com.tr";
            int port = 587;

            MailMessage ePosta = new();

            ePosta.From = new MailAddress("destek@replik.com.tr");
            ePosta.To.Add(new MailAddress("destek@replik.com.tr"));
            ePosta.Subject = req.Subject;
            ePosta.Body = req.Body;
            ePosta.Priority = MailPriority.Normal;
            SmtpClient smtp = new(host, port)
            {
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("destek@replik.com.tr", "@des@35!")
            };
            await smtp.SendMailAsync(ePosta);

            return Ok();
        }





        private void SendMail(string mail, string pass, string subject, string body)
        {



        }



    }
}
