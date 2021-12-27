using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implant.Utils
{
    public static class ImplantTaskUtils
    {
        
        public static object GetPropertyValue(this object T, string PropName)
        {
            return T.GetType().GetProperty(PropName) == null ? null : T.GetType().GetProperty(PropName).GetValue(T);
        }
        
        public static void SetPropertyValue(this object T, string PropName, string PropVal)
        {
            T.GetType().GetProperty(PropName).SetValue(T, PropVal);
        }
    }
}
