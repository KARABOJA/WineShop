using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Colors
    {
        public static List<Color> GetData()
        {
            List<Color> Items = new List<Color>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new Color()
                    {
                        Uid = Guid.Parse("F98252C5-72A8-450B-99F9-EE8EECD8E9C3"),
                        Idfc = 1,
                        Name = "White",
                        Description = "Blanc",
                        Enabled = true
                    });
                    Items.Add(new Color()
                    {
                        Uid = Guid.Parse("CCE49E95-4E45-4564-A834-F091E043C88C"),
                        Idfc = 2,
                        Name = "Pink",
                        Description = "Rosé",
                        Enabled = true
                    });
                    Items.Add(new Color()
                    {
                        Uid = Guid.Parse("69398FF3-7591-4C1A-B431-C484A22CA174"),
                        Idfc = 3,
                        Name = "Red",
                        Description = "Rouge",
                        Enabled = true
                    });
                    break;
                case Enums.Environment.Production:
                    //On va chercher les infos dans la base de données
                    break;
            }

            return Items;
        }
    }

    public class Color
    {
        public Guid Uid { get; set; }
        /// <summary>
        /// Id de fonction de la devise
        /// </summary>
        public int Idfc { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Color() { return; }

        public Color(
            Guid uid
            , int idfc
            , string name
            , string description
            , bool enabled)
        {
            Uid = uid;
            Idfc = idfc;
            Name = name;
            Description = description;
            Enabled = enabled;
        }
    }
}
