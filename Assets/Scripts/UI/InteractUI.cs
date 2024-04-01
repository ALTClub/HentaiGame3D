using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class InteractUI : MonoBehaviour
{
    [SerializeField] Transform promptsPoint;
    [SerializeField] GameObject prompt;
    [SerializeField] float spacing;
    private List<GameObject> promptList = new List<GameObject>();
    private void Start()
    {
        PlayerInteractor.OnInteractUpdate.AddListener(updateInteractions);
        PlayerInteractor.OnSellectInteraction.AddListener(sellectInteraction);
    }
    void updateInteractions(List<InteractAble> interactAbles)
    {
        foreach (GameObject item in promptList) Destroy(item);
        promptList.Clear();
        foreach (InteractAble interactAble in interactAbles)
        {
            GameObject prompt = Instantiate(this.prompt, promptsPoint.position, Quaternion.identity);
            prompt.transform.SetParent(promptsPoint);
            promptList.Add(prompt);
            prompt.GetComponent<PromptUI>().SetText(interactAble.GetName);
        }
        center();
        sellectInteraction(0);
    }
    void sellectInteraction(int id)
    {
        int i = 0;
        foreach (GameObject prompt in promptList)
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
        foreach (GameObject item in promptList) item.transform.Translate(0, -center, 0);
    }
}
