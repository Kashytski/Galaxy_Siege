﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController3: MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	public GameObject drawField;
	public InputField inputText;


	private double nextFire;
	private bool gameOver;
	private bool restart;
	private string text;
	private int score;
	private int a = 0;
	private int x = 1;
	
	void Start ()
	{
		drawField.SetActive (false);
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		if (gameOver) 
		{
			scoreText.text = "";
		}

		if (Input.GetButton("Fire1")&& Time.time > nextFire)
		{
			nextFire = Time.time + 0.25;
			CutScore();
		}

		if (score > 100) 
		{
			restart = false;
			restartText.text = "Game Passed";
		}
		
		if (restart)
		{
			drawField.SetActive (true);
			inputText.ActivateInputField();

			if (Input.GetKeyDown (KeyCode.F4))
			{
				Rating();
				Application.LoadLevel ("Level 1");
			}
			if (Input.GetKeyDown (KeyCode.F3))
			{

				Rating();
				Application.LoadLevel ("Menu");
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards[Random.Range(0,hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartText.text = "Press 'F3' for Main Menu\nPress 'F4' for Restart";
				restart = true;
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	void CutScore ()
	{
		score -= x;
		UpdateScore ();
	}

	public void CutExplosion ()
	{
		score -= 10;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		drawField.SetActive (true);
		inputText.ActivateInputField();

		gameOverText.text = "Game Over !\nYour score: "+(score+760) + "\n\nEnter your name";
		gameOver = true;
		x = 0;
	}

	public void Rating()
	{
		System.IO.StreamReader reader = new System.IO.StreamReader("C:\\Users\\kashytski\\Desktop\\array.txt");
		a = int.Parse (reader.ReadLine ()) + 1;
		reader.Close();
		
		System.IO.StreamWriter writer1 = new System.IO.StreamWriter("C:\\Users\\kashytski\\Desktop\\array.txt", false);
		writer1.WriteLine(a.ToString());
		writer1.Close();

		System.IO.StreamWriter writer = new System.IO.StreamWriter("C:\\Users\\kashytski\\Desktop\\score.txt", true);
		text = inputText.text;
		if (text=="") text="NoName";
		writer.WriteLine(text+"     "+(score+750));
		writer.Close();
	}
}