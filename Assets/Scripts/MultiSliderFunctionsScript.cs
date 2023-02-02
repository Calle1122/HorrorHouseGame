using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSliderFunctionsScript : MonoBehaviour
{
    [SerializeField] private GameObject qTEObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void QTEWin()
    {

    }

    public void QTELose()
    {
        qTEObject.SetActive(false);
    }
}
