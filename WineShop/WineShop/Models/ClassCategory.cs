using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Categories
    {
        public static List<Category> GetData()
        {
            List<Category> Items = new List<Category>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new Category(Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),0, "Dry", "Sec", true));
                    Items.Add(new Category(Guid.Parse("49EF2C19-6AC0-4337-B1CF-650B23633A7F"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),1, "Soft", "Doux", true));
                    Items.Add(new Category(Guid.Parse("AC8BB929-3FED-4381-9162-316591AE49E3"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),2, "Fruit", "Fruité", true));
                    Items.Add(new Category(Guid.Parse("AEBE3988-4A18-40B4-B280-524E70A0B3C8"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),3, "AOC", "AOC", true));
                    Items.Add(new Category(Guid.Parse("AFECB2B1-025A-4855-8C7C-02DBACF1B7D0"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),4, "Producteur", "Producteur", true));
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTableAdapter tbCategoryTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategoryDataTable tbCategoryDataTable = tbCategoryTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCategory.tbCategorySubRow item in tbCategoryDataTable.Rows)
                    {
                        Items.Add(new Category(item.Uid, item.UidCategory, item.Id, item.Name, Tools.App.Translate(item.Description,item.Uid), item.Enabled));
                    }
                    break;
            }

            return Items;
        }
        public static List<Category> GetDataByUidTypeCategory(Guid uidTypeCategory)
        {
            List<Category> Items = new List<Category>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uidTypeCategory == Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"))
                    {
                        Items.Add(new Category(Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), Guid.Empty,0, "Dry", "Sec", true));
                        Items.Add(new Category(Guid.Parse("49EF2C19-6AC0-4337-B1CF-650B23633A7F"), Guid.Empty,1, "Soft", "Doux", true));
                        Items.Add(new Category(Guid.Parse("AC8BB929-3FED-4381-9162-316591AE49E3"), Guid.Empty,2, "Fruit", "Fruité", true));
                        Items.Add(new Category(Guid.Parse("AEBE3988-4A18-40B4-B280-524E70A0B3C8"), Guid.Empty,3, "AOC", "AOC", true));
                        Items.Add(new Category(Guid.Parse("AFECB2B1-025A-4855-8C7C-02DBACF1B7D0"), Guid.Empty,4, "Producteur", "Producteur", true));
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTableAdapter tbCategoryTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategoryDataTable tbCategoryDataTable = tbCategoryTableAdapter.GetDataByUidCategoryType(uidTypeCategory);
                    foreach (Datas.Ecommerce.DataSetCategory.tbCategoryRow item in tbCategoryDataTable.Rows)
                    {
                        Items.Add(new Category(item.Uid, item.UidCategoryType, item.Id, item.Name,Tools.App.Translate( item.Description,item.Uid), item.Enabled));
                    }
                        break;
            }

            return Items;
        }
        public static List<Category> GetDataTop()
        {
            List<Category> Items = new List<Category>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new Category(Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 0,"Dry", "Sec", true));
                    Items.Add(new Category(Guid.Parse("49EF2C19-6AC0-4337-B1CF-650B23633A7F"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 1,"Soft", "Doux", true));
                    Items.Add(new Category(Guid.Parse("AC8BB929-3FED-4381-9162-316591AE49E3"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 2,"Fruit", "Fruité", true));
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCategoryTableAdapters.vwCategoryTopTableAdapter tbCategoryTopTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.vwCategoryTopTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.vwCategoryTopDataTable tbCategoryTopDataTable = tbCategoryTopTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCategory.vwCategoryTopRow item in tbCategoryTopDataTable.Rows)
                    {
                        Items.Add(new Category(item.Uid, item.UidCategoryType, item.Id, item.Name, Tools.App.Translate(item.Description,item.Uid), item.Enabled));
                    }
                    break;
            }

            return Items;
        }
        public static Category GetDataByName(string name)
        {
            Category Items = null;

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (name == "Dry")
                    {
                        return new Category(Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 0, "Dry", "Sec", true);
                    }
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTableAdapter tbCategoryTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategoryDataTable tbCategoryDataTable = tbCategoryTableAdapter.GetDataByName(name);
                    if (tbCategoryDataTable.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetCategory.tbCategoryRow item = (Datas.Ecommerce.DataSetCategory.tbCategoryRow)tbCategoryDataTable.Rows[0];
                        Items = new Category(item.Uid, item.UidCategoryType, item.Id, item.Name, Tools.App.Translate(item.Description, item.Uid), item.Enabled);
                    }
                    break;
            }

            return Items;
        }

        public static int CountArticlesByUid(Guid uid)
        {
            int iResult = 0;

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uid == Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706")) { iResult = 3; }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter adapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable dataTable = adapter.GetDataByUidCategory(uid);
                    iResult = dataTable.Count;
                    break;
            }

            return iResult;
        }
    }

    public class Category
    {
        /// <summary>
        /// Unique identifiant
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Unique identifiant
        /// </summary>
        public Guid UidTypeCategory { get; set; }
        /// <summary>
        /// Unique identifiant
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nom des types
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description des types
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Obtient ou définit l'activation de TypeCategory
        /// </summary>
        public bool Enabled { get; set; }

        public Category()
        {
            return;
        }
        public Category(Guid uid, Guid uidTypeCategory, int id, string name, string description, bool enabled)
        {
            Uid = uid;
            UidTypeCategory = uidTypeCategory;
            Id = id;
            Name = name;
            Description = description;
            Enabled = enabled;
            return;
        }
    }
}