using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.DBSource
{
    public class OrdersManager
    {
        public static List<Manager_OrderList_View> GetOrdersListbyCustomerID(Guid customerid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.Manager_OrderList_View
                                 join order in context.Orders on item.OrderID equals order.OrderID
                                 where order.CustomerID == customerid
                                 orderby item.CreateDate descending
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
        public static List<Manager_OrderList_View> GetOrdersList_AdminOnly()
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.Manager_OrderList_View
                                 orderby item.CreateDate descending
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
        public static List<Manager_OrderList_View> GetOrdersByDate(Guid customerid, DateTime startTime, DateTime endTime)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    DateTime newEndTime = endTime.AddDays(1);

                    var query = (from item in context.Manager_OrderList_View
                                 join order in context.Orders on item.OrderID equals order.OrderID
                                 where order.CustomerID == customerid && item.CreateDate <= newEndTime && item.CreateDate >= startTime
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
        public static List<Manager_OrderList_View> GetOrdersByDate_AdminOnly(DateTime start_t, DateTime end_t)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    DateTime newend_d = end_t.AddDays(1);

                    var query = (from item in context.Manager_OrderList_View
                                 where item.CreateDate <= newend_d && item.CreateDate >= start_t
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
        public static List<TicketComfirm_View> GetOrderDetailsbyOrderID(Guid customerid, int orderid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.TicketComfirm_View
                                 join order in context.Orders on item.OrderID equals order.OrderID
                                 where order.CustomerID == customerid && order.OrderID == orderid
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
        public static List<TicketComfirm_View> GetOrderDetailsbyOrderID_AdminOnly(int orderid)
        {
            using (ContextModel context = new ContextModel())
            {
                try
                {
                    var query = (from item in context.TicketComfirm_View
                                 where item.OrderID == orderid
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
    }
}
