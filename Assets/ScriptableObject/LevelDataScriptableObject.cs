using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "ScriptableObjects/Create LevelData", order = 1)]
public class LevelDataScriptableObject : ScriptableObject
{
    public List<LevelDetails> levelDetails;
}

[System.Serializable]
public class LevelDetails
{
    public int score;
    public int noOfNewObstacles;
    public float maxSpeed;
}
