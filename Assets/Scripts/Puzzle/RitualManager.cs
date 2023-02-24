using System.Collections;
using Events;
using UnityEngine;

namespace Puzzle
{
    public class RitualManager : MonoBehaviour
    {
        public bool item1Placed, item2Placed, item3Placed;
        [SerializeField] private PhantomTetherEvent startRitualEvent;
        [SerializeField] private PhantomTetherEvent ritualFinishedEvent;

        [SerializeField] private float ritualTime;

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
            StartCoroutine(RitualTimer());
        }

        private IEnumerator RitualTimer()
        {
            yield return new WaitForSeconds(ritualTime);
            
            ritualFinishedEvent.RaiseEvent();
        }
    }
}