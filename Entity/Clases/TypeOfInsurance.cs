using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Entity
{

    [Table("TypeOfInsurance")]
    public partial class TypeOfInsurance
    {
        public TypeOfInsurance()
        {
            Contracts = new HashSet<Contract>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeOfInsuranceID { get; set; }

        [Required]
        [StringLength(1)]
        public string InsuranceType { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
