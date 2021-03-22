using Juce.Logic.Nodes;
using UnityEngine;

namespace Juce.Logic.Graphs
{
    [CreateAssetMenu(fileName = "Logic", menuName = "Logic", order = 100)]
    [RequireNode(typeof(StartFlowNode))]
    public class LogicMainGraph : BaseLogicGraph
    {

    }
}
