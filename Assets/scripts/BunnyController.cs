using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    public float speed; // Velocidade de movimento do NPC
    public float changeDirectionInterval; // Intervalo para mudar a direção

    private float timeSinceLastDirectionChange;
    private bool isMovingUp = true;
    private bool isMovingLeft = true;

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastDirectionChange = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveInsideTheMap();
    }

    void MoveInsideTheMap()
    {
        // Atualiza o temporizador para mudar a direção
        timeSinceLastDirectionChange += Time.deltaTime;

        // Verifica se é hora de mudar a direção
        if (timeSinceLastDirectionChange >= changeDirectionInterval)
        {
            // Escolhe uma nova direção aleatória
            if(Random.Range(0, 2) == 1)
            {
                isMovingUp = !isMovingUp;
            }

            if(!isMovingLeft){
                if(Random.Range(0, 10) == 1){
                    isMovingLeft = !isMovingLeft;
                }
            }
            else{
                if(Random.Range(0, 2) == 1){
                    isMovingLeft = !isMovingLeft;
                }
            }

            // Reseta o temporizador
            timeSinceLastDirectionChange = 0f;
        }

        // Calcula o deslocamento com base na direção atual
        float verticalMovement = isMovingUp ? -speed : speed;
        float horizontalMovement = isMovingLeft ? -speed : speed;

        // Move o bicho
        transform.Translate(new Vector3(-horizontalMovement, verticalMovement, 0) * Time.deltaTime);

        // restringe o movimento do animal para nao fugir do mapa
        float clampedX = Mathf.Clamp(transform.position.x, 100, 1800); 
        float clampedY = Mathf.Clamp(transform.position.y, 250, 400);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
