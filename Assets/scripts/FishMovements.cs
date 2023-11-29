using System.Collections;
using UnityEngine;

public class FishMovements : MonoBehaviour
{
    public float speed;
    public float changeDirectionInterval;

    private float timeSinceLastDirectionChange;
    private int isMovingUp;
    private int isMovingRight;
    private bool spawnFromLeft;
    private GameObject spawner;

    private float minY = 100f;
    private float maxY = 800f;

    void Start()
    {
        timeSinceLastDirectionChange = changeDirectionInterval;

        if(spawnFromLeft){
            spawner = GameObject.FindGameObjectWithTag("LeftSpawner");
            isMovingRight = spawner.GetComponent<LeftFishSpawner>().MoveDirection();
        }
        else{
            spawner = GameObject.FindGameObjectWithTag("RightSpawner");
            isMovingRight = spawner.GetComponent<RightFishSpawner>().MoveDirection();
        }

        Debug.Log(spawnFromLeft);
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
    }

    void ClampPosition()
    {
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    public void DefineSpawn(bool boolean){
        spawnFromLeft = boolean;
    }
}
