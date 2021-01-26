using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class IntAdditionNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public int ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), IntAdditionInstruction.ValueAIn, ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), IntAdditionInstruction.ValueBIn, ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), IntAdditionInstruction.ResultOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IntAdditionInstruction>();
        }
    }
}
