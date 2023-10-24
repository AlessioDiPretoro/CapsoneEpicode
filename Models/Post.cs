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

        public int? idProduct { get; set; }

        public int? idPostResponse { get; set; }

        public bool isActive { get; set; }

        [Required]
        public string body { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "date")]
        public DateTime? dateEdit { get; set; }

        public virtual Product Product { get; set; }

        public virtual Users Users { get; set; }
    }
}