using System;
using System.Security.Cryptography;
using Cinemachine;
using Interaction;
using UnityEngine;

namespace Puzzle
{
    public class KeyTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject disableObject, interactableRef;
        [SerializeField] private GameObject key;
        [SerializeField] private Interactable interactable;
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
            interactable.ownerTransform.GetComponent<HumanInteraction>().StopInteract();
            Destroy(key);
            disableObject.SetActive(false);
            GameObject.Find("PuzzleManager").GetComponent<CabinetPuzzle>().UpdateState(5);
        }
    }
}
