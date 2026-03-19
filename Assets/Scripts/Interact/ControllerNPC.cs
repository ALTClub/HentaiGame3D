using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.AI;
using System.Collections;

public class ControllerNPC : Interactable
{
    [SerializeField] Interactable target;
    NavMeshAgent agent;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Interact(target));
    }
    public override void Interact(GameObject sender)
    {
        CustomEvent.Trigger(gameObject, "Interact");
        //DialogWindow.PlayDialog.Invoke(dialog);
    }
    public void SaveTrigger() => SaveManager.OnSave.Invoke();
    public IEnumerator Interact(Interactable interactable)
    {
        yield return StartCoroutine(WaitReachDestination(interactable.transform.position));
        interactable.Interact(gameObject);
    }
    public IEnumerator WaitReachDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        do { yield return new WaitForEndOfFrame(); }
        while (agent.remainingDistance > 0);
    }
}
