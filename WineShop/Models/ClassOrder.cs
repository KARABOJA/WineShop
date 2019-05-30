using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models.Views
{
    public class Orders

    {
        public List<Order> Items { get; set; } = new List<Order>();

        public static List<Order> GetData()
        {
            List<Order> Items = new List<Order>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    /*Items.Add(new Order()
                    {
                        Uid = Guid.Parse("EAF3EDEE-109B-4DB0-9EB7-501A29AB8275"),
                        Enabled = true;
                    UidCustomer =
                    NumCommand =
                    InvoiceNameFirst =
                    InvoiceNameLast =
                    InvoiceEmail =
                    InvoicePhone =
                    InvoiceCompany =
                    InvoiceAddress =
                    InvoiceZipCode =
                    InvoiceCity =
                    InvoiceCountry =
                    DeliveryNameFirst =
                    DeliveryNameLast =
                    DeliveryEmail =
                    DeliveryPhone =
                    DeliveryCompany =
                    DeliveryAddress =
                    DeliveryZipCode =
                    DeliveryCity =
                    DeliveryCountry =
                    DtOrdered =
                    DtShipped =
                    State =
                    Amount =
                    AmountVAT =
                    AmountATI =
                    AmountShipping =
                    AmountATIFinal =
                    DeliveryEmail =
                });*/
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter vwCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandDataTable vwCustomerCommandDataTable = vwCustomerCommandTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow item in vwCustomerCommandDataTable.Rows)
                    {
                        Items.Add(new Order(
                           item.Uid
                           , item.Enabled
                           , item.UidCustomer
                           , item.NumCommand
                           , item.InvoiceNameFirst
                           , item.InvoiceNameLast
                           , item.InvoiceEmail
                           , item.InvoicePhone
                           , item.InvoiceCompany
                           , item.InvoiceAddress
                           , item.InvoiceZipCode
                           , item.InvoiceCity
                           , item.InvoiceCountry
                           , item.DeliveryNameFirst
                           , item.DeliveryNameLast
                           , item.DeliveryEmail
                           , item.DeliveryPhone
                           , item.DeliveryCompany
                           , item.DeliveryAddress
                           , item.DeliveryZipCode
                           , item.DeliveryCity
                           , item.DeliveryCountry
                           , item.DtOrdered
                           , item.DtShipped
                           , item.State
                           , item.Amount
                           , item.AmountVAT
                           , item.AmountATI
                           , item.AmountShipping
                           , item.AmountATIFinal
                           , item.ShippingMode
                           , item.PaymentMode));
                    }
                    break;
            }
            return Items;
        }
        public static Order GetDataByUid(Guid uid)
        {
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uid == Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"))
                    {
                        /*Items.Add(new Order()
                        {
                            Uid = Guid.Parse("EAF3EDEE-109B-4DB0-9EB7-501A29AB8275"),
                            Enabled = true;
                        UidCustomer =
                        NumCommand =
                        InvoiceNameFirst =
                        InvoiceNameLast =
                        InvoiceEmail =
                        InvoicePhone =
                        InvoiceCompany =
                        InvoiceAddress =
                        InvoiceZipCode =
                        InvoiceCity =
                        InvoiceCountry =
                        DeliveryNameFirst =
                        DeliveryNameLast =
                        DeliveryEmail =
                        DeliveryPhone =
                        DeliveryCompany =
                        DeliveryAddress =
                        DeliveryZipCode =
                        DeliveryCity =
                        DeliveryCountry =
                        DtOrdered =
                        DtShipped =
                        State =
                        Amount =
                        AmountVAT =
                        AmountATI =
                        AmountShipping =
                        AmountATIFinal =
                        DeliveryEmail =
                    });*/
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter vwCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandDataTable vwCustomerCommandDataTable = vwCustomerCommandTableAdapter.GetDataByUid(uid);
                    if (vwCustomerCommandDataTable.Rows.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow item = (Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow)vwCustomerCommandDataTable.Rows[0];
                        return new Order(
                           item.Uid
                           , item.Enabled
                           , item.UidCustomer
                           , item.NumCommand
                           , item.InvoiceNameFirst
                           , item.InvoiceNameLast
                           , item.InvoiceEmail
                           , item.InvoicePhone
                           , item.InvoiceCompany
                           , item.InvoiceAddress
                           , item.InvoiceZipCode
                           , item.InvoiceCity
                           , item.InvoiceCountry
                           , item.DeliveryNameFirst
                           , item.DeliveryNameLast
                           , item.DeliveryEmail
                           , item.DeliveryPhone
                           , item.DeliveryCompany
                           , item.DeliveryAddress
                           , item.DeliveryZipCode
                           , item.DeliveryCity
                           , item.DeliveryCountry
                           , item.DtOrdered
                           , item.DtShipped
                           , item.State
                           , item.Amount
                           , item.AmountVAT
                           , item.AmountATI
                           , item.AmountShipping
                           , item.AmountATIFinal
                           , item.ShippingMode
                           , item.PaymentMode);
                    }
                    break;
            }
            return null;
        }
    }

    public class Order
    {
        public Guid Uid { get; set; } = Guid.Empty;
        public bool Enabled { get; set; } = false;
        public Guid UidCustomer { get; set; } = Guid.Empty;
        public string NumCommand { get; set; } = null;
        public string InvoiceNameFirst { get; set; } = null;
        public string InvoiceNameLast { get; set; } = null;
        public string InvoiceEmail { get; set; } = null;
        public string InvoicePhone { get; set; } = null;
        public string InvoiceCompany { get; set; } = null;
        public string InvoiceAddress { get; set; } = null;
        public string InvoiceZipCode { get; set; } = null;
        public string InvoiceCity { get; set; } = null;
        public string InvoiceCountry { get; set; } = null;
        public string DeliveryNameFirst { get; set; } = null;
        public string DeliveryNameLast { get; set; } = null;
        public string DeliveryEmail { get; set; } = null;
        public string DeliveryPhone { get; set; } = null;
        public string DeliveryCompany { get; set; } = null;
        public string DeliveryAddress { get; set; } = null;
        public string DeliveryZipCode { get; set; } = null;
        public string DeliveryCity { get; set; } = null;
        public string DeliveryCountry { get; set; } = null;
        public DateTime DtOrdered { get; set; } = DateTime.MinValue;
        public DateTime DtShipped { get; set; } = DateTime.MinValue;
        public short State { get; set; } = 0;
        public double Amount { get; set; } = 0;
        public double AmountVAT { get; set; } = 0;
        public double AmountATI { get; set; } = 0;
        public double AmountShipping { get; set; } = 0;
        public double AmountATIFinal { get; set; } = 0;
        public byte ShippingMode { get; set; } = 0;
        public byte PaymentMode { get; set; } = 0;

        public Order() { return; }
        public Order(
            Guid uid
            , bool enabled
            , Guid uidCustomer
            , string numCommand
            , string invoiceNameFirst
            , string invoiceNameLast
            , string invoiceEmail
            , string invoicePhone
            , string invoiceCompany
            , string invoiceAddress
            , string invoiceZipCode
            , string invoiceCity
            , string invoiceCountry
            , string deliveryNameFirst
            , string deliveryNameLast
            , string deliveryEmail
            , string deliveryPhone
            , string deliveryCompany
            , string deliveryAddress
            , string deliveryZipCode
            , string deliveryCity
            , string deliveryCountry
            , DateTime dtOrdered
            , DateTime dtShipped
            , short state
            , double amount
            , double amountVAT
            , double amountATI
            , double amountShipping
            , double amountATIFinal
            , byte shippingMode
            , byte paymentMode)
        {
            Uid = uid;
            Enabled = enabled;
            NumCommand = numCommand;
            InvoiceNameFirst = invoiceNameFirst;
            InvoiceNameLast = invoiceNameLast;
            InvoiceEmail = invoiceEmail;
            InvoicePhone = invoicePhone;
            InvoiceCompany = invoiceCompany;
            InvoiceAddress = invoiceAddress;
            InvoiceZipCode = invoiceZipCode;
            InvoiceCity = invoiceCity;
            InvoiceCountry = invoiceCountry;
            DeliveryNameFirst = deliveryNameFirst;
            DeliveryNameLast = deliveryNameLast;
            DeliveryEmail = deliveryEmail;
            DeliveryPhone = deliveryPhone;
            DeliveryCompany = deliveryCompany;
            DeliveryAddress = deliveryAddress;
            DeliveryZipCode = deliveryZipCode;
            DeliveryCity = deliveryCity;
            DeliveryCountry = deliveryCountry;
            DtOrdered = dtOrdered;
            DtShipped = dtShipped;
            State = state;
            Amount = amount;
            AmountVAT = amountVAT;
            AmountATI = amountATI;
            AmountShipping = amountShipping;
            AmountATIFinal = amountATIFinal;
            ShippingMode = shippingMode;
            PaymentMode = paymentMode;
            return;
        }
    }

    public class OrderDetail
    {
        public Guid Uid { get; set; } = Guid.Empty;
        public bool Enabled { get; set; } = false;
        public Guid UidCustomerCommand { get; set; } = Guid.Empty;
        public Guid UidProduct { get; set; } = Guid.Empty;
        public Guid UidCategoryType { get; set; } = Guid.Empty;
        public Guid UidCategory { get; set; } = Guid.Empty;
        public Guid UidCategorySub { get; set; } = Guid.Empty;
        public string Name { get; set; } = null;
        public string CodeProduct { get; set; } = null;
        public string QuantityOrdered { get; set; } = null;
        public Guid UidColor { get; set; } = Guid.Empty;
        public string Color { get; set; } = null;
        public Guid UidSize { get; set; } = Guid.Empty;
        public string Size { get; set; } = null;
        public string UnitPrice { get; set; } = null;
        public string Amount { get; set; } = null;
        public string VATRate { get; set; } = null;
        public string AmountVAT { get; set; } = null;
        public string AmountATI { get; set; } = null;

        public OrderDetail() { return; }
    }
}
