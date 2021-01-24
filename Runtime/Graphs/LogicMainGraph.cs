using Juce.OldLogic.Nodes;
using UnityEngine;

namespace Juce.OldLogic.Graphs
{
    [CreateAssetMenu(fileName = "OldLogic", menuName = "OldLogic", order = 100)]
    [RequireNode(typeof(StartFlowNode))]
    public class LogicMainGraph : LogicGraph
    {

    }
}
