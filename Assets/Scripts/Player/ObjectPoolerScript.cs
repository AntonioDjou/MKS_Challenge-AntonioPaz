using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerScript : MonoBehaviour
{
    public static ObjectPoolerScript objPoolerScript;
    public GameObject bulletPooling;
    public int bulletAmount;
    //public bool dynamicPooling;

    private List<GameObject> bulletList;
    
    void Awake()
    {
        objPoolerScript = this; // Instance of the Object Pooler.
        
    }
    void Start()
    {
        bulletList = new List<GameObject>();
        for(int i = 0; i < bulletAmount; i++) // For the amount of Bullets specified...
        {
            GameObject bullet = Instantiate(bulletPooling); // Create the ObjectPooling.
            bullet.SetActive(false); // Set the bullets inactive.
            bulletList.Add(bullet); // Add the generated bullet into the Bullet List.
        }        
    }

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < bulletList.Count; i++) // For the Amount
        {
            if(!bulletList[i].activeInHierarchy) // If that object is inactive on Unity's Hierarchy
            {
                return bulletList[i]; // Retun the object from that index from the bullet List
            }
        }

        /*if(dynamicPooling)
        {
            GameObject bullet = Instantiate(bulletPooling); // Create the ObjectPooling.
            bulletList.Add(bullet); // Add the generated bullet into the Bullet List.
            return bullet;
        }*/
        return null;
    }
}
