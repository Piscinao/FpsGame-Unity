using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float mouseSensibility = 100f;
    public float minAngle = -45f, maxAngle = 45f;

    public Transform transformPlayer;
    
    float rotation = 0f;
    void Start()
    {
        // Trava cursor
        Cursor.lockState = CursorLockMode.Locked;
        // Esconde cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;
        
        // soma ou subtrai conforme lida no mouse
        rotation -= mouseY;
        // rotacao trave no angulo minimo e angulo maximo sempre entre 90 ou -90
        rotation = Mathf.Clamp(rotation, minAngle, maxAngle);
        // quaternion metodo nativo que trabalha com o angulo da camera
        // transform da camera rotacao local dela, olha para cima e para baixo entre dois angulos
        transform.localRotation = Quaternion.Euler(rotation, 0, 0); 

        // Multiplica o valor de Y com o mouseX 
        transformPlayer.Rotate(Vector3.up * mouseX);
        
    }
}
