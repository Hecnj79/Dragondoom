using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjectileEnemy : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float fireRate = 1f;
    private float nextFireTime;
    public GameObject fireball;
    public GameObject fireballParent;
    private Transform player;
    public GameObject[] finalEnemies;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        finalEnemies = GameObject.FindGameObjectsWithTag("finalBoss");
        if (finalEnemies.Length == 0)
        {
            SceneManager.LoadScene("EndingScene");
        }

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if(distanceFromPlayer<lineOfSight && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(fireball, fireballParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

         private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime,  float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }
    
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if(myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            // currentState = EnemyState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
