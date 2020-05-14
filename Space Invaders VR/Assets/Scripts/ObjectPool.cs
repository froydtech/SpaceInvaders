using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //pool of objects
    public GameObject poolPrefab;

    //initial number of elements
    public int initialNum = 10;

    //collectino
    List<GameObject> pooledObjects;

    void Awake()
    {
        //initialize the list
        pooledObjects = new List<GameObject>();

        //create this initial number of objects
        for (int i = 0; i < initialNum; i++)
        {
            CreateObj();
        }
    }

    GameObject CreateObj()
    {
        //create the new object
        GameObject newObj = Instantiate(poolPrefab);

        //set this object to inactive

        newObj.SetActive(false);
        pooledObjects.Add(newObj);

        return newObj;

    }

    public GameObject GetObj()
    {
        //search list for an inactive object
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                //enable it
                pooledObjects[i].SetActive(true);

                //return the object
                return pooledObjects[i];
            }
        }

        //if there were no inactive pooled objects, create a new pool
        GameObject newObj = CreateObj();
        newObj.SetActive(true);
        return newObj;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
