using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class NavLinks
    {
        /// <summary>
        /// Obtient ou defini les
        /// </summary>
        public static List<NavLink> GetData()
        {
            List<NavLink> Items = new List<NavLink>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new NavLink()
                    {
                        Uid = Guid.NewGuid(),
                        Id = 0,
                        Name = "Home",
                        Description = "Accueil",
                        Enabled = true,

                        Symbol = "home",

                        ControllerView = string.Empty,
                        ActionView = string.Empty,

                        ControllerLink = "Home",
                        ActionLink = "Index",

                        First = true,
                    });
                    Items.Add(new NavLink()
                    {
                        Uid = Guid.NewGuid(),
                        Id = 1,
                        Name = "ContactUs",
                        Description = "Nous Contacter",
                        Enabled = true,

                        Symbol = string.Empty,

                        ControllerView = string.Empty,
                        ActionView = string.Empty,

                        ControllerLink = "Home",
                        ActionLink = "Contact",

                        First = false,
                    });
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    Datas.Ecommerce.DataSetNavLinkTableAdapters.tbNavLinkTableAdapter tbNavLinkTableAdapter = new Datas.Ecommerce.DataSetNavLinkTableAdapters.tbNavLinkTableAdapter() { Connection = Tools.Security.Connection.odbcConnection };
                    Datas.Ecommerce.DataSetNavLink.tbNavLinkDataTable tbNavLinkDataTable = tbNavLinkTableAdapter.GetData();
                    foreach (Datas.Ecommerce.DataSetNavLink.tbNavLinkRow item in tbNavLinkDataTable.Rows)
                    {
                        Items.Add(
                            new NavLink(
                                item.Uid
                                , item.Id
                                , item.Name
                                , Tools.App.Translate(item.Description, item.Uid)
                                , item.Enabled
                                , item.IsSymbolNull() ? null : item.Symbol
                                , item.IsControllerViewNull() ? null : item.ControllerView
                                , item.IsActionViewNull() ? null : item.ActionView
                                , item.IsControllerLinkNull() ? null : item.ControllerLink
                                , item.IsActionLinkNull() ? null : item.ActionLink
                                , item.First
                            )
                        );
                    }
                    break;
            }

            return Items;
        }
    }

    public class NavLink
    {
        public Guid Uid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        public string Symbol { get; set; }

        public string ControllerView { get; set; }
        public string ActionView { get; set; }

        public string ControllerLink { get; set; }
        public string ActionLink { get; set; }

        public bool First { get; set; }

        public NavLink()
        {
            return;
        }
        public NavLink(Guid uid, int Id, string name, string description, bool enabled, string symbol, string controllerView, string actionView, string controllerLink, string actionLink, bool first)
        {
            Uid = uid;
            Name = name;
            Description = description;
            Enabled = enabled;
            Symbol = symbol;
            ControllerView = controllerView;
            ActionView = actionView;
            ControllerLink = controllerLink;
            ActionLink = actionLink;
            First = first;
            return;
        }
    }
}