using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class CustomerInvoices
    {
        public static List<CustomerInvoice> GetData()
        {
            List<CustomerInvoice> Items = new List<CustomerInvoice>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new CustomerInvoice()
                    {
                        Uid = Guid.Parse("0F5F3F50-5343-4979-9C91-1E834267FA2B"),
                        UidCustomer = Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"),
                        DtOrdered = new DateTime(2017, 12, 5, 10, 24, 0),
                        DtShipped = new DateTime(2017, 12, 15, 10, 24, 0),
                        State = 0,
                        Amount = 100,
                        AmountVAT = 20,
                        AmountATI = 120,
                        Enabled = true
                    });
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.vwCustomerInvoiceTableAdapter vwCustomerInvoiceTableAdapter = new Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.vwCustomerInvoiceTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceDataTable vwCustomerInvoiceDataTable = vwCustomerInvoiceTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceRow item in vwCustomerInvoiceDataTable.Rows)
                    {
                        Items.Add(new CustomerInvoice()
                        {
                            Uid = item.Uid,
                            UidCustomer = item.UidCustomer,
                            NumInvoice = item.NumInvoice,
                            NumCommand = item.NumCommand,
                            DtOrdered = item.DtOrdered,
                            DtShipped = item.DtShipped,
                            State = item.State,
                            Amount = item.Amount,
                            AmountVAT = item.AmountVAT,
                            AmountATI = item.AmountATI,
                            AmountShipping = item.AmountShipping,
                            AmountATIFinal = item.AmountATIFinal,
                            CurrenctSymbole = item.CurrenctSymbole,
                            CurrencyDescription = item.CurrencyDescription,
                            Enabled = true,
                            StateDescription = item.StateDescription
                        });
                    }
                    break;

            }
            return Items;
        }
        public static List<CustomerInvoice> GetDataByUidCustomer(Guid UidCustomer)
        {
            List<CustomerInvoice> Items = new List<CustomerInvoice>();
            switch (Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (UidCustomer == Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"))
                    {
                        Items.Add(new CustomerInvoice()
                        {
                            Uid = Guid.Parse("0F5F3F50-5343-4979-9C91-1E834267FA2B"),
                            UidCustomer = Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"),
                            DtOrdered = new DateTime(2017, 12, 5, 10, 24, 0),
                            DtShipped = new DateTime(2017, 12, 15, 10, 24, 0),
                            State = 0,
                            Amount = 100,
                            AmountVAT = 20,
                            AmountATI = 120,
                            Enabled = true
                        });
                    }
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.vwCustomerInvoiceTableAdapter vwCustomerInvoiceTableAdapter = new Datas.Ecommerce.DataSetCustomerInvoiceTableAdapters.vwCustomerInvoiceTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceDataTable vwCustomerInvoiceDataTable = vwCustomerInvoiceTableAdapter.GetDataByUidCustomer(UidCustomer);
                    foreach (Datas.Ecommerce.DataSetCustomerInvoice.vwCustomerInvoiceRow item in vwCustomerInvoiceDataTable.Rows)
                    {
                        Items.Add(new CustomerInvoice()
                        {
                            Uid = item.Uid,
                            UidCustomer = item.UidCustomer,
                            NumInvoice = item.NumInvoice,
                            NumCommand = item.NumCommand,
                            DtOrdered = item.DtOrdered,
                            DtShipped = item.DtShipped,
                            State = item.State,
                            Amount = item.Amount,
                            AmountVAT = item.AmountVAT,
                            AmountATI = item.AmountATI,
                            AmountShipping = item.AmountShipping,
                            AmountATIFinal = item.AmountATIFinal,
                            CurrenctSymbole = item.CurrenctSymbole,
                            CurrencyDescription = item.CurrencyDescription,
                            Enabled = true,
                            StateDescription = item.StateDescription
                        });
                    }
                    break;

            }
            return Items;
        }
    }

    public class CustomerInvoice
    {
        public Guid Uid { get; set; }
        public Guid UidUserCreate { get; set; }
        public Guid UidUserUpdate { get; set; }
        public DateTime DtCreate { get; set; }
        public DateTime DtUpdate { get; set; }
        public bool Enabled { get; set; }
        public Guid UidCustomer { get; set; }
        public string NumInvoice { get; set; }
        public string NumCommand { get; set; }
        public string InvoiceNameFirst { get; set; }
        public string InvoiceNameLast { get; set; }
        public string InvoiceEmail { get; set; }
        public string InvoicePhone { get; set; }
        public string InvoiceCompany { get; set; }
        public string InvoiceAddress { get; set; }
        public string InvoiceZipCode { get; set; }
        public string InvoiceCity { get; set; }
        public string InvoiceCountry { get; set; }
        public string DeliveryNameFirst { get; set; }
        public string DeliveryNameLast { get; set; }
        public string DeliveryEmail { get; set; }
        public string DeliveryPhone { get; set; }
        public string DeliveryCompany { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryZipCode { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCountry { get; set; }
        public DateTime DtOrdered { get; set; }
        public DateTime DtShipped { get; set; }
        public short State { get; set; }
        public double Amount { get; set; }
        public double AmountVAT { get; set; }
        public double AmountATI { get; set; }
        public double AmountShipping { get; set; }
        public double AmountATIFinal { get; set; }
        public Guid UidCurrency { get; set; }
        public string CurrenctSymbole { get; set; }
        public string CurrencyDescription { get; set; }
        public string CurrencyRate { get; set; }
        public byte ShippingMode { get; set; }
        public byte PaymentMode { get; set; }
        public string StateDescription { get; set; }
    }
}