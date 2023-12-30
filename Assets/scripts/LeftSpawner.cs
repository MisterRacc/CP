using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject trashPrefab;
    public GameObject heartPrefab;
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

    public void SpawnFromLeft(){
        GameObject entity = null;
        Debug.Log("Scene: " + SceneManager.GetActiveScene().name);
        if (SceneManager.GetActiveScene().name == "Level 3"){
            Debug.Log("Entrou 1");
            entity = Random.Range(0, 2) == 1 ? Instantiate(fishPrefab) : Instantiate(trashPrefab);
        }    
        else if (SceneManager.GetActiveScene().name == "Level 4"){
            Debug.Log("Entrou 2");
            entity = Random.Range(0, 7) == 1 ? Instantiate(heartPrefab) : Instantiate(trashPrefab);
        }
        else{
            Debug.Log("Scene: " + SceneManager.GetActiveScene().name);
        }       

        MovementsUnderwater mu = entity.GetComponent<MovementsUnderwater>();
        mu.DefineSpawn(true);
        entity.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform, false);
        entity.transform.position = new Vector3(spawnX, spawnY, 0);
    }
}
