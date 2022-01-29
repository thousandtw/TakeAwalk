namespace TakeAwalk.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TicketComfirm_View
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketID { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string TicketName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(50)]
        public string TrainCompany { get; set; }

        [Key]
        [Column(Order = 4)]
        public DateTime ActivityStartDate { get; set; }

        [Key]
        [Column(Order = 5)]
        public DateTime ActivityEndDate { get; set; }

        [Key]
        [Column(Order = 6)]
        public decimal Price { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Quantity { get; set; }

        [Key]
        [Column(Order = 8)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Stocks { get; set; }

        [Key]
        [Column(Order = 9)]
        public bool IsEnabled { get; set; }

        [Key]
        [Column(Order = 10)]
        public DateTime CreateDate { get; set; }

        [Key]
        [Column(Order = 11)]
        public Guid Creator { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Guid? Modifier { get; set; }
    }
}
