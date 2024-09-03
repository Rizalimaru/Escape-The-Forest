using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priority = 10;
    [SerializeField]
    private float transitionTime = 0.5f;  // Kecepatan transisi
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;
    private CinemachineFramingTransposer virtualCameraFramingTransposer;
    private InputAction aimAction;
    private InputAction sprintAction;
    private CinemachineBrain cinemachineBrain;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCameraFramingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        aimAction = playerInput.actions["Aim"];
        sprintAction = playerInput.actions["Sprint"];

        if (cinemachineBrain != null)
        {
            SetBlendTime(transitionTime);
        }
    }

    void Update()
    {   
        // if (aimAction.ReadValue<float>() > 0)
        // {
        //     virtualCamera.Priority = priority;
        // }
        // else
        // {
        //     virtualCamera.Priority = 0;
        // }

        if(sprintAction.ReadValue<float>() > 0)
        {  
            virtualCameraNoise.m_AmplitudeGain = 3f;
            virtualCameraNoise.m_FrequencyGain = 1f;
        }
        else
        {   
            virtualCameraNoise.m_AmplitudeGain = 0.7f;
            virtualCameraNoise.m_FrequencyGain = 0.7f;
        }
    }

    void SetBlendTime(float time)
    {
        CinemachineBlendDefinition blend = new CinemachineBlendDefinition
        {
            m_Style = CinemachineBlendDefinition.Style.EaseInOut,
            m_Time = time
        };
        cinemachineBrain.m_DefaultBlend = blend;
    }
}
