using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class TypeCategories
    {
        public static List<TypeCategory> GetData()
        {
            List<TypeCategory> Items = new List<TypeCategory>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new TypeCategory(Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 0, "Wine", "Vin", true));
                    Items.Add(new TypeCategory(Guid.Parse("2FFAB5FC-39D3-4A3C-A687-DA6F57BB1D04"), 1, "Champagne", "Champagne", true));
                    Items.Add(new TypeCategory(Guid.Parse("1BD44720-89C5-4377-B020-1E69DC2291DC"), 2, "Sipirtueux", "Spirtueux", true));
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTypeTableAdapter tbCategoryTypeTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTypeTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategoryTypeDataTable tbCategoryTypeDataTable = tbCategoryTypeTableAdapter.GetData();
                    foreach(Datas.Ecommerce.DataSetCategory.tbCategoryTypeRow item in tbCategoryTypeDataTable.Rows)
                    {
                        Items.Add(new TypeCategory(item.Uid, item.Id, item.Name, Tools.App.Translate(item.Description,item.Uid), item.Enabled));
                    }
                    break;
            }

            return Items;
        }
        public static TypeCategory GetDataByUid(Guid uid)
        {
            TypeCategory Items = new TypeCategory();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uid == Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C")) { Items = new TypeCategory(Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 0, "Wine", "Vin", true); }
                    if (uid == Guid.Parse("2FFAB5FC-39D3-4A3C-A687-DA6F57BB1D04")) { Items = new TypeCategory(Guid.Parse("2FFAB5FC-39D3-4A3C-A687-DA6F57BB1D04"), 1, "Champagne", "Champagne", true); }
                    if (uid == Guid.Parse("1BD44720-89C5-4377-B020-1E69DC2291DC")) { Items = new TypeCategory(Guid.Parse("1BD44720-89C5-4377-B020-1E69DC2291DC"), 2, "Sipirtueux", "Spirtueux", true); }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTypeTableAdapter tbCategoryTypeTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTypeTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategoryTypeDataTable tbCategoryTypeDataTable = tbCategoryTypeTableAdapter.GetDataByUid(uid);
                    if (tbCategoryTypeDataTable.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetCategory.tbCategoryTypeRow item = (Datas.Ecommerce.DataSetCategory.tbCategoryTypeRow)tbCategoryTypeDataTable.Rows[0];
                        Items = new TypeCategory(item.Uid, item.Id, item.Name, Tools.App.Translate(item.Description,item.Uid), item.Enabled);
                    }
                    break;
            }

            return Items;
        }
        public static TypeCategory GetDataByName(string name)
        {
            TypeCategory Items = null;

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (name == "Wine")
                    {
                        return new TypeCategory(Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"), 0, "Wine", "Vin", true);
                    }
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTypeTableAdapter tbCategoryTypeTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategoryTypeTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategoryTypeDataTable tbCategoryTypeDataTable = tbCategoryTypeTableAdapter.GetDataByName(name);
                    if (tbCategoryTypeDataTable.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetCategory.tbCategoryTypeRow item = (Datas.Ecommerce.DataSetCategory.tbCategoryTypeRow)tbCategoryTypeDataTable.Rows[0];
                        Items = new TypeCategory(item.Uid, item.Id, item.Name, Tools.App.Translate(item.Description, item.Uid), item.Enabled);
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
                    if (uid == Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C")) { iResult = 3; }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter adapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable dataTable = adapter.GetDataByUidCategoryType(uid);
                    iResult = dataTable.Count;
                    break;
            }

            return iResult;
        }
    }

    /// <summary>
    /// type de produit
    /// </summary>
    public class TypeCategory
    {
        /// <summary>
        /// Unique identifiant
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Identifiant
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

        public TypeCategory()
        {
            return;
        }
        public TypeCategory(Guid uid, int id,string name,string description, bool enabled)
        {
            Uid = uid;
            Id = id;
            Name = name;
            Description = description;
            Enabled = enabled;
            return;
        }
    }
}