using UnityEngine;

namespace Puzzles.PushPull
{
    public class ShelfSlot : MonoBehaviour
    {
        public int slotIndex;
        private ShelfInteractable shelfInteractable;

        public ShelfInteractable ShelfInteractable
        {
            get => shelfInteractable;
            set => shelfInteractable = value;
        }
    }
}