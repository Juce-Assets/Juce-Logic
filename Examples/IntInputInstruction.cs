using Juce.Scripting;
using Juce.Scripting.Instructions;
using Juce.Scripting.Loging;

public class IntInputInstruction : ScriptInstruction
{
    public const string IntOut = nameof(IntOut);

    private int intValue;

    public void SetInput(int intValue)
    {
        this.intValue = intValue;
    }

    public override void RegisterPorts()
    {
        AddOutputPort<int>(IntOut);
    }

    protected override void Execute(Script script, IScriptLogger scriptLogger)
    {
        SetOutputPortValue(IntOut, intValue);
    }
}
