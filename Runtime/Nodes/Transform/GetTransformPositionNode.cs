using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Logic.Attributes;
using System;
using UnityEngine;

namespace Juce.Logic.Nodes
{
    [LogicNode(
        "Get Position",
        "Transform",
        new Type[] { typeof(BaseLogicUnityGraph) })
        ]
    public class GetTransformPositionNode : LogicNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public Transform TransformIn;

        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public bool LocalBoolIn;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public Vector3 PositionVector3Out;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(TransformIn), GetTransformPositionInstruction.TransformIn, null);
            LinkInputPortWithLogicPort(nameof(LocalBoolIn), GetTransformPositionInstruction.LocalBoolIn, LocalBoolIn);
            LinkOutputPortWithLogicPort(nameof(PositionVector3Out), GetTransformPositionInstruction.PositionVector3Out);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<GetTransformPositionInstruction>();
        }
    }
}
