using System;
using System.Security.Cryptography;
using Cinemachine;
using Interaction;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzle
{
    public class KeyTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject disableObject, interactableRef;
        [SerializeField] private GameObject key;
        [FormerlySerializedAs("interactable")] [SerializeField] private PickUpInteractable pickUpInteractable;
        [SerializeField] private CinemachineVirtualCamera cam;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == key)
            {
                Open();
            }
        }

        private void Open()
        {
            interactableRef.SetActive(true);
            CameraManagerScript.CurrentActiveCamera = cam;
            pickUpInteractable.ownerTransform.GetComponent<HumanPickupInteraction>().StopInteract();
            Destroy(key);
            disableObject.SetActive(false);
            GameObject.Find("PuzzleManager").GetComponent<CabinetPuzzle>().UpdateState(5);
        }
    }
}
