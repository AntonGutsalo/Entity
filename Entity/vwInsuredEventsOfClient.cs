namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("vwInsuredEventsOfClient")]
    public partial class vwInsuredEventsOfClient
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(100)]
        public string FullName { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "money")]
        public decimal Sum { get; set; }
    }
}
