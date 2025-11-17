using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SteamDistort : MonoBehaviour
{
    public Volume volume;
    LensDistortion lens;

    void Start()
    {
        volume.profile.TryGet(out lens);
    }

    void Update()
    {
        lens.intensity.value = 0.1f + Mathf.Sin(Time.time * 1.5f) * 0.03f;
    }
}
