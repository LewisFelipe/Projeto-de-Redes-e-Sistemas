using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthFlower : MonoBehaviour
{
    
    public static int healthFlower;
    private UIController uiController;
    private void Start()
    {
        uiController = FindObjectOfType<UIController>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            healthFlower++;
            uiController.UpdateCount();
            Destroy(gameObject);            
        }
    }    
}
