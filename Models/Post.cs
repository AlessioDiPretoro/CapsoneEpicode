namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            PostResponse = new HashSet<PostResponse>();
        }

        public int id { get; set; }

        public int idUser { get; set; }

        public int? idProduct { get; set; }

        public int? idPostResponse { get; set; }

        public bool isActive { get; set; }

        [Required]
        public string body { get; set; }

        public DateTime date { get; set; }

        public DateTime? dateEdit { get; set; }

        public virtual Product Product { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostResponse> PostResponse { get; set; }
    }
}
