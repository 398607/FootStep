using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour
{

	private AudioSource music = null;

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

	// the name of the audiosource prefab should be same as the name of the music.
	public void LoadMusic(string musicName)
	{
		music = Instantiate(Resources.Load<AudioSource>("Prefab/Storage/" + musicName));
		if (music == null)
		{
			Debug.Log(string.Format("AudioSource {0} cannot be read", musicName));
		}
	}
}
