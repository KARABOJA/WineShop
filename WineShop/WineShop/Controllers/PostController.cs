using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WineShop.Controllers
{
    public class PostController : Controller
    {
        // GET: Post
        [HttpPost]
        public ActionResult CustomerConnection(Models.Views.ModelLoginInscription modelLoginInscription)
        {
            if (Tools.Security.Customer.LogIn(modelLoginInscription.Login.Email, modelLoginInscription.Login.Password)) { return RedirectToAction("Profile", "Profile"); }
            else { return RedirectToAction("CustomerLogin", "Home");}
        }
        [HttpPost]
        public ActionResult PasswordRecovery(Models.Views.ModelLogin modelLogin)
        {
            Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter tbCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };

            if (tbCustomerTableAdapter.GetDataByEmail(modelLogin.Email).Count.Equals(1)) {
                string[] strArray = new string[] {
                    "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                    "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                string strNewPassword = "";
                string strAccessToken = "";

                Random rnd = new Random();
                for (int i = 0; i < 8; i++) { strNewPassword += strArray[rnd.Next(61)]; }

                for (int i = 0; i < 64; i++) { strAccessToken += strArray[rnd.Next(61)]; }

                MailMessage mailMessage = new MailMessage(new MailAddress(WineShop.Tools.Framework.SmtpFrom), new MailAddress(modelLogin.Email))
                {
                    Subject = "Modification mot de passe",
                    SubjectEncoding = System.Text.Encoding.UTF8,
                    BodyEncoding = System.Text.Encoding.UTF8,
                    IsBodyHtml = true,
                    Body = $"Votre nouveau mot de passe : {strNewPassword}<br /><br />Afin de modifier votre mot de passe veuillez cliquer sur ce lien : <a href='{WineShop.Tools.Framework.DomainName}/fr-FR/Home/ChangePassword?AccessToken={strAccessToken}'>{strAccessToken}</a>",
                };

                using (var client = new SmtpClient())
                {
                    client.Port = WineShop.Tools.Framework.SmtpPort;
                    client.Host = WineShop.Tools.Framework.SmtpHost;
                    client.EnableSsl = WineShop.Tools.Framework.SmtpEnableSsl;
                    client.UseDefaultCredentials = WineShop.Tools.Framework.SmtpUseDefaultCredentials;
                    if (WineShop.Tools.Framework.SmtpUseLogin) { client.Credentials = new NetworkCredential(WineShop.Tools.Framework.SmtpUsername, WineShop.Tools.Framework.SmtpPassword); }
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    try
                    {
                        client.Send(mailMessage);

                        tbCustomerTableAdapter.ResetPassword(strNewPassword, strAccessToken, modelLogin.Email);
                    }
                    catch (SmtpException ex) { }
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult RegisterCustomer(Models.Views.ModelLoginInscription modelLoginInscription)
        {
            Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter tbCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            if (modelLoginInscription.Inscription.Password == modelLoginInscription.Inscription.ConfirmPassword && tbCustomerTableAdapter.GetDataByEmail(modelLoginInscription.Inscription.Email).Count.Equals(0))
            {
                tbCustomerTableAdapter.Insert(
                    Guid.NewGuid()
                    , Guid.Empty
                    , Guid.Empty
                    , true
                    , modelLoginInscription.Inscription.FirstName
                    , modelLoginInscription.Inscription.LastName
                    , $"{modelLoginInscription.Inscription.FirstName} {modelLoginInscription.Inscription.LastName}"
                    , modelLoginInscription.Inscription.Email
                    , modelLoginInscription.Inscription.Phone
                    , modelLoginInscription.Inscription.Password
                    , DateTime.UtcNow
                    , null);

                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult ChangePassword(Models.Views.ModelChangePassword modelChangePassword)
        {
            if (modelChangePassword.NewPassword == modelChangePassword.ConfirmPassword)
            {
                Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter tbCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                tbCustomerTableAdapter.ChangePassword(
                    modelChangePassword.NewPassword
                    , modelChangePassword.Email
                    , modelChangePassword.AccessToken
                    , modelChangePassword.OldPassword);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("ChangePassword", "Home", new { @AccessToken = modelChangePassword.AccessToken });
            }
        }

        [HttpPost]
        public ActionResult CheckoutAddressInvoice(Models.Views.ModelCheckoutAddress modelCheckoutAddress)
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }

            Session["CheckoutAddressInvoice"] = modelCheckoutAddress;
            if (modelCheckoutAddress.CloneInvoiceAddress)
            {
                Session["CheckoutAddressDelivery"] = modelCheckoutAddress;
                return RedirectToAction("CheckoutShipping", "Home");
            }
            else { return RedirectToAction("CheckoutAddressDelivery", "Home"); }

        }
        [HttpPost]
        public ActionResult CheckoutAddressDelivery(Models.Views.ModelCheckoutAddress modelCheckoutAddress)
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }

            Session["CheckoutAddressDelivery"] = modelCheckoutAddress;

            return RedirectToAction("CheckoutShipping", "Home");

        }
        [HttpPost]
        public ActionResult CheckoutShipping(Models.Views.ModelCheckoutShipping modelCheckoutShipping)
        {

            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }

            Session["CheckoutShipping"] = modelCheckoutShipping;

            return RedirectToAction("CheckoutPayment", "Home");

        }
        [HttpPost]
        public ActionResult CheckoutPayment(Models.Views.ModelCheckoutPayment modelCheckoutPayment)
        {

            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }

            Session["CheckoutPayment"] = modelCheckoutPayment;

            return RedirectToAction("CheckoutReview", "Home");

        }

        [HttpPost]
        public ActionResult CheckoutReview(string Cmd)
        {
            //TODO : Create Command
            if (Session["CheckoutAddressInvoice"] == null) { return View("CheckoutAddressInvoice", "Home"); }
            if (Session["CheckoutAddressDelivery"] == null) { return View("CheckoutAddressDelivery", "Home"); }
            if (Session["CheckoutShipping"] == null) { return View("CheckoutShipping", "Home"); }
            if (Session["CheckoutPayment"] == null) { return View("CheckoutPayment", "Home"); }

            Models.Views.ModelCheckoutAddress checkoutAddressInvoice = (Models.Views.ModelCheckoutAddress)Session["CheckoutAddressInvoice"];
            Models.Views.ModelCheckoutAddress checkoutAddressDelivery = (Models.Views.ModelCheckoutAddress)Session["CheckoutAddressDelivery"];
            Models.Views.ModelCheckoutShipping checkoutShipping = (Models.Views.ModelCheckoutShipping)Session["CheckoutShipping"];
            Models.Views.ModelCheckoutPayment checkoutPayment = (Models.Views.ModelCheckoutPayment)Session["CheckoutPayment"];

            //Models.Views.ModelCheckoutAddress checkoutAddressInvoice = (Models.Views.ModelCheckoutAddress)Session["CheckoutAddressInvoice"];

            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandDetailsTableAdapter tbCustomerCommandDetailsTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandDetailsTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };

            Guid UidCommand = Guid.NewGuid();
            try
            {
                if (tbCustomerCommandTableAdapter.Insert(
                    UidCommand
                    , Tools.Security.Customer.GetCurrent().Uid
                    , Tools.Security.Customer.GetCurrent().Uid
                    , true

                    , Tools.Security.Customer.GetCurrent().Uid
                    //TODO: Script NumCommand
                    , Cmd

                    , checkoutAddressInvoice.FirstName
                    , checkoutAddressInvoice.LastName
                    , checkoutAddressInvoice.Email
                    , checkoutAddressInvoice.Phone
                    , checkoutAddressInvoice.Entreprise
                    , checkoutAddressInvoice.Address
                    , checkoutAddressInvoice.ZipCode
                    , checkoutAddressInvoice.City
                    , checkoutAddressInvoice.Country

                    , checkoutAddressDelivery.FirstName
                    , checkoutAddressDelivery.LastName
                    , checkoutAddressDelivery.Email
                    , checkoutAddressDelivery.Phone
                    , checkoutAddressDelivery.Entreprise
                    , checkoutAddressDelivery.Address
                    , checkoutAddressDelivery.ZipCode
                    , checkoutAddressDelivery.City
                    , checkoutAddressDelivery.Country

                    , DateTime.Now
                    , DateTime.Now
                    , 0
                    , Models.Baskets.GetAmount()
                    , Models.Baskets.GetAmountVAT()
                    , Models.Baskets.GetAmountATI()
                    , Tools.App.GetAmountShipping(checkoutShipping.ShippingMode)
                    , (Models.Baskets.GetAmountATI() + Tools.App.GetAmountShipping(checkoutShipping.ShippingMode))
                    , checkoutShipping.ShippingMode
                    , checkoutPayment.PaymentMode).Equals(1))
                {
                    foreach (Models.Basket item in Models.Baskets.GetData())
                    {
                        try
                        {
                            tbCustomerCommandDetailsTableAdapter.Insert(
                                Guid.NewGuid()
                                , Tools.Security.Customer.GetCurrent().Uid
                                , Tools.Security.Customer.GetCurrent().Uid
                                , true

                                , UidCommand
                                , item.UidProduct
                                , item.UidCategoryType
                                , item.UidCategory
                                , item.UidCategorySub
                                , item.Name
                                , item.CodeProduct
                                , item.Quantity
                                , item.UidColor
                                , item.Color
                                , item.UidSize
                                , item.Size
                                , item.UnitPrice
                                , item.AmountTotal
                                , item.VATRate
                                , item.AmountVAT
                                , item.AmountATI);
                        }
                        catch (Exception ex) { Debug.WriteLine(ex.Message); }
                    }
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return View();
        }

        [HttpPost]
        public ActionResult SendMail(WineShop.Models.Views.Contact contact)
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
            return View();
        }
    }
}