using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeaTimeDemo.Utility
{
    public static class SD
    {
        #region 角色
        public const string Role_Customer = "Customer";
        public const string Role_Employee = "Employee";
        public const string Role_Manager = "Manager";
        public const string Role_Admin = "Admin";
        #endregion

        #region 訂單狀態
        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusCancelled = "Cancelled";
        public const string StatusReady = "Ready";
        public const string StatusCompleted = "Completed"; 
        #endregion
    }
}
