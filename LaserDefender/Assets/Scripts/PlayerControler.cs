using UnityEngine;
using System.Collections;

public class PlayerControler : MonoBehaviour {
    public float speed;
    public float padding = 1f;
    float xmin;
    float xmax;
    public GameObject lazer;
    public float firingRate = 0.2f;
    public float Health;
    public AudioClip lazerShot;
    public AudioClip shipHit;
    private LevelManager manager;
    private ScoreKeeper scoreKeeper;
    public float maxHealth = 1000f;
    public float healthPercentage;
    public GameObject Shield;

	void Start()
    {
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        limitPlaySpace();
    }
	// Update is called once per frame
	void Update () {
        healthPercentage = Health / maxHealth;
        moveWithArrows();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (scoreKeeper.Waves <= 9)
            {
                InvokeRepeating("fire", 0.000000000000001f, firingRate);
            }
            if (scoreKeeper.Waves >= 10)
            {
                InvokeRepeating("fireMultiple", 0.000000000000001f, firingRate);
            }
            
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            if (scoreKeeper.Waves <= 9)
            {
                CancelInvoke("fire");
            }
            if (scoreKeeper.Waves >= 10)
            {
                CancelInvoke("fire");
                CancelInvoke("fireMultiple");
            }
            
        }
        
	}

    void moveWithArrows() //Basic left/right movement script
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void limitPlaySpace()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftPos.x + padding;
        xmax = rightPos.x - padding;
    } 
    
     void fire()
     {
        Instantiate(lazer, new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(lazerShot, transform.position, 20f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        EnemyLazer lazer = col.gameObject.GetComponent<EnemyLazer>();
        if (lazer)
        {
            AudioSource.PlayClipAtPoint(shipHit, transform.position, 20f);
            Debug.Log("Player Hit by a projectile");
            Health = Health - lazer.GetDamage();
            Debug.Log(Health);
            lazer.hit();
            if (Health <= 0)
            {
                manager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
                Destroy(this.gameObject);
                manager.loadLevel("HighScores");
            }
        }
        HealthPickup healthPickup = col.gameObject.GetComponent<HealthPickup>();
        if (healthPickup)
        {
            healthPickup.healthRestored = maxHealth * healthPickup.percentHealthRestore;
            healthPickup.collected();
            Health += healthPickup.healthRestored;
            Debug.Log(Health);
        }
        ShieldPickup shield = col.gameObject.GetComponent<ShieldPickup>();
        if (shield)
        {
            shield.collected();
            GameObject newShield = Instantiate(Shield, transform.position, Quaternion.identity) as GameObject;
            newShield.transform.parent = GameObject.Find("Player").transform;
        }
    }

    void fireMultiple()
    {
        Instantiate(lazer, new Vector3(transform.position.x + 0.05f, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(lazer, new Vector3(transform.position.x - 0.05f, transform.position.y, transform.position.z), Quaternion.identity);
        AudioSource.PlayClipAtPoint(lazerShot, transform.position, 20f);
    }

}