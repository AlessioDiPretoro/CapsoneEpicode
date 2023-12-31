namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            AuctionsDetails = new HashSet<AuctionsDetails>();
            AuctionsProducts = new HashSet<AuctionsProducts>();
            Order = new HashSet<Order>();
            Post = new HashSet<Post>();
            PostResponse = new HashSet<PostResponse>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [NotMapped]
        [Compare("password", ErrorMessage = "Le password non coincidono.")]
        public string confirmPassword { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "nome")]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "cognome")]
        public string surname { get; set; }

        [StringLength(50)]
        [Display(Name = "indirizzo")]
        public string address { get; set; }

        [StringLength(50)]
        [Display(Name = "citt�")]
        public string city { get; set; }

        [StringLength(5)]
        [Display(Name = "CAP")]
        public string cap { get; set; }

        [StringLength(2)]
        [Display(Name = "provincia")]
        public string prov { get; set; }

        [StringLength(20)]
        [Display(Name = "telefono")]
        public string phone { get; set; }

        [StringLength(50)]
        public string imgProfile { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        [Display(Name = "punti")]
        public int? points { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuctionsDetails> AuctionsDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuctionsProducts> AuctionsProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostResponse> PostResponse { get; set; }
    }
}