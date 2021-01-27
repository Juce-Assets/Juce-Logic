using Juce.Logic.Compiler;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using XNode;

namespace Juce.Logic.Nodes
{
    public class IfNode : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public bool ConditionIn;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public FlowConnection TrueFlowOut;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public FlowConnection FalseFlowOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ConditionIn), nameof(IfInstruction.ConditionIn), ConditionIn);
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<IfInstruction>();
        }

        public override void OnCompile(Script script)
        {
            IfInstruction currentInstruction = CompiledScriptInstruction as IfInstruction;

            NodePort trueConnection = GetOutputPort(nameof(TrueFlowOut)).Connection;
            NodePort falseConnection = GetOutputPort(nameof(FalseFlowOut)).Connection;

            FlowNode trueNode = null;
            FlowNode falseNode = null;

            if (trueConnection != null)
            {
                trueNode = trueConnection.node as FlowNode;
            }

            if(falseConnection != null)
            {
                falseNode = falseConnection.node as FlowNode;
            }

            if (trueNode != null)
            {
                FlowInstruction trueFlowScriptInstruction = trueNode.CompiledScriptInstruction as FlowInstruction;

                if (trueFlowScriptInstruction != null)
                {
                    currentInstruction.ConnectTrueFlow(trueFlowScriptInstruction);

                    new LogicGraphCompiler(LogicGraph).CompileFlow(trueNode);
                }
            }

            if (falseNode != null)
            {
                FlowInstruction falseFlowScriptInstruction = falseNode.CompiledScriptInstruction as FlowInstruction;

                if (falseFlowScriptInstruction != null)
                {
                    currentInstruction.ConnectFalseFlow(falseFlowScriptInstruction);

                    new LogicGraphCompiler(LogicGraph).CompileFlow(falseNode);
                }
            }
        }
    }
}
