using Juce.Scripting;
using System.Collections.Generic;
using System.Linq;
using XNode;

namespace Juce.Logic
{
    public abstract class InstructionNode : Node
    {
        private readonly Dictionary<NodePort, LogicPort> inputScriptLinks = new Dictionary<NodePort, LogicPort>();
        private readonly Dictionary<NodePort, LogicPort> outputScriptLinks = new Dictionary<NodePort, LogicPort>();

        public LogicGraph LogicGraph => graph as LogicGraph;
        public ScriptInstruction CompiledScriptInstruction { get; private set; }

        public List<NodePort> InputScriptLinks => inputScriptLinks.Keys.ToList();

        public override object GetValue(NodePort port)
        {
            return null;
        }

        protected void LinkInputPortWithLogicPort(string graphPortId, string logicPortId, object fallback)
        {
            inputScriptLinks.Add(GetInputPort(graphPortId), new LogicPort(logicPortId, fallback));
        }

        protected void LinkOutputPortWithLogicPort(string graphPortId, string logicPortId)
        {
            outputScriptLinks.Add(GetOutputPort(graphPortId), new LogicPort(logicPortId, default));
        }

        public bool TryGetInputLogicPort(NodePort nodePort, out LogicPort logicPort)
        {
            return inputScriptLinks.TryGetValue(nodePort, out logicPort);
        }

        public bool TryGetOutputLogicPort(NodePort nodePort, out LogicPort logicPort)
        {
            return outputScriptLinks.TryGetValue(nodePort, out logicPort);
        }

        public void Compile(Script script)
        {
            inputScriptLinks.Clear();
            outputScriptLinks.Clear();
            CompiledScriptInstruction = null;

            LinkScriptPorts();
            CompiledScriptInstruction = GenerateInstruction(script);
        }

        protected abstract void LinkScriptPorts();
        protected abstract ScriptInstruction GenerateInstruction(Script script);

        public virtual void OnCompile(Script script) { }
    }
}
