using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class QTEShowcaseControllerScript : MonoBehaviour
{
    [SerializeField] private TMP_Text qTEResultText;
    private bool showResultBool = false;
    public string winText = "You Winned!!!";
    public string loseText = "You Lossed!!!";
    public static bool successBool = false;

    // Start is called before the first frame update
    void Start()
    {
        qTEResultText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        

        if (showResultBool)
        {
            if (successBool)
            {
                qTEResultText.text = winText;
            } else
            {
                qTEResultText.text = loseText;
            }

            qTEResultText.gameObject.SetActive(true);
        }
    }
}
