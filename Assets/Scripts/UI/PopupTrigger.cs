using UnityEngine;

namespace UI
{
    public class PopupTrigger : MonoBehaviour
    {
        [SerializeField] private PopupWindow popup;

        private void OnTriggerEnter(Collider other)
        {
            popup.OpenClosePopup();
        }

        private void OnTriggerExit(Collider other)
        {
            popup.OpenClosePopup();
        }
    }
}
