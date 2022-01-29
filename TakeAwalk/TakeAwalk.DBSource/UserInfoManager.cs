using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeAwalk.ORM.DBModels;
using System.Web;
using System.Net.Mail;
using System.IO;

namespace TakeAwalk.DBSource
{
    public class UserInfoManager
    {
        public static void SendAutomatedEmail(string email, string body, string subject)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("takeawalk837@gmail.com", "TakeAwalk");   //信箱帳號 ,寄信人名稱
                mail.To.Add(email);
                mail.Priority = MailPriority.Normal;
                mail.Subject = subject;
                mail.Body = body;
                SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
                MySmtp.Credentials = new System.Net.NetworkCredential("takeawalk837@gmail.com", "take7308");  //信箱帳號 ,信箱密碼
                MySmtp.EnableSsl = true;
                MySmtp.Send(mail);
                MySmtp = null;
                mail.Dispose();
            }
            catch (Exception ex)
            {
                try
                {
                    writelog(ex.Message);
                }
                catch (Exception)
                {
                    throw;
                }
                throw ex;
            }
        }

        #region write log 紀錄觸發
        private static void writelog(string msg)
        {
            string logPath = @"C:\Log";

            // check path exist
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);                                 //新增log資料夾
            }

            FileStream fs = new FileStream(@"C:\Log\error.log", FileMode.Append);  //新增錯誤log檔

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString() + "----" + msg);
            sw.Close();
            fs.Close();
        }
        #endregion

        public static bool trySearch(string account, string email, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(email))
            {
                errorMsg = "請輸入帳號或信箱";
                return false;
            }

            var userInfo = UserInfoManager.GetUserInfoByAccount(account);

            if (userInfo == null)
            {
                errorMsg = "帳號輸入錯誤";
                return false;
            }
            if (string.Compare(userInfo.Account, account) == 0 &&
                string.Compare(userInfo.Email, email) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;
                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "請重新確認帳號與信箱";
                return false;
            }
        }
        public static UserInfo GetUserInfoByAccount(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static UserInfo GetUserInfoByID(Guid customerid)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.CustomerID == customerid
                         select item);

                    var obj = query.FirstOrDefault();
                    return obj;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static bool GetUserByAccount(string account)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Account == account
                         select item);

                    var obj = query.FirstOrDefault();
                    if (obj == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return true;
            }
        }
        public static bool GetUserByEmail(string email)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         where item.Email == email
                         select item);

                    var obj = query.FirstOrDefault();
                    if (obj == null)
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return true;
            }
        }
        public static List<UserInfo> GetUserInfoList_AdminOnly()
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query =
                        (from item in context.UserInfoes
                         select item);

                    var list = query.ToList();
                    return list;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }
        public static void CreateCustomer(UserInfo userInfo)
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
                Logger.WriteLog(ex);
            }
        }
        public static bool UpdatePWD(Guid CustomerID, string PWD)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query = (from item in context.UserInfoes
                                 where item.CustomerID == CustomerID
                                 select item).FirstOrDefault();
                    query.Password = PWD;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
        public static bool UpdateCustomer(Guid CustomerID, UserInfo userInfo)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var obj = (from item in context.UserInfoes
                               where item.CustomerID == CustomerID
                               select item).FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Name = userInfo.Name;
                        obj.IdNumber = userInfo.IdNumber;
                        obj.MobilePhone = userInfo.MobilePhone;
                        obj.Email = userInfo.Email;

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
        public static bool UpdateUserLevel(Guid CustomerID, int levelvalue)
        {
            try
            {
                using (ContextModel context = new ContextModel())
                {
                    var query = (from item in context.UserInfoes
                                 where item.CustomerID == CustomerID
                                 select item).FirstOrDefault();

                    query.UserLevel = levelvalue;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }
        }
    }
}
