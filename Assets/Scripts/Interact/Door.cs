using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] SaveData<bool> _isOpen;
    [SerializeField] float _openedAngle, _closedAngel;

    [SerializeField] float _rotateSpeed;
    [SerializeField] GameObject _door;
    public bool IsPlayerCollision = false;
    Coroutine rotator;
    private void Awake()
    {
        _isOpen = new(this, false, false);
        _prompt = (_isOpen.Value) ? "Закрыть" : "Открыть";
        Quaternion targetRotation = Quaternion.Euler(0, 0, (_isOpen.Value) ? _openedAngle : _closedAngel);
        _door.transform.localRotation = targetRotation;
    }

    public override void Interact()
    {
        toggle();
    }
    void toggle()
    {
        if (rotator != null) StopCoroutine(rotator);
        rotator = StartCoroutine(toggleDoor());
        _isOpen.Value = !_isOpen.Value;
        _prompt = (_isOpen.Value) ? "Закрыть" : "Открыть";
    }

    IEnumerator toggleDoor()
    {
        float t = 0;
        Quaternion targetRotation = Quaternion.Euler(0, 0, (!_isOpen.Value) ? _openedAngle : _closedAngel);
        while (_door.transform.localRotation != targetRotation)
        {
            if (!IsPlayerCollision || !_isOpen.Value)
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
}
