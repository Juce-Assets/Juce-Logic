using Juce.Logic.Compiler;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Execution;
using UnityEngine;

public class ExecuteLogicGraph : MonoBehaviour
{
    [SerializeField] private LogicMainGraph logicMainGraph;
    [SerializeField] private int inputInt = default;

    // Start is called before the first frame update
    void Start()
    {
        Script script = new LogicGraphCompiler(logicMainGraph).CompileFromStartFlowNode();

        bool found = script.TryGetScriptInstruction(out IntInputInstruction intInputInstruction);

        if(found)
        {
            intInputInstruction.SetInput(inputInt);
        }

        new ScriptExecutor(script).Execute();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
