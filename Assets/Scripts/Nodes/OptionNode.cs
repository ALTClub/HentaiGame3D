using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[UnitTitle("Option")]
[UnitCategory("Dialog")]
public class OptionNode : DialogNode
{
    [DoNotSerialize] public ValueInput Name;
    [DoNotSerialize] public ValueInput Aviable;
    [DoNotSerialize] public ValueOutput Option;
    [DoNotSerialize] public ControlOutput OnChoice;

    UnityEvent<int> onChoice = new();
    protected override void Definition()
    {
        Name = ValueInput<string>("Название", "По умолчанию");
        Aviable = ValueInput<bool>("Доступность", true);
        onChoice.AddListener(ctx => invoke(OnChoice));
        Option = ValueOutput<Option>("Option", (flow) =>
        {
            return new Option(flow.GetValue<string>(Name), flow.GetValue<bool>(Aviable), onChoice);
        });
        OnChoice = ControlOutput("OnChoice");
    }
}

