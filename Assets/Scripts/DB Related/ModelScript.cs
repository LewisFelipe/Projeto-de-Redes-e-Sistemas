using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelScript : MonoBehaviour
{
    public Text rank, name, entrie;

    public void SetEntries(string rank, string name, string entrie)
    {
        this.rank.text = rank;
        this.name.text = name;
        this.entrie.text = entrie;
    }
}
