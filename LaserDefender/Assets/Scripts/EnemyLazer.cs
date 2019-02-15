using UnityEngine;
using System.Collections;

public class EnemyLazer : MonoBehaviour
{
    public float projectileSpeed;
    public float damage;

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -projectileSpeed, 0f) * projectileSpeed * Time.deltaTime;
    }

    public void hit()
    {
        Destroy(this.gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
