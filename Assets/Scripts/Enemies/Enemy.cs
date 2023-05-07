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
    public int score = 10;

    // shoot
    public bool isShootable = false;
    public GameObject enemyBullet;
    public float bulletSpeed = 4f;
    public float timeBtwFire = 1.5f;
    private float fireCooldown;

	public AudioClip destroySound;
    AudioSource audioSource;

    public GameController gameController;

    void Start()
    {
        health = 100;
        gameController = FindObjectOfType<GameController>();
        tank = FindAnyObjectByType<Tank>();
        audioSource = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldown -= Time.deltaTime;
        if(fireCooldown < 0)
        {
            fireCooldown = timeBtwFire;
            // Shoot
            if(isShootable)
            {
                EnemyFireBullet();
            }
        }
    }

    void EnemyFireBullet()
    {
        GameObject bulletTmp = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();
		Vector3 tankPos = FindObjectOfType<Tank>().transform.position;
		Vector3 direction = tankPos - transform.position;
        rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);

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
        gameController.IncreaseScore(score);
        gameController.DieEnemy();
		Instantiate(genericEffect, transform.position, Quaternion.identity);
        tank.IncreaseExp(exp);
        if(audioSource && destroySound)
        {
            audioSource.PlayOneShot(destroySound);
        }
	}
}
