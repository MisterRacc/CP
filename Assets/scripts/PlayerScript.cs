using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public LogicScript logic;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(getIfHit()){
            logic.setBunnyCaught(false);
            setIfHit(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Fire") || collision.CompareTag("Enemy")){
            logic.takeDamage();
            setIfHit(true);
        }
    }

    public void setIfHit(bool boolean){
        hit = boolean;
    }

    public bool getIfHit(){
        return hit;
    }
}
