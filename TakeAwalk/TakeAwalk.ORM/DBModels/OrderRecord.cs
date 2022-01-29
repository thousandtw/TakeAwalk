namespace TakeAwalk.ORM.DBModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderRecord")]
    public partial class OrderRecord
    {
        [Key]
        public int OrderID { get; set; }

        public Guid CustomerID { get; set; }

        public Guid TrainTicketID { get; set; }

        public int OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? CancellationDate { get; set; }
    }
}
