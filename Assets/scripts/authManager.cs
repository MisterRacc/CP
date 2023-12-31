using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class authManager : MonoBehaviour
{

    public Text logTxt;
    public Text usernameTxt;

    async void Start(){
        await UnityServices.InitializeAsync();
    }

    public async void SignIn(){
        
        await signInAnonymous();
    }

    async Task signInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            print("Sign in successful!");
            print("Player ID: " + AuthenticationService.Instance.PlayerId);

            int guestNumber = PlayerPrefs.GetInt("GuestNumber", 1);
            guestNumber++;
            PlayerPrefs.SetInt("GuestNumber", guestNumber);
            PlayerPrefs.SetInt("Score", 0);

            //Reset dos estados dos levels
            PlayerPrefs.SetInt("Level" + 1 + "Completed", 1);
            PlayerPrefs.SetInt("Level" + 2 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 3 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 4 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 5 + "Completed", 0);
            PlayerPrefs.SetInt("Level" + 6 + "Completed", 0);

            
            print("Guest Number: " + guestNumber);
            usernameTxt.text = "Welcome Guest" + guestNumber;

            StartCoroutine(Waiter());
   
        }
        catch (AuthenticationException e)
        {
            print("Sign in failed!");
            Debug.LogException(e);
        }
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StartScreen");     
    }

}
