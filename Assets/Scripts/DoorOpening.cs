using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpening : MonoBehaviour
{
    public bool canOpen = false;

    private void Update()
    {
        if (canOpen)
        {
            Vector3 doorOpen = new Vector3(0f, -100f, 0f);

            transform.eulerAngles = doorOpen;
        }

    }
}
