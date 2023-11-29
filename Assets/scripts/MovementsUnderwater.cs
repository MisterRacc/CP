using System.Collections;
using UnityEngine;

public class MovementsUnderwater : MonoBehaviour
{
    public float speed;
    public float changeDirectionInterval;

    private float timeSinceLastDirectionChange;
    private int isMovingUp;
    private int isMovingRight;
    private bool spawnFromLeft;
    private GameObject spawner;
    private float minY = 100;
    private float maxY = 800;
    private float deadZone;

    void Start()
    {
        timeSinceLastDirectionChange = changeDirectionInterval;

        if(spawnFromLeft){
            spawner = GameObject.FindGameObjectWithTag("LeftSpawner");
            isMovingRight = spawner.GetComponent<LeftSpawner>().MoveDirection();
            deadZone = 2400;
        }
        else{
            spawner = GameObject.FindGameObjectWithTag("RightSpawner");
            isMovingRight = spawner.GetComponent<RightSpawner>().MoveDirection();
            deadZone = -400;
        }
    }

    void Update()
    {
        MoveInsideTheMap();
    }

    void MoveInsideTheMap()
    {
        timeSinceLastDirectionChange += Time.deltaTime;

        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            isMovingUp = Random.Range(0, 2) == 1 ? 1 : -1;
            timeSinceLastDirectionChange = 0;
        }

        transform.position += Vector3.up*speed*isMovingUp*Time.deltaTime;
        transform.position += Vector3.right*speed*isMovingRight*Time.deltaTime;

        ClampPosition();

        CheckDeadzone();
    }

    void ClampPosition()
    {
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    public void DefineSpawn(bool boolean){
        spawnFromLeft = boolean;
    }

    void CheckDeadzone(){
        if(spawnFromLeft && transform.position.x > deadZone || !spawnFromLeft && transform.position.x < deadZone) Destroy(gameObject);
    }
}
