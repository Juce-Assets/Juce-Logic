using Juce.Logic.Compiler;
using Juce.Logic.Graphs;
using Juce.Scripting;
using Juce.Scripting.Execution;
using Juce.Scripting.Loging;
using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

namespace Juce.Logic.Runtime
{
    public class GraphTestOld : MonoBehaviour
    {
        [SerializeField] private BaseLogicGraph logicGraph = default;

        private void Awake()
        {
            LogicGraphCompiler logicGraphCompiler = new LogicGraphCompiler(logicGraph);

            var compileWatch = System.Diagnostics.Stopwatch.StartNew();

            Script script = logicGraphCompiler.CompileFromStartFlowNode();

            compileWatch.Stop();

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

            var watch = System.Diagnostics.Stopwatch.StartNew();

            scriptExecutor.Execute(new ScriptLogger());

            // the code that you want to measure comes here
            watch.Stop();

            UnityEngine.Debug.Log($"Compile: {compileWatch.ElapsedMilliseconds} | Execute: {watch.ElapsedMilliseconds}");
        }
    }
}
