using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private Vector3 previousPosition;
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
		if (collision.CompareTag("Tank")) // kiểm tra đối tượng va chạm có phải là tank không
		{
			// Lưu lại vị trí trước đó của tank
			previousPosition = collision.transform.position;

			// Di chuyển tank trở lại vị trí trước đó
			collision.transform.position = previousPosition;
			Destroy(this.gameObject);
		}
	}
}
