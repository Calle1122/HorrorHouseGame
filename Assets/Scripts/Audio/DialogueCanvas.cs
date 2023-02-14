using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Audio
{
    public class DialogueCanvas : MonoBehaviour
    {
        public static DialogueCanvas Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI dialogueTxt, objectiveTxt;
        [SerializeField] private Image ritualItem1Image, ritualItem2Image, ritualItem3Image;
        [SerializeField] private Sprite ritualItem1Sprite, ritualItem2Sprite, ritualItem3Sprite;

        [Header("Debug Information")]
        [SerializeField] private bool isPlaying;
        private Queue<DialogueSo> _dialogueQueue = new Queue<DialogueSo>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            dialogueTxt.enabled = false;
            isPlaying = false;
        }

        public void QueueDialogue(DialogueSo dialogue)
        {
            _dialogueQueue.Enqueue(dialogue);

            if (!isPlaying)
            {
                isPlaying = true;
                StartCoroutine(PlayDialogue());
            }
        }

        private IEnumerator PlayDialogue()
        {
            //Get dialogue and wait for sound to start (roughly .5 seconds)
            DialogueSo dialogueToPlay = _dialogueQueue.Dequeue();
            SoundManager.Instance.PlaySfx(dialogueToPlay.dialogueSound);
            yield return new WaitForSeconds(.5f);
            
            //Set text and enable it
            dialogueTxt.text = dialogueToPlay.dialogueText;
            dialogueTxt.enabled = true;
            
            //Wait for dialogue to finish and disable the text
            yield return new WaitForSeconds(dialogueToPlay.dialogueSound.clips[0].length);
            dialogueTxt.enabled = false;
            
            //If there are more queued... play next!
            if (_dialogueQueue.Count > 0)
            {
                StartCoroutine(PlayDialogue());
            }
            else
            {
                isPlaying = false;
            }
        }

        public void UpdateMainObjective(string newObjective)
        {
            //TODO: Include crossing off old objective before fading (some animation) to the new objective.
            
            objectiveTxt.text = newObjective;
        }
        
        public void UnlockRitualItem(int itemToUnlock)
        {
            switch (itemToUnlock)
            {
                case 1:
                    ritualItem1Image.sprite = ritualItem1Sprite;
                    break;
                case 2:
                    ritualItem2Image.sprite = ritualItem2Sprite;
                    break;
                case 3:
                    ritualItem3Image.sprite = ritualItem3Sprite;
                    break;
                default:
                    Debug.LogError("Invalid ritual item index when trying to unlock ritual item UI.", this);
                    break;
            }
        }
    }
}