using Juce.Logic.Types;
using System;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Juce.Logic.Nodes
{
    [CustomNodeEditor(typeof(WhenAllNode))]
    public class WhenAllNodeEditor : NodeEditor
    {
        private WhenAllNode Node => target as WhenAllNode;

        private string newFieldName;

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Node.FlowIn)));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Node.FlowOut)));

            DrawAddPort();

            foreach (XNode.NodePort dynamicPort in Node.DynamicOutputs)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        Node.RemoveDynamicPort(dynamicPort);
                        break;
                    }

                    GUILayout.Label(dynamicPort.fieldName);

                    Rect rect = GUILayoutUtility.GetLastRect();
                    Vector2 position = rect.position + new Vector2(rect.width, 0);
                    NodeEditorGUILayout.PortField(position, dynamicPort);
                }
                EditorGUILayout.EndHorizontal();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAddPort()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Field name:", GUILayout.ExpandWidth(false));
                newFieldName = GUILayout.TextField(newFieldName);
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Add"))
            {
                Node.AddDynamicOutput(
                    typeof(FlowConnection),
                    XNode.Node.ConnectionType.Override,
                    XNode.Node.TypeConstraint.Strict,
                    newFieldName
                    );

                newFieldName = string.Empty;
            }
        }
    }
}
