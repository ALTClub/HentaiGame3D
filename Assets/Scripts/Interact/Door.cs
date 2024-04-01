using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] bool isOpen = false;
    [SerializeField] float rotateAngle;

    [SerializeField] float rotateSpeed;
    [SerializeField] GameObject door;

    Coroutine rotator;
    Quaternion rotation;

    private void Start()
    {
        rotation = door.transform.rotation;
    }

    public override void Interact()
    {
        toggle();
    }

    void toggle()
    {
        if (rotator != null) StopCoroutine(rotator);
        rotator = StartCoroutine(toggleDoor());
        isOpen = !isOpen;
        _name = (isOpen) ? "Закрыть" : "Открыть";
    }

    IEnumerator toggleDoor()
    {
        float t = 0;
        Quaternion targetRotation = (!isOpen) ? Quaternion.Euler(0, rotation.y + rotateAngle, 0) : rotation;
        while (door.transform.rotation != targetRotation)
        {
            t += Time.fixedDeltaTime;
            door.transform.rotation = Quaternion.Lerp(door.transform.rotation, targetRotation, t / rotateSpeed);
            yield return new WaitForFixedUpdate();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startDirection = door.transform.right * door.transform.localScale.x;
        Gizmos.DrawRay(door.transform.position, Quaternion.Euler(0, rotation.y + rotateAngle, 0) * startDirection);
    }
}
