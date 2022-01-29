namespace TakeAwalk.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TicketID { get; set; }

        public int Quantity { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid Creator { get; set; }

        public DateTime? ModifyDate { get; set; }

        public Guid? Modifier { get; set; }

        public virtual Order Order { get; set; }

        public virtual TrainTicket TrainTicket { get; set; }
    }
}
