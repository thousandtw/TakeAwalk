using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeAwalk.Auth
{
    public class UserInfoModel
    {
        public Guid CustomerID { get; set; }
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public int UserLevel { get; set; }
    }
}
