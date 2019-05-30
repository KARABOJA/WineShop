using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace WineShop.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult ViewTemp()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Category()
        {
            return View();
        }
        public ActionResult SearchResult()
        {
            return View();
        }
        public ActionResult Baskets()
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        /*****************************************
         * ****    Checkout                   ****
         * **************************************/
        public ActionResult CheckoutAddressInvoice()
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }

            if (Session["CheckoutAddressInvoice"] != null) { return View((WineShop.Models.Views.ModelCheckoutAddress)Session["CheckoutAddressInvoice"]); }
            else
            {
                WineShop.Models.Views.ModelCheckoutAddress modelCheckoutAddress = new Models.Views.ModelCheckoutAddress()
                {
                    FirstName = WineShop.Tools.Security.Customer.GetCurrent().NameFirst,
                    LastName = WineShop.Tools.Security.Customer.GetCurrent().NameLast,
                    Email = WineShop.Tools.Security.Customer.GetCurrent().EmailAddress,
                    Phone = WineShop.Tools.Security.Customer.GetCurrent().PhoneNumber,

                    Address = WineShop.Tools.Security.Customer.GetCurrent().InvoiceAddress,
                    ZipCode = WineShop.Tools.Security.Customer.GetCurrent().InvoiceZipCode,
                    City = WineShop.Tools.Security.Customer.GetCurrent().InvoiceCity,
                    Country = WineShop.Tools.Security.Customer.GetCurrent().InvoiceCountry,

                    CloneInvoiceAddress = true
                };
                return View(modelCheckoutAddress);
            }
        }
        public ActionResult CheckoutAddressDelivery()
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }
            if (Session["CheckoutAddressInvoice"] == null) { return View("CheckoutAddressInvoice", "Home"); }

            if (Session["CheckoutAddressDelivery"] != null) { return View((WineShop.Models.Views.ModelCheckoutAddress)Session["CheckoutAddressDelivery"]); }
            else
            {
                WineShop.Models.Views.ModelCheckoutAddress modelCheckoutAddress = new Models.Views.ModelCheckoutAddress()
                {
                    FirstName = WineShop.Tools.Security.Customer.GetCurrent().NameFirst,
                    LastName = WineShop.Tools.Security.Customer.GetCurrent().NameLast,
                    Email = WineShop.Tools.Security.Customer.GetCurrent().EmailAddress,
                    Phone = WineShop.Tools.Security.Customer.GetCurrent().PhoneNumber,

                    Address = WineShop.Tools.Security.Customer.GetCurrent().DeliveryAddress,
                    ZipCode = WineShop.Tools.Security.Customer.GetCurrent().DeliveryZipCode,
                    City = WineShop.Tools.Security.Customer.GetCurrent().DeliveryCity,
                    Country = WineShop.Tools.Security.Customer.GetCurrent().DeliveryCountry,

                    CloneInvoiceAddress = false
                };
                return View(modelCheckoutAddress);
            }
        }
        public ActionResult CheckoutShipping()
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }
            if (Session["CheckoutAddressInvoice"] == null) { return View("CheckoutAddressInvoice", "Home"); }
            if (Session["CheckoutAddressDelivery"] == null) { return View("CheckoutAddressDelivery", "Home"); }

            if (Session["CheckoutShipping"] != null) { return View((WineShop.Models.Views.ModelCheckoutShipping)Session["CheckoutShipping"]); }
            else
            {
                WineShop.Models.Views.ModelCheckoutShipping modelCheckoutShipping = new Models.Views.ModelCheckoutShipping();
                return View(modelCheckoutShipping);
            }
        }
        public ActionResult CheckoutPayment()
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }
            if (Session["CheckoutAddressInvoice"] == null) { return View("CheckoutAddressInvoice", "Home"); }
            if (Session["CheckoutAddressDelivery"] == null) { return View("CheckoutAddressDelivery", "Home"); }
            if (Session["CheckoutShipping"] == null) { return View("CheckoutShipping", "Home"); }

            Datas.Ecommerce.DataSetBasketTableAdapters.tbBasketTableAdapter tbBasketTableAdapter = new Datas.Ecommerce.DataSetBasketTableAdapters.tbBasketTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter();

            //tbCustomerCommandTableAdapter.Insert();
            if (Session["CheckoutPayment"] != null) { return View((WineShop.Models.Views.ModelCheckoutPayment)Session["CheckoutPayment"]); }
            else
            {
                return View(new Models.Views.ModelCheckoutPayment());
            }
        }
        public ActionResult CheckoutReview()
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }

            CamatSoft.PaymentTools.Paybox.Module module = new CamatSoft.PaymentTools.Paybox.Module
            {
                //PBX_3DS = "O",
                PBX_NOM_MARCHAND = "WineShop",
                PBX_CMD = $"O{DateTime.UtcNow.ToString("yyMMddHHmmssfff")}",
                PBX_ANNULE = $"{Tools.Framework.DomainName}/{Tools.App.Language.GetCurrent().CultureInfo}/Home/CheckoutCancel",
                PBX_ATTENTE = $"{Tools.Framework.DomainName}/{Tools.App.Language.GetCurrent().CultureInfo}/Home/CheckoutWaitValidation",
                PBX_EFFECTUE = $"{Tools.Framework.DomainName}/{Tools.App.Language.GetCurrent().CultureInfo}/Home/CheckoutComplete",
                PBX_REFUSE = $"{Tools.Framework.DomainName}/{Tools.App.Language.GetCurrent().CultureInfo}/Home/CheckoutReject",
                PBX_TOTAL = (Models.Baskets.GetAmountATIFinal() * 100).ToString(),
                PBX_PORTEUR = Tools.Security.Customer.GetCurrent().EmailAddress
            };

            switch (((Models.Views.ModelCheckoutPayment)Session["CheckoutPayment"]).PaymentMode)
            {
                case 0: module.PBX_TYPECARTE = CamatSoft.PaymentTools.Paybox.Consts.PBX_TYPEPAIEMENT.CARTE._VISA; break;
                case 1: module.PBX_TYPECARTE = CamatSoft.PaymentTools.Paybox.Consts.PBX_TYPEPAIEMENT.CARTE._EUROCARD_MASTERCARD; break;
                case 2: module.PBX_TYPECARTE = CamatSoft.PaymentTools.Paybox.Consts.PBX_TYPEPAIEMENT.CARTE._AMEX; break;
            }

            string s = "";
            if (module.CalculateHMAC()) { s = module.CreateHtmlFormControls(); }

            Models.Views.ModelCheckoutResume modelCheckoutResume = new Models.Views.ModelCheckoutResume { HtlmsControls = s, module = module };

            return View(modelCheckoutResume);
        }

        public ActionResult CheckoutComplete(string Cmd)
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }
            Session["Basket"] = null;
            Session["CheckoutAddressInvoice"] = null;
            Session["CheckoutAddressDelivery"] = null;
            Session["CheckoutShipping"] = null;
            Session["CheckoutPayment"] = null;
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            tbCustomerCommandTableAdapter.sp_set_state_customer_command(Cmd, 1);
            return View(new Models.Views.CheckoutComplete() { Cmd = Cmd });
        }
        public ActionResult CheckoutCancel(string Cmd)
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }
            Session["Basket"] = null;
            Session["CheckoutAddressInvoice"] = null;
            Session["CheckoutAddressDelivery"] = null;
            Session["CheckoutShipping"] = null;
            Session["CheckoutPayment"] = null;
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            tbCustomerCommandTableAdapter.sp_set_state_customer_command(Cmd, -1);
            return View(new Models.Views.CheckoutComplete() { Cmd = Cmd });
        }
        public ActionResult CheckoutReject(string Cmd)
        {
            if (WineShop.Models.Baskets.Count.Equals(0)) { return RedirectToAction("Index", "Home"); }
            else if (WineShop.Tools.Security.Customer.GetCurrent() == null) { return RedirectToAction("CustomerLogin", "Home"); }
            Session["Basket"] = null;
            Session["CheckoutAddressInvoice"] = null;
            Session["CheckoutAddressDelivery"] = null;
            Session["CheckoutShipping"] = null;
            Session["CheckoutPayment"] = null;
            Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.tbCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            tbCustomerCommandTableAdapter.sp_set_state_customer_command(Cmd, -2);
            return View(new Models.Views.CheckoutComplete() { Cmd = Cmd });
        }

        public ActionResult PasswordRecovery()
        {
            Models.Views.ModelLogin modelLogin = new Models.Views.ModelLogin();
            return View(modelLogin);
        }


        public ActionResult CustomerLogin()
        {
            Models.Views.ModelLoginInscription modelLoginInscription = new Models.Views.ModelLoginInscription();
            return View(modelLoginInscription);
        }
        public ActionResult CustomerLogout()
        {
            Tools.Security.Customer.LogOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ChangePassword(string AccessToken)
        {
            Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter tbCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetCustomer.tbCustomerDataTable tbCustomer = tbCustomerTableAdapter.GetDataByAccessToken(AccessToken);
            if (tbCustomer.Count.Equals(1))
            {
                Datas.Ecommerce.DataSetCustomer.tbCustomerRow customerRow = (Datas.Ecommerce.DataSetCustomer.tbCustomerRow)tbCustomer.Rows[0];
                return View(new WineShop.Models.Views.ModelChangePassword() { Email = customerRow.Email, AccessToken = AccessToken });
            }
            else { return RedirectToAction("Index", "Home"); }
        }
    }
}