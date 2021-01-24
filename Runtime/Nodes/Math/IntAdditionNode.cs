using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.OldLogic.Nodes
{
    public class IntAdditionNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public int ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.Strict)]
        public int ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), nameof(IntAdditionInstruction.ValueAIn), ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), nameof(IntAdditionInstruction.ValueBIn), ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), nameof(IntAdditionInstruction.ResultOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntAdditionInstruction>();
        }
    }
}
