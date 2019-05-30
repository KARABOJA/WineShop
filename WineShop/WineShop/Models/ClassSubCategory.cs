using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class SubCategories
    {
        public static List<SubCategory> GetData()
        {
            List<SubCategory> Items = new List<SubCategory>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new SubCategory(Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"), Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), 0, "Margaux", "Margaux", true));
                    Items.Add(new SubCategory(Guid.Parse("39C4048C-C4B0-4891-A393-AF9D76E95F74"), Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), 1, "Saint-Estèphe", "Saint-Estèphe", true));
                    Items.Add(new SubCategory(Guid.Parse("0FA0D4E2-233E-4FD6-8B85-498920A3DF99"), Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), 2, "Haut-Médoc", "Haut-Médoc", true));
                    Items.Add(new SubCategory(Guid.Parse("F4409BCD-B167-4D19-A677-F870D475AD1C"), Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), 3, "Pauillac", "Pauillac", true));
                    Items.Add(new SubCategory(Guid.Parse("934575E4-9F46-4093-BD2F-C69F7434E4FF"), Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), 4, "Saint-Julien", "Saint-Julien", true));
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategorySubTableAdapter tbCategorySubTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategorySubTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategorySubDataTable tbCategorySubDataTable = tbCategorySubTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetCategory.tbCategorySubRow item in tbCategorySubDataTable.Rows)
                    {
                        Items.Add(new SubCategory(item.Uid, item.UidCategory, item.Id, item.Name, Tools.App.Translate(item.Description,item.Uid), item.Enabled));
                    }
                    break;
            }

            return Items;
        }
        public static SubCategory GetDataByName(string name)
        {
            SubCategory Items = null;

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (name == "Margaux")
                    {
                        return new SubCategory(Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"), Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"), 0, "Margaux", "Margaux", true);
                    }
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategorySubTableAdapter tbCategorySubTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategorySubTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategorySubDataTable tbCategorySubDataTable = tbCategorySubTableAdapter.GetDataByName(name);
                    if (tbCategorySubDataTable.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetCategory.tbCategorySubRow item = (Datas.Ecommerce.DataSetCategory.tbCategorySubRow)tbCategorySubDataTable.Rows[0];
                        Items = new SubCategory(item.Uid, item.UidCategory, item.Id, item.Name, Tools.App.Translate(item.Description, item.Uid), item.Enabled);
                    }
                    break;
            }

            return Items;
        }
        public static List<SubCategory> GetDataByUidCategory(Guid uidCategory)
        {
            List<SubCategory> Items = new List<SubCategory>();

            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uidCategory == Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"))
                    {
                        Items.Add(new SubCategory(Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"), uidCategory, 0, "Margaux", "Margaux", true));
                        Items.Add(new SubCategory(Guid.Parse("39C4048C-C4B0-4891-A393-AF9D76E95F74"), uidCategory, 1, "Saint-Estèphe", "Saint-Estèphe", true));
                        Items.Add(new SubCategory(Guid.Parse("0FA0D4E2-233E-4FD6-8B85-498920A3DF99"), uidCategory, 2, "Haut-Médoc", "Haut-Médoc", true));
                        Items.Add(new SubCategory(Guid.Parse("F4409BCD-B167-4D19-A677-F870D475AD1C"), uidCategory, 3, "Pauillac", "Pauillac", true));
                        Items.Add(new SubCategory(Guid.Parse("934575E4-9F46-4093-BD2F-C69F7434E4FF"), uidCategory, 4, "Saint-Julien", "Saint-Julien", true));
                    }
                    break;
                case Enums.Environment.Production:
                    Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategorySubTableAdapter tbCategorySubTableAdapter = new Datas.Ecommerce.DataSetCategoryTableAdapters.tbCategorySubTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetCategory.tbCategorySubDataTable tbCategorySubDataTable = tbCategorySubTableAdapter.GetDataByUidCategory(uidCategory);
                    foreach (Datas.Ecommerce.DataSetCategory.tbCategorySubRow item in tbCategorySubDataTable.Rows)
                    {
                        Items.Add(new SubCategory(item.Uid, item.UidCategory, item.Id, item.Name, Tools.App.Translate(item.Description,item.Uid), item.Enabled));
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
                    if (uid == Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E")) { iResult = 3; }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter adapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable dataTable = adapter.GetDataByUidCategorySub(uid);
                    iResult = dataTable.Count;
                    break;
            }

            return iResult;
        }
    }
    public class SubCategory
    {
        /// <summary>
        /// Unique identifiant
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Unique identifiant
        /// </summary>
        public Guid UidCategory { get; set; }
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

        public SubCategory()
        {
            return;
        }
        public SubCategory(Guid uid, Guid uidCategory, int id, string name, string description, bool enabled)
        {
            Uid = uid;
            UidCategory = uidCategory;
            Id = id;
            Name = name;
            Description = description;
            Enabled = enabled;
            return;
        }
    }
}