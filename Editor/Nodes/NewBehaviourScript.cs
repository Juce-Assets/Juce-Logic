using XNodeEditor;

namespace Juce.Logic.Nodes
{
    [CustomNodeEditor(typeof(StartFlowNode))]
    public class StartFlowNodeEditor : NodeEditor
    {
        private StartFlowNode node;

        public override void OnBodyGUI()
        {
            if (node == null)
            {
                node = target as StartFlowNode;
            }

            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(node.FlowOut)));

            serializedObject.ApplyModifiedProperties();
        }
    }
}
