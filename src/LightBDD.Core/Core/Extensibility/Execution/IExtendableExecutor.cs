using System;
using System.Threading.Tasks;
using LightBDD.Core.Execution;
using LightBDD.Core.Metadata;

namespace LightBDD.Core.Extensibility.Execution
{
    public interface IExtendableExecutor
    {
        Task ExecuteScenario(IScenarioInfo scenario, Func<Task> scenarioInvocation);
        Task ExecuteStep(IStep step, Func<Task> stepInvocation);
    }
}