using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float moveSpeed = 3f;
    public int maxLevel = 10;
    public int level = 1;
    public int maxHealth;
    public int health;
    public int maxExp;
    public int exp = 0;
    public int damageDefend = 100;
    private bool isDie = false;

	public AudioClip hitSound;
	AudioSource audioSource;

    GameController gameController;

	UIManager ui;

    public Vector3 moveInput;

	private Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
    {
        ui = FindObjectOfType<UIManager>();
        gameController = FindObjectOfType<GameController>();
		audioSource = FindObjectOfType<AudioSource>();
		UpdateMaxExp();
        UpDateMaxHealth();
        health = maxHealth;
		rb = GetComponent<Rigidbody2D>();
		transform.localScale = new Vector3(-1f, 1f, 1f);
	}


    // Update is called once per frame
    void Update()
    {
        if (isDie)
        {
            return;
        }
		Move();
        ui.SetHealthText("HP: " + health.ToString() + "/" + maxHealth.ToString());
        LevelUp();
    }

    public void IncreaseHealth()
    {
        if(health >= maxHealth)
        {
            health = maxHealth;
            return;
        }
        health += 10;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Enemy"))
        {
			Enemy enemy = collision.gameObject.GetComponent<Enemy>();
			if (enemy != null)
			{
				enemy.TakeDamage(damageDefend);
                this.TakeDamage(enemy.damage);
			}
		}
	}
    
	private void UpdateMaxExp()
    {
        maxExp = 100 * level;
    }

    private void UpDateMaxHealth()
    {
        maxHealth = 100 * level;
    }

    private void IncreaseSpeed()
    {
        moveSpeed += 0.2f;
    }

    private void Move()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");

        if ((transform.position.x <= -39 && moveInput.x < 0) ||
            (transform.position.x >= 39 && moveInput.x > 0) ||
            (transform.position.y <= -24 && moveInput.y < 0) || 
            (transform.position.y >=24 && moveInput.y > 0))
		{
			return;
		}

		transform.position += moveInput * moveSpeed * Time.deltaTime;

        float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        if(angle == 180f)
        {
            angle = 0f;
        }

        if(moveInput.x == 0 && moveInput.y == 0) 
        {
            return;
        }

		Quaternion rotation = Quaternion.Euler(0f, 0f, angle - 90);
		transform.rotation = rotation;
        
	}

    public void TakeDamage(int damage)
    {
        health -= damage;
        audioSource.PlayOneShot(hitSound);
        ui.SetHealthText("HP: " + health.ToString() + "/" + maxHealth.ToString());
        if(health <= 0)
        {
			ui.ShowGameOverPanel(true);
			isDie = true;
        }

    }


	public bool IsDie()
    {
        return isDie;
        
    }

    public void LevelUp()
    {
        if(level >= maxLevel)
        {
            return;
        }
        if(exp >= maxExp)
        {
            level++;
            exp -= maxExp;
            UpdateMaxExp();
            UpDateMaxHealth();
            gameController.SetMaxEnemyNumber(level);
            IncreaseSpeed();
            ui.SetLevelText("Level: " + level.ToString());
		}
    }

    public void IncreaseExp(int _exp)
    {
        if(exp >= maxExp)
        {
            exp = maxExp;
            return;
        }
        exp += _exp;
        ui.SetExpText("Exp: " + exp.ToString() + "/" + maxExp.ToString());
    }
}
