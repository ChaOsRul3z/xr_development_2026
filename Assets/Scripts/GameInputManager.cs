using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInputManager : MonoBehaviour
{
    [SerializeField] protected GameManager gameManager;

    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameManager.AddScore(10);
            Debug.Log("Score: " + gameManager.Score);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SceneExecutionWorker.Instance.SwitchScene("Scene_2");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneExecutionWorker.Instance.SwitchScene("GuardScene");
        }
    }
}