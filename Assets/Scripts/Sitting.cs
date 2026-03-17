using System.Collections;
using UnityEngine;

public class Sitting : MonoBehaviour
{
    Animator anim;
    SaveData<bool> isSitting;
    void Start()
    {
        isSitting = new(this, false, false);
        TryGetComponent(out anim);
        if (isSitting.Value)
        {
            anim?.SetBool("IsSitting", enabled);
            if (gameObject.CompareTag("Player"))
                InputManager.Input.Player.Movement.Disable();
        }
    }

    public void Sit(Transform place)
    {
        isSitting.Value = !isSitting.Value;
        StartCoroutine(sit(0.5f, place, isSitting.Value));
    }
    IEnumerator sit(float speed, Transform place, bool enabled)
    {
        anim.SetBool("IsSitting", enabled);
        if (enabled)
        {
            if (gameObject.CompareTag("Player"))
                InputManager.Input.Player.Movement.Disable();
        }

        float t = 0;
        Vector3 startPos = transform.position,
            targetPos = enabled ? place.position : transform.position + transform.right * 0.5f + transform.up * 0.3f;
        Quaternion startRot = transform.rotation,
            targetRot = enabled ? place.rotation : transform.rotation;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            transform.rotation = Quaternion.Lerp(startRot, targetRot, t);
            t += Time.fixedDeltaTime * speed;
            yield return new WaitForFixedUpdate();
        }

        if (!enabled)
        {
            if (gameObject.CompareTag("Player"))
                InputManager.Input.Player.Movement.Enable();
        }
    }
}
