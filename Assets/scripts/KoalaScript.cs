using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoalaScript : MonoBehaviour
{
    private LogicLevel6 logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel6>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Babys")){
            logic.IncreaseTimer(2*logic.GetLeavesAmount()); // increases babys lifetime 2 sec for each leave caught
            logic.ResetLeaveCounter();
        }
    }
}
