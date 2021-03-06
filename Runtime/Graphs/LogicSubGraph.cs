﻿using Juce.Logic.Nodes;
using UnityEngine;

namespace Juce.Logic.Graphs
{
    [CreateAssetMenu(fileName = "Logic", menuName = "SubGraph", order = 100)]
    [RequireNode(typeof(SubGraphInNode), typeof(SubGraphOutNode))]
    public class LogicSubGraph : BaseLogicGraph
    {

    }
}
