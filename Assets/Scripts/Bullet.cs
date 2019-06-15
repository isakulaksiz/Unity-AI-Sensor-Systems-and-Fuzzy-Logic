using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, 3f);
    }


    private void OnCollisionEnter(Collision collision)//Carptıgı gameobejct 
    {
       HealthBehaviour hb =  collision.gameObject.GetComponent<HealthBehaviour>();
        //Carpılan objenin sağlığı varmı yokmu?

        if (hb)//hb null değilse
        {
            hb.TakeDamage(20);
        }

        Destroy(gameObject, 0.5f);
    }
}
