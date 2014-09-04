using UnityEngine;
using System.Collections;

public class Waves : MonoBehaviour
{
	public float score;
	public float money;
	public GUISkin guiSkin1;
	public float enemyCountInWave;
	public int wave;
	public int waveDuration;
	public int waitDuration;
	public ArrayList createTimes;
	public GameObject rusher;
	public GameObject kamikaze;
	public float life;
	public float maxLife;
	public float enemyhp;
	public GameObject machinegun;
	public GameObject shotgun;
	public GameObject rifle;
	float timer;
	GameObject go;
	int machinegunCost = 500;
	int shotgunCost = 600;
	int rifleCost = 600;
	int wavePhase;

	// Use this for initialization
	void Start ()
	{
		createTimes = new ArrayList();
		life = maxLife;
	}

	void StartWave ()
	{
		for (float t = waveDuration / enemyCountInWave; t < waveDuration; t += waveDuration / enemyCountInWave)
			createTimes.Add(t + Random.Range(-waveDuration / enemyCountInWave, waveDuration / enemyCountInWave));
		timer = 0;
		wavePhase = 1;
	}

	public void Reset ()
	{
		if (score > PlayerPrefs.GetInt("Score", 0))
			PlayerPrefs.SetInt("Score", Mathf.RoundToInt(score));
		Application.LoadLevel(Application.loadedLevel);
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		foreach (float t in createTimes)
		{
			if (timer > t)
			{
				float r = Random.Range(0, 100);
				if (r < 50)
					go = (GameObject) GameObject.Instantiate(rusher, new Vector2(-46, Random.Range(-12, 21)), Quaternion.identity);
				else
					go = (GameObject) GameObject.Instantiate(kamikaze, new Vector2(-46, Random.Range(-12, 21)), Quaternion.identity);
				createTimes.Remove(t);
				break;
			}
		}
		if (wavePhase == 1 && createTimes.Count == 0 && GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
		{
			wavePhase = 0;
			timer = 0;
		}
		else if (wavePhase == 0 && timer > waitDuration)
		{
			if (wave % 2 == 1)
			{
				enemyhp -= 1;
				enemyCountInWave += wave * enemyCountInWave;
			}
			else
			{
				enemyhp += 2;
				enemyCountInWave -= 10;
			}
			wave ++;
			StartWave ();
		}
	}

	void OnGUI ()
	{
		GUI.skin = guiSkin1;
		GUI.Label(new Rect(0, 0, 9999999999, 50), "Life: " + life + "/" + maxLife);
		GUI.Label(new Rect(0, 25, 9999999999, 50), "Score: " + Mathf.Round(score));
		GUI.Label(new Rect(0, 50, 9999999999, 50), "$" + Mathf.Round(money));
		GUI.Label(new Rect(0, 75, 9999999999, 50), "Bestscore: " + PlayerPrefs.GetInt("Score", 0));
		GUILayout.BeginArea(new Rect(25, 475, Screen.width - 50, 100));
		if (GUILayout.Button("Machinegun--$" + machinegunCost, GUILayout.Width(150)) && Mathf.Round(money) >= machinegunCost)
		{
			money -= machinegunCost;
			go = (GameObject) GameObject.Instantiate(machinegun, transform.position, Quaternion.identity);
		}
		else if (GUILayout.Button("Shotgun--$" + shotgunCost, GUILayout.Width(150)) && Mathf.Round(money) >= shotgunCost)
		{
			money -= shotgunCost;
			go = (GameObject) GameObject.Instantiate(shotgun, transform.position, Quaternion.identity);
		}
		else if (GUILayout.Button("Rifle--$" + shotgunCost, GUILayout.Width(150)) && Mathf.Round(money) >= rifleCost)
		{
			money -= rifleCost;
			go = (GameObject) GameObject.Instantiate(rifle, transform.position, Quaternion.identity);
		}
		GUILayout.BeginHorizontal();
		GUILayout.Space(590);
		GUILayout.BeginVertical();
		GUILayout.Space(-75);
		if (GUILayout.Button("Full heal--$" + (maxLife * 10), GUILayout.Width(150)) && Mathf.Round(money) >= life * 10)
		{
			money -= life * 10;
			life = maxLife;
		}
		else if (GUILayout.Button("Max life--$1000",  GUILayout.Width(150)) && Mathf.Round(money) >= 1000)
		{
			money -= 1000;
			maxLife += 100;
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.BeginHorizontal();
		GUILayout.Space(750);
		GUILayout.BeginVertical();
		GUILayout.Space(-79);
		if (GUILayout.Button("Restart game", GUILayout.Width(150)))
			Reset ();
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	}
}
