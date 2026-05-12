using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public List<GameObject> Checkpoints { get; private set; } =  new List<GameObject>();
	
    protected int score = 0;

    void Awake()
    {        
        this.Checkpoints.AddRange(
            GameObject.FindGameObjectsWithTag("Checkpoint")
        );

        this.Checkpoints.Sort((a, b) => a.name.CompareTo(b.name));
    }

    private void AddScore()
    {
        score += 10;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AddScore();
            Debug.Log("Score: " + this.score);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            SwitchScene("Scene_2");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchScene("GuardScene");
        }
    }

    void SwitchScene(string sceneName = "Scene_2")
    {
        SceneManager.LoadScene(sceneName);
    }
}