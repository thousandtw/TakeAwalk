using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TakeAwalk.ORM.DBModels
{
    public partial class ContextModel : DbContext
    {
        public ContextModel()
            : base("name=DefaultConnectionString")
        {
        }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TrainTicket> TrainTickets { get; set; }
        public virtual DbSet<UserInfo> UserInfoes { get; set; }
        public virtual DbSet<Manager_OrderList_View> Manager_OrderList_View { get; set; }
        public virtual DbSet<Manager_TicketList_View> Manager_TicketList_View { get; set; }
        public virtual DbSet<OrderList_View> OrderList_View { get; set; }
        public virtual DbSet<TicketComfirm_View> TicketComfirm_View { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TrainTicket>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<TrainTicket>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.TrainTicket)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.MobilePhone)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Account)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.UserInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manager_OrderList_View>()
                .Property(e => e.Total)
                .HasPrecision(38, 0);

            modelBuilder.Entity<Manager_TicketList_View>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<OrderList_View>()
                .Property(e => e.Total)
                .HasPrecision(38, 0);

            modelBuilder.Entity<TicketComfirm_View>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);
        }
    }
}
