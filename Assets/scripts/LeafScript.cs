using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafScript : MonoBehaviour
{
    public float speed;
    public float changeDirectionInterval;

    private float timeSinceLastDirectionChange;
    private int isMovingRight;
    private float minX = 100;
    private float maxX = 1800;
    private float deadZone = -200;
    private LogicLevel6 logic;

    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel6>();
    }

    // Update is called once per frame
    void Update()
    {
        FallDown();
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            if(logic.GetLeavesAmount() < 5){
                Destroy(gameObject);
                logic.SetLeavesCount(1);
            }
            else{
                Debug.Log("VAI DAR AS FOLHAS AOS TEUS FILHOTES!");
            }
        }
    }

    void FallDown(){
        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            isMovingRight = Random.Range(0, 2) == 1 ? 1 : -1;
            timeSinceLastDirectionChange = 0;
        }

        transform.position += Vector3.down*speed*Time.deltaTime;
        transform.position += Vector3.right*speed*isMovingRight*Time.deltaTime;

        ClampPosition();
        CheckDeadzone();
    }

    void ClampPosition()
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    void CheckDeadzone(){
        if(transform.position.y < deadZone) Destroy(gameObject);
    }
}
