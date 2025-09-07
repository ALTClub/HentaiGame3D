using UnityEngine;

public class TransformSaver : MonoBehaviour
{
    CharacterController charController;
    void Awake()
    {
        TryGetComponent(out charController);
        SaveManager.OnSave.AddListener(save);
        load();
    }
    void load()
    {
        var saveData = SaveManager.GetSaveData<float[][]>(this);
        if (saveData == default) return;
        var pos = arrayToVector(saveData[0]);
        if (charController != default) charController.enabled = false;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(arrayToVector(saveData[1]));
        transform.localScale = arrayToVector(saveData[2]);
        if (charController != default) charController.enabled = true;
    }
    void save()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        float[][] data = new float[3][]
        {
            vectorToArray(transform.position),
            vectorToArray(rotation),
            vectorToArray(transform.localScale)
        };
        SaveManager.UpdateSaveData(this, data);
    }
    float[] vectorToArray(Vector3 vector) => new float[] { vector.x, vector.y, vector.z };
    Vector3 arrayToVector(float[] array) => new Vector3(array[0], array[1], array[2]);
}
