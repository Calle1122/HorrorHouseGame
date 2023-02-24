using System;
using System.Collections;
using System.Collections.Generic;
using GameConstants;
using UnityEngine;
using UnityEngine.Serialization;

public class GhostTouch : MonoBehaviour
{
    [FormerlySerializedAs("reference")] [SerializeField] private GameObject targetObject;
    //[SerializeField] private float animationDuration;
    private Renderer _objectRenderer;
    
    public static int AmplitudeID = Shader.PropertyToID("_Amplitude");
    public static int FrequencyID = Shader.PropertyToID("_Frequency");
    public static int SpeedID = Shader.PropertyToID("_AnimationSpeed");

    private float _currentAmp, _currentFre, _currentSpe;
    private float _desiredAmp, _desiredFre, _desiredSpe;

    private Coroutine _currentLerp;

    private void Awake()
    {
        _objectRenderer = targetObject.GetComponent<Renderer>();

        _currentAmp = 0; //max 1
        _currentFre = 1; //max 8
        _currentSpe = 0; //max 5

        _desiredAmp = 0;
        _desiredFre = 1;
        _desiredSpe = 0;

        _currentLerp = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag(Tags.GhostTag))
        {
            return;
        }
    
        if (_currentLerp != null)
        {
            StopCoroutine(_currentLerp);
        }

        _desiredAmp = .025f;
        _desiredFre = 8;
        _desiredSpe = 5;
        _currentLerp = StartCoroutine(ToggleWobble());
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag(Tags.GhostTag))
        {
            return;
        }
        
        StartCoroutine(DelayToggleWobble(.15f));
    }

    private IEnumerator DelayToggleWobble(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (_currentLerp != null)
        {
            StopCoroutine(_currentLerp);
        }

        _desiredAmp = 0;
        _desiredFre = 1;
        _desiredSpe = 0;
        _currentLerp = StartCoroutine(ToggleWobble());
    }

    private IEnumerator ToggleWobble()
    {
        var currentTime = 0f;

        _currentAmp = _objectRenderer.material.GetFloat(AmplitudeID);
        _currentFre = _objectRenderer.material.GetFloat(FrequencyID);
        _currentSpe = _objectRenderer.material.GetFloat(SpeedID);
        
        while (currentTime < .15f)
        {
            currentTime += Time.deltaTime;
            var newAmp = Mathf.Lerp(_currentAmp, _desiredAmp, currentTime / .15f);
            var newFre = Mathf.Lerp(_currentFre, _desiredFre, currentTime / .15f);
            var newSpe = Mathf.Lerp(_currentSpe, _desiredSpe, currentTime / .15f);
            
            _objectRenderer.material.SetFloat(AmplitudeID, newAmp);
            _objectRenderer.material.SetFloat(FrequencyID, newFre);
            _objectRenderer.material.SetFloat(SpeedID, newSpe);
            yield return null;
        }
    }
}
