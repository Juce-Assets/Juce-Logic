using Juce.Scripting;
using Juce.Scripting.Instructions;
using System.Collections.Generic;

namespace Juce.Logic.Nodes
{
    public abstract class ListGetValueNode<TNode, TInstruction> : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited, backingValue = ShowBackingValue.Never)]
        public List<TNode> ListIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public int IndexIntIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public TNode ValueOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ListIn), ListGetValueInstruction<TInstruction>.ListIn, null);
            LinkInputPortWithLogicPort(nameof(IndexIntIn), ListGetValueInstruction<TInstruction>.IndexIntIn, IndexIntIn);
            LinkOutputPortWithLogicPort(nameof(ValueOut), ListGetValueInstruction<TInstruction>.ValueOut);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<ListGetValueInstruction<TInstruction>>();
        }
    }
}
