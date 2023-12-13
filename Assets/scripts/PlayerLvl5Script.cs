using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLvl5Script : MonoBehaviour
{
    private bool inRestrictedArea;
    private Vector3 safePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetRestricted()){
            safePosition = transform.position;
        }
        else{
            BackToWhereYouCanBe();
        }
    }

    void OnTriggerStay2D(Collider2D collision){
        if(collision.CompareTag("Restricted")){
            Setrestricted(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Restricted")){
            Setrestricted(false);
        }
    }

    void Setrestricted(bool boolean){
        inRestrictedArea = boolean;
    }

    bool GetRestricted(){
        return inRestrictedArea;
    }

    void BackToWhereYouCanBe(){
        transform.position = safePosition;
    }
}
