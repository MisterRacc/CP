using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class createAccountManager : MonoBehaviour
{
    public void CreateAccountRedirect(){
        SceneManager.LoadScene("CreateAccountScreen");  
    }
}
