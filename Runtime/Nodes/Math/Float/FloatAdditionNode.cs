using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class FloatAdditionNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public float ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), FloatAdditionInstruction.ValueAIn, ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), FloatAdditionInstruction.ValueBIn, ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), FloatAdditionInstruction.ResultOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<FloatAdditionInstruction>();
        }
    }
}
