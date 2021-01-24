﻿using Juce.OldLogic.Graphs;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace Juce.OldLogic.Nodes
{
    [CustomNodeEditor(typeof(SubGraphNode))]
    public class SubGraphNodeEditor : NodeEditor
    {
        private SubGraphNode Node => target as SubGraphNode;

        private SerializedProperty subGraphSerializedProperty;

        private LogicSubGraph logicSubGraph;

        private LogicSubGraph lastLogicSubGraph;

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            GatherProperties();

            DrawSubGraphReference();

            DrawRefreshPorts();

            base.OnBodyGUI();

            serializedObject.ApplyModifiedProperties();
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

            NodeEditorGUILayout.PropertyField(subGraphSerializedProperty);

            logicSubGraph = subGraphSerializedProperty.objectReferenceValue as LogicSubGraph;

            if (lastLogicSubGraph != logicSubGraph)
            {
                HandleSubGraphChange(lastLogicSubGraph, logicSubGraph);
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

        private void HandleSubGraphChange(LogicSubGraph lastLogicSubGrap, LogicSubGraph newLogicSubGraph)
        {
            if(newLogicSubGraph == null)
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

            foreach(NodePort port in inNode.DynamicOutputs)
            {
                NodePort newDynamicPort = Node.AddDynamicInput(
                    port.ValueType,
                    port.connectionType,
                    port.typeConstraint,
                    port.fieldName
                    );

                bool oldConnectionFound = oldConnectionsIn.TryGetValue(port.fieldName, out NodePort oldConnection);

                if (!oldConnectionFound)
                {
                    continue;
                }

                newDynamicPort.Connect(oldConnection);
            }

            foreach (NodePort port in outNode.DynamicInputs)
            {
                NodePort newDynamicPort = Node.AddDynamicOutput(
                    port.ValueType,
                    port.connectionType,
                    port.typeConstraint,
                    port.fieldName
                    );

                bool oldConnectionFound = oldConnectionsOut.TryGetValue(port.fieldName, out NodePort oldConnection);

                if (!oldConnectionFound)
                {
                    continue;
                }

                newDynamicPort.Connect(oldConnection);
            }
        }
    }
}
