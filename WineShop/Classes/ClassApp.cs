using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using WineShop.Models;

namespace WineShop.Tools
{
    public class App
    {
        public class Currency
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static Models.Currency GetCurrent()
            {
                if (HttpContext.Current.Session["CurrentCurrency"] == null)
                {
                    //SetCurrent(Guid.NewGuid(), 1, "Euro", "€", "€ Euro", 1, true);
                    Models.Currency currency = new Models.Currency();
                    switch (Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            currency = new Models.Currency()
                            {
                                Uid = Guid.NewGuid(),
                                Idfc = 1,
                                Name = "Euro",
                                Symbol = "€",
                                Description = "€ EUR",
                                Rate = 1,
                                Enabled = true
                            };
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            currency = Models.Currencies.GetDataDefault();
                            break;
                    }
                    Tools.App.Currency.SetCurrent(currency);
                }
                if (HttpContext.Current.Session["CurrentCurrency"] != null) { return (Models.Currency)HttpContext.Current.Session["CurrentCurrency"]; }
                return null;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="uid">Unique identifiant de la devise</param>
            /// <param name="idfc">Id de fonction de la devise</param>
            /// <param name="name">Nom de la devise</param>
            /// <param name="symbol">Symbole de la devise</param>
            /// <param name="description">Description de la devise</param>
            /// <param name="rate">Taux de la devise</param>
            /// <param name="enabled">Etat activé de la devise</param>
            public static void SetCurrent(
                Guid uid
                , int idfc//arguments de fonction
                , string name
                , string symbol
                , string description
                , double rate
                , bool enabled
                , bool defaultValue)
            {
                Models.Currency currency = new Models.Currency(uid, idfc, name, symbol, description, rate, enabled, defaultValue);//arguments de constructeur provenant des arguments de fonction
                HttpContext.Current.Session["CurrentCurrency"] = currency;
            }
            public static void SetCurrent(Models.Currency currency) { HttpContext.Current.Session["CurrentCurrency"] = currency; }
        }

        public class Language
        {
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public static Models.Language GetCurrent()
            {
                if (HttpContext.Current.Session["CurrentLanguage"] == null)
                {
                    //SetCurrent(Guid.NewGuid(), 1, "Euro", "€", "€ Euro", 1, true);
                    Models.Language language = new Models.Language();
                    switch (Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            language = new Models.Language()
                            {
                                Uid = Guid.NewGuid(),
                                Idfc = 1,
                                Name = "France",
                                Description = "Français",
                                CodeIso = "FR",
                                CultureInfo = "fr-FR",
                                Enabled = true,
                                DefaultValue = true
                            };
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            language = Models.Languages.GetDataByDefault();
                            break;
                    }
                    Tools.App.Language.SetCurrent(language);
                }
                if (HttpContext.Current.Session["CurrentLanguage"] != null) { return (Models.Language)HttpContext.Current.Session["CurrentLanguage"]; }
                return null;
            }
            public static void SetCurrent(
                Guid uid
                , int idfc
                , string name
                , string description
                , string codeIso
                , string cultureInfo
                , bool enabled
                , bool defaultValue)
            {
                Models.Language language = new Models.Language(uid, idfc, name, description, codeIso, cultureInfo, enabled, defaultValue);
                HttpContext.Current.Session["CurrentLanguage"] = language;
            }
            public static void SetCurrent(Models.Language language) { HttpContext.Current.Session["CurrentLanguage"] = language; }
        }

        /// <summary>
        /// test 
        /// </summary>
        public static Compagny Compagny
        {
            get
            {
                Compagny compagny = new Compagny();
                switch(Tools.App.Environment)
                {
                    case Enums.Environment.Test:
                        compagny = new Compagny()
                        {
                            Uid = Guid.NewGuid(),
                            Name = "WineShop",
                            Phone = "04.93.19.41.21",
                            Mail = "Contact@ufip-france.com",
                            Enabled = true,
                            Days = "Lundi-Vendredi",
                            Hours = "09:00 - 20:00",
                            Address = "11 rue des grenouilléres, 06000 Nice, France"
                        };
                        break;
                    case Enums.Environment.Production:
                        //On va chercher les infos dans la base de données
                        Datas.Ecommerce.DataSetCompagnyTableAdapters.tbCompagnyTableAdapter tbCompagnyTableAdapter = new Datas.Ecommerce.DataSetCompagnyTableAdapters.tbCompagnyTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                        Datas.Ecommerce.DataSetCompagny.tbCompagnyDataTable tbCompagnyDataTable = tbCompagnyTableAdapter.GetData();
                        Datas.Ecommerce.DataSetCompagny.tbCompagnyRow item = (Datas.Ecommerce.DataSetCompagny.tbCompagnyRow)tbCompagnyDataTable.Rows[0];
                        compagny = new Compagny()
                        {
                            Uid = item.Uid,
                            Name = item.Name,
                            Phone = item.Phone,
                            Mail = item.Mail,
                            Enabled = item.Enabled,
                            Days = item.Days,
                            Hours = item.Hours,
                            Address = item.Address,
                            ZipCode = item.IsZipCodeNull() ? null : item.ZipCode,
                            City = item.IsCityNull() ? null : item.City,
                            Country = item.IsCountryNull() ? null : item.Country,
                        };
                        break;
                }
                return compagny;
            }
        }

