using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //public material switcheroo
    public Material deadMaterial;

    //movementrange

    public float rangeH = 5;
    public float rangeV = 1;

    //speed
    public float speed = 2;
    public float lifetime = 25;
    //direction

    int direction = 1;

    //accumulated movement
    float accMovement = 0;

    //available states (horizontal, vertical, dead)
    enum State { MovingHorizontally, MovingVertically, Dead }
    //keep track of current state

    State currState;


    // Start is called before the first frame update
    void Start()
    {
        //initial state
        currState = State.MovingHorizontally;
    }

    // Update is called once per frame
    void Update()
    {
        //no updates if'n we're dead
        if (currState == State.Dead)
        {
            return;
        }

        //calculate movement steps/speed
        float movement = speed * Time.deltaTime;
        //update the accumulated movement to check if limit reached to move down more
        accMovement += movement;

        //chechk movement horizontal/vertical

        if (currState == State.MovingHorizontally)
        {
            //check to see if we've moved beyond the range
            if (accMovement > rangeH)
            {
                //change state
                currState = State.MovingVertically;
                //change direction
                //direction *= -1;
                //reset acc movement
                accMovement = 0;
            }
            else
            {
                transform.position += transform.forward * movement * direction;
            }
        }
        else
        {
            if (accMovement > rangeV)
            {
                currState = State.MovingHorizontally;

                direction *= -1;

                accMovement = 0;
            }
            else
            {
                transform.position += Vector3.down * movement;
            }
        }
    }

    public void KillEnemy()
    {
        //check to see if dead
        if (currState == State.Dead)
        {

            return;        
        }

        //make dead
        currState = State.Dead;

        //remove kinematic
        GetComponent<Rigidbody>().isKinematic = false;

        //remove trigger
        GetComponent<Collider>().isTrigger = false;

        gameObject.tag = null;
        //material change
        gameObject.GetComponentInChildren<Renderer>().sharedMaterial = deadMaterial;


        EnemyManager gm2 = GameObject.FindObjectOfType<EnemyManager>();
        gm2.UpdateEnemies();
        GameManager gm = GameObject.FindObjectOfType<GameManager>();
        //update the score by one
        gm.Updatescore();
        if (gm2.ReturnEnemies() < 1)
        {
            gm2.StartNewWave();
        }

        Destroy(gameObject, lifetime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (currState == State.Dead)
        {
            return;
        }
        else if (other.CompareTag("Ground"))
        {
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            //update the score by one
            gm.GameOverGround();
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            //update the score by one
            gm.GameOverPlayer();
        }

    }
}
