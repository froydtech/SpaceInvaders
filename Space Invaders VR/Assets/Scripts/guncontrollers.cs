﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guncontrollers : MonoBehaviour
{
    //gun
    public GameObject gun;

    //bullet prefab
    public GameObject bulletPrefab;

    //bullet speed
    public float bulletSpeed;

    //object pool
    ObjectPool bulletPool;

    //grab the pool component

    private void Awake()
    {
        bulletPool = GetComponent<ObjectPool>();
    }



    // Update is called once per frame

    public void HandleInput()
    {

        //spawn a bullet
        GameObject newBullet = bulletPool.GetObj();

            //poisition will be that of the gun
            newBullet.transform.position = gun.transform.position;

            //get rigid body from bullet to apply things to
            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

            //give the bullet some velocity
            bulletRb.velocity = gun.transform.forward * bulletSpeed;

    }
}
