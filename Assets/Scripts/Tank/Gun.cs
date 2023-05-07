using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

	// ??n
	public GameObject bullet;
	// kcach l?a 
	public Transform firePos;
	// H?ng súng
	public GameObject muzzle;
	public GameObject fireEffect;

	public float TimeBtwFire = 0.2f;
	public float bulletForce;

	private float timeBtwFire;

	// Start is called before the first frame update
	void Start()
    {
	}

    // Update is called once per frame
    void Update()
    {
		RotateGun();
		timeBtwFire -= Time.deltaTime;

		if (Input.GetMouseButton(0) && timeBtwFire < 0)
		{
			FireBullet();
		}
	}

	void RotateGun()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 lookDir = mousePos - transform.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90;

		Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
		transform.rotation = rotation;
	}

	void FireBullet()
	{
		timeBtwFire = TimeBtwFire;

		GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
		
		Instantiate(muzzle, firePos.position, transform.rotation, transform);

		Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
		rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
	}
}
