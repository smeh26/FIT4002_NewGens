using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Managers
{
    public class ManagersUtil
    {
        public bool StringLengthValidator(List<String> stringArray, List<int> lengthLimitArray)
        {
            int n = stringArray.Count;
            int m = lengthLimitArray.Count;

            if (n != m)
            {
                return false;
            }

            for (int i = 0; i < n; i++)
            {
                if (stringArray[i].Length > lengthLimitArray[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
