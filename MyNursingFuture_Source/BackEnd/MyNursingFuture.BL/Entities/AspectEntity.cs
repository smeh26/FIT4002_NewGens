using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.DL.Models;

namespace MyNursingFuture.BL.Entities
{
    public class AspectEntity: Aspect, IEntity
    {
        public string DomainName { get; set; }
        public string DomainFramework { get; set; }
        public List<ActionEntity> ActionsList { get; set; }
    }
}
