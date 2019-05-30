[HttpPost]
        public ActionResult SendMail(CVtheque.Models.Views.Contact contact)
        {
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
            return RedirectToAction("Contact", "Home");
        }