using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Helpers
{
    public static class IntegerHelpers
    {
        public static bool IsPositiveIntegerGreaterThanZero(string? value)
        {
            if (int.TryParse(value, out int result))
            {
                return result > 0;
            }

            return false;
        }
    }
}
