using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraEffects : MonoBehaviour
{
    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    IEnumerator LerpCamera(Camera camera, float startY, float endY)
    {
        float timeElapsed = 0;
        float duration = 0.5f;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;

            camera.transform.position = new Vector3(camera.transform.position.x, Mathf.Lerp(startY, endY, t), camera.transform.position.z);
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        camera.transform.position = new Vector3(camera.transform.position.x, endY, camera.transform.position.z);
    }
}
