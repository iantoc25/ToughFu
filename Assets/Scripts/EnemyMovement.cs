using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyMovement : MonoBehaviour {
public int EnemySpeed;
public int XMoveDirection;
     bool moveRight;
    public int viata = 2;

  

    private Color colorChangew = Color.white;
    private Renderer rend;
    private Color colorChange = Color.red;

    public Transform coindrop;
    public GameObject coinprefab;
    public AudioSource audio;


    void Start()
    {
        moveRight = true;
        

    }

    // Update is called once per frame
    void Update()
    {
        // die
        if (viata == 0)
        {
            rend = GetComponent<Renderer>();
            rend.material.color = colorChange;
            Destroy(gameObject, 0.1f);

            StartCoroutine(coinspawn());

        }

        //switch velocity
        if (!moveRight)
        {
            XMoveDirection = 1;
        }
        else
        {
            XMoveDirection = -1;
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
    }

       
      
        //take damage
        void OnCollisionEnter2D(Collision2D col)
        {

        if (col.gameObject.tag == "Bullet")

        {
            viata--;
            StartCoroutine(colorchange());
            audio.Play();
        }





        //turn
        if (col.gameObject.tag == "V_Wall" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Player" || col.gameObject.tag == "HPup")
            {
                if (XMoveDirection == -1)
                {
                    moveRight = false;
                }
                else
                {
                    moveRight = true;
                }
            }
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

