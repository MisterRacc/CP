using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    public float speed; // Velocidade de movimento do coelho
    public float changeDirectionInterval; // Intervalo para mudar a direção
    private float timeSinceLastDirectionChange;
    private bool isMovingUp = true;
    private bool isMovingLeft = true;
    private Transform target;
    public LogicScript logic;
    private bool areTouching = false;

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
        if(!logic.IsBunnyCaught()){
            MoveInsideTheMap();
        }
        else{
            MoveBehindPlayer();
        }

        Debug.Log(areTheyTouching());
    }

    // verifica se o player está em contacto com o coelho
    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            setTouching(true);
        }
    }

    // diz que o coelho e o player ja nao estao em contacto
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            setTouching(false);
        }
    }

    void MoveInsideTheMap(){
        // Atualiza o temporizador para mudar a direção
        timeSinceLastDirectionChange += Time.deltaTime;

        // Verifica se é hora de mudar a direção
        if (timeSinceLastDirectionChange >= changeDirectionInterval){
            // Escolhe uma nova direção aleatória
            if(Random.Range(0, 2) == 1){
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

    void MoveBehindPlayer(){
        if(Vector2.Distance(transform.position, target.position) > 3){
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);
        }
    }

    public void setTouching(bool boolean){
        areTouching = boolean;
    }

    public bool areTheyTouching(){
        return areTouching;
    }
}
