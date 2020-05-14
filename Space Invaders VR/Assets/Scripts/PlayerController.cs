using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //gun
    public GameObject gun;

    //bullet prefab
    public GameObject bulletPrefab;

    //bullet speed
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (Input.GetButton("Fire1"))
        {
            //spawn a bullet
            GameObject newBullet = Instantiate(bulletPrefab);

            //poisition will be that of the gun
            newBullet.transform.position = gun.transform.position;

            //get rigid body from bullet to apply things to
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

            //give the bullet some velocity
            bulletRb.velocity = gun.transform.forward * bulletSpeed;

        }
    }
}
