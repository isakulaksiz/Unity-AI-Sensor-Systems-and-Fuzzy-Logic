using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBehaviour : MonoBehaviour
{
    public Image healthBar;
    public Text healthTxt;
    float health=100f;
    
    public void TakeDamage(float amount)//Sınavda varmış
    {
       health -=health*(amount/100);
        healthTxt.text = string.Format("% {0}", health);
        healthBar.fillAmount = health / 100f;

        if (health <= 1 )
            Destroy(this.gameObject);
            

    }
}
