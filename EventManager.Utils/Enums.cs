using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Utils
{
    public static class Enums
    {
        public enum AssignmentStatus : int
        {
            None = 0,
            Applied = 1,
            Reserve = 2,
            Accepted = 3,
        }
    }
}
