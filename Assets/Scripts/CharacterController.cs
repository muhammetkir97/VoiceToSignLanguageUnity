using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private Transform cameraTransform;


    void Start()
    {
        SetCharacterStatus(0); 

        SetCameraZoom(0.5f);
        //Invoke("LateSet",3);
    }

    void LateSet()
    {
        SetCharacterStatus(3);
    }

    void Update()
    {
        
    }

    public void SetCameraZoom(float zoomLevel)
    {
        float zPos = Mathf.Lerp(-3f, -0.8f, zoomLevel);
        Vector3 newCameraPos = new Vector3(0, 1.37f, zPos);

        iTween.MoveTo(cameraTransform.gameObject, newCameraPos, 0.3f);
    }

    public void SetCharacterStatus(int newStatus)
    {
        CharacterStatus tempStatus = (CharacterStatus) newStatus;
        float currentTalkBlend = characterAnimator.GetFloat("TalkBlend");
        float targetTalkBlend = 0;

        float currentIdleBlend = characterAnimator.GetFloat("IdleBlend");
        float targetIdleBlend = 0;

        float currentMainBlend = characterAnimator.GetFloat("IdleTalkBlend");
        float targetMainBlend = 0;

        switch(tempStatus)
        {
            case CharacterStatus.word1:
                targetTalkBlend = 0;
                targetMainBlend = 0;
                break;
            case CharacterStatus.word2:
                targetTalkBlend = 1;
                targetMainBlend = 0;
                break;
            case CharacterStatus.idle1:
                targetIdleBlend = 0;
                targetMainBlend = 1;
                break;
            case CharacterStatus.idle2:
                targetIdleBlend = 0.5f;
                targetMainBlend = 1;
                break;
            case CharacterStatus.idle3:
                targetIdleBlend = 1;
                targetMainBlend = 1;
                break;
            
        }

        iTween.ValueTo(gameObject, iTween.Hash("from", currentTalkBlend, "to", targetTalkBlend, "time", 0.3f, "onupdate", "SetTalkBlend"));
        iTween.ValueTo(gameObject, iTween.Hash("from", currentIdleBlend, "to", targetIdleBlend, "time", 0.3f, "onupdate", "SetIdleBlend"));
        iTween.ValueTo(gameObject, iTween.Hash("from", currentMainBlend, "to", targetMainBlend, "time", 0.3f, "onupdate", "SetMainBlend"));
    }

    public void SetIdleBlend(float newValue)
    {
        characterAnimator.SetFloat("IdleBlend", newValue);
    }

    public void SetTalkBlend(float newValue)
    {
        characterAnimator.SetFloat("TalkBlend", newValue);
    }

    public void SetMainBlend(float newValue)
    {
        characterAnimator.SetFloat("IdleTalkBlend",newValue);
    }
}



public enum CharacterStatus
{
    word1,
    word2,
    idle1,
    idle2,
    idle3
}
