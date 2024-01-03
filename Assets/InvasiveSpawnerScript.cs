using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvasiveSpawnerScript : MonoBehaviour
{
    public GameObject InvasivePrefab;
    
    private float spawnRate = 8;
    private float timer;
    private float minX = -200;
    private float maxX = 850;
    private float minY = -280;
    private float maxY = -100;
    private PlayerLvl5Script ps;
    private LogicLevel5 logic;
    private int plantsCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel5>();
    }

    // Update is called once per frame
    void Update()
    {
        if(plantsCount < 1)
        {
            if(timer < spawnRate)
            {
                timer += Time.deltaTime;
            }
            else
            {
                if(!ps.GetPlantTimerStop())
                {
                    spawnPlants();
                }
            }
        }
    }

    void spawnPlants()
    {
        float randomX, randomY;
        Vector3 spawnPosition;

        do
        {
            randomX = Random.Range(minX, maxX);
            randomY = Random.Range(minY, maxY);
            spawnPosition = new Vector3(randomX, randomY, 0);
        } while(logic.IsTooCloseToOtherPlants(spawnPosition) || logic.IsInButtonsArea(spawnPosition));

        GameObject plant = Instantiate(InvasivePrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        plant.transform.SetParent(GameObject.FindGameObjectWithTag("Invasive Spawner").transform, false);

        logic.AddSpawnedPositions(spawnPosition);

        plantsCount++;
        spawnRate = Random.Range(8, 11);
        timer = 0;
    }

    public void DestroyPlant(GameObject plant)
    {
        Vector3 destroyedPosition = new Vector3(plant.transform.position.x, plant.transform.position.y, 0);

        logic.RemoveSpawnedPositions(destroyedPosition);
        Destroy(plant);
        plantsCount--;
    }

    public int GetInvasivePlantsCount()
    {
        return plantsCount;
    }
}
