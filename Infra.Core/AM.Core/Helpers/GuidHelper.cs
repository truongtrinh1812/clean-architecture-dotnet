using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.Core.Helpers
{
    public static class GuidHelper
    {
        [DebuggerStepThrough]
        public static Guid NewGuid(string? guid = null)
        {
            return string.IsNullOrWhiteSpace(guid) ? Guid.NewGuid() : new Guid(guid);
        }
    }
}
