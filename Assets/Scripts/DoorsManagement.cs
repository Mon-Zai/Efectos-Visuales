using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsManagement : MonoBehaviour
{
    public Dictionary<Key, Transform> keys = new Dictionary<Key, Transform>();
    public Transform[] doors;

    private void Update()
    {
        if (keys.ContainsKey(Key.FlashlightDoor))
        {
            keys[Key.FlashlightDoor] = doors[0];

            //doors[0].gameObject.SetActive(false);
        }
    }

    public void AddKey(Key key)
    {
        if (!keys.ContainsValue(doors[0]))keys.Add(Key.FlashlightDoor, doors[0]);
    }
}

public enum Key
{
    FlashlightDoor
}
