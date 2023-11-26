using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public float moveSpeed = 150;
    public float counterMoveSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)){
            counterMovement();
        }
        else{
            movement();
        }
    }

    void movement(){
        transform.position += (Vector3.right*moveSpeed)*Time.deltaTime;
    }

    void counterMovement(){
        transform.position += (Vector3.left*counterMoveSpeed)*Time.deltaTime;
    }
}
