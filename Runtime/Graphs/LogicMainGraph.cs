using Juce.Logic.Nodes;
using UnityEngine;

namespace Juce.Logic.Graphs
{
    [CreateAssetMenu(fileName = "OldLogic", menuName = "OldLogic", order = 100)]
    [RequireNode(typeof(StartFlowNode))]
    public class LogicMainGraph : LogicGraph
    {

    }
}
