using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text lunarStonesCount;
    public TMP_Text healthFlower;

    private void Update()
    {
        UpdateCount();
    }

    public void UpdateCount()
    {
        lunarStonesCount.text = LunarStone.lunarStones.ToString();
        healthFlower.text = HealthFlower.healthFlower.ToString();
    }

}
