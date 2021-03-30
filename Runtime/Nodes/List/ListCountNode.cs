using Juce.Scripting;
using Juce.Scripting.Instructions;
using System.Collections.Generic;

namespace Juce.Logic.Nodes
{
    public abstract class ListCountNode<TNode, TInstruction> : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited, backingValue = ShowBackingValue.Never)]
        public List<TNode> ListIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public int CountIntOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ListIn), ListCountInstruction<TInstruction>.ListIn, ListIn);
            LinkOutputPortWithLogicPort(nameof(CountIntOut), ListCountInstruction<TInstruction>.CountIntOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<ListCountInstruction<TInstruction>>();
        }
    }
}
