using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantScript : MonoBehaviour
{
    public Text timertext;
    public int initialTime;

    private PlantsSpawnerScript spawner;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<PlantsSpawnerScript>();
        time = initialTime;
        UpdateTimertext();
        InvokeRepeating("UpdateTimer", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateTimer()
    {
        time--;
        UpdateTimertext();

        if(time == 0){
            spawner.DestroyPlant(gameObject);
        }
    }

    void UpdateTimertext()
    {
        timertext.text = $"{time.ToString()}";
    }

    public void IncreaseTimer(int amount)
    {
        time += amount;
        UpdateTimertext();
    }
}
