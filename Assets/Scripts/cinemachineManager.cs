using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cinemachineManager : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] UiManager uiManager;

    [Header("Objects")]
    public CinemachineBrain cinemachineBrain;
    public GameObject mainCamera, shopCamera , menuCamera;
  

    // Update is called once per frame
    void Update()
    {
        cinemachineGameStart();
    }

    public void cinemachineGameStart()
    {
        if (GameSetting.gamesettingInstance.OnstartGame)
        {
            if (!cinemachineBrain.IsBlending)
            {
                ICinemachineCamera menuCam = menuCamera.GetComponent<ICinemachineCamera>();
                bool menuCamLive = CinemachineCore.Instance.IsLive(menuCam);
                if (!menuCamLive)
                {
                    GameSetting.gamesettingInstance.startGame = true;
                    uiManager.OnPlayGame();
                }
            }
        }
    }
}
