using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logoutManager : MonoBehaviour
{

    public Text logoutMsg;

    public void Logout()
    {

        PlayFabClientAPI.ForgetAllCredentials();
        logoutMsg.text = "Logging out...";
        StartCoroutine(Waiter());
        
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("AuthScreen");   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
