using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public bool kamikaze;
	public bool shield;
	public float maxhp;
	public float hp;
	public int speed;
	Vector2 vel;
	Transform target;

	// Use this for initialization
	void Start ()
	{
		maxhp = GameObject.Find("Scripts").GetComponent<Waves>().enemyhp;
		hp = maxhp;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (kamikaze)
		{
			if (target != null)
			{
				GetComponent<CircleCollider2D>().radius = 50;
				vel = target.position - transform.position;
			}
			else
				vel = Vector2.right;
			vel *= 9999999999;
			vel = Vector2.ClampMagnitude(vel, speed * Time.deltaTime);
			rigidbody2D.velocity = vel;
		}
		else
		{
			vel = Vector2.right;
			vel *= 9999999999;
			vel = Vector2.ClampMagnitude(vel, speed * Time.deltaTime);
			rigidbody2D.velocity = vel;
		}
		if (transform.position.x > 39)
		{
			GameObject.Find("Scripts").GetComponent<Waves>().life -= maxhp;
			if (GameObject.Find("Scripts").GetComponent<Waves>().life <= 0)
				GameObject.Find("Scripts").GetComponent<Waves>().Reset ();
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (GetComponent<CircleCollider2D>() != null && other.tag == "Tower")
		{
			GetComponent<CircleCollider2D>().radius = Vector2.Distance(transform.position, other.transform.position) / 2 - 2;
			target = other.transform;
		}
	}
	
	void OnTriggerStay2D (Collider2D other)
	{
		if (GetComponent<CircleCollider2D>() != null && other.tag == "Tower")
		{
			GetComponent<CircleCollider2D>().radius = Vector2.Distance(transform.position, other.transform.position) / 2 - 2;
			target = other.transform;
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Tower")
		{
			Destroy(coll.gameObject);
			Destroy(gameObject);
		}
	}
}
