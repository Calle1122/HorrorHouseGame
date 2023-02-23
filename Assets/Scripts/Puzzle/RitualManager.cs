using Events;
using UnityEngine;

namespace Puzzle
{
    public class RitualManager : MonoBehaviour
    {
        public bool item1Placed, item2Placed, item3Placed;
        [SerializeField] private PhantomTetherEvent startRitualEvent;

        public void PlaceItem(int itemIndex)
        {
            switch (itemIndex)
            {
                case 1:
                    item1Placed = true;
                    break;
                case 2:
                    item2Placed = true;
                    break;
                case 3:
                    item3Placed = true;
                    break;
            }
            
            CheckToStartRitual();
        }
        
        private void CheckToStartRitual()
        {
            if (!item1Placed || !item2Placed || !item3Placed)
            {
                return;
            }
            
            startRitualEvent.RaiseEvent();
        }
    }
}