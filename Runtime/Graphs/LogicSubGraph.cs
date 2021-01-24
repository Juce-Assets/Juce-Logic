using Juce.OldLogic.Nodes;
using UnityEngine;
using XNode;

namespace Juce.OldLogic.Graphs
{
    [CreateAssetMenu(fileName = "OldLogic", menuName = "OldSubGraph", order = 100)]
    [RequireNode(typeof(SubGraphInNode), typeof(SubGraphOutNode))]
    public class LogicSubGraph : LogicGraph
    {

    }
}
