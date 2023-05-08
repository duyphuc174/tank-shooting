using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	public int damage = 50;
	public AudioClip hitSound;
	public AudioSource aus;
	// Start is called before the first frame update
	void Start()
    {
        aus = FindObjectOfType<AudioSource>();
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
				aus.PlayOneShot(hitSound);
			}
			Destroy(this.gameObject);
		}
	}
}
