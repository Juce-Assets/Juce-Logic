using Juce.Logic.Compiler;
using Juce.Scripting;
using Juce.Scripting.Instructions;
using System.Collections.Generic;
using XNode;

namespace Juce.Logic.Nodes
{
    public abstract class ForeachNode<T> : FlowNode
    {
        [Input(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Inherited)]
        public List<T> ValuesListIn;

        [Output(connectionType = ConnectionType.Override, typeConstraint = TypeConstraint.Strict)]
        public FlowConnection IterationFlowOut;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public T IterationValueOut;

        [Output(connectionType = ConnectionType.Multiple, typeConstraint = TypeConstraint.None)]
        public int IterationIndexOut;

        protected override void LinkScriptPorts()
        {
            LinkInputPortWithLogicPort(nameof(ValuesListIn), nameof(ForeachInstruction<T>.ValuesListIn), ValuesListIn);

            LinkOutputPortWithLogicPort(nameof(IterationValueOut), nameof(ForeachInstruction<T>.IterationValueOut));
            LinkOutputPortWithLogicPort(nameof(IterationIndexOut), nameof(ForeachInstruction<T>.IterationIndexOut));
        }

        protected override ScriptInstruction GenerateInstruction(Script script)
        {
            return script.CreateScriptInstruction<ForeachInstruction<T>>();
        }

        public override void OnCompile(Script script)
        {
            ForeachInstruction<T> currentInstruction = CompiledScriptInstruction as ForeachInstruction<T>;

            NodePort iterationConnection = GetOutputPort(nameof(IterationFlowOut)).Connection;

            if(iterationConnection == null)
            {
                return;
            }

            FlowNode iterationNode = iterationConnection.node as FlowNode;

            if (iterationNode != null)
            {
                FlowInstruction iterationFlowInstruction = iterationNode.CompiledScriptInstruction as FlowInstruction;

                if (iterationFlowInstruction != null)
                {
                    currentInstruction.ConnectIterationFlow(iterationFlowInstruction);

                    new LogicGraphCompiler(LogicGraph).CompileFlow(iterationNode);
                }
            }
        }
    }
}
