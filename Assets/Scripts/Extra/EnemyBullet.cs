using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	public int damage = 50;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Tank"))
		{
			Tank tank = collision.gameObject.GetComponent<Tank>();

			if (tank != null)
			{
				tank.TakeDamage(damage);
			}
			Destroy(this.gameObject);
		}
	}
}
