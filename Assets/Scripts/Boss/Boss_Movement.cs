using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Movement : MonoBehaviour
{
    
    public int EnemySpeed;
    public int YMoveDirection;
    public int XMoveDirection=1;
    public bool moveRight;
    bool moveUp;
    private Color colorChangew = Color.white;

    
    public int viata = 30;
    private Renderer rend;
    private Color colorChange = Color.red;


    public Transform coindrop;
    public GameObject coinprefab;

    void Start()
    {
       
        InvokeRepeating("Dash", 10f, 10f);
        moveUp = true;
        moveRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Die
        if (viata == 0)
        {
            rend = GetComponent<Renderer>();
            rend.material.color = colorChange;
            Destroy(gameObject, 0.1f);
            StartCoroutine(coinspawn());
            StartCoroutine(coinspawn());
        }

        //change velocity
        if (!moveUp)
        {
            YMoveDirection = -1;
        }
        else
        {
            YMoveDirection = 1;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,YMoveDirection) * EnemySpeed;


       

    }

   

    //damage
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Bullet")

        {
            viata--;
            StartCoroutine(colorchange());
        }


        //turn up down


        if (col.gameObject.tag == "H_Wall" || col.gameObject.tag == "Player" || col.gameObject.tag == "Ground" )
        {
            if (YMoveDirection == 1)
            {
                moveUp = false;
            }
            else
            {
                moveUp = true;
            }
        }

        //turn left right
        if (col.gameObject.tag == "V_Wall")
        {
            if (XMoveDirection == 1)
            {
                moveRight = false;
                Vector2 localscale = gameObject.transform.localScale;
                localscale.x *= -1;
                transform.localScale = localscale;
            }
            else
            {
                moveRight = true;
                Vector2 localscale = gameObject.transform.localScale;
                localscale.x *= -1;
                transform.localScale = localscale;
            }
            
        }

    }

    void Dash()
    {
        if (!moveRight)
        {
            XMoveDirection = -1;
        }
        else
        {
            XMoveDirection = 1;
        }

        InvokeRepeating("Dashmove", 0.5f, 0.002f);
        
    }

    void Dashmove()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection,YMoveDirection) * EnemySpeed * 5;

    }

    IEnumerator colorchange()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorChange;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = colorChangew;
    }


    IEnumerator coinspawn()
    {
        Instantiate(coinprefab, coindrop.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
        yield return new WaitForSeconds(0.2f);
    }

}
