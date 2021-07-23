using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DellFanManagement.DellSmbiosSmiLib.DellSmi
{
    public enum SmiPasswordCheckResult : uint
    {
        Correct = 0,
        Incorrect = 2
    }
}
