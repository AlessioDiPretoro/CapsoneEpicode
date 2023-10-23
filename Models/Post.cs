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
        public int id { get; set; }

        public int idUser { get; set; }

        public int? idVisibility { get; set; }

        public int? idUserResponse { get; set; }

        public bool isActive { get; set; }

        [Required]
        [StringLength(50)]
        public string title { get; set; }

        [Required]
        public string body { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateEdit { get; set; }

        public virtual PostVisibility PostVisibility { get; set; }

        public virtual Users Users { get; set; }
    }
}
