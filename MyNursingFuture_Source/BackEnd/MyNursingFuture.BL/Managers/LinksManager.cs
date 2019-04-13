using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface ILinksManager : IManager<LinkEntity>
    {

    }
    public class LinksManager: ILinksManager
    {
        public Result Get()
        {
            var con = new DapperConnectionManager();
            var query = new QueryEntity
            {
                Entity = new {},
                Query = @"SELECT * FROM Links where Active = 1 ORDER BY Type ASC"
            };
            return con.ExecuteQuery<LinkEntity>(query);
        }

        public Result Get(int id)
        {
            throw new NotImplementedException();
        }

        public Result Update(LinkEntity entity)
        {
            throw new NotImplementedException();
        }

        public Result Insert(LinkEntity entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Result SetPublished(int id, bool published = true)
        {
            throw new InvalidOperationException();
        }
    }
}
