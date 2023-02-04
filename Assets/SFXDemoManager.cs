using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using TMPro;

public class SFXDemoManager : MonoBehaviour
{
    [SerializeField] AudioSource cabinetDoorOpen;
    [SerializeField] AudioSource cabinetDoorUnlock;
    [SerializeField] AudioSource cabinetKeyInsert;
    [SerializeField] AudioSource couchSliding;
    [SerializeField] AudioSource frontDoorOpening;
    [SerializeField] AudioSource frontDoorUnlock;
    [SerializeField] AudioSource ghostInteract;
    [SerializeField] AudioSource ghostMovement;
    [SerializeField] AudioSource ghostPortal;
    [SerializeField] AudioSource humanFootsteps;
    [SerializeField] AudioSource humanInteract;
    [SerializeField] AudioSource pickupItem;
    [SerializeField] AudioSource thunkSound;
    [SerializeField] AudioSource UINavigate;
    [SerializeField] AudioSource UISelect;
    [SerializeField] AudioSource ZoomToCabinetTransition;

    public static bool cabinetDoorOpenBool = false;
    public static bool cabinetDoorUnlockBool = false;
    public static bool cabinetKeyInsertBool = false;
    public static bool couchSlidingBool = false;
    public static bool frontDoorOpeningBool = false;
    public static bool frontDoorUnlockBool = false;
    public static bool ghostInteractBool = false;
    public static bool ghostMovementBool = false;
    public static bool ghostPortalBool = false;
    public static bool humanFootstepsBool = false;
    public static bool humanInteractBool = false;
    public static bool pickupItemBool = false;
    public static bool thunkSoundBool= false;
    public static bool UINavigateBool = false;
    public static bool UISelectBool = false;
    public static bool zoomToCabinetTransitionBool = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageSFX();
    }

    private void PlayAudioClip(AudioSource clipToPlay)
    {
        clipToPlay.Play();
    }
    private void StopAudioClip(AudioSource clipToStop)
    {
        clipToStop.Stop();
    }

    private void ManageSFX()
    {
        // one-shots

        if (cabinetDoorOpenBool)
        {
            cabinetDoorOpenBool = false;
            PlayAudioClip(cabinetDoorOpen);
        }

        if (cabinetDoorUnlockBool)
        {
            cabinetDoorUnlockBool = false;
            PlayAudioClip(cabinetDoorUnlock);
        }

        if (cabinetKeyInsertBool)
        {
            cabinetKeyInsertBool = false;
            PlayAudioClip(cabinetKeyInsert);
        }

        if (couchSlidingBool)
        {
            couchSlidingBool = false;
            PlayAudioClip(couchSliding);
        }

        if (frontDoorOpeningBool)
        {
            frontDoorOpeningBool = false;
            PlayAudioClip(frontDoorOpening);
        }

        if (frontDoorUnlockBool)
        {
            frontDoorUnlockBool = false;
            PlayAudioClip(frontDoorUnlock);
        }

        if (ghostInteractBool)
        {
            ghostInteractBool = false;
            PlayAudioClip(ghostInteract);
        }

        if (ghostPortalBool)
        {
            ghostPortalBool = false;
            PlayAudioClip(ghostPortal);
        }

        if (humanInteractBool)
        {
            humanInteractBool = false;
            PlayAudioClip(humanInteract);
        }

        if (pickupItemBool)
        {
            pickupItemBool = false;
            PlayAudioClip(pickupItem);
        }

        if (thunkSoundBool)
        {
            thunkSoundBool = false;
            PlayAudioClip(thunkSound);
        }

        if (UINavigateBool)
        {
            UINavigateBool = false;
            PlayAudioClip(UINavigate);
        }

        if (UISelectBool)
        {
            UISelectBool = false;
            PlayAudioClip(UISelect);
        }

        if (zoomToCabinetTransitionBool)
        {
            zoomToCabinetTransitionBool = false;
            PlayAudioClip(ZoomToCabinetTransition);
        }

        // looping

        if (ghostMovementBool)
        {
            if (!ghostMovement.isPlaying)
            {
                PlayAudioClip(ghostMovement);
            }
        }

        if (!ghostMovementBool)
        {
            if (ghostMovement.isPlaying)
            {
                StopAudioClip(ghostMovement);
            }
        }

        if (humanFootstepsBool)
        {
            if (!humanFootsteps.isPlaying)
            {
                PlayAudioClip(humanFootsteps);
            }
        }

        if (!humanFootstepsBool)
        {
            if (humanFootsteps.isPlaying)
            {
                StopAudioClip(humanFootsteps);
            }
        }
    }
}
