using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractUI : MonoBehaviour
{
    [SerializeField] Transform _promptsPoint;
    [SerializeField] GameObject _promptPrefab;
    [SerializeField] float _spacing;

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

        foreach (var interactable in interactables)
        {
            GameObject prompt = Instantiate(this._promptPrefab, _promptsPoint.position, Quaternion.identity);

            promptList.Add(prompt);

            prompt.transform.SetParent(_promptsPoint);
            prompt.GetComponent<PromptUI>().SetText(interactable.GetPrompt);
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
            Vector3 pos = _promptsPoint.position;
            pos.y += _spacing * i;
            promptList[i].transform.position = pos;
        }

        float center = (promptList[promptList.Count - 1].transform.position.y - _promptsPoint.position.y) / 2;

        foreach (var item in promptList)
        {
            item.transform.Translate(0, -center, 0);
        }
    }
}
