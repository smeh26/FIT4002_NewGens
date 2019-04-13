using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace MyNursingFuture.BL.Managers
{
    public enum CacheTypes
    {
        Framework,
        WelcomeEmail,
        FeedbackEmail,
        ResetPasswordEmail,
        ContactEmail,
        ReportEmail
    }
    public interface ICacheManager
    {
        object Get(CacheTypes t);
        void Add(CacheTypes t, object o);
    }
    public class CacheManager : ICacheManager
    {
        public void Add(CacheTypes t, object o)
        {
            ObjectCache cache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) };
            cache.Add(t.ToString(), o, policy);
        }

        public object Get(CacheTypes t)
        {
            ObjectCache cache = MemoryCache.Default;
            var data = cache.Get(t.ToString());
            return data;
        }
    }
}
