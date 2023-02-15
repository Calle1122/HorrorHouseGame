using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        private static MainMenu mainMenu;
        [SerializeField] private GameObject settingsMenu;

        private void Start()
        {
            mainMenu = GetComponentInChildren<MainMenu>();
        }

        private void Update()
        {
            // Used for testing for now... will be changed to work with our input system later. :)

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                settingsMenu.SetActive(!settingsMenu.activeSelf);
            }
        }

        public static void ShowPopup()
        {
            mainMenu.PopUp();
        }
    }
}