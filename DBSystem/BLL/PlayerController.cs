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
    public class PlayerController //Product
    {
        public List<Player> Player_GetByTeam(int teamid)
        {
            using (var context = new Context())
            {
                IEnumerable<Player> results =
                    context.Database.SqlQuery<Player>("Player_GetByTeam @TeamID"
                        , new SqlParameter("@TeamID", teamid));
                return results.ToList();
            }
        }
        public List<Player> Player_GetByAgeGender(int age, string gender)
        {
            using (var context = new Context())
            {
                IEnumerable<Player> results =
                    context.Database.SqlQuery<Player>("Player_GetByAgeGender @Age, @Gender"
                        , new SqlParameter("Age", age)
                        , new SqlParameter("Gender", gender));
                return results.ToList();
            }
        }
        public List<Player> Player_List()
        {
            using (var context = new Context())
            {
                return context.Players.ToList();
            }
        }
        public Player Player_Find(int playerid)
        {
            using (var context = new Context())
            {
                return context.Players.Find(playerid);
            }
        }
        public int Player_Add(Player item)
        {
            using (var context = new Context())
            {
                context.Players.Add(item);
                context.SaveChanges();
                return item.PlayerID;
            }
        }

        public int Products_Update(Player item)
        {
            using (var context = new Context())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges();
            }
        }

        public int Players_Delete(int playerid)
        {
            using (var context = new Context())
            {
                var existing = context.Players.Find(playerid);
                if (existing == null)
                {
                    throw new Exception("Record has been remove from database");
                }
                context.Players.Remove(existing);
                return context.SaveChanges();
            }
        }
    }
}
