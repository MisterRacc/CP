using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController2 : MonoBehaviour
{
    public float speed; // Velocidade de movimento do coelho
    public float changeDirectionInterval; // Intervalo para mudar a direção
    private float timeSinceLastDirectionChange;
    private bool isMovingUp = true;
    private bool isMovingLeft = true;
    private Transform target;
    private LogicScript logic;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastDirectionChange = 0;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
