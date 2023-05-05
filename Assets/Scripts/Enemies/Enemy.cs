using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public GameObject genericEffect;
    public int damage = 30;
    public Tank tank;
    public int exp = 10;
    void Start()
    {
        health = 100;
        tank = FindAnyObjectByType<Tank>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if(health <= 0) 
        {
            Die();
        }
    }

    public void Die() 
    {
        Destroy(this.gameObject);
		Instantiate(genericEffect, transform.position, Quaternion.identity);
        tank.IncreaseExp(exp);
        
	}
}
