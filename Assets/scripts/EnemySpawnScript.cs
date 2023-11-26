using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate;
    private float timer;
    private float maxHeight = -100;
    private float minHeight = -400;

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
        float randomHeight = Random.Range(-400, -100);

        GameObject enemy = Instantiate(enemyPrefab, new Vector3(1300, randomHeight, 0), Quaternion.identity);
        enemy.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        timer = 0;
    }
}
