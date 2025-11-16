using UnityEngine;
using UnityEngine.Rendering;

public class RoomColorFilterTrigger : MonoBehaviour
{
    public Volume volume;
    private float targetWeight = 0f;

    void Update()
    {
        volume.weight = Mathf.Lerp(volume.weight, targetWeight, Time.deltaTime * 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            targetWeight = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            targetWeight = 0;
    }
}
