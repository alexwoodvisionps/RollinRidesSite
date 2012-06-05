using System;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Models;
using System.Reflection;
namespace RollingRides.WebApp.Components.BusinessLogic.Common
{
    public static class ListViewHelper
    {
        public static object GetProperty(this object obj, string name)
        {
            return obj.GetType().InvokeMember(name, BindingFlags.GetProperty | BindingFlags.Public, Type.DefaultBinder, obj,
                                       null);
        }
        public static void FillAccountType(this DropDownList ddl, bool needHeaderLabel, UserType myLoggedInType)
        {
            if(needHeaderLabel)
            {
                ddl.Items.Add(new ListItem("Choose Account Type", ""));
            }
            if(myLoggedInType == UserType.Admin)
            {
                ddl.Items.Add(new ListItem("Administrator", "1"));
                ddl.Items.Add(new ListItem("Corperate", "2"));
            }
            ddl.Items.Add(new ListItem("Basic User", "3"));
        }
    }
}