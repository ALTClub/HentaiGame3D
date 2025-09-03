using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] bool _isOpen = false;
    [SerializeField] float _openedAngle, _closedAngel;

    [SerializeField] float _rotateSpeed;
    [SerializeField] GameObject _door;
    public bool IsPlayerCollision = false;
    Coroutine rotator;

    public override void Interact()
    {
        toggle();
    }
    void toggle()
    {
        if (rotator != null) StopCoroutine(rotator);
        rotator = StartCoroutine(toggleDoor());
        _isOpen = !_isOpen;
        _prompt = (_isOpen) ? "Закрыть" : "Открыть";
    }

    IEnumerator toggleDoor()
    {
        float t = 0;
        Quaternion targetRotation = Quaternion.Euler(0, 0, (!_isOpen) ? _openedAngle : _closedAngel);
        while (_door.transform.localRotation != targetRotation)
        {
            if (!IsPlayerCollision || !_isOpen)
            {
                t += Time.fixedDeltaTime;
                _door.transform.localRotation = Quaternion.Lerp(_door.transform.localRotation, targetRotation, t / _rotateSpeed);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 startDirection = transform.right * _door.transform.localScale.x;
        Gizmos.DrawRay(_door.transform.position, Quaternion.Euler(0, _openedAngle, 0) * startDirection);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_door.transform.position, Quaternion.Euler(0, _closedAngel, 0) * startDirection);
    }
    protected override void onSave()
    {
        SaveManager.UpdateSaveData(this, _isOpen);
    }
    protected override void onLoad()
    {
        var saveData = SaveManager.GetSaveData<bool>(this);
        if (saveData == default) return;
        _isOpen = saveData;
        _prompt = (_isOpen) ? "Закрыть" : "Открыть";
        Quaternion targetRotation = Quaternion.Euler(0, 0, (_isOpen) ? _openedAngle : _closedAngel);
        _door.transform.localRotation = targetRotation;
    }
}
