using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{ /// <summary>
  /// Connexion
  /// </summary>
    public class Connection
    {/// <summary>
     /// Unique identifiant du profil de connection
     /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Nom du profile de connection
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// prenom du profil de connection
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// nom entier du profil de connection
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        ///detail du profile
        /// </summary>
        public string Profil { get; set; }
        /// <summary>
        /// Etat activé du profile
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Session { get; set; }

        public void CreateTest()
        {
            Uid = Guid.NewGuid();
            Name = "Jacques";
            FirstName = "Daniels";
            FullName = "xxx";
            Profil = "Actif";
        }
        public static void LogOut()
        {
            HttpContext.Current.Session["currentuser"] = null;
        }
    }
}