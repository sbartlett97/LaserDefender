using UnityEngine;
using System.Collections;

public class ExtraFormationManager : MonoBehaviour {
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
    private float Width = 15f;
    private float Height = 4.5f;
    float spawnDelay = 0.5f;
    public AudioClip hyperDrive;
    public bool active = false;

    // Use this for initialization
    void Start () {
        limitPlaySpace();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0f));
    }

    // Update is called once per frame
    void Update () {
        movement();
        checkEdge();
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

    public bool AllMembersDead()
    {
        foreach(Transform childPositionGameObject in transform){
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }
        }
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
    public bool checkActive()
    {
        if (active == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SpawnEnemies()
    {
        Transform nextPos = NextFreePosition();
        if (nextPos)
        {
            GameObject enemy = Instantiate(enemyPrefab, nextPos.position, Quaternion.identity) as GameObject;
            AudioSource.PlayClipAtPoint(hyperDrive, nextPos.position, 10f);
            enemy.transform.parent = nextPos;
        }
        if (NextFreePosition())
        {
            Invoke("SpawnEnemies", spawnDelay);
        }
        
    }
}
