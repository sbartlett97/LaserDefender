using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {
    public float percentHealthRestore = 0.25f;
    public float healthRestored;
    public float dropSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -dropSpeed, 0f) * dropSpeed * Time.deltaTime;
    }

    public void collected()
    {
        Destroy(this.gameObject);
    }
}
