using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.DBSource
{
    public class TicketManager
    {
        public static List<TrainTicket> GetTrainTicketsList()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.TrainTickets
                                 where item.IsEnabled == true
                                 select item);

                    var list = query.ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    return null;
                }
            }
        }
        public static List<TrainTicket> GetTrainTicketsList_AdminOnly()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.TrainTickets
                                 select item);

                    var list = query.ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    return null;
                }
            }
        }
        public static Manager_TicketList_View GetTrainTicketsDetailbyID_AdminOnly(int ticketid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.Manager_TicketList_View
                                 where item.TicketID == ticketid
                                 select item).FirstOrDefault();



                    var obj = query;
                    return obj;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    return null;
                }
            }
        }
        public static List<TrainTicket> GetTrainTicketsList_OrderByInventory()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.TrainTickets
                                 orderby item.Stocks descending
                                 select item);

                    var list = query.ToList();
                    return list;
                }
                catch (Exception ex)
                {
                    Logger.WriteLog(ex);
                    return null;
                }
            }
        }
        public static void CreateTicketOrders_OrdersTable(Order orders)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.Orders.Add(orders);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static void CreateTicketOrders_OrderDetailsTable(OrderDetail orderdetails)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.OrderDetails.Add(orderdetails);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static bool ReturnStock(int ticketid, int quantity)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.TrainTickets.Where(o => o.TicketID == ticketid).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Stocks += quantity;

                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static bool IsEnoughStocks(int ticketid, int quantity, string ticketname, out string StocksMsg)
        {
            using (ContextModel context = new ContextModel())
            {
                TrainTicket trainticket = new TrainTicket();
                trainticket.Stocks = context.TrainTickets.First(o => o.TicketID == ticketid).Stocks;

                int stocks = trainticket.Stocks;
                string Msg;

                if (stocks < quantity)
                {
                    Msg = $"勾選失敗。票券: {ticketname}庫存不足，請按取消後重勾選或調整數量";
                    StocksMsg = Msg;
                    return false;
                }
                else
                {
                    StocksMsg = null;
                    return true;
                }
            }

        }
        public static bool UpdateStock(int ticketid, int quantity)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.TrainTickets.Where(o => o.TicketID == ticketid).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Stocks -= quantity;

                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static void DeleteTicketOrders(int orderid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = context.Orders.Where(o => o.OrderID == orderid).FirstOrDefault();
                    var obj2 = context.OrderDetails.Where(o => o.OrderID == orderid);

                    foreach (var item in obj2)
                    {
                        context.OrderDetails.Remove(item);
                    }

                    if (obj != null)
                    {
                        context.Orders.Remove(obj);
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }
        public static bool UpdateTickets(int ticketid, TrainTicket trainticket)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = (from item in context.TrainTickets
                               where item.TicketID == ticketid
                               select item).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.TicketName = trainticket.TicketName;
                        obj.TrainCompany = trainticket.TrainCompany;
                        obj.ActivityStartDate = trainticket.ActivityStartDate;
                        obj.ActivityEndDate = trainticket.ActivityEndDate;
                        obj.Price = trainticket.Price;
                        obj.Stocks = trainticket.Stocks;
                        obj.Modifier = trainticket.Modifier;
                        obj.ModifyDate = trainticket.ModifyDate;
                        obj.IsEnabled = trainticket.IsEnabled;

                        context.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static void CreateTickets(TrainTicket trainticket)
        {

            try
            {
                using (ContextModel context = new ContextModel())
                {
                    context.TrainTickets.Add(trainticket);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }

    }
}
