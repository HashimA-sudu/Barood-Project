using UnityEngine;
using System.Collections;

public class SwordSwing : MonoBehaviour
{
    public float swingSpeed = 10f;
    private bool isSwinging = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isSwinging)
        {
            StartCoroutine(SwingRoutine());
        }
    }

    IEnumerator SwingRoutine()
    {
        isSwinging = true;
        Quaternion startRot = transform.localRotation;
        Quaternion endRot = Quaternion.Euler(0, 0, 90) * startRot; // Swings 90 degrees

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        // Return to original position
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * swingSpeed;
            transform.localRotation = Quaternion.Lerp(endRot, startRot, t);
            yield return null;
        }

        isSwinging = false;
    }
}