using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaseManager.Core.Domain.Entities
{
    /// <summary>
    /// Représente une image associée à un bien immobilier.
    /// </summary>
    public class PropertyImage
    {
        #region Propriétés principales
        public int Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        #endregion

        #region Relations
        public int PropertyId { get; set; }
        public Property? Property { get; set; }
        #endregion
    }
}
