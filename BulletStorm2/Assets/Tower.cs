using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour
{
	public float shootRate;
	public int shootNum;
	public float shootSpread;
	float shootTimer;
	public GameObject bullet;
	GameObject go;
	public bool placing;

	// Use this for initialization
	void Start ()
	{
		shootTimer = Time.timeSinceLevelLoad + shootRate;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (placing)
		{
			collider2D.enabled = false;
			Vector2 createLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			createLoc.x = Mathf.Round(createLoc.x / 2) * 2 + 0.0f;
			createLoc.y = Mathf.Round(createLoc.y / 2) * 2 + 0.0f;
			if (createLoc.x <= 36.5 && createLoc.y >= -12)
				transform.position = createLoc;
			if (Input.GetKeyDown(KeyCode.Mouse0) || Input.)
			{
				placing = false;
				collider2D.enabled = true;
				shootTimer = Time.timeSinceLevelLoad + shootRate;
			}
			return;
		}
		if (Time.timeSinceLevelLoad > shootTimer)
		{
			shootTimer = Time.timeSinceLevelLoad + shootRate;
			for (int i = 0; i < shootNum; i ++)
			{
				float angToMouse = Mathf.Atan2(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y, Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x) + Random.Range(-shootSpread, shootSpread);
				Vector2 vecToMouse = new Vector2(Mathf.Cos(angToMouse), Mathf.Sin(angToMouse));
				go = (GameObject) GameObject.Instantiate(bullet, transform.position, Quaternion.LookRotation(Vector3.forward, vecToMouse));
				go.GetComponent<Bullet>().vel = vecToMouse;
			}
		}
	}
}
