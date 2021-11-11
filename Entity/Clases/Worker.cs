using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Entity
{
    public partial class Worker
    {
        public Worker()
        {
            Contracts = new HashSet<Contract>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(1)]
        public string Sex { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int BrunchID { get; set; }

        public virtual Brunch Brunch { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
