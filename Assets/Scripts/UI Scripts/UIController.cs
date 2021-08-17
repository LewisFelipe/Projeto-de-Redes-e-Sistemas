using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text lunarStonesCount;

    private void Update()
    {
        UpdateCount();
    }

    private void UpdateCount()
    {
        lunarStonesCount.text = LunarStone.lunarStones.ToString();
    }

}
