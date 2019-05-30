using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SocialsNetworks
    {
        /// <summary>
        /// Obtient ou defini les
        /// </summary>
        public static List<SocialNetwork> GetData()
        {
            List<SocialNetwork> Items = new List<SocialNetwork>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new SocialNetwork()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 0,
                        Name = "Facebook",
                        Symbol = "facebook",
                        Url = "https://www.facebook.com/",
                        Script = "",
                        UseScript = false,
                        Enabled = true
                    });
                    Items.Add(new SocialNetwork()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 1,
                        Name = "Twitter",
                        Symbol = "twitter",
                        Url = "https://www.twitter.com/",
                        Script = "",
                        UseScript = false,
                        Enabled = true
                    });
                    Items.Add(new SocialNetwork()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 2,
                        Name = "Instagram",
                        Symbol = "instagram",
                        Url = "https://www.instagram.com/",
                        Script = "",
                        UseScript = false,
                        Enabled = true
                    });
                    Items.Add(new SocialNetwork()
                    {
                        Uid = Guid.NewGuid(),
                        Idfc = 3,
                        Name = "Pinterest",
                        Symbol = "pinterest",
                        Url = "https://www.pinterest.com/",
                        Script = "",
                        UseScript = false,
                        Enabled = true
                    });
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetSocialNetworkTableAdapters.tbSocialNetworkTableAdapter tbSocialNetworkTableAdapter = new Datas.Ecommerce.DataSetSocialNetworkTableAdapters.tbSocialNetworkTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetSocialNetwork.tbSocialNetworkDataTable tbSocialNetworkDataTable = tbSocialNetworkTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetSocialNetwork.tbSocialNetworkRow item in tbSocialNetworkDataTable.Rows)
                    {
                        Items.Add(new SocialNetwork(item.Uid, item.Id, item.Name, item.Symbol, item.Url, item.IsScriptNull()?null:item.Script, item.UseScript, item.Enabled));
                    }
                    break;
            }

            return Items;
        }
    }
    /// <summary>
    /// Resaux sociaux
    /// </summary>
    /// <remarks>
    ///     <![CDATA[Créé le 30/11/2017 - 11:00:00]]>
    ///     <![CDATA[Mis à jour le 01/12/2017 - 09:30:00]]>
    /// </remarks>
    public class SocialNetwork
    {
        /// <summary>
        /// Unique identifiant du profil de connection
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Identifiant du profil de connection
        /// </summary>
        public int Idfc { get; set; }
        /// <summary>
        /// Nom des Reseaux Sociaux
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Symbole des reseaux sociaux
        /// </summary>
        public string Symbol { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Script { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool UseScript { get; set; }
        /// <summary>
        /// Etat activé des reseaux sociaux
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Construteur
        /// </summary>
        public SocialNetwork() { return; }
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="idfc"></param>
        /// <param name="name"></param>
        /// <param name="symbol"></param>
        /// <param name="url"></param>
        /// <param name="Script"></param>
        /// <param name="useScript"></param>
        /// <param name="enabled"></param>
        public SocialNetwork(Guid uid, int idfc, string name, string symbol, string url, string script, bool useScript, bool enabled)
        {
            Uid = uid;
            Idfc = idfc;
            Name = name;
            Symbol = symbol;
            Url = url;
            Script = script;
            UseScript = useScript;
            Enabled = enabled;
            return;
        }
    }
}
