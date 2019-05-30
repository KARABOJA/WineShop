using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Baskets
    {
        public static List<Basket> GetData()
        {
            if (HttpContext.Current.Session["Basket"] != null) { return (List<Basket>)HttpContext.Current.Session["Basket"]; }
            return new List<Basket>();
        }
        public static int Count
        {
            get
            {
                int iResult = 0;
                List<Basket> Items = GetData();
                foreach (Basket item in Items) { iResult += item.Quantity; }
                return iResult;
            }
        }

        public static double GetAmount()
        {
            double dResult = 0;
            foreach (Basket item in GetData()) { dResult += item.AmountTotal; }
            return dResult;
        }
        public static double GetAmountVAT()
        {
            double dResult = 0;
            foreach (Basket item in GetData()) { dResult += item.AmountVAT; }
            return dResult;
        }
        public static double GetAmountATI()
        {
            double dResult = 0;
            foreach (Basket item in GetData()) { dResult += item.AmountATI; }
            return dResult;
        }
        public static double GetAmountShipping()
        {
            if (HttpContext.Current.Session["CheckoutShipping"] != null)
            {
                switch (((Models.Views.ModelCheckoutShipping)HttpContext.Current.Session["CheckoutShipping"]).ShippingMode)
                {
                    case 0: return 22.50;
                    case 1: return 10.00;
                    case 2: return 33.85;
                }
            }
            return 0;
        }
        public static double GetAmountATIFinal()
        {
            return GetAmountATI() + GetAmountShipping();
        }

        public static bool SetData(Guid uid, string codeProduct, string name, string description, double price, Guid uidcolor, Guid uidsize,string size, string color)
        {
            try
            {
                List<Basket> Items = GetData();
                IEnumerable<Basket> itemsTmp = Items.Where(x => x.Uid == uid);
                if (itemsTmp != null && itemsTmp.Count().Equals(1))
                {
                    itemsTmp.ElementAt(0).AddQuantity(1);
                }
                else
                {
                    Models.Article atricle = Models.Articles.GetDataByCode(codeProduct);
                    Items.Add(
                        new Basket()
                        {
                            Uid = uid,
                            CodeProduct = codeProduct,
                            Name = name,
                            Description = description,
                            UidColor = uidcolor,
                            UidSize = uidsize,
                            Size = size,
                            Color = color,

                            Quantity = 1,

                            UnitPrice = price,
                            AmountTotal = price,
                            VATRate = Tools.Framework.VATRate,
                            AmountVAT = (price * Tools.Framework.VATRate),
                            AmountATI = price + (price * Tools.Framework.VATRate),

                            UidCategory= atricle.UidCategory,
                            UidCategorySub= atricle.UidCategorySub,
                            UidCategoryType= atricle.UidCategoryType,
                            UidProduct= atricle.Uid
                        }
                    );
                }
                HttpContext.Current.Session["Basket"] = Items;
                return true;
            }
            catch { return false; }
        }
        public static bool DelData(Guid uid)
        {
            try
            {
                List<Basket> Items = GetData();
                foreach(Basket item in Items)
                {
                    if (item.Uid == uid)
                    {
                        if (item.Quantity == 1) { Items.Remove(item); }
                        else { item.RemoveQuantity(1); }
                    }
                }
                HttpContext.Current.Session["Basket"] = Items;
                return true;
            }
            catch { return false; }
        }
        public static bool ClearData()
        {
            try { HttpContext.Current.Session["Basket"] = null; return true; }
            catch { return false; }
        }
    }

    public class Basket
    {
        public Guid Uid { get; set; }
        /// <summary>
        /// Récupère la valeur de SizeUid 
        /// </summary>
        public Guid UidProduct { get; set; }
        /// <summary>
        /// Récupère la valeur de SizeUid 
        /// </summary>
        public Guid UidCategoryType { get; set; }
        /// <summary>
        /// Récupère la valeur de SizeUid 
        /// </summary>
        public Guid UidCategory { get; set; }
        /// <summary>
        /// Récupère la valeur de SizeUid 
        /// </summary>
        public Guid UidCategorySub { get; set; }
        /// <summary>
        /// Récupère la valeur de SizeUid 
        /// </summary>
        public Guid UidSize { get; set; }
        /// <summary>
        /// Récupère la valeur de ColorUid
        /// </summary>
        public Guid UidColor { get; set; }

        public string CodeProduct { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Size { get; set; }
        public string Color { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }
        public double AmountTotal { get; set; }
        public double VATRate { get; set; }
        public double AmountVAT { get; set; }
        public double AmountATI { get; set; }
        public string CurrencySymbol { get; set; }
        public bool Enabled { get; set; }

        public void AddQuantity(int quantity)
        {
            Quantity = Quantity + quantity;

            AmountTotal = Quantity * UnitPrice;
            AmountVAT = (AmountTotal * VATRate);
            AmountATI = AmountTotal + AmountVAT;
        }
        public void RemoveQuantity(int quantity)
        {
            Quantity = Quantity - quantity;

            AmountTotal = Quantity * UnitPrice;
            AmountVAT = (AmountTotal * VATRate);
            AmountATI = AmountTotal + AmountVAT;
        }
    }

}