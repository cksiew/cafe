using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonLib.Utilities
{
    public static class IdentifierGenerator
    {
        public static string GenerateUIDFromCurrentDate(int length)
        {
            string uniqueString = Guid.NewGuid().ToString("N").Substring(0, length);
            return uniqueString;
        }
    }
}
