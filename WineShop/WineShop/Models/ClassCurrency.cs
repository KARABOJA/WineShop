using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    /// <summary>
    /// Class Devises
    /// </summary>
    public class Currencies
    {
        /// <summary>
        /// Methode de test
        /// </summary>
        public static List<Currency> GetData()
        {
            List<Currency> Items = new List<Currency>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new Currency()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 1,
                        Name = "Euro",
                        Symbol = "€",
                        Description = "€ EUR",
                        Rate = 1,
                        Enabled = true
                    });
                    Tools.App.Currency.SetCurrent(Guid.NewGuid(), 1, "Euro", "€", "€ Euro", 1, true);
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCurrencyTableAdapters.tbCurrencyTableAdapter tbCurrencyTableAdapter = new Datas.Ecommerce.DataSetCurrencyTableAdapters.tbCurrencyTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCurrency.tbCurrencyDataTable tbCurrencyDataTable = tbCurrencyTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCurrency.tbCurrencyRow item in tbCurrencyDataTable.Rows) { Items.Add(new Currency(item.Uid, item.Idfc, item.Name, item.Symbol, item.Description, item.Rate, item.Enabled)); }
                    Datas.Ecommerce.DataSetCurrency.tbCurrencyRow currencyRow = (Datas.Ecommerce.DataSetCurrency.tbCurrencyRow)tbCurrencyDataTable.Rows[0];
                    Tools.App.Currency.SetCurrent(currencyRow.Uid, currencyRow.Idfc, currencyRow.Name, currencyRow.Symbol, currencyRow.Description, currencyRow.Rate, currencyRow.Enabled);
                    break;
            }

            return Items;

        }
    }

    /// <summary>
    /// Devise
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Unique identifiant de la devise
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Id de fonction de la devise
        /// </summary>
        public int Idfc { get; set; }
        /// <summary>
        /// Nom de la devise
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Symbole de la devise
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// Description de la devise
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Taux de la devise
        /// </summary>
        public double Rate { get; set; }
        /// <summary>
        /// Etat activé de la devise
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Currency() { return; }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="uid">Unique identifiant de la devise</param>
        /// <param name="idfc">Id de fonction de la devise</param>
        /// <param name="name">Nom de la devise</param>
        /// <param name="symbol">Symbole de la devise</param>
        /// <param name="description">Description de la devise</param>
        /// <param name="rate">Taux de la devise</param>
        /// <param name="enabled">Etat activé de la devise</param>
        public Currency(
            Guid uid
            , int idfc//arguments de fonction
            , string name
            , string symbol
            , string description
            , double rate
            , bool enabled)
        {
            Uid = uid;
            Idfc = idfc;
            Name = name;
            Symbol = symbol;
            Description = description;
            Rate = rate;
            Enabled = enabled;
        }
    }
}