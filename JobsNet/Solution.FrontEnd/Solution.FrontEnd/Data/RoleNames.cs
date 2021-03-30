using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solution.FrontEnd.Models
{
    // https://jorgeramon.me/2015/how-to-seed-users-and-roles-in-an-asp-net-mvc-application/
    /// <summary>
    /// Storing Role Names into a class
    /// </summary>
    public class RoleNames
    {
        public const string ROLE_ADMINISTRATOR = "Administrator";
        public const string ROLE_OFERENTE = "Oferente";
        public const string ROLE_EMPLEADOR = "Empleador";
        public static List <string> RoleList () { 
            return new List <string> (){
                ROLE_ADMINISTRATOR, ROLE_OFERENTE, ROLE_EMPLEADOR
            }; 
        }
    }
}