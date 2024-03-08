using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Models
{
    public class ModelDeployment(string name)
    {
        public string Name { get; } = name;
    }
}
