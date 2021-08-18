using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarStone : MonoBehaviour
{
    public static int lunarStones;
    private UIController uiController;

    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            lunarStones++;
            uiController.UpdateCount();
            Destroy(gameObject);            
        }
    }
}
