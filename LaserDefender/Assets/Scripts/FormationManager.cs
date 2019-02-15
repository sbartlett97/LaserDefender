using UnityEngine;
using System.Collections;

public class FormationManager : MonoBehaviour {
    public float speed;
    public float padding = 1f;
    float xmin;
    float xmax;
    bool moveLeft = true;
    bool moveRight = false;
    float newX;
    float currentX;
    public GameObject enemyPrefab;
    public GameObject heavyEnemy;
    public float heavySpawnChance;
    private float Width = 9.5f;
    private float Height = 4.5f;
    float spawnDelay = 0.5f;
    public AudioClip hyperDrive;
    private ScoreKeeper scoreKeeper;
    private ExtraFormationManager extraManager;
    private ExtraFormationManager extraManagerTwo;
    public AudioClip alert;
    public bool playAlert = false;


    // Use this for initialization
    void Start () {
        extraManager = GameObject.Find("ExtraEnemyFormation").GetComponent<ExtraFormationManager>();
        extraManagerTwo = GameObject.Find("ExtraEnemyFormation2").GetComponent<ExtraFormationManager>();
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        limitPlaySpace();
        SpawnEnemies();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0f));
    }

    // Update is called once per frame
    void Update () {
        movement();
        checkEdge();
        
            if(AllMembersDead() && extraManager.active && extraManager.AllMembersDead() && !extraManagerTwo.active)
            {
                SpawnEnemies();
                extraManager.SpawnEnemies();
            
            }else if (AllMembersDead() && !extraManager.active && !extraManagerTwo.active)
            {
                Debug.Log("Spawning New Enemies");
                SpawnEnemies();
            }else if (AllMembersDead() && extraManager.active && extraManager.AllMembersDead()&& extraManagerTwo.active && extraManagerTwo.AllMembersDead())
        {
            SpawnEnemies();
            extraManager.SpawnEnemies();
            extraManagerTwo.SpawnEnemies();
        }
        
        
        
	}

    void limitPlaySpace()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        xmin = leftPos.x + padding;
        xmax = rightPos.x - padding;
    }

    void movement()
    {
        if (moveLeft == true)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else if (moveRight == true)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        currentX = transform.position.x;
    }

    void checkEdge()
    {
        if (currentX == xmin)
        {
            moveLeft = false;
            moveRight = true;
        } else if (currentX == xmax)
        {
            moveLeft = true;
            moveRight = false;
        }
    }

    bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform){
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
        scoreKeeper.incrementWaves();
        return true;
    }

    Transform NextFreePosition()
    {
        
        foreach(Transform childPositionGameObject in transform){
            if(childPositionGameObject.childCount < 1)
            {
                return childPositionGameObject;
            }
        }
        return null;
    }

   
    void SpawnEnemies()
    {
        Transform nextPos = NextFreePosition();
        if (nextPos)
        {
            if ( 5 <= scoreKeeper.Waves && scoreKeeper.Waves <= 9)
            {
                if (playAlert)
                {
                    AudioSource.PlayClipAtPoint(alert, transform.position, 10f);
                    playAlert = false;
                }
                heavySpawnChance = 0.5f;
                float probability = Random.value * heavySpawnChance;
                if (probability <=  0.25f)
                {
                    GameObject enemy = Instantiate(heavyEnemy, nextPos.position, Quaternion.identity) as GameObject;
                    AudioSource.PlayClipAtPoint(hyperDrive, nextPos.position, 10f);
                    enemy.transform.parent = nextPos;
                    Debug.Log("Heavy enemy spawned");
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
                    AudioSource.PlayClipAtPoint(hyperDrive, nextPos.position, 10f);
                    enemy.transform.parent = nextPos;
                    Debug.Log("No Hevy Enemy");
                }

            }else if (10 <= scoreKeeper.Waves)
            {
                heavySpawnChance = 0.5f;
                float probability = Random.value * heavySpawnChance;
                if (probability <= 0.85f)
                {
                    GameObject enemy = Instantiate(heavyEnemy, nextPos.position, Quaternion.identity) as GameObject;
                    AudioSource.PlayClipAtPoint(hyperDrive, nextPos.position, 10f);
                    enemy.transform.parent = nextPos;
                    Debug.Log("Heavy enemy spawned");
                }
                else
                {
                    GameObject enemy = Instantiate(enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
                    AudioSource.PlayClipAtPoint(hyperDrive, nextPos.position, 10f);
                    enemy.transform.parent = nextPos;
                    Debug.Log("No Hevy Enemy");
                }

            }else
            {
                GameObject enemy = Instantiate(enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
                AudioSource.PlayClipAtPoint(hyperDrive, nextPos.position, 10f);
                enemy.transform.parent = nextPos;
                Debug.Log("No Heavy Enemy");
            }

        }
        if (NextFreePosition())
        {
            Invoke("SpawnEnemies", spawnDelay);
        }else
        {
            scoreKeeper.wavesIncremented = false;
        }
        
    }
}
