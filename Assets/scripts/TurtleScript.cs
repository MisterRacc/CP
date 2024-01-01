using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleScript : MonoBehaviour
{
    private LogicLevel4 logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel4>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Trash")){
            logic.TakeDamage(); 
        }
        else if(collision.CompareTag("SeaStar")){
            logic.IncreaseScore();
            collision.gameObject.SetActive(false);
        } 
        else if(collision.CompareTag("Heart")){
            logic.increaseLives(1);
            collision.gameObject.SetActive(false);
        }
    }
}
