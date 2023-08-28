using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.CinemachineTriggerAction.ActionSettings;

public class CameraControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera mainCamera;
    private GameMode gameMode;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    void Start()
    {
        GameDataMode gmode = GameManager.Instance.GetGameDataModeFromGameMode(gameMode);

        mainCamera.fieldOfView = gmode.fieldOfViewCam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
