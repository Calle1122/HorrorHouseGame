using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public static bool moneyShotBool = false;
    [SerializeField] private GameObject moneyShotObject;
    // Start is called before the first frame update
    void Start()
    {
        moneyShotObject.SetActive(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (moneyShotBool)
        {
            moneyShotBool = false;
            StartCoroutine(MoneyShot());
        }
    }

    private IEnumerator MoneyShot()
    {
        //yield return new WaitForSecondsRealtime(0.25f);
        moneyShotObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        moneyShotObject.SetActive(false);
    }
}
