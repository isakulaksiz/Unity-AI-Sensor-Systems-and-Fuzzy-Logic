using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletPrefab;
    float moveSpeed = 1000f;


    float timeFromLastShoot;
    
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        //Qaternion rotasyon identity birim matris boş bırakamadığımız için eklendi
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
    }


    public void Shoot(float shootFreq)
    {
        if ((timeFromLastShoot+=Time.deltaTime)>= 1/shootFreq)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
            timeFromLastShoot = 0;
        }
        
    }


}
