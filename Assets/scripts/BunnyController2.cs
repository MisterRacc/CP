using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController2 : MonoBehaviour
{
    public float speed; // Velocidade de movimento do coelho
    private Transform target;
    private Level2LogicScript logic;
    private bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Level2LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(getIfHit()){
            setIfHit(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){ 
        if(collision.CompareTag("Fire") || collision.CompareTag("Enemy")){
            Debug.Log("Player hit");
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
