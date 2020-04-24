using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using DBSystem.DAL;
using DBSystem.ENTITIES;

namespace DBSystem.BLL
{
    public class Controller03 //Supplier
    {
        public List<Guardians> List()
        {
            using (var context = new Context())
            {
                return context.Entity03s.ToList();
            }
        }
    }
}
