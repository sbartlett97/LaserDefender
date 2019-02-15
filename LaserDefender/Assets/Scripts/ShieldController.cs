using UnityEngine;
using System.Collections;

public class ShieldController : MonoBehaviour {
    public int maxHits;
    private int hitsTaken;
    public AudioClip generate;
    public AudioClip fall;
    public AudioClip hit;
	// Use this for initialization
	void Start () {
        AudioSource.PlayClipAtPoint(generate, transform.position, 20f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        EnemyLazer lazer = col.gameObject.GetComponent<EnemyLazer>();

        if (lazer)
        {
            hitsTaken += 1;
            AudioSource.PlayClipAtPoint(hit, transform.position, 10f);
            lazer.hit();
            if(hitsTaken == maxHits)
            {
                AudioSource.PlayClipAtPoint(fall, transform.position, 10f);
                Destroy(this.gameObject);
            }
        }
    }
}
