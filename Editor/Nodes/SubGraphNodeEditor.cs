using Juce.Logic.Graphs;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Juce.Logic.Nodes
{
    [CustomNodeEditor(typeof(SubGraphNode))]
    public class SubGraphNodeEditor : NodeEditor
    {
        private SubGraphNode Node => target as SubGraphNode;

        private SerializedProperty subGraphSerializedProperty;

        private LogicSubGraph logicSubGraph;

        private bool needsToHandleSubraphChange;

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            GatherProperties();

            DrawSubGraphReference();

            DrawRefreshPorts();

            EditorGUILayout.Space();

            DrawPorts();

            serializedObject.ApplyModifiedProperties();

            if(needsToHandleSubraphChange)
            {
                needsToHandleSubraphChange = false;

                HandleSubGraphChange();
            }
        }

        private void GatherProperties()
        {
            if(subGraphSerializedProperty == null)
            {
                subGraphSerializedProperty = serializedObject.FindProperty("logicSubGraph");
            }

            logicSubGraph = subGraphSerializedProperty.objectReferenceValue as LogicSubGraph;
        }

        private void DrawSubGraphReference()
        {
            LogicSubGraph lastLogicSubGraph = subGraphSerializedProperty.objectReferenceValue as LogicSubGraph;

            NodeEditorGUILayout.PropertyField(subGraphSerializedProperty, new GUIContent("Sub Graph"));

            logicSubGraph = subGraphSerializedProperty.objectReferenceValue as LogicSubGraph;

            if (lastLogicSubGraph != logicSubGraph)
            {
                needsToHandleSubraphChange = true;
            }
        }

        private void DrawRefreshPorts()
        {
            if(GUILayout.Button("Refresh ports"))
            {
                RebuildPorts();
            }
        }

        public override int GetWidth()
        {
            return 300;
        }

        private void HandleSubGraphChange()
        {
            if(logicSubGraph == null)
            {
                ClearPorts();
            }
            else
            {
                RebuildPorts();
            }
        }

        private void ClearPorts()
        {
            Node.ClearDynamicPorts();
        }

        private void RebuildPorts()
        {
            if(logicSubGraph == null)
            {
                return;
            }

            SubGraphInNode inNode = logicSubGraph.GetNode<SubGraphInNode>();
            SubGraphOutNode outNode = logicSubGraph.GetNode<SubGraphOutNode>();

            Dictionary<string, NodePort> oldConnectionsIn = new Dictionary<string, NodePort>();
            Dictionary<string, NodePort> oldConnectionsOut = new Dictionary<string, NodePort>();

            foreach(NodePort port in Node.DynamicInputs)
            {
                if (port.Connection == null)
                {
                    continue;
                }

                oldConnectionsIn.Add(port.fieldName, port.Connection);
            }

            foreach (NodePort port in Node.DynamicOutputs)
            {
                if (port.Connection == null)
                {
                    continue;
                }

                oldConnectionsOut.Add(port.fieldName, port.Connection);
            }

            Node.ClearDynamicPorts();

            foreach (NodePort port in outNode.DynamicInputs.Reverse())
            {
                NodePort newDynamicPort = Node.AddDynamicOutput(
                    port.ValueType,
                    port.connectionType,
                     XNode.Node.TypeConstraint.None,
                    port.fieldName
                    );

                bool oldConnectionFound = oldConnectionsOut.TryGetValue(port.fieldName, out NodePort oldConnection);

                if (!oldConnectionFound)
                {
                    continue;
                }

                newDynamicPort.Connect(oldConnection);
            }

            foreach (NodePort port in inNode.DynamicOutputs.Reverse())
            {
                NodePort newDynamicPort = Node.AddDynamicInput(
                    port.ValueType,
                    port.connectionType,
                    XNode.Node.TypeConstraint.Inherited,
                    port.fieldName
                    );

                bool oldConnectionFound = oldConnectionsIn.TryGetValue(port.fieldName, out NodePort oldConnection);

                if (!oldConnectionFound)
                {
                    continue;
                }

                newDynamicPort.Connect(oldConnection);
            }
        }

        private void DrawPorts()
        {
            foreach (NodePort port in Node.Inputs)
            {
                NodeEditorGUILayout.PortField(port);
            }

            foreach (NodePort port in Node.Outputs)
            {
                NodeEditorGUILayout.PortField(port);
            }
        }
    }
}
