using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantScript : MonoBehaviour
{
    public Text timertext;
    public int initialTime;

    private PlantsSpawnerScript spawner;
    private InvasiveSpawnerScript invasiveSpawner;
    private LogicLevel5 logic;
    private int time;
    private PlayerLvl5Script ps;
    private float timerCountdown = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel5>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<PlantsSpawnerScript>();
        invasiveSpawner = GameObject.FindGameObjectWithTag("Invasive Spawner").GetComponent<InvasiveSpawnerScript>();
        time = initialTime;
        UpdateTimertext();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ps.GetPlantTimerStop())
        {
            timerCountdown -= Time.deltaTime;

            if (timerCountdown <= 0)
            {
                UpdateTimer();
                timerCountdown = LoseHealth();
            }
        }
    }

    void UpdateTimer()
    {
        time--;
        UpdateTimertext();

        if (time == 0)
        {
            spawner.DestroyPlant(gameObject);
            logic.TakeDamage();
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

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(ps.GetWaterDropped())
            {
                IncreaseTimer(10);
                ps.SetWaterDropped(false);
            }
        }
    }

    float LoseHealth()
    {
        if(invasiveSpawner.GetInvasivePlantsCount() > 0)
        {
            return 0.5f;
        }
        else
        {
            return 1.0f;
        }
    }
}
