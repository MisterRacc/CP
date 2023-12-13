using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicLevel5 : MonoBehaviour
{
    private PlayerLvl5Script player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerInteractions(){
        if(player.GetInWaterArea()){
            player.AppearBucket();
        }
        else if(player.GetInContactWithPlant()){
            player.DisappearBucket();
            player.SetWaterDropped(true);
        }
    }
}
