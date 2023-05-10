using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    private Animator enemyAnimation;
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    public int maxHealth = 100;
    int currentHealth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimation = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position)>= minRange)
        {
            FollowPlayer();
        }
        
    }

    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        enemyAnimation.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
        
        
    }
    
    public void Die()
    { 
        PlayerManager.Instance.QuestCompletion();
    }

}
