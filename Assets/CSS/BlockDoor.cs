using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDoor : MonoBehaviour
{
    public FInishMan finish;
    public Rigidbody rb;
    public SpriteRenderer spriteRenderer;
    public float distanceCoefficient = 1.5f; // Коэффициент расстояния
    public int index;
    public GameObject effect;
    private void Start()
    {
        finish = FindAnyObjectByType<FInishMan>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            finish.score.ElevateScore(1);
            finish.pl.erase.Play(); 
            // Расстояние между центрами объектов
            float distance = Vector3.Distance(transform.position, collision.transform.position);
            Instantiate(effect,transform.position,Quaternion.identity,null);
            // Если расстояние больше чем заданный коэффициент
            if (distance > distanceCoefficient)
            {
                rb.isKinematic = true;
            }
        }
        if(index>0)
        {
            if (collision.gameObject.tag == "Down")
            {
                finish.TheEnd();
            }
        }
    }

   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Block")
        {
        
            rb.isKinematic = false;
        }
    }
}
