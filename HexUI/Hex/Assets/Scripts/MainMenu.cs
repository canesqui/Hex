using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void LoadNextScene ()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
		Debug.Log ("Next Scene Loaded");
	}

	public void LoadScene (string name)
	{
		SceneManager.LoadScene(name);
		Debug.Log ("Scene Loaded: " + name);
	}

	public void Quit ()
	{
		Debug.Log ("Quit Request");
		Application.Quit();
		Debug.Log ("Quit Successful");
	}

}
