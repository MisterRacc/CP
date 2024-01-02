using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashScriptLvl1 : MonoBehaviour
{
    private LogicScript logic;
    private PlayerScript ps;
    private float spinVelocity = 100.0f;
    private float moveSpeed = 100.0f;
    private float amplitude = 1.0f;
    private Vector3 initialPosition;
    private float deadZone = -1100;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckIfPicked();

    }

    void Movement(){
        float movement = amplitude*Mathf.Sin(moveSpeed*Time.time);
        transform.position += new Vector3(0, movement, 0);
        transform.position += (Vector3.left*moveSpeed)*Time.deltaTime;
        transform.Rotate(Vector3.forward*spinVelocity*Time.deltaTime);

        if(transform.position.x < deadZone){
            Destroy(gameObject);
        }
    }

    void CheckIfPicked(){
        if(logic.GetCanDeleteTrash()){
            logic.SetCanDeleteTrash(false);
            ps.SetTouchingTrash(false);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){ 
        if(collision.CompareTag("Fire")){
            Destroy(gameObject);
        }
    }
}
