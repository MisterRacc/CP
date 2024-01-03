using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvasiveScript : MonoBehaviour
{
    public Text timertext;
    public int initialTime;

    private InvasiveSpawnerScript spawner;
    private LogicLevel5 logic;
    private int time;
    private PlayerLvl5Script ps;
    private float timerCountdown = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLvl5Script>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicLevel5>();
        spawner = GameObject.FindGameObjectWithTag("Invasive Spawner").GetComponent<InvasiveSpawnerScript>();
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
                timerCountdown = 1.0f;
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
        }
    }

    void UpdateTimertext()
    {
        timertext.text = $"{time.ToString()}";
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(ps.GetDestroyInvasive())
            {
                spawner.DestroyPlant(gameObject);
                ps.SetDestroyInvasive(false);
            }
        }
    }
}
