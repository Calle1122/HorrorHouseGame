using UnityEngine;

namespace Puzzle
{
    public class ItemController : MonoBehaviour
    {
        public void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}
