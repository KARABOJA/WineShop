using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models.Views
{
    public class Contact
    {
     /// <summary>
     /// Unique identifiant du contact
     /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Nom du contact
        /// </summary>
        public string NameLast{ get; set; }
        /// <summary>
        /// Prenom du contact
        /// </summary>
        public string NameFirst { get; set; }
        /// <summary>
        /// Mail du contact
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// telephone du contact
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// nom de la société
        /// </summary>
        public string Compagny { get; set; }
        /// <summary>
        /// Pays du contact
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        ///Ville du contact
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Object du mail
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Detail du message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Etat activé du profile
        /// </summary>
        public bool Enabled { get; set; }
        

        public void CreateTest()
        {
            Uid = Guid.NewGuid();
            NameLast = "Donnie";
            NameFirst = "Brasco";
            Mail = "DonnieBrascol@hotmail.fr";
            Phone = "06.12.34.56.78";
            Compagny = "NAS";
            Country = "U.S";
            City = "Miami";
            Subject = "Besoin d'aide";
            Message = "xxxxxxxxxxxxxx";
        }
        
    }
}
