using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {
    public float Health;
    public GameObject lazer;
    public float shotsPerSecond;
    public int ScoreValue = 150;
    private ScoreKeeper scoreKeeper;
    public AudioClip lazerShot;
    public AudioClip destroy;
    public GameObject healthDrop;
    public GameObject shieldDrop;
    public float dropChance;

    void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {  
        PlayerLazer lazer = col.gameObject.GetComponent<PlayerLazer>();
        if (lazer)
        {
            Debug.Log("Hit by a projectile");
            Health = Health - lazer.GetDamage();
            lazer.hit();
            if (Health <= 0)
            {
                var random = Random.value;
                if (random <= dropChance)
                {
                   if(random <= 0.04)
                    {
                        Instantiate(healthDrop, transform.position, Quaternion.identity);
                    }else
                    {
                        Instantiate(shieldDrop, transform.position, Quaternion.identity);
                    }
                    
                }
                AudioSource.PlayClipAtPoint(destroy, transform.position, 40f);
                Destroy( this.gameObject);      
                scoreKeeper.incrementScore(ScoreValue);
            }
        }
    }

    void Update()
    {
        float probability = Time.deltaTime * shotsPerSecond;
        if (Random.value < probability)
        {
            fire();
        }
        
    }

    void fire()
    {
        Instantiate(lazer, new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(lazerShot, transform.position, 20f);
    }
}
