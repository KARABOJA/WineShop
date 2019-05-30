using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class CustomerAddress
    {
        public Guid Uid { get; set; }
        public Guid UidCustomer { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Country2 { get; set; }
        public string City { get; set; }
        public string City2 { get; set; }
        public string ZipCode { get; set; }
        public string ZipCode2 { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string BoolShipping { get; set; }
        public bool Enabled { get; set; }

        public static CustomerAddress GetData()
        {
            CustomerAddress customerAddress = new CustomerAddress();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    customerAddress = new CustomerAddress()
                    {
                        Uid = Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"),
                        UidCustomer = Guid.Parse("xxx"),
                        Company = "compagny",
                        Country = "country",
                        City = "city",
                        //City2 = "city2",
                        ZipCode = "zipcode",
                        //ZipCode2 = "zipcode2",
                        Address = "Address",
                        //Address2 = "Address2",
                        BoolShipping = "BoolShipping",
                        Enabled = true
                    };
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    /*Datas.Ecommerce.DataSetCustomerAddressTableAdapters.tbUserTableAdapter tbUserTableAdapter = new Datas.Ecommerce.DataSetCustomerAddressTableAdapters.tbUserTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCustomerAddress.tbUserDataTable tbUserDataTable = tbUserTableAdapter.GetData();
                    foreach (Datas.Ecommerce.CustomerAddress.tbUserRow item in tbUserDataTable.Rows)
                    {
                        customer = new CustomerAddress()
                        {
                            Uid = Guid.Parse("A450E9D6-0E27-4708-BE84-94B225AF782A"),
                            UidCustomer = Guid.Parse("xxx"),
                            Company = "compagny",
                            Country = "country",
                            City = "city",
                            //City2 = "city2",
                            ZipCode = "zipcode",
                            //ZipCode2 = "zipcode2",
                            Address = "Address",
                            //Address2 = "Address2",
                            BoolShipping = "BoolShipping",
                            Enabled = true
                        };
                    }*/
                    break;

            }
            return customerAddress;
        }
    }
}