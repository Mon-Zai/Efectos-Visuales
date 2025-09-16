using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactionDistance = 3f;
    public KeyCode interactionKey = KeyCode.E;
    public Transform playerCamera;

    void OnTriggerStay(Collider other)
    {
        /*if (Input.GetKeyDown(interactionKey))
        {*/

            IInteractable interactable = other.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                Debug.Log("Interacted with object: " + interactable);
            }

        //}
    }
}
