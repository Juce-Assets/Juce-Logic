using Juce.OldLogic.Compiler;
using Juce.OldLogic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Execution;
using UnityEngine;

namespace Juce.Logic.Runtime
{
    public class GraphTestOld : MonoBehaviour
    {
        [SerializeField] private LogicGraph logicGraph = default;

        private void Awake()
        {
            LogicGraphCompiler logicGraphCompiler = new LogicGraphCompiler(logicGraph);

            Script script = logicGraphCompiler.Compile();

            ScriptExecutor scriptExecutor = new ScriptExecutor(script);

            scriptExecutor.Execute();
        }
    }
}
