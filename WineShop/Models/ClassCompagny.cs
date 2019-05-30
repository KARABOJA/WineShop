using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    /// <summary>
    /// Société
    /// </summary>
    public class Compagny
    {
        /// <summary>
        /// Unique identifiant de la société
        /// </summary>
        public Guid Uid { get; set; }
        /// <summary>
        /// Nom de la société
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Adresse de la société
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Adresse de la société
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Adresse de la société
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Adresse de la société
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Telephone de la société
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Mail de la société
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Etat activé des reseaux sociaux
        /// </summary>
        public bool Enabled { get; set; }

        public string Days { get; set; }
        public string Hours { get; set; }

    }
}