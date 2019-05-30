using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Sizes
    {
        public static List<Size> GetData()
        {
            List<Size> Items = new List<Size>();
            switch(Tools.App.Environment)
            {
                case Enums.Environment.Test:
                    Items.Add(new Size()
                    {
                        Uid = Guid.Parse("143907C1-AC18-4B06-9715-CA467E5FF403"),
                        Idfc = 1,
                        Name = "375",
                        Description = "37,5 Cl",
                        Enabled = true
                    });
                    Items.Add(new Size()
                    {
                        Uid = Guid.Parse("0A5112D2-0147-4A7F-8ECF-A39E3C93AB16"),
                        Idfc = 2,
                        Name = "500",
                        Description = "50 Cl",
                        Enabled = true
                    });
                    Items.Add(new Size()
                    {
                        Uid = Guid.Parse("FC9EA376-B8CD-4468-8B9F-8461EBD90CBE"),
                        Idfc = 3,
                        Name = "1000",
                        Description = "1 L",
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

    public class Size
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
        public Size() { return; }

        public Size(
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
