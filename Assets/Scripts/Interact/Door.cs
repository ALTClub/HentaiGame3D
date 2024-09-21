using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] bool _isOpen = false;
    [SerializeField] float _rotateAngle;

    [SerializeField] float _rotateSpeed;
    [SerializeField] GameObject _door;

    Coroutine rotator;
    Quaternion rotation;

    private void Start()
    {
        rotation = _door.transform.localRotation;
    }

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
        Quaternion targetRotation = (!_isOpen) ? Quaternion.Euler(0, 0, _rotateAngle) : rotation;
        while (_door.transform.localRotation != targetRotation)
        {
            t += Time.fixedDeltaTime;
            _door.transform.localRotation = Quaternion.Lerp(_door.transform.localRotation, targetRotation, t / _rotateSpeed);
            yield return new WaitForFixedUpdate();
        }
    }
    public void StopRotation() { if (rotator != null) StopCoroutine(rotator); }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startDirection = transform.right * _door.transform.localScale.x;
        Gizmos.DrawRay(_door.transform.position, Quaternion.Euler(0, _rotateAngle, 0) * startDirection);
    }
}
