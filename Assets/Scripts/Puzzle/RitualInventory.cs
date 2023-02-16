using System.Collections.Generic;
using Audio;
using UnityEngine;

namespace Puzzle
{
    public class RitualInventory : MonoBehaviour
    {
        public List<bool> itemList;

        public void UnlockItem(int itemIndex)
        {
            itemList[itemIndex - 1] = true;
            DialogueCanvas.Instance.UnlockRitualItem(itemIndex);
        }
        
        public bool CheckForAllItems()
        {
            foreach (bool itemStatus in itemList)
            {
                if (!itemStatus)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
