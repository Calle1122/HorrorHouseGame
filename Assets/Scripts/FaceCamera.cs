using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCam;

    private void Update()
    {
        transform.LookAt(mainCam.transform);
    }

    private void OnEnable()
    {
        mainCam = Camera.main;
    }
}