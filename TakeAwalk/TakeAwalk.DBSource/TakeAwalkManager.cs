using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeAwalk.ORM.DBModels;

namespace TakeAwalk.dbSource
{
    public class TakeAwalkManager
    {
        public static void CreateCustomer (UserInfo userInfo)
        {
          
            try
            {
                using (ContextModel context = new ContextModel())
                {
                   
                    context.UserInfoes.Add(userInfo);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Logger.Writelog(ex);

            }
        }
    }
}
