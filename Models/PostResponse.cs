namespace Stones.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostResponse")]
    public partial class PostResponse
    {
        public int id { get; set; }

        public int idPost { get; set; }

        [Required]
        public string body { get; set; }

        public int idUser { get; set; }

        public bool isActive { get; set; }

        public DateTime date { get; set; }

        public DateTime? dateEdit { get; set; }

        public virtual Post Post { get; set; }

        public virtual Users Users { get; set; }
    }
}
