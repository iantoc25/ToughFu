using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player_Score : MonoBehaviour
{
    public Text playerHpUI;
    public Text playerMoneyUI;
    public static int playerscore = 0;
    public static  int j;
    public static int viata = 7;


    private Color colorChange = Color.red;
    private Renderer rend;
    private Color colorChangew = Color.white;

    public static bool hpcheck = true;

    void Start()

    {
       
        j = playerscore;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!hpcheck)
        {
            Destroy(GameObject.FindWithTag("HPup"));
            Destroy(GameObject.FindWithTag("HPuptext"));
        }


        updatemoney();
        updatehp();
       mori();
        esc();
    }


    private void OnTriggerEnter2D(Collider2D trig)

    {
       

        //hp up 

        if (trig.gameObject.tag == "HPup")
        {
            viata = viata + 1;
            Destroy(trig.gameObject);
            hpcheck = false;

        }




    }



    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Boss_Bullet")
        {
            viata = viata - 1;
            if (viata > 0) 
            StartCoroutine(colorchange());
        }

        if (col.gameObject.tag == "Boss")
        {
            viata = viata - 2;
            if (viata > 0)
            StartCoroutine(colorchange()); }



        if (col.gameObject.tag == "Spikes" )
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (playerscore > 9)
            playerscore = playerscore - 10;
            viata--;
           
        }


        if (col.gameObject.tag == "Enemy")
        {
            viata = viata - 1;
            StartCoroutine(colorchange()); 
        }


        if (col.gameObject.tag == "Coin")
        {
            playerscore = playerscore + 1;
            Destroy(col.gameObject);

        }

    }



    void mori()
    {
        if (viata < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            viata = 7;
        }
    }

   


    void updatehp()
    {
        playerHpUI.text = ("Hp: " + viata);
    }

    void updatemoney()
    {
        playerMoneyUI.text = ("Rice: " + playerscore);
    }


    public void esc()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            playerscore = j;
    }


    IEnumerator colorchange()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colorChange;
        yield return new WaitForSeconds(0.2f);
        rend.material.color = colorChangew;
    }



}
