using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopupWindow : MonoBehaviour
    {
        [HideInInspector]
        public bool canInteract;
        private bool _isOpen, _largeItemOpen;

        [SerializeField] private List<GameObject> popupObjects;
        [SerializeField] private List<Image> itemSprites;
        [SerializeField] private List<Sprite> itemLargeSprites;

        [SerializeField] private Image largeImage;

        private List<Outline> _itemOutlines = new List<Outline>();
        private int _selectIndex = 0;

        private float _targetSize = 0f;
        private float _largeTargetSize = 0f;

        [SerializeField] private float animationDuration;

        [SerializeField] [Header("Popup Item")]
        private bool popupHasItem;

        [SerializeField] private int itemPosition;
        [SerializeField] private Sprite substituteSprite;
        [SerializeField] private GameObject popupItem;
        [SerializeField] private DefaultEvent eventToTrigger;
        private bool _hasGottenItem = false;

        private Coroutine _popupWindowLerp, _largeItemLerp;

        private void Awake()
        {
            itemPosition--;

            foreach (GameObject item in popupObjects)
            {
                _itemOutlines.Add(item.GetComponent<Outline>());
            }

            ResetSpriteColor();
        }

        private void OnEnable()
        {
            Game.Input.OnHumanInteract.AddListener(OnHumanInteract);
            Game.Input.OnHumanCancel.AddListener(OnHumanCancel);
        }

        private void OnDisable()
        {
            Game.Input.OnHumanInteract.RemoveListener(OnHumanInteract);
            Game.Input.OnHumanCancel.AddListener(OnHumanCancel);
        }

        private void OnHumanCancel()
        {
            if (!canInteract || !_isOpen)
            {
                return;
            }

            if (!_largeItemOpen)
            {
                ClosePopup(); 
            }
            else
            {
                CloseItem();
            }
        }

        private void OnHumanInteract()
        {
            if (!canInteract)
            {
                return;
            }

            if (_isOpen)
            {
                OpenItem();
            }
            else
            {
                OpenPopup();
            }
        }

        private void ClosePopup()
        {
            Game.Input.HumanInputMode = InputMode.Free;
            
            _isOpen = false;
            _targetSize = 0f;

            if (_popupWindowLerp != null)
            {
                StopCoroutine(_popupWindowLerp);
            }
            _popupWindowLerp = StartCoroutine(StartLerp(transform, _targetSize));
        }

        private void OpenPopup()
        {
            Game.Input.HumanInputMode = InputMode.MovementLimited;
            
            _isOpen = true;
            _targetSize = 1f;

            ResetSelection();
            if (_popupWindowLerp != null)
            {
                StopCoroutine(_popupWindowLerp);
            }
            _popupWindowLerp = StartCoroutine(StartLerp(transform, _targetSize));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && _isOpen)
            {
                SelectPrevious();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && _isOpen)
            {
                SelectNext();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && _isOpen)
            {
                OpenItem();
            }
        }

        private IEnumerator StartLerp(Transform target, float targetScale)
        {
            var currentTime = 0f;
            var currentScale = target.localScale;
            while (currentTime < animationDuration)
            {
                currentTime += Time.deltaTime;
                var newScale = Vector3.Lerp(currentScale, new Vector3(targetScale, targetScale, targetScale), currentTime/animationDuration);
                target.localScale = newScale;
                yield return null;
            }
        }

        private void OpenItem()
        {
            _largeItemOpen = true;
            
            _largeTargetSize = 1f;

            if (_largeItemLerp != null)
            {
                StopCoroutine(_largeItemLerp);
            }
            _largeItemLerp = StartCoroutine(StartLerp(largeImage.transform, _largeTargetSize));
            
            largeImage.sprite = itemLargeSprites[_selectIndex];

            if (popupHasItem && !_hasGottenItem && _selectIndex == itemPosition)
            {
                _hasGottenItem = true;
                popupItem.SetActive(true);

                if (eventToTrigger != null)
                {
                    eventToTrigger.RaiseEvent();
                }
            }
        }

        private void CloseItem()
        {
            _largeItemOpen = false;
            
            _largeTargetSize = 0f;

            if (_largeItemLerp != null)
            {
                StopCoroutine(_largeItemLerp);
            }
            _largeItemLerp = StartCoroutine(StartLerp(largeImage.transform, _largeTargetSize));
            
            if (_hasGottenItem)
            {
                itemLargeSprites[itemPosition] = substituteSprite;
            }
        }

        public void ResetSelection()
        {
            CloseItem();
            _selectIndex = 0;
            foreach (Outline outline in _itemOutlines)
            {
                outline.enabled = false;
            }

            ResetSpriteColor();
            _itemOutlines[_selectIndex].enabled = true;
            itemSprites[_selectIndex].color = Color.white;
        }

        public void SelectNext()
        {
            CloseItem();
            _itemOutlines[_selectIndex].enabled = false;
            itemSprites[_selectIndex].color = Color.grey;
            _selectIndex++;
            if (_selectIndex == popupObjects.Count)
            {
                _selectIndex = 0;
            }

            _itemOutlines[_selectIndex].enabled = true;
            itemSprites[_selectIndex].color = Color.white;
        }

        public void SelectPrevious()
        {
            CloseItem();
            _itemOutlines[_selectIndex].enabled = false;
            itemSprites[_selectIndex].color = Color.grey;
            _selectIndex--;
            if (_selectIndex < 0)
            {
                _selectIndex = popupObjects.Count - 1;
            }

            _itemOutlines[_selectIndex].enabled = true;
            itemSprites[_selectIndex].color = Color.white;
        }

        private void ResetSpriteColor()
        {
            foreach (Image sprite in itemSprites)
            {
                sprite.color = Color.grey;
            }
        }
    }
}