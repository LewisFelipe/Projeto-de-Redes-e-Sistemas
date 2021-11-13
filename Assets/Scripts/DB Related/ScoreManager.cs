using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using Realms.Sync;

public class ScoreManager : MonoBehaviour
{
    public static double runTime{get; private set;}
    public static int score;
    private void Start()
    {
        score = 0;
    }

    private void Update()
    {
        runTime += Time.deltaTime;
    }

    private void OnRunEnd()
    {
        int highestScore = 0; //Temporário
        if(score > highestScore)
        {
            highestScore = score;
        }

        double bestRun = 0; //Temporário
        if(runTime < bestRun || bestRun == -1)
        {
            bestRun = runTime;
        }
    }
}
