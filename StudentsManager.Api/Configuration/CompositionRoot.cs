using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsManager.Api.Configuration
{
    public class CompositionRoot
    {
        // If you need the whole container then change parameter type from IRegistrator to IContainer
        public CompositionRoot(IRegistrator r)
        {
        }
    }
}
