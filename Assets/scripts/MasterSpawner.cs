using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSpawner : MonoBehaviour
{
    public float spawnRate;
    private float timer;
    public GameObject ls;
    public GameObject rs;

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
            if(Random.Range(0, 2) == 1) ls.GetComponent<LeftSpawner>().SpawnFromLeft();
            else rs.GetComponent<RightSpawner>().SpawnFromRight();

            timer = 0;
        }
    }
}
