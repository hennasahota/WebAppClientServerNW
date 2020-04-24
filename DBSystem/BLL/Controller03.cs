using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;
using System.ComponentModel;

namespace DBSystem.BLL
{
    [DataObject]
    public class Controller03 //Supplier
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Guardian> List()
        {
            using (var context = new Context())
            {
                return context.Guardians.ToList();
            }
        }
    }
}
