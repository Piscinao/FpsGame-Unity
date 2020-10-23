using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
   public bool hasBreathing = true;
   public float minHigh = -0.05f;
   public float maxHigh = 0.04f;

    // faz um arrasta e solta de valores
   [Range(0f, 5f)]
   public float breathForce = 1f;

   float movement;
  
    void Update()
    {
        if(hasBreathing)
        {
            // Lerp = fornece 2 valores e um tempo para que um valor alcance o outro em um determinado tempo
            movement = Mathf.Lerp(movement, maxHigh, Time.deltaTime * 1 * breathForce);
            transform.localPosition = new Vector3(transform.localPosition.x, movement, transform.localPosition.z);
            if(movement >= maxHigh - 0.01f)
            {
                // condicional que faz o inverso
                hasBreathing = !hasBreathing;
            }
        }

        else
        {
            // Lerp = fornece 2 valores e um tempo para que um valor alcance o outro em um determinado tempo
            movement = Mathf.Lerp(movement, minHigh, Time.deltaTime * 1 * breathForce);
            transform.localPosition = new Vector3(transform.localPosition.x, movement, transform.localPosition.z);
            if(movement <= minHigh + 0.01f)
            {
                // condicional que faz o inverso
                hasBreathing = !hasBreathing;
            }
        }
        
        // se a respiração for diferente de 0 então ele ta tendo força está respirando
        // força que a força chegue a 1 como se estive descansado
        if(breathForce != 0)
        {
            breathForce = Mathf.Lerp(breathForce, 1f, Time.deltaTime * 0.2f);
        }
        
    }
}
