using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    public Flock flock;
    public float speed = 0.1f;
    private float rotationSpeed = 4.0f;
    private Vector3 averageHeading;
    private Vector3 averagePosition;
    private float neighbourDistance = 3.0f;
    private bool turning = false;
    
    void Start()
    {
        speed = Random.Range(0.5f, 1f);
    }

    
    void Update()
    {
        //verifica se o peixe está fora dos limites do flock
        if(Vector3.Distance(transform.position, flock.transform.position) >= flock.tankSize){
            turning = true;
        }
        else
            turning = false;
        
        //faz o peixe voltar para dentro do flock
        if(turning){
            Vector3 direction = flock.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                Quaternion.LookRotation(direction),
                                                rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1.0f);
        }
        //chance aleatória para aplicar as regras de flocking
        else{
            if(Random.Range(0, 5) < 1){
                ApplyRules();
        }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }

    private void ApplyRules(){
        FlockUnit[] units;
        units = flock.allFish;

        //vector que aponta para o centro do grupo
        Vector3 vCentre = Vector3.zero;
        //vetor que aponta para fora do grupo
        Vector3 vAvoid = Vector3.zero;
        //velocidade do grupo
        float gSpeed = 0.1f;

        Vector3 goalPos = flock.goalPos;

        float dist;

        //número de peixes do flock que estão próximos deste peixe
        int groupSize = 0;

        //observa todos os peixes do flock
        foreach(FlockUnit unit in units){
            if(unit.gameObject != this.gameObject){
                dist = Vector3.Distance(unit.transform.position, this.transform.position);
                //verifica se os peixes estão próximos
                if(dist <= neighbourDistance){
                    vCentre += unit.transform.position;
                    groupSize++;

                    //verifica se os peixes estão muito próximos
                    if(dist < 1.0f){
                        vAvoid += this.transform.position - unit.transform.position;
                    }

                    gSpeed += unit.speed;
                }
            }
        }

        //calcula a direção que o peixe deve se mover se ele estiver dentro de um grupo
        if(groupSize > 0){
            vCentre = vCentre/groupSize + (goalPos - this.transform.position);
            speed = gSpeed/groupSize;

            Vector3 direction = (vCentre + vAvoid) - transform.position;
            if(direction != Vector3.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    rotationSpeed * Time.deltaTime);
            }
        }
    }
}
