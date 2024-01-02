using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate;
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

        GameObject enemy = Instantiate(enemyPrefab, new Vector3(0, randomHeight, 0), Quaternion.identity);
        enemy.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform, false);
        
        timer = 0;
    }
}
