using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float normalSpeed;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    private float currentSpeed;
    private float speedChangeDuration = 10;
    private float speedChangeTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // mexer o quadrado com as setas ou wasd
        // moveCharacter(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        // mexer quando seguras com o rato na tela ou com os dedos (😏)
        if(Input.GetMouseButtonDown(0)){
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        }

        if(Input.GetMouseButton(0)){
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        }
        else{
            touchStart = false;
        }

        // Verifica se a mudança de velocidade está ativa
        if (speedChangeTimer > 0.0f)
        {
            speedChangeTimer -= Time.deltaTime;

            // Se o temporizador atingir zero, restaura a velocidade normal
            if (speedChangeTimer <= 0.0f)
            {
                currentSpeed = normalSpeed;
            }
        }
    }

    private void FixedUpdate(){
        if(touchStart){
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);
        }
    }

    

    void moveCharacter(Vector2 direction){
        

        if (player.position.x >= minX) {
            changeDirection(direction);
            player.Translate(direction * GetSpeed() * Time.deltaTime);
        } else {
            player.position = new Vector3(minX, player.position.y, player.position.z);
        }

        if (player.position.x <= maxX) {
            changeDirection(direction);
            player.Translate(direction * GetSpeed() * Time.deltaTime);
        } else {
            player.position = new Vector3(maxX, player.position.y, player.position.z);
        }

        if (player.position.y >= minY) {
            changeDirection(direction);
            player.Translate(direction * GetSpeed() * Time.deltaTime);
        } else {
            player.position = new Vector3(player.position.x, minY, player.position.z);
        }

        if (player.position.y <= maxY) {
            changeDirection(direction);
            player.Translate(direction * GetSpeed() * Time.deltaTime);
        } else {
            player.position = new Vector3(player.position.x, maxY, player.position.z);
        }
    }

    private void changeDirection(Vector2 direction){
        Vector3 currentScale = player.localScale;

        float scaleX = currentScale.x;
        float scaleY = currentScale.y;
        float scaleZ = currentScale.z;


        if(direction.x < 0 && scaleX > 0){
            player.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        } 
        else if(direction.x > 0 && scaleX < 0){
            player.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        } 
    }   

    public float GetSpeed(){
        return currentSpeed;
    }

    public void ActivateSpeedBoost(float boostAmount)
    {
        currentSpeed += boostAmount;
        speedChangeTimer = speedChangeDuration;
    }
}
