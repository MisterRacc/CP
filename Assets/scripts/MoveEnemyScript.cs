using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemyScript : MonoBehaviour
{
    public float moveSpeed = 150;
    private float deadZone = -1100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    private void move(){
        transform.position += (Vector3.left*moveSpeed)*Time.deltaTime;

        if(transform.position.x < deadZone){
            Destroy(gameObject);
        }
    }
}
