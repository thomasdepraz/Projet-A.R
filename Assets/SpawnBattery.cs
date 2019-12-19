using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBattery : MonoBehaviour
{
    public GameObject[] Batteries;
    Vector3 SpawnPos;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < Batteries.Length; i++)
        {
            SpawnPos = new Vector3(gameObject.transform.position.x+Random.Range(-10,10), gameObject.transform.position.y+ Random.Range(-10, 10), gameObject.transform.position.z);
            Instantiate(Batteries[i],SpawnPos,gameObject.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
