using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsSpawnerScript : MonoBehaviour
{
    public GameObject PlantPrefab;
    
    private float spawnRate = 3;
    private float timer;
    private float minX = -200;
    private float maxX = 850;
    private float minY = -200;
    private float maxY = -100;
    private float minDistance = 100;
    private List<Vector3> spawnedPositions = new List<Vector3>();

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
            spawnPlants();
        }
    }

    void spawnPlants(){
        float randomX, randomY;
        Vector3 spawnPosition;

        do{
            randomX = Random.Range(minX, maxX);
            randomY = Random.Range(minY, maxY);
            spawnPosition = new Vector3(randomX, randomY, 0);
        } while(IsTooCloseToOtherPlants(spawnPosition));

        GameObject plant = Instantiate(PlantPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        plant.transform.SetParent(GameObject.FindGameObjectWithTag("Spawner").transform, false);

        spawnedPositions.Add(spawnPosition);

        spawnRate = Random.Range(5, 11);
        timer = 0;
    }

    bool IsTooCloseToOtherPlants(Vector3 newPosition){
        foreach (Vector3 existingPosition in spawnedPositions){
            float distance = Vector3.Distance(newPosition, existingPosition);
            if (distance < minDistance){
                return true;
            }
        }
        return false;
    }

    public void DestroyPlant(GameObject plant){
        Vector3 destroyedPosition = new Vector3(plant.transform.position.x, plant.transform.position.y, 0);

        spawnedPositions.Remove(destroyedPosition);
        Destroy(plant);
    }
}
