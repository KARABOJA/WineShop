using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Web;
using WineShop.Models;

namespace WineShop.Tools
{
    public class Security
    {
        public class Customer
        {
            public static void LogOut()
            {
                HttpContext.Current.Session["CurrentCustomer"] = null;
            }

            public static bool LogIn(string email, string password)
            {
                switch (Tools.App.Environment)
                {
                    case Enums.Environment.Test:
                        if (email == "john.doe@dns.com" && password == "azerty*2017")
                        {
                            HttpContext.Current.Session["CurrentCustomer"] = new Models.Customer()
                            {
                                Uid = Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"),
                                NameFirst = "John",
                                NameLast = "Doe",
                                FullName = "John Doe",
                                EmailAddress = "john.doe@dns.com",
                                PhoneNumber = "+33(0) 6.00.00.00.00",
                                Password = "azerty*2017",
                                Enabled = true
                            };
                            return true;
                        }
                        break;
                    case Enums.Environment.Production:
                        //On va chercher les infos dans la base de données
                        Datas.Ecommerce.DataSetCustomerTableAdapters.vwCustomerTableAdapter vwCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.vwCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                        Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerLoginTableAdapter tbCustomerLoginTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerLoginTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                        Datas.Ecommerce.DataSetCustomer.vwCustomerDataTable vwCustomerDataTable = vwCustomerTableAdapter.GetDataByEmail(email);
                        if (vwCustomerDataTable.Count.Equals(1))
                        {
                            Datas.Ecommerce.DataSetCustomer.vwCustomerRow customerRow = (Datas.Ecommerce.DataSetCustomer.vwCustomerRow)vwCustomerDataTable.Rows[0];
                            if (customerRow.Password == password)
                            {
                                try
                                {
                                    tbCustomerLoginTableAdapter.Insert(
                                        Guid.NewGuid()
                                        , customerRow.Uid
                                        , customerRow.Uid
                                        , true
                                        , customerRow.Uid);
                                }
                                catch { }
                                HttpContext.Current.Session["CurrentCustomer"] = new Models.Customer()
                                {
                                    Uid = customerRow.Uid,
                                    NameFirst = customerRow.NameFirst,
                                    NameLast = customerRow.NameLast,
                                    FullName = customerRow.FullName,
                                    EmailAddress = customerRow.Email,
                                    PhoneNumber = customerRow.Phone,
                                    Password = customerRow.Password,
                                    Enabled = customerRow.Enabled,

                                    InvoiceAddress = customerRow.IsInvoiceAddressNull() ? null : customerRow.InvoiceAddress,
                                    InvoiceZipCode = customerRow.IsInvoiceZipCodeNull() ? null : customerRow.InvoiceZipCode,
                                    InvoiceCity = customerRow.IsInvoiceCityNull() ? null : customerRow.InvoiceCity,
                                    InvoiceCountry = customerRow.IsInvoiceCountryNull() ? null : customerRow.InvoiceCountry,

                                    DeliveryAddress = customerRow.IsDeliveryAddressNull() ? null : customerRow.DeliveryAddress,
                                    DeliveryZipCode = customerRow.IsDeliveryZipCodeNull() ? null : customerRow.DeliveryZipCode,
                                    DeliveryCity = customerRow.IsDeliveryCityNull() ? null : customerRow.DeliveryCity,
                                    DeliveryCountry = customerRow.IsDeliveryCountryNull() ? null : customerRow.DeliveryCountry,

                                    PhotoBytes = customerRow.IsPhotoNull() ? null : customerRow.Photo,
                                    PhotoContentType = customerRow.IsPhotoContentTypeNull() ? "image/png" : customerRow.PhotoContentType
                                };
                                return true;
                            }
                        }
                        break;

                }
                return false;
            }

            public static bool ReloadCustomer()
            {
                if (HttpContext.Current.Session["CurrentCustomer"] != null)
                {
                    switch (Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            HttpContext.Current.Session["CurrentCustomer"] = new Models.Customer()
                            {
                                Uid = Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"),
                                NameFirst = "John",
                                NameLast = "Doe",
                                FullName = "John Doe",
                                EmailAddress = "john.doe@dns.com",
                                PhoneNumber = "+33(0) 6.00.00.00.00",
                                Password = "azerty*2017",
                                Enabled = true
                            };
                            return true;
                        case Enums.Environment.Production:
                            Datas.Ecommerce.DataSetCustomerTableAdapters.vwCustomerTableAdapter vwCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.vwCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                            Datas.Ecommerce.DataSetCustomer.vwCustomerDataTable vwCustomerDataTable = vwCustomerTableAdapter.GetDataByUid(GetCurrent().Uid);
                            if (vwCustomerDataTable.Count.Equals(1))
                            {
                                Datas.Ecommerce.DataSetCustomer.vwCustomerRow customerRow = (Datas.Ecommerce.DataSetCustomer.vwCustomerRow)vwCustomerDataTable.Rows[0];
                                HttpContext.Current.Session["CurrentCustomer"] = new Models.Customer()
                                {
                                    Uid = customerRow.Uid,
                                    NameFirst = customerRow.NameFirst,
                                    NameLast = customerRow.NameLast,
                                    FullName = customerRow.FullName,
                                    EmailAddress = customerRow.Email,
                                    PhoneNumber = customerRow.Phone,
                                    Password = customerRow.Password,
                                    Enabled = customerRow.Enabled,

                                    InvoiceAddress = customerRow.IsInvoiceAddressNull() ? null : customerRow.InvoiceAddress,
                                    InvoiceZipCode = customerRow.IsInvoiceZipCodeNull() ? null : customerRow.InvoiceZipCode,
                                    InvoiceCity = customerRow.IsInvoiceCityNull() ? null : customerRow.InvoiceCity,
                                    InvoiceCountry = customerRow.IsInvoiceCountryNull() ? null : customerRow.InvoiceCountry,

                                    DeliveryAddress = customerRow.IsDeliveryAddressNull() ? null : customerRow.DeliveryAddress,
                                    DeliveryZipCode = customerRow.IsDeliveryZipCodeNull() ? null : customerRow.DeliveryZipCode,
                                    DeliveryCity = customerRow.IsDeliveryCityNull() ? null : customerRow.DeliveryCity,
                                    DeliveryCountry = customerRow.IsDeliveryCountryNull() ? null : customerRow.DeliveryCountry,

                                    PhotoBytes = customerRow.IsPhotoNull() ? null : customerRow.Photo,
                                    PhotoContentType = customerRow.IsPhotoContentTypeNull() ? "image/png" : customerRow.PhotoContentType
                                };
                                return true;
                            }
                            break;
                    }
                }
                return false;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static Models.Customer GetCurrent()
            {
                if (HttpContext.Current.Session["CurrentCustomer"] != null) { return (Models.Customer)HttpContext.Current.Session["CurrentCustomer"]; }
                return null;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static Models.Customer SetCurrent(Models.Customer customer)
            {
                HttpContext.Current.Session["CurrentCustomer"] = customer;
                return customer;
            }
        }

        public class Connection
        {
            public static OdbcConnection odbcConnection
            {
                get
                {
                    if (Tools.App.Environment == Enums.Environment.Production) { return new System.Data.Odbc.OdbcConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbProductionConnectionString"].ConnectionString); }
                    else { return new System.Data.Odbc.OdbcConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbWineShopConnectionString"].ConnectionString); }
                }
            }
        }
    }
}