using System.Collections;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField]
    bool isOpen = false;

    [SerializeField]
    float rotateAngle;

    [SerializeField]
    float rotateSpeed;

    [SerializeField]
    GameObject door;

    Coroutine rotator;
    Quaternion rotation;

    private void Start()
    {
        rotation = door.transform.localRotation;
    }

    public override void Interact()
    {
        toggle();
    }

    void toggle()
    {
        if (rotator != null)
            StopCoroutine(rotator);
        rotator = StartCoroutine(toggleDoor());
        isOpen = !isOpen;
        prompt = (isOpen) ? "Закрыть" : "Открыть";
    }

    IEnumerator toggleDoor()
    {
        float t = 0;
        Quaternion targetRotation = (!isOpen) ? Quaternion.Euler(0, 0, rotateAngle) : rotation;
        while (door.transform.localRotation != targetRotation)
        {
            t += Time.fixedDeltaTime;
            door.transform.localRotation = Quaternion.Lerp(
                door.transform.localRotation,
                targetRotation,
                t / rotateSpeed
            );
            yield return new WaitForFixedUpdate();
        }
    }

    public void StopRotation()
    {
        if (rotator != null)
            StopCoroutine(rotator);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 startDirection = transform.right * door.transform.localScale.x;
        Gizmos.DrawRay(door.transform.position, Quaternion.Euler(0, rotateAngle, 0) * startDirection);
    }
}
