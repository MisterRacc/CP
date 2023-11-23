// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using PlayFab;
// using PlayFab.ClientModels;
// using Newtonsoft.Json;
// using UnityEngine.UI;

// public class PlayfabManager : MonoBehaviour
// {

//     public Text messageText;

//     public InputField usernameInput, passwordInput;

//     public void RegisterButton(){
//         var request = new RegisterPlayFabUserRequest{
//             Username = usernameInput.text,
//             Password = passwordInput.text,
//             RequireBothUsernameAndEmail = false
//         };
//         PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
//     }

//     void OnRegisterSuccess(RegisterPlayFabUserResult result){
//         messageText.text = "Registered and logged in!";
//     }


//     public void LoginButton(){

//     }

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }

