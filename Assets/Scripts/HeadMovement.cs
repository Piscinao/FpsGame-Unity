using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadMovement : MonoBehaviour
{
    private float time = 0.0f;
    public float velocity = 0.15f;
    public float force = 0.1f;
    public float originPoint = 0.0f;
    
    float waveCuts;
    float horizontal;
    float vertical;
    Vector3 savePosition;

    // variaveis para sons de passos do personagem
    AudioSource audioSource;
    public AudioClip[] audioClip;
    public int stepsIndex;

     void Start()
     {
         audioSource = GetComponent<AudioSource>();
         stepsIndex = 0;
     }
    void Update()
    {
        waveCuts = 0.0f;
        horizontal= Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // pega a posição local do transform da posição
        savePosition = transform.localPosition;

        // função nativa unity retorna o valor absoluto mathabs, msm q for negativo retorna positivo
        // horizontal do getAxis retorna um valor entre -1 e 1 dependendo da força
        // se for =0 a 0 e =0 não estier apertando o tempo é igual a 0 dependendo do tempo
        // Não apertando nada vai resetar o tempo
        if(Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            time = 0.0f;
        }

        // Se apertar alguma coisa faz o seno do tempo

        else
        {
        // funçao nativa unity calcula o seno do tempo onde ele calcula o valor de -1 e -
            waveCuts = Mathf.Sin(time);
            time = time + velocity;

            // controla o tempo se nao o tempo fica somando infinitamente
            // assim q foi maior reseta e continua fazendo o calculo de movimentacao
            if(time > Mathf.PI * 2)
            {
                time = time - (Mathf.PI * 2);
            }
        }


        // se aperta alguma coisa ele cortaonda é diferente de 0
        // os calculos fazem a movimentacao da cabeça de subir e descer
        if(waveCuts != 0)
        {
            // controla o quanto a cabeça vai abaixar ou levantar conforme anda
            float movementChange = waveCuts * force;
            // mesma função do primeiro if
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            movementChange = totalAxes * movementChange;
            savePosition.y = originPoint + movementChange;
        } 
        // a cabeça para voltar pra posição de origem da camera
        else
        {
            savePosition.y = originPoint;
        }

     


        // sempre passa a posicao pro transform que contem o script pois é um update
        transform.localPosition = savePosition;

        // chama o metodo do som
        StepSounds();
        
    }
    
    // método que define o som dos passos
    void StepSounds()
    {
        // movimento da cabeça[wavecuts] recebe valor constantemente através do tempo 
        // quando seta no valor -0.95 a cabeça está mais próxima do chão, quase não tenha som no momento do passo
        if(waveCuts <= -0.95f && !audioSource.isPlaying)
        {
            audioSource.clip = audioClip[stepsIndex];
            // toca audio
            audioSource.Play();
            // acrescenta os sons
            stepsIndex++;
            if(stepsIndex >= 4)
            {
                stepsIndex = 0;
            }
        }
    }

}
