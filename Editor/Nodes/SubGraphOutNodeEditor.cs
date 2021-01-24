using Juce.OldLogic.Types;
using System;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Juce.OldLogic.Nodes
{
    [CustomNodeEditor(typeof(SubGraphOutNode))]
    public class SubGraphOutNodeEditor : NodeEditor
    {
        private SubGraphOutNode Node => target as SubGraphOutNode;

        private string newFieldName;

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            DrawAddPort();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(Node.FlowIn)));

            foreach (XNode.NodePort dynamicPort in Node.DynamicInputs)
            {
                EditorGUILayout.BeginHorizontal();
                {
                    NodeEditorGUILayout.PortField(dynamicPort);

                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        Node.RemoveDynamicPort(dynamicPort);
                        break;
                    }
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
                SelectTypesPopup.Show((Type type) =>
                {
                    XNode.NodePort dynamicNodePort = Node.AddDynamicInput(
                        type,
                        XNode.Node.ConnectionType.Multiple,
                        XNode.Node.TypeConstraint.Strict,
                        newFieldName
                        );


                    newFieldName = string.Empty;
                });
            }
        }
    }
}
