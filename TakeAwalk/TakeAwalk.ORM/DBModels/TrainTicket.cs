namespace TakeAwalk.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TrainTicket")]
    public partial class TrainTicket
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrainTicket()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int TicketID { get; set; }

        [Required]
        [StringLength(50)]
        public string TicketName { get; set; }

        [Required]
        [StringLength(50)]
        public string TrainCompany { get; set; }

        public DateTime ActivityStartDate { get; set; }

        public DateTime ActivityEndDate { get; set; }

        public decimal Price { get; set; }

        public int Stocks { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid Creator { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Guid? Modifier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
