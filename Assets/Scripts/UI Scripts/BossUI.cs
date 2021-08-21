using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUI : MonoBehaviour
{
    public GameObject bossUI;

    void Start()
    {
        bossUI.transform.LeanMoveLocalY(-Screen.height - 400, 0f);
        bossUI.SetActive(false);        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bossUI.SetActive(true);
            bossUI.transform.LeanMoveLocalY(-450f, 0.5f).setEaseOutExpo().delay = 0.1f;
        }        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bossUI.transform.LeanMoveLocalY(-Screen.height - 400, 1.5f).setEaseInExpo().delay = 0.5f;
        }        
    }

}
