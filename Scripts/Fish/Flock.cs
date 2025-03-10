using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public int tankSize = 5;
    [SerializeField] private GameObject fishPrefab;
    public int numFish = 10;
    public FlockUnit[] allFish;

    public Vector3 goalPos = Vector3.zero;

    void Start()
    {
        allFish = new FlockUnit[numFish];
        //spawna todos os peixes iniciais do flock
        /* for(int i = 0; i < numFish; i++){
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-tankSize, tankSize),
                                    transform.position.y + Random.Range(-tankSize, tankSize),
                                    transform.position.z + Random.Range(-tankSize, tankSize));

            var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            GameObject obj = Instantiate(fishPrefab, pos, rotation);
            allFish[i] = obj.GetComponent<FlockUnit>();
            allFish[i].flock = this;
        } */
        for(int i = 0; i < numFish; i++){
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-tankSize, tankSize),
                                    transform.position.y + Random.Range(-tankSize, tankSize),
                                    transform.position.z + Random.Range(-tankSize, tankSize));

            var rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            //GameObject obj = Instantiate(fishPrefab, pos, rotation);
            GameObject obj = transform.GetChild(i).gameObject;
            obj.transform.position = pos;
            obj.transform.rotation = rotation;
            allFish[i] = obj.GetComponent<FlockUnit>();
            allFish[i].flock = this;
        }
    }

    void Update()
    {
        //altera a posição do ponto objetivo
        if(Random.Range(0, 10000) < 50){
            goalPos = new Vector3(transform.position.x + Random.Range(-tankSize, tankSize),
                                transform.position.y + Random.Range(-tankSize, tankSize),
                                transform.position.z + Random.Range(-tankSize, tankSize));
        }
    }
}
