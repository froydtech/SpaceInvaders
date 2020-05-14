using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //the number of enemies on Z axis
    public int numZ = 0;

    //number of enemies on X axis
    public int numX = 0;

    //separation between enemies
    public float separation;

    public GameObject enemyPrefab;

    //number of enemies
    

    int counter = 0;
    int counter2 = 0;
    int layercount = 0;
    public int layers = 1;
    public int numEnemies;



    public void CreateEnemyWave()
    {        
        layercount = 0;
        counter = 0;
        counter2 = 0;

        Vector3 startPos = transform.position;
        while (layercount < layers)
        {        
            while (counter < numX)
            {
                while (counter2 < numZ)
                {
                    //spawn enemy
                    GameObject newEnemy = Instantiate(enemyPrefab);

                    newEnemy.transform.position = new Vector3(startPos.x + separation * counter2, startPos.y + separation * layercount, startPos.z + separation * counter);
                    counter2++;
                }
                counter++;
                counter2 = 0;
            }
            layercount++;
            counter = 0;
            counter2 = 0;
        }        
    }


    // Start is called before the first frame update
    void Start()
    {
        //CreateEnemyWave();
        numEnemies = numZ * numX * layers;
    }

    // Update is called once per frame
    public void StartNewWave()
    {
        numZ++;
        numX++;
        layers++;
        numEnemies = numZ * numX * layers;
        CreateEnemyWave();
            
    }

    public void UpdateEnemies()
    {
        numEnemies--;

    }

    public int ReturnEnemies()
    {
        return numEnemies;
    }
}
