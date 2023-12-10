using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawnerScript : MonoBehaviour
{
    public GameObject leafPrefab;
    public float spawnRate;
    private float timer;
    private float minX = -800;
    private float maxX = 900;

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
            spawnLeaves();
        }
    }

    void spawnLeaves(){
        float randomX = Random.Range(minX, maxX);

        GameObject leaf = Instantiate(leafPrefab, new Vector3(randomX, 800, 0), Quaternion.identity);
        leaf.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);

        timer = 0;
    }
}
