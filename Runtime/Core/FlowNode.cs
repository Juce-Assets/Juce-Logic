using XNode;

namespace Juce.OldLogic
{
    public abstract class FlowNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public FlowConnection FlowIn;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)] 
        public FlowConnection FlowOut;
    }
}
