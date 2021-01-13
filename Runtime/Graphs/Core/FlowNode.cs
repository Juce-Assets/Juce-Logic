using XNode;

namespace Juce.Logic
{
    public abstract class FlowNode : InstructionNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public FlowConnection FlowIn;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public FlowConnection FlowOut;
    }
}
