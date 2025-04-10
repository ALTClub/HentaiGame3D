using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[UnitTitle("Phrase")]
[UnitCategory("Dialog")]
public class PhraseNode : DialogNode
{
    [DoNotSerialize] public ValueInput Name;
    [DoNotSerialize] public ValueInput Text;
    [DoNotSerialize] public ValueInput Source;
    [DoNotSerialize] public ValueInput VoiceActing;
    [DoNotSerialize] public ValueInput CanSkip;

    [DoNotSerialize] public ControlInput Input;
    [DoNotSerialize] public ControlOutput OnReaded;
    [DoNotSerialize] public ControlOutput Next;

    UnityEvent<int> onReaded = new();
    UnityEvent<int> onNext = new();
    protected override void Definition()
    {
        Name = ValueInput<string>("Спикер", "");
        Text = ValueInput<string>("Текст");
        Source = ValueInput<AudioSource>("Source", null);
        VoiceActing = ValueInput<AudioClip>("Озвучка", null);
        CanSkip = ValueInput<bool>("Можно скипать", true);

        onReaded.AddListener(ctx => invoke(OnReaded));
        onNext.AddListener(ctx => invoke(Next));

        Next = ControlOutput("Next");
        OnReaded = ControlOutput("OnReaded");

        Input = ControlInput("Input", (flow) =>
        {
            flow.GetValue<AudioSource>(Source)?.PlayOneShot(flow.GetValue<AudioClip>(VoiceActing));
            Phrase phrase = new(onReaded, onNext, flow.GetValue<string>(Name), flow.GetValue<string>(Text), flow.GetValue<bool>(CanSkip));
            DialogWindow.Read.Invoke(phrase);
            return null;
        });
    }
}
