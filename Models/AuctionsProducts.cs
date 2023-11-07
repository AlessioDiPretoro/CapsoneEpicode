namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AuctionsProducts
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuctionsProducts()
        {
            AuctionsDetails = new HashSet<AuctionsDetails>();
        }

        public int id { get; set; }

        public int idProduct { get; set; }

        public DateTime dataStart { get; set; }

        public DateTime dataEnd { get; set; }

        [Column(TypeName = "money")]
        public decimal startPrice { get; set; }

        public bool isActive { get; set; }

        public int? idWinner { get; set; }
        public int? idAuctionWinner { get; set; }
        public bool isPayed { get; set; }
        public decimal? endPrice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuctionsDetails> AuctionsDetails { get; set; }

        public virtual Product Product { get; set; }

        public virtual Users Users { get; set; }
    }
}