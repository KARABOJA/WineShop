using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Languages
    {
        public static List<Language> GetData()
        {
            List<Language> Items = new List<Language>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new Language()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 1,
                        Name = "France",
                        CodeIso = "FR",
                        Description = "Français",
                        Enabled = true
                    });
                    /*Items.Add(new Language()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 2,
                        Name = "Italia",
                        CodeIso = "IT",
                        Description = "Italiano",
                        Enabled = true
                    });*/

                    Tools.App.Language.SetCurrent(Guid.NewGuid(), 1, "France", "Français", "FR", "fr-FR", true);
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetLanguageTableAdapters.tbLanguageTableAdapter tbLanguageTableAdapter = new Datas.Ecommerce.DataSetLanguageTableAdapters.tbLanguageTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetLanguage.tbLanguageDataTable tbLanguageDataTable = tbLanguageTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetLanguage.tbLanguageRow item in tbLanguageDataTable.Rows) { Items.Add(new Language(item.Uid, item.Idfc, item.Name, item.Description, item.CodeIso, item.CultureInfo, item.Enabled)); }
                    Datas.Ecommerce.DataSetLanguage.tbLanguageRow languageRow = (Datas.Ecommerce.DataSetLanguage.tbLanguageRow)tbLanguageDataTable.Rows[0];
                    Tools.App.Language.SetCurrent(languageRow.Uid, languageRow.Idfc, languageRow.Name, languageRow.Description, languageRow.CodeIso, languageRow.CultureInfo, languageRow.Enabled);
                    break;
            }

            return Items;
        }
    }

    public class Language
    {
        public Guid Uid { get; set; }
        /// <summary>
        /// Id de fonction de la devise
        /// </summary>
        public int Idfc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CodeIso { get; set; }
        public string CultureInfo { get; set; }
        public bool Enabled { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Language() { return; }

        public Language(
            Guid uid
            , int idfc
            , string name
            , string description
            , string codeIso
            , string cultureInfo
            , bool enabled)
        {
            Uid = uid;
            Idfc = idfc;
            Name = name;
            Description = description;
            CodeIso = codeIso;
            CultureInfo = cultureInfo;
            Enabled = enabled;
        }


    }
}