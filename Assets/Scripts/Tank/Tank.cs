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
    private bool gameOver = false;

    UIManager ui;

    public Vector3 moveInput;

	private Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
    {
        ui = FindObjectOfType<UIManager>();
        UpdateMaxExp();
        UpDateMaxHealth();
        health = maxHealth;
		rb = GetComponent<Rigidbody2D>();
		transform.localScale = new Vector3(-1f, 1f, 1f);
	}


    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            return;
        }
        Move();
        LevelUp();
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
        moveSpeed += 0.5f;
    }

    private void Move()
    {
		moveInput.x = Input.GetAxis("Horizontal");
		moveInput.y = Input.GetAxis("Vertical");

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
        ui.SetHealthText("HP: " + health.ToString() + "/" + maxHealth.ToString());
        if(health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOver = true;
        ui.ShowGameOverPanel(true);
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
