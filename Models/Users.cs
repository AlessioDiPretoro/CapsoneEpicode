namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            Order = new HashSet<Order>();
            Post = new HashSet<Post>();
        }

        public int id { get; set; }

        [Remote("IsUsernameValid", "Validations", ErrorMessage = "Username già presente, sceglierne uno differente")]
        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Remote("IsEmailValid", "Validations", ErrorMessage = "Email già presente")]
        [Required]
        [StringLength(50)]
        public string email { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string surname { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(50)]
        public string city { get; set; }

        [StringLength(2)]
        public string prov { get; set; }

        [StringLength(20)]
        public string phone { get; set; }

        [StringLength(50)]
        public string imgProfile { get; set; }

        [StringLength(50)]
        public string role { get; set; }

        public int points { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Order { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }
    }
}