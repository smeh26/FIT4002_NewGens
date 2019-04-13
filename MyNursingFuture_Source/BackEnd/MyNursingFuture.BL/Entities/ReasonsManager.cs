using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Entities
{
    public interface IReasonsManager
    {
        Result Get();
        Result Edit(List<ReasonEntity> list);
    }
    public class ReasonsManager: IReasonsManager
    {
        public Result Get() {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new { },
                Query = @"SELECT * FROM Reasons ORDER BY Ix ASC"
            };
            return con.ExecuteQuery<ReasonEntity>(query);
        }
        public Result Edit(List<ReasonEntity> list)
        {

            return null;
        }
    }
}
