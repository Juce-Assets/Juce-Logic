using Juce.Scripting;
using Juce.Scripting.Instructions;

namespace Juce.Logic.Nodes
{
    public class FloatMaxNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueAIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public float ValueBIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public float ResultOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValueAIn), FloatMaxInstruction.ValueAIn, ValueAIn);
            LinkInputPortWithLogicPort(nameof(ValueBIn), FloatMaxInstruction.ValueBIn, ValueBIn);
            LinkOutputPortWithLogicPort(nameof(ResultOut), FloatMaxInstruction.ResultOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<FloatMaxInstruction>();
        }
    }
}
