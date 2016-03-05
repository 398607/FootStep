using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	// inspector
	public Timer TimerPrefab;

	[HideInInspector]
	public Timer Timer;
	public static GameManager Instance = null;

	private ScoreListener scoreListener = null;

	public static float GetTime()
	{
		return Instance.Timer.Time;
	}

	public static ScoreListener ScoreListener()
	{
		return Instance.scoreListener;
	}

	// Use this for initialization
	void Start ()
	{
		if (Instance == null)
			Instance = this;
		Timer = Instantiate(TimerPrefab);
		Timer.StartTimer();

		scoreListener = new ScoreListener();
		scoreListener.AddScoreBoard(GameObject.FindObjectOfType<ScoreBoard>());
	}
}
