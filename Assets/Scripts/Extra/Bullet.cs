using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 50;
    Tank tank;
    // Start is called before the first frame update
    void Start()
    {
        tank = FindObjectOfType<Tank>();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                tank.IncreaseHealth();
            }
            Destroy(this.gameObject);
        }
	}
}
