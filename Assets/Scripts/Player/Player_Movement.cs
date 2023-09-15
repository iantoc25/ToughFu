using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    
    public int PlayerSpeed = 7;
   public bool FacingRight = false;
    public int PlayerJumpPower = 250;
    private float MoveX;
    public bool atinspejos = false;
    public int Nrsarituri = 0;
    public static int nivel ;
   
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    public bool isFacingRight = true;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;

    private bool dashlock=false;

    private static bool dashactive;






    void Start()
    {

        //make level 3 left exit visible in lvl 1
        rb = GetComponent<Rigidbody2D>();

        if (dashactive == true && SceneManager.GetActiveScene().buildIndex == 1)
            GameObject.FindWithTag("Scorbura").GetComponent<Renderer>().enabled = true;


    }




    void Update()
    {
        //destroy bosswall

        if (GameObject.FindWithTag("Boss") == null)

        {
            dashlock = true;

            Destroy(GameObject.FindWithTag("BossWall"));

        }


        Animatii();
        pauza();

        //stop boss from spawning in lvl 2.1
        if (dashactive == true)
            Destroy(GameObject.FindWithTag("Boss"));

        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        //jump

        if (Input.GetButtonDown("Jump") && Nrsarituri < 2)
        {
            atinspejos = false;
            Nrsarituri++;
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && dashlock  )
        {
            StartCoroutine(Dash());
        }

        Flip();
    }
    //dash
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

   
   //flip sprite
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }


    }

    private void OnCollisionEnter2D(Collision2D obiectatins)
    {
        //double jump counter
        if (obiectatins.gameObject.tag == "Ground")
        {
            atinspejos = true;
            Nrsarituri = 0;

        }
        //level endings
        if (obiectatins.gameObject.tag == "EndOfLevel")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            

        }


        if (obiectatins.gameObject.tag == "Eol3l")
        {
            SceneManager.LoadScene("Level1");
            dashactive = true;
           

        }

        if (obiectatins.gameObject.tag == "Eol3r")
        {
            SceneManager.LoadScene("Level2.1");



        }

        if (obiectatins.gameObject.tag == "Eol1l")
        {
            SceneManager.LoadScene("Level3.1");
          


        }


        if (obiectatins.gameObject.tag == "Eol2l")
        {
            SceneManager.LoadScene("Level3");



        }





    }
    

    

    void Animatii()
    {
        if (atinspejos == false)
            GetComponent<Animator>().SetBool("isjumping", true);
        else
            GetComponent<Animator>().SetBool("isjumping", false);

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && atinspejos == true)
            GetComponent<Animator>().SetBool("iswalking", true);
        else
            GetComponent<Animator>().SetBool("iswalking", false);
    }

   

   public void pauza()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            nivel = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene("Menu");
        }
        


    }

   public void resume()
    {
       
        SceneManager.LoadScene(nivel); 
    }

    public void play()
    {
        SceneManager.LoadScene("Level1");
        nivel = 1;
    }



    private IEnumerator Dash()
    {
        canDash = false;     
        isDashing = true;
        float originalGravity = rb.gravityScale; 
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}