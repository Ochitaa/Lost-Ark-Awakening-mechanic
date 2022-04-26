using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedProjectile : MonoBehaviour
{
    [SerializeField] float secondsUntilDestroy = 3f;
    public float health = 10f;




    private void Awake()
    {

    }

    private void Start()
    {

        Destroy(gameObject, secondsUntilDestroy);
    }
    private void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            health = health - 20f;

        }
        if (health <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Destroy(gameObject);
    }

}
