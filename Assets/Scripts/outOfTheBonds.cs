using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outOfTheBonds : MonoBehaviour
{
    private bool estaDentro;

    public GameObject luzPlayer;
    [SerializeField] private Animator myAnimationController;
    
    void OnTriggerEnter(){
        estaDentro = true;
    }

    void OnTriggerExit(){
        estaDentro = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(estaDentro == true){
            luzPlayer.SetActive(true);
            myAnimationController.SetBool("PlayerOut", false);
            
        } else {
            luzPlayer.SetActive(false);
            myAnimationController.SetBool("PlayerOut", true);
        }
    }
}
