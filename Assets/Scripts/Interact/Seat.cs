using UnityEngine;

public class Seat : Interactable
{
    [SerializeField] Transform _place;
    SaveData<string> user;
    private void Awake()
    {
        user = new(this, false, "");
    }
    public override void Interact(GameObject sender)
    {
        bool isEngaged = user.Value != "" && user.Value != ((object)sender).ToString();
        if (isEngaged) return;
        user.Value = user.Value == "" ? ((object)sender).ToString() : "";
        sender.GetComponent<Sitting>().Sit(_place);
    }

    public override string GetPrompt { get { return _prompt[user.Value == "" ? 0 : 1]; } }
    public override bool IsAviable => user.Value == PlayerInteractor.Player || user.Value == "";
}
