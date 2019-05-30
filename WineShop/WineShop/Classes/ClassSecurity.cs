using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
                switch(Tools.App.Environment)
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
                        Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter tbCustomerTableAdapter = new Datas.Ecommerce.DataSetCustomerTableAdapters.tbCustomerTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                        Datas.Ecommerce.DataSetCustomer.tbCustomerDataTable tbCustomerDataTable = tbCustomerTableAdapter.GetDataByEmail(email);
                        if(tbCustomerDataTable.Count.Equals(1))
                        {
                            Datas.Ecommerce.DataSetCustomer.tbCustomerRow customerRow = (Datas.Ecommerce.DataSetCustomer.tbCustomerRow)tbCustomerDataTable.Rows[0];
                            if (customerRow.Password == password)
                            {
                                HttpContext.Current.Session["CurrentCustomer"] = new Models.Customer()
                                {
                                    Uid = customerRow.Uid,
                                    NameFirst = customerRow.NameFirst,
                                    NameLast = customerRow.NameLast,
                                    FullName = customerRow.FullName,
                                    EmailAddress = customerRow.Email,
                                    PhoneNumber = customerRow.Phone,
                                    Password = customerRow.Password,
                                    Enabled = customerRow.Enabled
                                };
                                return true;
                            }
                        }
                        break;

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