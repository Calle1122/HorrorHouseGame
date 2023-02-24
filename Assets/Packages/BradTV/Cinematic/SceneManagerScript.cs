using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using TMPro;

public class SceneManagerScript : MonoBehaviour
{
    private Keyboard myKB;
    public static CinemachineVirtualCamera currentCam;

    [SerializeField] private CinemachineVirtualCamera startCam;
    [SerializeField] private List<CinemachineVirtualCamera> myVCams = new List<CinemachineVirtualCamera>();
    [SerializeField] private List<float> panelDurationList = new List<float>();
    [SerializeField] private GameObject transitionEffect;
    [SerializeField] private TMP_Text minutesText;
    [SerializeField] private TMP_Text secondsText;
    [SerializeField] private TMP_Text camText;

    private int cameraIndex = 0;
    private int panelTimeIndex = 0;
    private float secondsCounter = 0f;
    private float minutesCounter = 0f;

    private void Awake()
    {
        myKB = Keyboard.current;
    }

    // Start is called before the first frame update
    void Start()
    {
        transitionEffect.SetActive(false);
        SwitchToCam(startCam);
        StartCoroutine(BradsTimer());
        StartCoroutine(CamSequence());
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (myKB.spaceKey.wasPressedThisFrame)
        {
            cameraIndex = (cameraIndex + 1) % myVCams.Count;

            SwitchToCam(myVCams[cameraIndex]);
        }
        */
        if (myKB.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        secondsText.text = secondsCounter.ToString("00");
        minutesText.text = minutesCounter.ToString("00") + ":";
        camText.text = currentCam.gameObject.name;
    }

    public void SwitchToCam(CinemachineVirtualCamera newCurrentCam)
    {
        /*
        if(newCurrentCam.Priority != 10)
        {
            foreach(CinemachineVirtualCamera cammy in myVCams)
            {
                cammy.Priority = 0;
            }
        }
        */
        if(newCurrentCam.gameObject.tag == "HardCutTo")
        {
            TransitionEffect();
        }
        newCurrentCam.Priority = 10;
        currentCam = newCurrentCam;
    }

    public void TransitionEffect()
    {
        transitionEffect.SetActive(false);
        transitionEffect.SetActive(true);
    }

    private IEnumerator BradsTimer()
    {
        yield return new WaitForSecondsRealtime(1f);
        secondsCounter++;
        secondsCounter = secondsCounter % 60;
        if(secondsCounter == 0)
        {
            minutesCounter++;
        }
        StartCoroutine(BradsTimer());
    }

    private void BradsCameraCuts()
    {

    }

    private IEnumerator CamSequence()
    {
        yield return new WaitForSecondsRealtime(panelDurationList[panelTimeIndex]);
        SwitchToCam(myVCams[cameraIndex + 1]);
        cameraIndex++;
        panelTimeIndex++;
        StartCoroutine(CamSequence());
        
    }

}
