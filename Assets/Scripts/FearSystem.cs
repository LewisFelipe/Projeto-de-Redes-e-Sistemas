using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FearSystem : MonoBehaviour
{
    public GameObject[] campFirePosition;
    List<float> distance = new List<float>();    
    Transform player;
    static float fearState;
    public float maxDistanceThreshold, minDistanceThreshold;

    float TestDistance()
    {
        float smallestDistance;
        for (int campFireIndex = 0; campFireIndex < campFirePosition.Length; campFireIndex++)
        {
            distance.Add(Vector3.Distance(player.position, campFirePosition[campFireIndex].transform.position));
        }
        distance.Sort();
        smallestDistance = distance[0];
        distance.Clear();
        return smallestDistance;
    }

    void FearManager()
    {
        if(TestDistance() >= maxDistanceThreshold || TestDistance() <= minDistanceThreshold)
        {
            fearState = Mathf.InverseLerp(TestDistance(), minDistanceThreshold, maxDistanceThreshold);
        }
        else
        {
            fearState = 0;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        campFirePosition = GameObject.FindGameObjectsWithTag("CampFire");
    } 
}
