using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    private float spawnX = -400;
    private float spawnY = 500;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int MoveDirection(){
        return 1;
    }

    public void SpawnFishiesFromLeft(){
        GameObject fish = Instantiate(fishPrefab);
        FishMovements fm = fish.GetComponent<FishMovements>();
        fm.DefineSpawn(true);
        fish.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        fish.transform.position = new Vector3(spawnX, spawnY, 0);
    }
}
