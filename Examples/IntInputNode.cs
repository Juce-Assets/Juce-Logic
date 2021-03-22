using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Logic.Attributes;
using System;
using Juce.Logic.Nodes;
using Juce.Scripting.Loging;
using Juce.Scripting.Instructions;

[LogicNode(
       "Int",
       "Input",
       new Type[] { typeof(BaseLogicGraph) })
       ]
public class IntInputNode : LogicNode
{
    [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
    public int IntOut;

    protected override void LinkScriptPorts()
    {
        LinkOutputPortWithLogicPort(nameof(IntOut), IntInputInstruction.IntOut);
    }

    protected override ScriptInstruction GenerateInstruction(Script script)
    {
        return script.CreateScriptInstruction<IntInputInstruction>();
    }
}
