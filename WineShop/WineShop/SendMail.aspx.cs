using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WineShop
{
    public partial class SendMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Form != null && Request.Form.Count > 0)
            {
                SmtpClient smtpClient = new SmtpClient();

                MailMessage mailMessage = new MailMessage(new MailAddress(Tools.Framework.SmtpFrom), new MailAddress(Request.Form["To"]))
                {
                    Subject = Request.Form["Object"],
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    //Body = contact.Message,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    //Priority = MailPriority.High,
                };
                mailMessage.CC.Add(new MailAddress(Request.Form["Email"]));

                smtpClient.Host = Tools.Framework.SmtpHost;
                smtpClient.Port = Tools.Framework.SmtpPort;
                    smtpClient.EnableSsl = Tools.Framework.SmtpEnableSsl;
                smtpClient.UseDefaultCredentials = Tools.Framework.SmtpUseDefaultCredentials;
                if (Tools.Framework.SmtpUseLogin)
                {
                    smtpClient.Credentials = new NetworkCredential(Tools.Framework.SmtpUsername, Tools.Framework.SmtpPassword);
                }
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpMail.ImageSignPath = MapPath("/Images/Logo.png");

                string sBody = "";
                    sBody += Environment.NewLine + $"Bonjour,<br /><br />Vous avez reçu une message de votre site internet.<br /><br />";
                    sBody += Environment.NewLine + $"{Request.Form["NameLast"]} {Request.Form["NameFirst"]}<br />";
                    sBody += Environment.NewLine + $"Société : {Request.Form["Society"]}<br />";
                    sBody += Environment.NewLine + $"Courriel : {Request.Form["Email"]}<br />";
                    sBody += Environment.NewLine + $"Téléphone : {Request.Form["Phone"]}<br />";
                    sBody += Environment.NewLine + $"<br />-------------------------------------------------------------<br />";
                    sBody += Environment.NewLine + $"Message :<br />";
                    sBody += Environment.NewLine + $"<br />{Request.Form["Message"]}";
                    sBody += Environment.NewLine + $"<br />-------------------------------------------------------------<br />";
                    sBody += Environment.NewLine + $"<br />";
                    sBody += Environment.NewLine + $"<span style='font-size: 8pt;'>Cordialement,</span><br />";
                    sBody += Environment.NewLine + $"<br />";
                    sBody += Environment.NewLine + $"<table cellpadding='0' cellspacing='0' border='0'>";
                    sBody += Environment.NewLine + $"    <tr>";
                    sBody += Environment.NewLine + $"        <td colspan='2'><hr /></td>";
                    sBody += Environment.NewLine + $"    </tr>";
                    sBody += Environment.NewLine + $"    <tr>";
                    sBody += Environment.NewLine + $"        <td style='text-align:left; width:120px;'><img src='cid:ImageSign' style='max-width:100px; max-height:100px;' /></td>";
                    sBody += Environment.NewLine + $"        <td>";
                    sBody += Environment.NewLine + $"            <span style='font-size: 10pt;color:#451f0c;'><strong style='color:#22af4b;'>{Tools.App.Compagny.Name}</strong><br />";
                    sBody += Environment.NewLine + $"            {Tools.App.Compagny.Address}<br />";
                    sBody += Environment.NewLine + $"            06600 Antibes<br />";
                    sBody += Environment.NewLine + $"            {Tools.App.Compagny.Phone}<br />";
                    sBody += Environment.NewLine + $"            {Tools.App.Compagny.Mail}</span>";
                    sBody += Environment.NewLine + $"        </td>";
                    sBody += Environment.NewLine + $"    </tr>";
                    sBody += Environment.NewLine + $"    <tr>";
                    sBody += Environment.NewLine + $"        <td colspan='2'><hr /></td>";
                    sBody += Environment.NewLine + $"    </tr>";
                    sBody += Environment.NewLine + $"</table>";

                mailMessage.Body = sBody;


                    try { smtpClient.Send(mailMessage); Response.Write("success"); }
                    catch(Exception exSend) { Response.Write("failed"); }
            }
            else { Response.Write("failed"); }
        }
    }
}


/*
 * 
            try
            {
                MailMessage mailMessage = new MailMessage(new MailAddress(ConfigurationManager.AppSettings["EmailFrom"]), new MailAddress(ConfigurationManager.AppSettings["EmailTo"]))
                {
                    Subject = contact.Subject,
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    Body = contact.Message,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    //Priority = MailPriority.High,
                };

                using (var client = new SmtpClient())
                {
                    client.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    client.Host = ConfigurationManager.AppSettings["Host"];
                    client.EnableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);
                    client.UseDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
                    if (bool.Parse(ConfigurationManager.AppSettings["UseLogin"])) { client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]); }
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch (SmtpException ex) { Debug.WriteLine(ex.Message); }
                }
            }
            catch (SmtpException ex) { Debug.WriteLine(ex.Message); }
 * 
 * */
