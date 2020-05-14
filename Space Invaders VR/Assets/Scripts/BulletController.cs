using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public bool EnteredTrigger;
    Vector3 startPos;
    float traveleddistance;
    public float maxDist = 20;

    
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //remove kinematic
            GetComponent<Rigidbody>().isKinematic = false;

            //remove trigger
            GetComponent<Collider>().isTrigger = false;
            //Kill enemy and change enemy specific stuff
            other.gameObject.GetComponent<EnemyController>().KillEnemy();
            
        }
        else if (other.CompareTag("Graffiti"))
        {
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            gm.Initgame();
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        traveleddistance = Vector3.Distance(startPos, transform.position);
        if (traveleddistance > maxDist)
        {
            Destroy(gameObject);
        }
    }
}
