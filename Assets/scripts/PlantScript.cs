using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantScript : MonoBehaviour
{
    public Text timertext;
    public int initialTime;

    private PlantsSpawnerScript spawner;
    private LogicLevel5 logic;
    private int time;
    private PlayerLvl5Script ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel5>();
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
        if(!ps.GetPlantTimerStop()){
            time--;
            UpdateTimertext();

            if(time == 0){
                spawner.DestroyPlant(gameObject);
                logic.TakeDamage();
            }
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

    void OnTriggerStay2D(Collider2D collision){
        if(collision.CompareTag("Player")){
            if(ps.GetWaterDropped()){
                IncreaseTimer(10);
                ps.SetWaterDropped(false);
            }
        }
    }
}
