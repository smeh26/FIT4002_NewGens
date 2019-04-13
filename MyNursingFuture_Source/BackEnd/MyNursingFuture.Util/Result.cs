using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.Util
{
    public class Result
    {
        private bool _success { get; set; }
        public Result(bool success = true)
        {
            this.Success = success;
        }

        public string Message { get; set; }

        public bool Success {
            get {
                return _success;
            }
            set {
                if (!value)
                {
                    this.Entity = null;                    
                }
                this._success = value;
            }
        }

        public object Entity { get; set; }
    }
}
