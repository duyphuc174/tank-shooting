using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public bool roaming = true;
    public float moveSpeed;
    public float nextWPDistance;

    public Seeker seeker;
    public bool updateContinuesPath;
    bool reachDestination = false; // Kiểm tra đi hết đường chưa
    Path path;
    Coroutine moveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CalculatePath", 0f, 0.5f);
        reachDestination = true;

	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculatePath()
    {
        Vector2 target = FindTarget();

        if(seeker.IsDone() && (reachDestination || updateContinuesPath))
        {
            seeker.StartPath(transform.position, target, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if(p.error)
        {
            return;
        }
        path = p;
        // Move to target
        MoveToTarget();
    }

    void MoveToTarget()
    {
        if(moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;
        reachDestination = false;

        while(currentWP < path.vectorPath.Count)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - (Vector2)transform.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            float distance = Vector2.Distance(transform.position, path.vectorPath[currentWP]);
            if(distance < nextWPDistance)
            {
                currentWP++;
            }
            yield return null;
        }

        reachDestination = false;
    }

    Vector2 FindTarget()
    {
        Vector3 tankPos = FindObjectOfType<Tank>().transform.position;
        
        if(roaming == true)
        {
            return (Vector2)tankPos + (Random.Range(10f, 50f) * new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized);
        }
        else
        {
            return tankPos;
        }
    }
}
