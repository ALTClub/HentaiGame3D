
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Unity.Cinemachine.CinemachineFreeLook _cinemachine;
    Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        SaveManager.OnSave.AddListener(save);
        load();
    }
    public Vector3 Rotate(Vector3 direction, Transform player)
    {
        // --- Character rotation --- 
        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Relate the front with the Z direction (depth) and right with X (lateral movement)
        forward = forward * direction.z;
        right = right * direction.x;

        if (direction.x != 0 || direction.z != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            player.rotation = Quaternion.Slerp(player.rotation, rotation, 0.15f);
        }

        // --- End rotation ---
        Vector3 verticalDirection = Vector3.up * direction.y;
        Vector3 horizontalDirection = forward + right;
        return verticalDirection + horizontalDirection;
    }
    void load()
    {
        var saveData = SaveManager.GetSaveData<float[]>(this);
        if (saveData == default) return;
        _cinemachine.m_XAxis.Value = saveData[0];
        _cinemachine.m_YAxis.Value = saveData[1];
    }
    void save()
    {
        float[] data = new float[]
        {
            _cinemachine.m_XAxis.Value,
            _cinemachine.m_YAxis.Value
        };
        SaveManager.UpdateSaveData(this, data);
    }
}
