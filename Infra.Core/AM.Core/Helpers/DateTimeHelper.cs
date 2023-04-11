using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Helpers
{
    public static class DateTimeHelper
    {
        [DebuggerStepThrough]
        public static DateTime NewDateTime()
        {
            return DateTimeOffset.Now.UtcDateTime;
        }
    }
}
