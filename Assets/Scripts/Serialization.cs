using System.Collections.Generic;

[System.Serializable]
public class Serialization<TKey, TValue>
{
    public List<TKey> Keys;
    public List<TValue> Values;

    public Serialization() { }

    public Serialization(Dictionary<TKey, TValue> dict)
    {
        Keys = new List<TKey>(dict.Keys);
        Values = new List<TValue>(dict.Values);
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        var dict = new Dictionary<TKey, TValue>();
        for (int i = 0; i < Keys.Count; i++)
        {
            dict[Keys[i]] = Values[i];
        }
        return dict;
    }
}