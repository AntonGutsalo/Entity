using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Entity
{

    public partial class InsuredEvent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EventsID { get; set; }

        [Column(TypeName = "money")]
        public decimal Sum { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }

        public DateTime Date { get; set; }

        public int ContractID { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
