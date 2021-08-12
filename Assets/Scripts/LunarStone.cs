using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunarStone : MonoBehaviour
{
    public static int lunarStones;

    private void OnTriggerEnter()
    {
        lunarStones++;
        Destroy(gameObject);
    }
}
