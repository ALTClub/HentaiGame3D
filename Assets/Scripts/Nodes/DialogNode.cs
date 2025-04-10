using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DialogNode : Unit
{
    protected void invoke(ControlOutput output)
    {
        var reference = GraphInstances.OfPooled(this.graph);
        var flow = Flow.New(reference.ToList().LastOrDefault());
        flow.Run(output);
    }
}
