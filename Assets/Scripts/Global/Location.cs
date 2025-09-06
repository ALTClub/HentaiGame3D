using UnityEngine;

public class Location : MonoBehaviour
{
    public string Name;
    public static string Current { get; private set; }

    void Start()
    {
        Current = Name;
    }
}
