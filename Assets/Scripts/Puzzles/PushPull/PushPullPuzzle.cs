using System.Collections.Generic;
using System.Linq;
using Audio;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzles.PushPull
{
    public class PushPullPuzzle : MonoBehaviour
    {
        public UnityEvent onSolved;

        [SerializeField] private List<GameObject> shelfGameObjects = new List<GameObject>();
        [SerializeField] private List<GameObject> shelfSlotsObjects = new List<GameObject>();

        public SfxSO pushSfx;

        private readonly List<ShelfSlot> correctSlots = new List<ShelfSlot>();
        private readonly List<ShelfSlot> shelfSlots = new List<ShelfSlot>();

        private bool _isSolved;

        private void Awake()
        {
            // Set Slots Indexes
            var currentIndex = 0;
            foreach (var shelfSlotObject in shelfSlotsObjects)
            {
                shelfSlots.Add(shelfSlotsObjects[currentIndex].GetComponent<ShelfSlot>());
                shelfSlotObject.GetComponent<ShelfSlot>().slotIndex = currentIndex;
                currentIndex++;
            }

            // Assign shelves to their slots
            shelfSlots[2].Shelf = shelfGameObjects[0].GetComponent<Shelf>();
            shelfSlots[4].Shelf = shelfGameObjects[1].GetComponent<Shelf>();
            shelfSlots[6].Shelf = shelfGameObjects[2].GetComponent<Shelf>();
            shelfSlots[8].Shelf = shelfGameObjects[3].GetComponent<Shelf>();

            // Assign shelves current slots references
            shelfSlots[2].Shelf.currentSlot = shelfSlots[2];
            shelfSlots[4].Shelf.currentSlot = shelfSlots[4];
            shelfSlots[6].Shelf.currentSlot = shelfSlots[6];
            shelfSlots[8].Shelf.currentSlot = shelfSlots[8];

            // Set correct slots
            correctSlots.Add(shelfSlots[0]);
            correctSlots.Add(shelfSlots[2]);
            correctSlots.Add(shelfSlots[7]);
            correctSlots.Add(shelfSlots[9]);
        }

        private void Start()
        {
            foreach (var shelfGameObject in shelfGameObjects)
            {
                if (!shelfGameObject.TryGetComponent<ShelfInteractable>(out var shelfInteractable))
                {
                    continue;
                }

                shelfInteractable.EnableInteractable();
            }

            EnablePuzzle();
        }

        public void EnablePuzzle()
        {
            foreach (var shelfGameObject in shelfGameObjects)
            {
                shelfGameObject.GetComponent<Shelf>().EnableInteraction();
                
            }
        }

        public void DisablePuzzle()
        {
            foreach (var shelfGameObject in shelfGameObjects)
            {
                shelfGameObject.GetComponent<Shelf>().DisableInteraction();
            }
        }

        public bool CanMoveShelf(ShelfInteractable movingShelfInteractable, bool isPrimaryDirRight,
            out int directionIndex)
        {
            // Direaction index: 0 = can't move at all, 1 = primary direction possible, 2 = secondary direction possible, 
            if (isPrimaryDirRight)
            {
                if (movingShelfInteractable.Shelf.currentSlot.slotIndex + 1 <= shelfSlots.Count &&
                    shelfSlots[movingShelfInteractable.Shelf.currentSlot.slotIndex + 1].Shelf == null)
                {
                    // Can move right if primary direction is right
                    directionIndex = 1;
                    return true;
                }

                if (movingShelfInteractable.Shelf.currentSlot.slotIndex - 1 >= 0 &&
                    shelfSlots[movingShelfInteractable.Shelf.currentSlot.slotIndex - 1].Shelf == null)
                {
                    // Can move left is primary direction is right
                    directionIndex = 2;
                    return true;
                }
            }

            if (!isPrimaryDirRight)
            {
                if (movingShelfInteractable.Shelf.currentSlot.slotIndex - 1 >= 0 &&
                    shelfSlots[movingShelfInteractable.Shelf.currentSlot.slotIndex - 1].Shelf == null)
                {
                    directionIndex = 1;
                    return true;
                }

                if (movingShelfInteractable.Shelf.currentSlot.slotIndex + 1 <= shelfSlots.Count &&
                    shelfSlots[movingShelfInteractable.Shelf.currentSlot.slotIndex + 1].Shelf == null)
                {
                    directionIndex = 2;
                    return true;
                }
            }

            directionIndex = 0;
            return false;
        }

        public void CheckSolved()
        {
            if (_isSolved || correctSlots.Any(correctSlot => correctSlot.Shelf == null))
            {
                return;
            }
            _isSolved = true;
            onSolved.Invoke();
            DisablePuzzle();
        }

        public void SetNewShelfPosition(Shelf shelf, bool movingRight)
        {
            if (movingRight)
            {
                shelfSlots[shelf.currentSlot.slotIndex + 1].Shelf =
                    shelf;
                shelfSlots[shelf.currentSlot.slotIndex].Shelf = null;

                shelf.currentSlot = shelfSlots[shelf.currentSlot.slotIndex + 1];
            }
            else
            {
                shelfSlots[shelf.currentSlot.slotIndex - 1].Shelf =
                    shelf;
                shelfSlots[shelf.currentSlot.slotIndex].Shelf = null;
                shelf.currentSlot = shelfSlots[shelf.currentSlot.slotIndex - 1];
            }
        }
    }
}