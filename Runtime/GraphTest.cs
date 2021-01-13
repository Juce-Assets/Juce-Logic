using Juce.Logic;
using Juce.Logic.Compiler;
using Juce.Scripting;
using Juce.Scripting.Execution;
using System;
using UnityEngine;

namespace Assets.Juce.Juce_Logic.Runtime
{
    public class GraphTest : MonoBehaviour
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
