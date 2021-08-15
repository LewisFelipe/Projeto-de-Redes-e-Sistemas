using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class luzPlayer : MonoBehaviour
{
    [SerializeField] private Animator myAnimationController;
    
    private void OnTriggerEnter(Collider other){

        if (other.CompareTag("Player")){
            myAnimationController.SetBool("PlayerOut", false);
        }
        
    }

    private void OnTriggerExit(Collider other){

        if (other.CompareTag("Player")){
            myAnimationController.SetBool("PlayerOut", true);
        }
        
    }
}
