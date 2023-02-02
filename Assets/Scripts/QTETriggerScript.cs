using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTETriggerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject qTEObjectPrefab;

    private bool qteFailed = false;
    private bool qteWon = false;

    // Start is called before the first frame update
    void Start()
    {
        resultText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            GameObject qTEObject = Instantiate(qTEObjectPrefab);
            
        }
    }

}
