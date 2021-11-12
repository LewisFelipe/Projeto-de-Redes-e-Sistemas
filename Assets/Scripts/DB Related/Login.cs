using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Realms;
using Realms.Sync;

public class Login : MonoBehaviour
{
    public static bool isLogged = false;
    public Text nick;
    public Text password;
    
    private Realm realm;
    private RankingModel rankingModel;
    
    private void OnEnable()
    {
        realm = Realm.GetInstance();
        rankingModel = realm.Find<RankingModel>(nick.text);
        if(rankingModel == null)
        {
            realm.Write(() => { rankingModel = realm.Add(new RankingModel(nick.text, password.text, -1, 0)); });
        }
    }

    private void OnDisable()
    {

    }

    public void OnPLay()
    {

    }
}
