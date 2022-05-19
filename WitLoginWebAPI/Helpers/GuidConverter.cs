using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WitLoginWebAPI.Helpers
{
    public class GuidConverter
    {
        public static Guid ConvertToGuid(string strGuid)
        {
            Guid objGuid = Guid.Empty;
            if (Guid.TryParse(strGuid, out objGuid))
            {
                return objGuid;
            }
            else
            {
                return objGuid;
            }
        }
    }
}
