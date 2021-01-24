using XNodeEditor;

namespace Juce.OldLogic.Nodes
{
    [CustomNodeEditor(typeof(StartFlowNode))]
    public class StartFlowNodeEditor : NodeEditor
    {
        private StartFlowNode Node => target as StartFlowNode;

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Node.FlowOut)));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
