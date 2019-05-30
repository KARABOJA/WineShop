using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Articles
    {
        public List<Article> Items { get; set; } = new List<Article>();

        public static List<Article> GetDataByUidCategorySub(Guid uidCategorySub)
        {
            List<Article> Items = new List<Article>();

            switch (Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uidCategorySub == Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"))
                    {
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("0EA0868E-8D55-451F-82DF-107FA2A4F70E"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("143907C1-AC18-4B06-9715-CA467E5FF403"),
                            UidColor = Guid.Parse("69398FF3-7591-4C1A-B431-C484A22CA174"),

                            CodeProduct = "01",
                            Name = "MontonCadet",
                            Description = "Monton Cadet",
                            UnitPrice = 10.32d,
                            CurrencySymbol = "€",
                            Notes = 4d,

                            Size = "37,5 Cl",
                            Color = "Rouge",
                        });
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("9E074601-0885-478D-A320-4C28A629C2C4"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("F98252C5-72A8-450B-99F9-EE8EECD8E9C3"),

                            CodeProduct = "02",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Blanc",
                        });
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("78216DDD-F76F-4F56-8F12-616C34A73AD7"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("CCE49E95-4E45-4564-A834-F091E043C88C"),

                            CodeProduct = "03",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Rosé",
                        });
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter vwArticleTableAdapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable vwArticleDataTable = vwArticleTableAdapter.GetDataByUidCategorySub(uidCategorySub);
                    foreach (Datas.Ecommerce.DataSetArticle.vwArticleRow item in vwArticleDataTable.Rows)
                    {
                        Items.Add(new Article(
                            item.Uid
                            , item.UidCategoryType
                            , item.UidCategory
                            , item.UidCategorySub
                            , item.UidColor
                            , item.ColorDescription
                            , item.UidSize
                            , item.SizeDescription
                            , item.UidCurrency
                            , item.Description
                            , item.Notes
                            , item.Name
                            , item.UnitPrice
                            , item.Images
                            , item.Favorit
                            , item.CurrencyDescription
                            , item.CurrencySymbol
                            , item.CodeProduct
                            , item.Enabled));
                    }
                    break;
            }

            return Items;
        }
        public static List<Article> GetDataByUidCategory(Guid uidCategory)
        {
            List<Article> Items = new List<Article>();

            switch (Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uidCategory == Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"))
                    {
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("0EA0868E-8D55-451F-82DF-107FA2A4F70E"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("143907C1-AC18-4B06-9715-CA467E5FF403"),
                            UidColor = Guid.Parse("69398FF3-7591-4C1A-B431-C484A22CA174"),

                            CodeProduct = "01",
                            Name = "MontonCadet",
                            Description = "Monton Cadet",
                            UnitPrice = 10.32d,
                            CurrencySymbol = "€",
                            Notes = 4d,

                            Size = "37,5 Cl",
                            Color = "Rouge",
                        });
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("9E074601-0885-478D-A320-4C28A629C2C4"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("F98252C5-72A8-450B-99F9-EE8EECD8E9C3"),

                            CodeProduct = "02",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Blanc",
                        });
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("78216DDD-F76F-4F56-8F12-616C34A73AD7"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("CCE49E95-4E45-4564-A834-F091E043C88C"),

                            CodeProduct = "03",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Rosé",
                        });
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter vwArticleTableAdapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable vwArticleDataTable = vwArticleTableAdapter.GetDataByUidCategory(uidCategory);
                    foreach (Datas.Ecommerce.DataSetArticle.vwArticleRow item in vwArticleDataTable.Rows)
                    {
                        Items.Add(new Article(
                            item.Uid
                            , item.UidCategoryType
                            , item.UidCategory
                            , item.UidCategorySub
                            , item.UidColor
                            , item.ColorDescription
                            , item.UidSize
                            , item.SizeDescription
                            , item.UidCurrency
                            , item.Description
                            , item.Notes
                            , item.Name
                            , item.UnitPrice
                            , item.Images
                            , item.Favorit
                            , item.CurrencyDescription
                            , item.CurrencySymbol
                            , item.CodeProduct
                            , item.Enabled));
                    }
                    break;
            }

            return Items;
        }
        public static List<Article> GetDataByUidCategoryType(Guid uidCategoryType)
        {
            List<Article> Items = new List<Article>();

            switch (Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uidCategoryType == Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"))
                    {
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("0EA0868E-8D55-451F-82DF-107FA2A4F70E"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("143907C1-AC18-4B06-9715-CA467E5FF403"),
                            UidColor = Guid.Parse("69398FF3-7591-4C1A-B431-C484A22CA174"),

                            CodeProduct = "01",
                            Name = "MontonCadet",
                            Description = "Monton Cadet",
                            UnitPrice = 10.32d,
                            CurrencySymbol = "€",
                            Notes = 4d,

                            Size = "37,5 Cl",
                            Color = "Rouge",
                        });
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("9E074601-0885-478D-A320-4C28A629C2C4"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("F98252C5-72A8-450B-99F9-EE8EECD8E9C3"),

                            CodeProduct = "02",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Blanc",
                        });
                        Items.Add(new Article()
                        {
                            Uid = Guid.Parse("78216DDD-F76F-4F56-8F12-616C34A73AD7"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("CCE49E95-4E45-4564-A834-F091E043C88C"),

                            CodeProduct = "03",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Rosé",
                        });
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter vwArticleTableAdapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable vwArticleDataTable = vwArticleTableAdapter.GetDataByUidCategoryType(uidCategoryType);
                    foreach (Datas.Ecommerce.DataSetArticle.vwArticleRow item in vwArticleDataTable.Rows)
                    {
                        Items.Add(new Article(
                            item.Uid
                            , item.UidCategoryType
                            , item.UidCategory
                            , item.UidCategorySub
                            , item.UidColor
                            , item.ColorDescription
                            , item.UidSize
                            , item.SizeDescription
                            , item.UidCurrency
                            , item.Description
                            , item.Notes
                            , item.Name
                            , item.UnitPrice
                            , item.Images
                            , item.Favorit
                            , item.CurrencyDescription
                            , item.CurrencySymbol
                            , item.CodeProduct
                            , item.Enabled));
                    }
                    break;
            }

            return Items;
        }

        public void CreateTestSuggestion()
        {
            Items = new List<Article>();
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Monton Cadet",
                Description = "Bon vin de table",
                UnitPrice = 10.32d,
                CurrencySymbol = "€",
                Notes = 4d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Monchaty",
                Description = "Bon vin de table",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
        }
        public void CreateTestPopular()
        {
            Items = new List<Article>();
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 10.32d,
                CurrencySymbol = "€",
                Notes = 4d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Monchaty",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
            Items.Add(new Article()
            {
                Uid = Guid.Empty,
                Name = "Chateau Peynaud-Bagnac",
                Description = "Bon vin de tabl",
                UnitPrice = 100.32d,
                CurrencySymbol = "€",
                Notes = 5d
            });
        }

        public static Article GetDataByUid(Guid uid)
        {
            Article article = null;

            switch (Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (uid == Guid.Parse("0EA0868E-8D55-451F-82DF-107FA2A4F70E"))
                    {
                        return new Article()
                        {
                            Uid = Guid.Parse("0EA0868E-8D55-451F-82DF-107FA2A4F70E"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("143907C1-AC18-4B06-9715-CA467E5FF403"),
                            UidColor = Guid.Parse("69398FF3-7591-4C1A-B431-C484A22CA174"),

                            CodeProduct = "01",
                            Name = "MontonCadet",
                            Description = "Monton Cadet",
                            UnitPrice = 10.32d,
                            CurrencySymbol = "€",
                            Notes = 4d,

                            Size = "37,5 Cl",
                            Color = "Rouge",
                        };
                    }
                    else if (uid == Guid.Parse("9E074601-0885-478D-A320-4C28A629C2C4"))
                    {
                        return new Article()
                        {
                            Uid = Guid.Parse("9E074601-0885-478D-A320-4C28A629C2C4"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("F98252C5-72A8-450B-99F9-EE8EECD8E9C3"),

                            CodeProduct = "02",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Blanc",
                        };
                    }
                    else if (uid == Guid.Parse("78216DDD-F76F-4F56-8F12-616C34A73AD7"))
                    {
                        return new Article()
                        {
                            Uid = Guid.Parse("78216DDD-F76F-4F56-8F12-616C34A73AD7"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("CCE49E95-4E45-4564-A834-F091E043C88C"),

                            CodeProduct = "03",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Rosé",
                        };
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter vwArticleTableAdapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable vwArticleDataTable = vwArticleTableAdapter.GetDataByUid(uid);
                    if (vwArticleDataTable.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetArticle.vwArticleRow item = (Datas.Ecommerce.DataSetArticle.vwArticleRow)vwArticleDataTable.Rows[0];
                        article = new Article(
                            item.Uid
                            , item.UidCategoryType
                            , item.UidCategory
                            , item.UidCategorySub
                            , item.UidColor
                            , item.ColorDescription
                            , item.UidSize
                            , item.SizeDescription
                            , item.UidCurrency
                            , item.Description
                            , item.Notes
                            , item.Name
                            , item.UnitPrice
                            , item.Images
                            , item.Favorit
                            , item.CurrencyDescription
                            , item.CurrencySymbol
                            , item.CodeProduct
                            , item.Enabled);
                    }
                    break;
            }

            return article;
        }
        public static Article GetDataByCode(string codeProduct)
        {
            Article article = null;

            switch (Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    if (codeProduct == "01")
                    {
                        return new Article()
                        {
                            Uid = Guid.Parse("0EA0868E-8D55-451F-82DF-107FA2A4F70E"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("143907C1-AC18-4B06-9715-CA467E5FF403"),
                            UidColor = Guid.Parse("69398FF3-7591-4C1A-B431-C484A22CA174"),

                            CodeProduct = "01",
                            Name = "MontonCadet",
                            Description = "Monton Cadet",
                            UnitPrice = 10.32d,
                            CurrencySymbol = "€",
                            Notes = 4d,

                            Size = "37,5 Cl",
                            Color = "Rouge",
                        };
                    }
                    else if (codeProduct == "02")
                    {
                        return new Article()
                        {
                            Uid = Guid.Parse("9E074601-0885-478D-A320-4C28A629C2C4"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("F98252C5-72A8-450B-99F9-EE8EECD8E9C3"),

                            CodeProduct = "02",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Blanc",
                        };
                    }
                    else if (codeProduct == "03")
                    {
                        return new Article()
                        {
                            Uid = Guid.Parse("78216DDD-F76F-4F56-8F12-616C34A73AD7"),

                            UidCategorySub = Guid.Parse("34695A0B-8846-4D41-B07D-DA46556D932E"),
                            UidCategory = Guid.Parse("659F9BFF-C9A5-47CE-A7A5-1401ACDC9706"),
                            UidCategoryType = Guid.Parse("9B13C02D-385D-4CB2-AF8C-52390E8D287C"),

                            UidSize = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                            UidColor = Guid.Parse("CCE49E95-4E45-4564-A834-F091E043C88C"),

                            CodeProduct = "03",
                            Name = "Minuty",
                            Description = "Château Minuty",
                            UnitPrice = 100.32d,
                            CurrencySymbol = "€",
                            Notes = 5d,

                            Size = "50 Cl",
                            Color = "Rosé",
                        };
                    }
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter vwArticleTableAdapter = new Datas.Ecommerce.DataSetArticleTableAdapters.vwArticleTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetArticle.vwArticleDataTable vwArticleDataTable = vwArticleTableAdapter.GetDataByCodeProduct(codeProduct);
                    if (vwArticleDataTable.Count.Equals(1))
                    {
                        Datas.Ecommerce.DataSetArticle.vwArticleRow item = (Datas.Ecommerce.DataSetArticle.vwArticleRow)vwArticleDataTable.Rows[0];
                        article = new Article(
                            item.Uid
                            , item.UidCategoryType
                            , item.UidCategory
                            , item.UidCategorySub
                            , item.UidColor
                            , item.ColorDescription
                            , item.UidSize
                            , item.SizeDescription
                            , item.UidCurrency
                            , item.Description
                            , item.Notes
                            , item.Name
                            , item.UnitPrice
                            , item.Images
                            , item.Favorit
                            , item.CurrencyDescription
                            , item.CurrencySymbol
                            , item.CodeProduct
                            , item.Enabled);
                    }
                    break;
            }

            return article;
        }
    }

    /// <summary>
    ///  Description complète de la classe Article. 
    /// </summary>
    public class Article
    {  /// </summary>
       ///Récupère la valeur de Uid 
       /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Récupère la valeur de TypeCategorieUid 
        /// </summary>
        public Guid UidCategoryType { get; set; }
        /// <summary>
        /// Récupère la valeur de CategorieUid 
        /// </summary>
        public Guid UidCategory { get; set; }
        /// <summary>
        /// Récupère la valeur de SubCategorieUid 
        /// </summary>
        public Guid UidCategorySub { get; set; }
        /// <summary>
        /// Récupère la valeur de ColorUid
        /// </summary>
        public Guid UidColor { get; set; }
        /// <summary>
        /// Récupère la valeur de SizeUid 
        /// </summary>
        public Guid UidSize { get; set; }
        /// <summary>
        /// Récupère la valeur de CurrencyUid
        /// </summary>
        public Guid UidCurrency { get; set; }
        /// <summary>
        /// Récupère la valeur de Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Récupère la valeur de Notes
        /// </summary>
        public double Notes { get; set; }
        /// <summary>
        /// Récupère la valeur de Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Récupère la valeur de UnitPrice
        /// </summary>
        public double UnitPrice { get; set; }
        /// <summary>
        /// Récupère la valeur de UnitPrice
        /// </summary>
        public double UnitPriceATI
        {
            get
            {
                return UnitPrice + (UnitPrice * Tools.Framework.VATRate);
            }
        }
        /// <summary>
        /// Récupère la valeur de Size
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// Récupère la valeur de Color
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// Récupère la valeur de Images
        /// </summary>
        public string Images { get; set; }
        /// <summary>
        /// Récupère la valeur de Favorit
        /// </summary>
        public bool Favorit { get; set; }
        /// <summary>
        /// Récupère la valeur de CurrencyDescription
        /// </summary>
        public string CurrencyDescription { get; set; }
        /// <summary>
        /// Récupère la valeur de CurrencySymbol
        /// </summary>
        public string CurrencySymbol { get; set; }
        /// <summary>
        /// Récupère la valeur de CodeProduct
        /// </summary>
        public string CodeProduct { get; set; }
        /// <summary>
        /// Récupère la valeur de Enabled
        /// </summary>
        public bool Enabled { get; set; }

        public Article() { return; }

        public Article(
            Guid uid
            , Guid uidCategoryType
            , Guid uidCategory
            , Guid uidSubCategory
            , Guid uidColor
            , string color
            , Guid uidSize
            , string size
            , Guid uidCurrency
            , string description
            , double notes
            , string name
            , double unitPrice
            , string images
            , bool favorit
            , string currencyDescription
            , string currencySymbol
            , string codeProduct
            , bool enabled)
        {
            Uid = uid;
            UidCategoryType = uidCategoryType;
            UidCategory = uidCategory;
            UidCategorySub = uidSubCategory;
            UidColor = uidColor;
            Color = color;
            UidSize = uidSize;
            Size = size;
            UidCurrency = uidCurrency;
            Description = description;
            Notes = notes;
            Name = name;
            UnitPrice = unitPrice;
            Images = images;
            Favorit = favorit;
            CurrencyDescription = currencyDescription;
            CurrencySymbol = currencySymbol;
            CodeProduct = codeProduct;
            Enabled = enabled;
            return;
        }
    }
}


