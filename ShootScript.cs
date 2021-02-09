using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    public Transform Gun;

    Vector2 direction;

    public GameObject Bullet;

    public float BulletSpeed;

    public Transform ShootPoint;

    public float fireRate;

    float ReadyForNextShot;

    public Animator GunAnimator;

    //handle camera shaking
    private Shake shake;

    void start() 
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePos - (Vector2)Gun.position;
        FaceMouse();

        if (Input.GetMouseButton(0))
        {
            if (Time.time > ReadyForNextShot) 
            {
                ReadyForNextShot = Time.time + 1 / fireRate;
                Shoot();
                //shake.camShake();
            }

        }
    }

    void FaceMouse()
    {
        Gun.transform.right = direction;
    }

    void Shoot() 
    {
        GameObject BulletIns = Instantiate(Bullet,ShootPoint.position,ShootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * BulletSpeed);
        GunAnimator.SetTrigger("Shoot");
        Destroy(BulletIns,2);
    }

}
