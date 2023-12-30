using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RightSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public GameObject trashPrefab;
    public GameObject heartPrefab;
    private float spawnX = 2400;
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
        return -1;
    }

    public void SpawnFromRight(){
        GameObject entity = null;
        if (SceneManager.GetActiveScene().name == "Level 3"){
            entity = Random.Range(0, 2) == 1 ? Instantiate(fishPrefab) : Instantiate(trashPrefab);
        }    
        else if (SceneManager.GetActiveScene().name == "Level 4"){
            entity = Random.Range(0, 7) == 1 ? Instantiate(heartPrefab) : Instantiate(trashPrefab);
        }
        MovementsUnderwater mu = entity.GetComponent<MovementsUnderwater>();
        mu.DefineSpawn(false);
        entity.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform, false);
        entity.transform.position = new Vector3(spawnX, spawnY, 0);
    }
}
