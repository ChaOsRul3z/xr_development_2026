using System.Collections.Generic;
using UnityEngine;

public sealed class GameEnvironment
{
    private static GameEnvironment _instance;
    public List<GameObject> Checkpoints { get; private set; } =  new List<GameObject>();

    public static GameEnvironment Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameEnvironment();
                _instance.Checkpoints.AddRange(
                    GameObject.FindGameObjectsWithTag("Checkpoint")
                );

                Instance.Checkpoints.Sort((a, b) => a.name.CompareTo(b.name));
            }
            return _instance;
        }
    }
}
