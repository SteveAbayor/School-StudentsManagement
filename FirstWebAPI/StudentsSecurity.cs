using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentsDataAccess;

namespace FirstWebAPI
{
    public class StudentsSecurity
    {
        public static bool Login(string username, string password)
        {
            using(StudentsDBEntities entities = new StudentsDBEntities())
            {
                return entities.Securities.Any(security => security.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && security.Password == password);
            }
        }
    }
}