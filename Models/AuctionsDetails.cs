namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AuctionsDetails
    {
        public int id { get; set; }

        public int idAuction { get; set; }

        public int idUser { get; set; }

        public DateTime data { get; set; }

        [Column(TypeName = "money")]
        public decimal price { get; set; }

        public virtual AuctionsProducts AuctionsProducts { get; set; }

        public virtual Users Users { get; set; }
    }
}
