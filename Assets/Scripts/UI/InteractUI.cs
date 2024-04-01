using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class InteractUI : MonoBehaviour
{
    [SerializeField] Transform promptsPoint;
    [SerializeField] GameObject promptPrefab;
    [SerializeField] float spacing;

    private List<GameObject> promptList = new List<GameObject>();

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

        foreach (var interactable in interactables)
        {
            GameObject prompt = Instantiate(this.promptPrefab, promptsPoint.position, Quaternion.identity);

            promptList.Add(prompt);

            prompt.transform.SetParent(promptsPoint);
            prompt.GetComponent<PromptUI>().SetText(interactable.GetName);
        }

        center();
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

    void center()
    {
        if (promptList.Count == 0) return;

        for (int i = 0; i < promptList.Count; i++)
        {
            Vector3 pos = promptsPoint.position;
            pos.y += spacing * i;
            promptList[i].transform.position = pos;
        }

        float center = (promptList[promptList.Count - 1].transform.position.y - promptsPoint.position.y) / 2;

        foreach (var item in promptList)
        {
            item.transform.Translate(0, -center, 0);
        }
    }
}
