using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{

	public AudioSource music;

	public bool Functioning = false;

	public void PlayPause()
	{
		if (!Functioning)
			return;

		if (music.isPlaying)
		{
			music.Pause();
		}
		else
		{
			music.Play();
		}
	}

	// Use this for initialization
	void Start ()
	{
		music = Instantiate(music);
		// PlayPause();
		music.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
