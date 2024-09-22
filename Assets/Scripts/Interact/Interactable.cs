using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected string prompt;
    private void Awake()
    {
        SaveManager.OnSave.AddListener(onSave);
        onLoad();
    }
    public abstract void Interact();
    public string GetPrompt { get { return prompt; } }
    protected virtual void onSave() { }
    protected virtual void onLoad() { }
}
