using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CreditsManager : MonoBehaviour
    {
        [SerializeField] private float timePerSlide;

        [SerializeField] private Image creditImage;
        [SerializeField] private List<Sprite> creditSprites;

        private int _currentSpriteIndex = 0;
        
        private void OnEnable()
        {
            PlayCredits();
        }

        private void PlayCredits()
        {
            creditImage.sprite = creditSprites[_currentSpriteIndex];
            StartCoroutine(FadePortraits());
        }

        private IEnumerator FadePortraits()
        {
            _currentSpriteIndex++;
            
            yield return new WaitForSeconds(timePerSlide);
            
            if (_currentSpriteIndex >= creditSprites.Count)
            {
                Application.Quit();
            }

            var currentTime = 0f;
            var startAlpha = creditImage.color.a;
            while (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
                var newAlpha = Mathf.Lerp(startAlpha, 0, currentTime/1f);
                creditImage.color = new Color(creditImage.color.r, creditImage.color.g, creditImage.color.b, newAlpha);
                yield return null;
            }
            
            creditImage.sprite = creditSprites[_currentSpriteIndex];

            currentTime = 0f;
            startAlpha = creditImage.color.a;
            while (currentTime < 1f)
            {
                currentTime += Time.deltaTime;
                var newAlpha = Mathf.Lerp(startAlpha, 1, currentTime/1f);
                creditImage.color = new Color(creditImage.color.r, creditImage.color.g, creditImage.color.b, newAlpha);
                yield return null;
            }

            StartCoroutine(FadePortraits());
        }
    }
}
