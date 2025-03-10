using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed = 4.0f;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        if(Random.Range(1, 10) > 2){
            Vector3 goal = new Vector3(transform.position.x + Random.Range(-1, 1),
                                    transform.position.y + Random.Range(-1, 1),
                                    transform.position.z + Random.Range(-1, 1));
            Vector3 direction = goal - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                rotationSpeed * Time.deltaTime);
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
}
