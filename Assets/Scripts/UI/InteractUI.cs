using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] Transform _promptsPoint;
    [SerializeField] GameObject _promptPrefab;

    List<GameObject> promptList = new List<GameObject>();

    void Start()
    {
        PlayerInteractor.OnInteractUpdate.AddListener(updateInteractions);
        PlayerInteractor.OnSellectInteraction.AddListener(selectInteraction);
    }

    void updateInteractions(List<Interactable> interactables)
    {
        foreach (var item in promptList)
        {
            Destroy(item);
        }

        promptList.Clear();

        if (interactables == default) return;
        foreach (var interactable in interactables)
        {
            GameObject prompt = Instantiate(_promptPrefab, _promptsPoint);
            promptList.Add(prompt);
            PromptUI ui = prompt.GetComponent<PromptUI>();
            ui.Enabled(interactable.IsAviable);
            ui.SetText(interactable.GetPrompt);
        }
        selectInteraction(0);
    }

    void selectInteraction(int id)
    {
        int i = 0;
        foreach (var prompt in promptList)
        {
            prompt.GetComponent<PromptUI>().Select(i == id);
            i++;
        }
    }
}
