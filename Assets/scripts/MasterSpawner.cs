using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    public float spawnRate;
    private float timer;
    public GameObject lfs;
    public GameObject rfs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < spawnRate){
            timer += Time.deltaTime;
        }
        else{
            if(Random.Range(0, 2) == 1){
                lfs.GetComponent<LeftFishSpawner>().SpawnFishiesFromLeft();
            }
            else{
                rfs.GetComponent<RightFishSpawner>().SpawnFishiesFromRight();
            }

            timer = 0;
        }
    }
}
