using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    public Transform player;
    public float speed = 200;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void FixedUpdate(){
        if(touchStart){
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f);
            moveCharacter(direction);
        }
    }

    // https://www.youtube.com/watch?v=uVxnvXonGXY fiquei nos 8 min mas tmb é imagens para o joystick depois vê-se isso

    void moveCharacter(Vector2 direction){
        if(player.position.x >= minX) player.Translate(direction*speed*Time.deltaTime);
        else player.position = new Vector3(minX, player.position.y, player.position.z);

        if(player.position.x <= maxX) player.Translate(direction*speed*Time.deltaTime);
        else player.position = new Vector3(maxX, player.position.y, player.position.z);

        if(player.position.y >= minY) player.Translate(direction*speed*Time.deltaTime);
        else player.position = new Vector3(player.position.x, minY, player.position.z);

        if(player.position.y <= maxY) player.Translate(direction*speed*Time.deltaTime);
        else player.position = new Vector3(player.position.x, maxY, player.position.z);
    }
}
