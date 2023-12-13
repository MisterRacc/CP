using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLvl5Script : MonoBehaviour
{
    public GameObject bucket;

    private bool inRestrictedArea;
    private Vector3 safePosition;
    private bool inWaterArea;
    private bool inContactWithPlant;
    private bool WaterDropped;

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
        else if(collision.CompareTag("WaterZone")){
            SetInWaterArea(true);
        }
        else if(collision.CompareTag("Plants")){
            SetContactWithPlant(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.CompareTag("Restricted")){
            Setrestricted(false);
        }
        else if(collision.CompareTag("WaterZone")){
            SetInWaterArea(false);
        }
        else if(collision.CompareTag("Plants")){
            SetContactWithPlant(false);
        }
    }

    void Setrestricted(bool boolean){
        inRestrictedArea = boolean;
    }

    bool GetRestricted(){
        return inRestrictedArea;
    }

    public void SetInWaterArea(bool boolean){
        inWaterArea = boolean;
    }

    public bool GetInWaterArea(){
        return inWaterArea;
    }

    public void SetContactWithPlant(bool boolean){
        inContactWithPlant = boolean;
    }

    public bool GetInContactWithPlant(){
        return inContactWithPlant;
    }

    public void SetWaterDropped(bool boolean){
        WaterDropped = boolean;
    }

    public bool GetWaterDropped(){
        return WaterDropped;
    }

    void BackToWhereYouCanBe(){
        transform.position = safePosition;
    }

    public void AppearBucket(){
        bucket.SetActive(true);
    }

    public void DisappearBucket(){
        bucket.SetActive(false);
    }
}
