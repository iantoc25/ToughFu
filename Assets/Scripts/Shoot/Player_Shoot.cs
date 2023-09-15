using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    public float fireRate = 2f;
    public Transform firingPoint;
    public GameObject bulletPrefab;


    float timeUntilFire;
    Player_Movement pm;




    private void Start()
    {
        pm = gameObject.GetComponent<Player_Movement>();
    }


    private void Update()
{
 

        if (Input.GetKeyDown(KeyCode.X) && timeUntilFire < Time.time)
        {
            Shoot();
            timeUntilFire = Time.time + fireRate;

        }

    }



   
   

    void Shoot()
    {

        float angle = pm.isFacingRight ? 180f : 0f;
        Instantiate(bulletPrefab, firingPoint.position, Quaternion.Euler(new Vector3(0f, 0f, angle)));

    }
}