using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsMenu;

        private void Update()
        {
            // Used for testing for now... will be changed to work with our input system later. :)

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                settingsMenu.SetActive(!settingsMenu.activeSelf);
            }
        }
    }
}