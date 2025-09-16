using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
   /* private void Update() //BORRAR, ES SOLO PARA TESTEOS
    {
        if(Input.GetKeyDown(KeyCode.P))StartCoroutine(Shake(0.5f, 0.2f));
    }*/

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);

            elapsed += Time.deltaTime;

            yield return null; 
        }

        transform.localPosition = originalPos;
    }
}
