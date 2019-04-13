using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IAnswersManager : IManager<AnswerEntity>
    {
        
    }
    public class AnswersManager:IAnswersManager
    {
        public Result Get()
        {
            throw new NotImplementedException();
        }

        public Result Get(int id)
        {
            throw new NotImplementedException();
        }

        public Result Update(AnswerEntity entity)
        {
            throw new NotImplementedException();
        }

        public Result Insert(AnswerEntity entity)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Result SetPublished(int id, bool published = true)
        {
            throw new NotImplementedException();
        }
    }
}
