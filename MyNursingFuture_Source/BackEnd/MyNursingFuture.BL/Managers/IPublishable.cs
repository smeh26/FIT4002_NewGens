using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.Util;

namespace MyNursingFuture.BL.Managers
{
    public interface IPublishable
    {
        Result SetPublished(int id, bool published = true);
    }
}