        public static string Translate(string value, Guid uidLine)
        {
            return Translate(value, uidLine, Language.GetCurrent().Uid);
        }
        public static string Translate(string value, Guid uidLine, Guid uidLanguage)
        {
            Datas.Ecommerce.DataSetParamsTableAdapters.tbTranslateTableAdapter adapter = new Datas.Ecommerce.DataSetParamsTableAdapters.tbTranslateTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetParams.tbTranslateDataTable dataTable = adapter.GetDataByUidLineUidLanguage(uidLine, uidLanguage);
            if (dataTable.Count.Equals(1)) { return ((Datas.Ecommerce.DataSetParams.tbTranslateRow)dataTable.Rows[0]).Description; }
            else { return value; }
        }

        public static Enums.Environment Environment
        {
            get
            {
                switch(ConfigurationManager.AppSettings["Environment"])
                {
                    case "Test": return Enums.Environment.Test;
                    case "Production": return Enums.Environment.Production;
                    default: return Enums.Environment.None;
                }
            }
        }

        public static double GetAmountShipping(int idfc)
        {
            switch (idfc)
            {
                case 0:return 22.50;
                case 1:return 10.00;
                case 2: return 33.85;
                default: return 0;
            }
        }

        public static string GetPayment(int idfc)
        {
            switch (idfc)
            {
                case 0: return "VISA";
                case 1: return "MASTERCARD";
                case 2: return "AMERICAN EXPRESS";
                default: return "";
            }
        }
    }

    public class Framework
    {
        public static string GetParams(EnumParams enumParams) { return GetParams(enumParams, null); }
        public static string GetParams(EnumParams enumParams, string defaultValue)
        {
            Datas.Ecommerce.DataSetParamsTableAdapters.tbParamsTableAdapter adapter = new Datas.Ecommerce.DataSetParamsTableAdapters.tbParamsTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetParams.tbParamsDataTable dataTable = adapter.GetDataByName(enumParams.ToString());
            if (dataTable.Count.Equals(1)) { return ((Datas.Ecommerce.DataSetParams.tbParamsRow)dataTable.Rows[0]).Value; }
            else { return defaultValue; }
        }
        public static double GetParams(EnumParams enumParams, double defaultValue)
        {
            Datas.Ecommerce.DataSetParamsTableAdapters.tbParamsTableAdapter adapter = new Datas.Ecommerce.DataSetParamsTableAdapters.tbParamsTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
            Datas.Ecommerce.DataSetParams.tbParamsDataTable dataTable = adapter.GetDataByName(enumParams.ToString());
            if (dataTable.Count.Equals(1))
            {
                string s = ((Datas.Ecommerce.DataSetParams.tbParamsRow)dataTable.Rows[0]).Value;
                if (!string.IsNullOrEmpty(s))
                {
                    double d = 0.2d;
                    double.TryParse(GetParams(enumParams), out d);
                    return d;
                }
                return defaultValue;
            }
            else { return defaultValue; }
        }

        public static string PriceFormat
        {
            get
            {
                string result = "0.00";
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.PriceFormat, result);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static string DomainName
        {
            get
            {
                string result = "";
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = "http://localhost:60213";
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.DomainName);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static string PercentFormat
        {
            get
            {
                string result = "0.00 %";
                try
                {
                    switch (Tools.App.Environment)
                    {
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.PercentFormat);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }

        //-------------------------------------------------//
        //----    SMTP                                 ----//
        public static string SmtpHost
        {
            get
            {
                string result = "";
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"].ToString();
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.SmtpHost);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static int SmtpPort
        {
            get
            {
                int result = 25;
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"].ToString());
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            int.TryParse(GetParams(EnumParams.SmtpPort), out result);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static bool SmtpEnableSsl
        {
            get
            {
                bool result = false;
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpEnableSsl"].ToString());
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            bool.TryParse(GetParams(EnumParams.SmtpEnableSsl), out result);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static bool SmtpUseDefaultCredentials
        {
            get
            {
                bool result = false;
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpUseDefaultCredentials"].ToString());
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            bool.TryParse(GetParams(EnumParams.SmtpUseDefaultCredentials), out result);
                            break;
                    }
                    return result;
                }
                catch { }
                return result;
            }
        }
        public static bool SmtpUseLogin
        {
            get
            {
                bool result = false;
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpUseLogin"].ToString());
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            bool.TryParse(GetParams(EnumParams.SmtpUseLogin), out result);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static string SmtpPassword
        {
            get
            {
                string result = "";
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"].ToString();
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.SmtpPassword);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static string SmtpUsername
        {
            get
            {
                string result = "";
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = System.Configuration.ConfigurationManager.AppSettings["SmtpUsername"].ToString();
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.SmtpUsername);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static string SmtpFrom
        {
            get
            {
                string result = "";
                try
                {
                    switch(Tools.App.Environment)
                    {
                        case Enums.Environment.Test:
                            result = System.Configuration.ConfigurationManager.AppSettings["SmtpFrom"].ToString();
                            break;
                        case Enums.Environment.Production:
                            //On va chercher les infos dans la base de données
                            result = GetParams(EnumParams.SmtpFrom);
                            break;
                    }
                }
                catch { }
                return result;
            }
        }
        public static double VATRate
        {
            get
            {
                return GetParams(EnumParams.VATRate, 0.2d);
            }
        }
        //-------------------------------------------------//

        public enum EnumParams
        {
            None,
            PriceFormat,
            PercentFormat,

            DomainName,

            SmtpHost,
            SmtpPort,
            SmtpEnableSsl,
            SmtpUseDefaultCredentials,
            SmtpUseLogin,
            SmtpPassword,
            SmtpUsername,
            SmtpFrom,
            VATRate,
        }
    }
}