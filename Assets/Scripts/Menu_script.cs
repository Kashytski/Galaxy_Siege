using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class Menu_script : MonoBehaviour {

	public GameObject exitMenuButton;
	public GameObject startButton;
	public GameObject ratingButton;
	public GameObject infoButton;
	public GameObject exitButton;
	public GameObject panel;
	public GUIText infoText;

	public void PressStart()
	{
		Application.LoadLevel ("Level 1");
	}

	public void PressRating()	
	{
		Visible ();
		panel.SetActive (true);

		System.IO.StreamReader reader = new System.IO.StreamReader("C:\\Users\\kashytski\\Desktop\\score.txt");
		System.IO.StreamReader reader1 = new System.IO.StreamReader("C:\\Users\\kashytski\\Desktop\\array.txt");
		int i = int.Parse (reader1.ReadLine ());
		if (i <= 10) 
		{
			infoText.text = reader.ReadToEnd ();
		}

		else 
		{
			int c = i - 10;

			while (c>0)
			{
				reader.ReadLine ();
				c--;
			}

			infoText.text = reader.ReadToEnd ();
		}

		reader1.Close ();
		reader.Close ();

	}
	
	public void PressInfo()	
	{
		Visible ();
		panel.SetActive (true);
		infoText.text = "Данное игровое приложение\n" +
						"было создано Кашицким Иваном,\n" +
						"с использованием бесплатной\n" +
						"межплатформенной среды разработки\n" +
						"компьютерных игр Unity, в качестве\n" +
						"дипломной работы. galaxy_siege_v1.4.1\n" +
						"является наиболее актуальной версией\n" +
						"приложения, на данный момент";

	}

	public void PressExit()	
	{
		Application.Quit ();
	}

	public void PressMenuExit()	
	{
		Application.LoadLevel ("Menu");
	}
	
	private void Visible()
	{
		startButton.SetActive (false);
		ratingButton.SetActive (false);
		infoButton.SetActive (false);
		exitButton.SetActive (false);
		exitMenuButton.SetActive (true);
	}
}