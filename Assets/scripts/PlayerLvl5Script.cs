using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLvl5Script : MonoBehaviour
{
    public GameObject bucket;

    private bool inRestrictedArea;
    private Vector3 safePosition;
    private bool inWaterArea;
    private bool inContactWithPlant;
    private bool WaterDropped;
    private bool plantTimerStop;
    private float stopTimer;
    private float stopDuration = 10;
    private bool inContactWithInvasive;
    private bool destroyInvasive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GetRestricted())
        {
            safePosition = transform.position;
        }
        else
        {
            BackToWhereYouCanBe();
        }

        if(stopTimer > 0)
        {
            stopTimer -= Time.deltaTime;

            if (stopTimer <= 0.0f)
            {
                plantTimerStop = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("StopPlantsTimer"))
        {
            SetPlantTimerStop();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Restricted"))
        {
            Setrestricted(true);
        }
        else if(collision.CompareTag("WaterZone"))
        {
            SetInWaterArea(true);
        }
        else if(collision.CompareTag("Plants"))
        {
            SetContactWithPlant(true);
        }
        else if(collision.CompareTag("Invasive"))
        {
            SetInContactWithInvasive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Restricted"))
        {
            Setrestricted(false);
        }
        else if(collision.CompareTag("WaterZone"))
        {
            SetInWaterArea(false);
        }
        else if(collision.CompareTag("Plants"))
        {
            SetContactWithPlant(false);
        }
    }

    void Setrestricted(bool boolean)
    {
        inRestrictedArea = boolean;
    }

    bool GetRestricted()
    {
        return inRestrictedArea;
    }

    public void SetInWaterArea(bool boolean)
    {
        inWaterArea = boolean;
    }

    public bool GetInWaterArea()
    {
        return inWaterArea;
    }

    public void SetContactWithPlant(bool boolean)
    {
        inContactWithPlant = boolean;
    }

    public bool GetInContactWithPlant()
    {
        return inContactWithPlant;
    }

    public void SetInContactWithInvasive(bool boolean)
    {
        inContactWithInvasive = boolean;
    }

    public bool GetInContactWithInvasive()
    {
        return inContactWithInvasive;
    }

    public void SetWaterDropped(bool boolean)
    {
        WaterDropped = boolean;
    }

    public bool GetWaterDropped()
    {
        return WaterDropped;
    }

    void BackToWhereYouCanBe()
    {
        transform.position = safePosition;
    }

    public void AppearBucket()
    {
        bucket.SetActive(true);
    }

    public void DisappearBucket()
    {
        bucket.SetActive(false);
    }

    public void SetPlantTimerStop()
    {
        plantTimerStop = true;
        stopTimer = stopDuration;
    }

    public bool GetPlantTimerStop()
    {
        return plantTimerStop;
    }

    public void SetDestroyInvasive(bool boolean)
    {
        destroyInvasive = boolean;
    }

    public bool GetDestroyInvasive()
    {
        return destroyInvasive;
    }
}
