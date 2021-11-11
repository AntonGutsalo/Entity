using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Entity
{

    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            Contracts = new HashSet<Contract>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public int IdentificationCode { get; set; }

        public double Coefficient { get; set; }

        public virtual ICollection<Contract> Contracts { get; set; }
    }
}
