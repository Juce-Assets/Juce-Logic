using Juce.Logic.Compiler;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Execution;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Juce.Logic.Runtime
{
    public class GraphTestOld : MonoBehaviour
    {
        [SerializeField] private LogicGraph logicGraph = default;

        private void Awake()
        {
            LogicGraphCompiler logicGraphCompiler = new LogicGraphCompiler(logicGraph);

            Script script = logicGraphCompiler.Compile();

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
            };

            string json = JsonConvert.SerializeObject(script, settings);

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            File.WriteAllText($"{desktopPath}\\script.json", json);

            Script newScript = JsonConvert.DeserializeObject<Script>(json, settings);

            ScriptExecutor scriptExecutor = new ScriptExecutor(newScript);

            scriptExecutor.Execute();
        }
    }
}
