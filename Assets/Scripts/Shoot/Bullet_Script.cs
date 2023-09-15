using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Script : MonoBehaviour
{
    public float bulletSpeed = 15f;
    public Rigidbody2D rb;
   
   

    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed * -1 ;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
       
        Destroy(gameObject);
    }




}
