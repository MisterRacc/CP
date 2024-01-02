using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawnerScript : MonoBehaviour
{
    public GameObject trashPrefab;
    
    private float spawnRate = 2.0f;
    private float timer;
    private float maxHeight = 150.0f;
    private float minHeight = -250.0f;

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
            spawnEnemies();
        }
    }

    void spawnEnemies(){
        float randomHeight = Random.Range(minHeight, maxHeight);

        GameObject trash = Instantiate(trashPrefab, new Vector3(0, randomHeight, 0), Quaternion.identity);
        trash.transform.SetParent(GameObject.FindGameObjectWithTag("TrashSpawner").transform, false);

        timer = 0;
        spawnRate = Random.Range(8, 11);
    }
}
