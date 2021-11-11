using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace Entity
{

    [Table("Contract")]
    public partial class Contract
    {
        public Contract()
        {
            InsuredEvents = new HashSet<InsuredEvent>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContractID { get; set; }

        public DateTime CreateDate { get; set; }

        public int TypeOfInsuranceID { get; set; }

        public int BranchID { get; set; }

        public int InsuranceAgentID { get; set; }

        public int Term { get; set; }

        public int ClientID { get; set; }

        [StringLength(4000)]
        public string Refusal { get; set; }

        public virtual Client Client { get; set; }

        public virtual Worker Worker { get; set; }

        public virtual TypeOfInsurance TypeOfInsurance { get; set; }

        public virtual ICollection<InsuredEvent> InsuredEvents { get; set; }
    }
}
