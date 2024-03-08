using PromptGPT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptGPT.Services
{
    public class ModelDeploymentService
    {
        public ModelDeployment GetDefaultModelDeployment()
        {
            return new ModelDeployment("default");
        }
    }
}
