using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private LogicScript logic;
    private bool hit = false;
    private bool fireResistance;
    private float fireDuration = 5;
    private float fireTimer = 0;
    private bool projectileResistance;
    private float projectileDuration = 5;
    private float projectileTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0.0f)
        {
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0.0f)
            {
                fireResistance = false;
            }
        }

        if (projectileTimer > 0.0f)
        {
            projectileTimer -= Time.deltaTime;

            if (projectileTimer <= 0.0f)
            {
                projectileResistance = false;
            }
        }

        if(getIfHit()){
            logic.setBunnyCaught(false);
            setIfHit(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){ 
        if(collision.CompareTag("Fire")){
            if(!GetFire()){
                Debug.Log("Player hit");
                logic.takeDamage(-1);
                setIfHit(true);
            }
        }
        else if(collision.CompareTag("Enemy")){
            if(!GetProjectile()){
                Debug.Log("Player hit");
                logic.takeDamage(-1);
                setIfHit(true);
            }
        }
    }

    public void setIfHit(bool boolean){
        hit = boolean;
    }

    public bool getIfHit(){
        return hit;
    }

    public void SetFire(){
        fireResistance = true;
        fireTimer = fireDuration;
    }

    public bool GetFire(){
        return fireResistance;
    }

    public void SetProjectile(){
        projectileResistance = true;
        projectileTimer = projectileDuration;
    }

    public bool GetProjectile(){
        return projectileResistance;
    }
}
