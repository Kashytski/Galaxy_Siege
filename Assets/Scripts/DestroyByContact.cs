using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;

	private int gc;
	private GameController gameController;
	private GameController2 gameController2;
	private GameController3 gameController3;
		
	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
			gc = 1;
		}

		if (gameController == null)
		{ 
				gameController2 = gameControllerObject.GetComponent <GameController2>();
				gc = 2;
				
				if (gameController2 == null)
				{ 
					gameController3 = gameControllerObject.GetComponent <GameController3>();
					gc = 3;

					if (gameController3 == null)
					{ 
						Debug.Log ("Cannot find 'GameController' script");
					}
				}
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.CompareTag ("Boundary") || other.CompareTag ("Enemy")) 
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.CompareTag("Player") )
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			switch (gc)
			{
				case 1: 
						{
							gameController.CutExplosion();
							gameController.GameOver ();
						}
					break;
				case 2: 
						{
							gameController2.CutExplosion();
							gameController2.GameOver ();
						}
					break;
				case 3: 
						{
							gameController3.CutExplosion();
							gameController3.GameOver ();
						}
				break;
			}
		}

		switch (gc)
		{
			case 1: 
				gameController.AddScore (scoreValue);
				break;
			case 2: 
				gameController2.AddScore (scoreValue);
				break;
			case 3: 	
				gameController3.AddScore (scoreValue);
				break;
		}

		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}