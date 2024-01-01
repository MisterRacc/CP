using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopperScript : MonoBehaviour
{
    private int time = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTimer()
    {
        time--;

        if(time == 0){
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
