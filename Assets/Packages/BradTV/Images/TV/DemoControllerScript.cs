using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoControllerScript : MonoBehaviour
{
    [SerializeField] private GameObject glitchEffectA;
    public float glitchFrequency = 5f;
    //public float glitchRandomnessMultiplier = 1f;



    // Start is called before the first frame update
    void Start()
    {
        glitchEffectA.SetActive(false);
        StartCoroutine(GlitchRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator GlitchRoutine()
    {
        glitchEffectA.SetActive(true);
        float randoFloat = Random.Range(1, (int)glitchFrequency + 1);
        randoFloat = (randoFloat / glitchFrequency) + glitchFrequency;
        yield return new WaitForSecondsRealtime(randoFloat);
        glitchEffectA.SetActive(false);
        StartCoroutine(GlitchRoutine());
    }
}
