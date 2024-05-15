using Microsoft.Azure.Management.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Azure.Commands.ResourceManager.Cmdlets.SdkModels.DeploymentStacks
{
    public class PSDeploymentStackValidationError
    {     
        public PSDeploymentStackValidationError(ErrorDetail error) 
        {
            Code = error?.Code;
            Message = error?.Message;
            Target = error?.Target;
            
            Details = new List<PSDeploymentStackValidationError>();
            if (error?.Details != null)
            {
                foreach (var err in error.Details)
                {
                    Details.Add(new PSDeploymentStackValidationError(err));
                }
            }
        }
        public string Code { get; set; }

        public string Message { get; set; }

        public string Target { get; set; }

        public List<PSDeploymentStackValidationError> Details { get; set; }
    }
}
