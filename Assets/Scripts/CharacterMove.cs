using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BASA
{
public class CharacterMove : MonoBehaviour
{
    public CharacterController control;
    // Velocidade global do personagem
    public float velocity = 6f;
    // Altura máxima do personagem
    public float jumpHigh = 3f;
    // Gravidade global
    private float gravity = -20f;

    private int extraJump = 1;

    // Componente próximo ao pe do personagem que checa se está nm chão ou não
    public Transform checkFloor;   
    // Raioesfera checa se a esfera ta no chão ou não, se pode ou não pular agaixar
    public float sphereRay = 0.4f;

    // Classifica um objeto consegue trabalhar com essa layer com um tipo especifico consegue saber
    // quando o personagem pisa ou não no chão atraves dessa layer
    public LayerMask floorMask;
    // booleano que checa se esta ou não no chão
    public bool onTheFloor;
    // Velocidade que o personagem estará caindo não precisa colocar publico
    Vector3 velocityFall;

    // Variaveis para movimentação de Abaixar
    public Transform cameraTransform;
    public bool isLowered;
    // Bloqueia o levantar
    public bool liftBlocked;
    public float liftedHeight, heightLowered, standingCameraPosition, cameraPositionLowered;
    //raio de alinhamento para verificar se em objeto em cima ou não
    RaycastHit hit;

    void Start()
    {
        // quando começar ele vai pegar o controller e atribuir ao componente
        control = GetComponent<CharacterController>();
        // define que o personagem não começa abaixado
        isLowered = false;
        // busca camera principal que está em cena
        cameraTransform = Camera.main.transform;
        
    }
    void Update()
    {
        // Chama a cada frame do jogo 
        // cria esfera na posição do checa chão e ele identifica uma determinada layer
        // se estiver em contato com a layer esta no chão é true do contrário false
        // Physics.CheckSphere método default
        onTheFloor = Physics.CheckSphere(checkFloor.position, sphereRay, floorMask);

        // suaviza o pulo na caida de acordo com a gravidade
        // a força da gravidade sempre estará em -2
        if(onTheFloor && velocityFall.y < 0)
        {
            velocityFall.y = -2f;
        }

        // retorna 1 ou -1 dependendo do botão para verificar comandos project settings
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // manipula basicamente o transform do objeto manda o componente para direita
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        // basicamente retorna o movimento de acordo com a velocidade e o metodo default Move e deltaTime
        control.Move(move * velocity * Time.deltaTime);

        // se pressionar o botão jump(space) e estiver no chão então


        if(Input.GetButtonDown("Jump") && extraJump > 0)

        // if(Input.GetButtonDown("Jump") && onTheFloor == true)

        // Tira a raiz quadrada do valor e faz de acordo com a velocidade
        {
             extraJump--;
            velocityFall.y = Mathf.Sqrt(jumpHigh * -2f* gravity);
        }

        if(onTheFloor){
            extraJump = 1;
        }

        velocityFall.y += gravity * Time.deltaTime;

        control.Move(velocityFall * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            goDown();
        }

        
    }

    // método para abaixar

    void goDown()
    {
        isLowered =  !isLowered;

        if(isLowered)
        {
            control.height = heightLowered;
            // Manipula a camera quando for abaixar
            cameraTransform.localPosition =new Vector3(0, cameraPositionLowered, 0);
        }

        else
        {
            control.height = liftedHeight;
            // Manipula a camera quando for levantar
            cameraTransform.localPosition = new Vector3(0, standingCameraPosition, 0);
        }
        


    }

    // Método nativo simplesmente desenha a esfera
        void OnDrawGizmosSelected() 
    {
        // amarelo o que for desenhar
        Gizmos.color = Color.yellow;
        // define o que quer desenhar, esfera irá estar na posição checkFloor no raio da esfera
        Gizmos.DrawSphere(checkFloor.position, sphereRay);
    }
}
}