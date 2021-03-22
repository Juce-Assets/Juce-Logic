using XNode;

namespace Juce.Logic.Nodes
{
    public abstract class FlowNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public FlowConnection FlowIn;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public FlowConnection FlowOut;
    }
}
