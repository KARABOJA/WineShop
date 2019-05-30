using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
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
        public string Name { get; set; }
        /// <summary>
        /// Mail du contact
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// telephone du contact
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///Objet du contact
        /// </summary>
        public string Objet { get; set; }
        /// <summary>
        ///Message du contact
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Etat activé du profile
        /// </summary>
        public bool Enabled { get; set; }
        

        public void CreateTest()
        {
            Uid = Guid.NewGuid();
            Name = "Jacquie";
            Mail = "Jacquie&Michel@hotmail.fr";
            Phone = "06.12.34.56.78";
            Objet = "Besoin d'aide";
            Message = "xxxxxxxxxxxxxx";
        }
        
    }
}
