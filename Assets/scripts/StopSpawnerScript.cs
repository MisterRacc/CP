using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpawnerScript : MonoBehaviour
{
    public GameObject stopPrefab;

    private int spawnprob = 5;
    private float minX = -200;
    private float maxX = 850;
    private float minY = -280;
    private float maxY = -100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int chance = Random.Range(1, 10001);

        if(chance <= spawnprob){
            spawnStop();
        }
    }

    void spawnStop(){
        float randomX, randomY;
        Vector3 spawnPosition;

        do{
            randomX = Random.Range(minX, maxX);
            randomY = Random.Range(minY, maxY);
            spawnPosition = new Vector3(randomX, randomY, 0);
        } while(IsInButtonsArea(spawnPosition));

        GameObject stop = Instantiate(stopPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        stop.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
    }

    bool IsInButtonsArea(Vector3 position){
        if(position.y <= -200 && position.x >= 200) return true;
        return false;
    }
}
