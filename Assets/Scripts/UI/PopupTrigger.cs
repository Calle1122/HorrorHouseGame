using GameConstants;
using UnityEngine;

namespace UI
{
    public class PopupTrigger : MonoBehaviour
    {
        [SerializeField] private PopupWindow popup;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                popup.canInteract = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Tags.PlayerTag))
            {
                popup.canInteract = false;
            }
        }
    }
}
