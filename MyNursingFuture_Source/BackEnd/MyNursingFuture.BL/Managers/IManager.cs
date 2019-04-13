using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IManager<IEntity> : IPublishable
    {
        Result Get();
        Result Get(int id);
        Result Update(IEntity entity);
        Result Insert(IEntity entity);
        Result Delete(int id);
    }
    
}
