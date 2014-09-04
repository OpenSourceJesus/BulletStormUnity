using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
	public Vector2 vel;
	public int speed;
	public float damage;

	// Use this for initialization
	void Start ()
	{
		renderer.material.color = new Color(1, Random.Range(0, .5f), Random.Range(0, .5f));
		transform.localScale = Vector3.one * damage;
		GetComponent<LineRenderer>().SetWidth(.1f * damage, .1f * damage);
	}
	
	// Update is called once per frame
	void Update ()
	{
		vel *= 9999999999;
		vel = Vector2.ClampMagnitude(vel, speed * Time.deltaTime);
		rigidbody2D.velocity = vel;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Enemy" && !other.isTrigger)
		{
			other.GetComponent<Enemy>().hp -= damage;
			if (other.GetComponent<Enemy>().hp <= 0)
			{
				GameObject.Find("Scripts").GetComponent<Waves>().score += other.GetComponent<Enemy>().maxhp * (other.GetComponent<Enemy>().speed / 2 * Time.fixedDeltaTime);
				GameObject.Find("Scripts").GetComponent<Waves>().money += other.GetComponent<Enemy>().maxhp * (other.GetComponent<Enemy>().speed / 2 * Time.fixedDeltaTime);
				Destroy(other.gameObject);
			}
			Destroy (gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.name == "Wall")
			Destroy(gameObject);
	}
}
