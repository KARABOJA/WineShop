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
                        DateOrder = new DateTime(2017, 12, 5, 10, 24, 0),
                        DateShipped = new DateTime(2017, 12, 15, 10, 24, 0),
                        State = 0,
                        Amount = 100,
                        AmountVAT = 20,
                        AmountATI = 120,
                        Enabled = true
                    });
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandDataTable tbCustomerCommandDataTable = tbCustomerCommandTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow item in tbCustomerCommandDataTable.Rows)
                    {
                        Items.Add( new CustomerInvoice()
                        {
                            Uid = item.Uid,
                            UidCustomer = item.UidCustomer,
                            DateOrder = item.DtOrdered,
                            DateShipped = item.DtShipped,
                            State = item.State,
                            Amount = item.Amount,
                            AmountVAT = item.AmountVAT,
                            AmountATI = item.AmountATI,
                            Enabled = true
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
                            DateOrder = new DateTime(2017, 12, 5, 10, 24, 0),
                            DateShipped = new DateTime(2017, 12, 15, 10, 24, 0),
                            State = 0,
                            Amount = 100,
                            AmountVAT = 20,
                            AmountATI = 120,
                            Enabled = true
                        });
                    }
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter tbCustomerCommandTableAdapter = new Datas.Ecommerce.DataSetCustomerCommandTableAdapters.vwCustomerCommandTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandDataTable tbCustomerCommandDataTable = tbCustomerCommandTableAdapter.GetDataByUidCustomer(UidCustomer);
                    foreach (Datas.Ecommerce.DataSetCustomerCommand.vwCustomerCommandRow item in tbCustomerCommandDataTable.Rows)
                    {
                        Items.Add(new CustomerInvoice()
                        {
                            Uid = item.Uid,
                            UidCustomer = item.UidCustomer,
                            DateOrder = item.DtOrdered,
                            DateShipped = item.DtShipped,
                            State = item.State,
                            Amount = item.Amount,
                            AmountVAT = item.AmountVAT,
                            AmountATI = item.AmountATI,
                            Enabled = true
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
        public Guid UidCustomer { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateShipped { get; set; }
        public short State { get; set; }
        public double Amount { get; set; }
        public double AmountVAT { get; set; }
        public double AmountATI { get; set; }
        public bool Enabled { get; set; }
    }
}