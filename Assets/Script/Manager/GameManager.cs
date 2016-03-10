using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// inspector
	public Timer TimerPrefab;
	public NoteCreator NoteCreatorPrefab;

	[HideInInspector]
	public Timer Timer;
	public static GameManager Instance = null;

	private readonly ScoreListener _scoreListener = null;
	private NoteCreator _noteCreator = null;
	private MusicPlayer _musicPlayer = null;

	// GUI
	private Button startButton = null;

	public static float GetTime()
	{
		if (Instance.Timer == null)
			return 0f;
		return Instance.Timer.Time;
	}

	public static bool Playing()
	{
		return Instance.Timer.Functioning;
	}

	public static ScoreListener ScoreListener()
	{
		return Instance._scoreListener;
	}

	public GameManager()
	{
		if (Instance == null)
			Instance = this;

		_scoreListener = new ScoreListener();
	}

	// Use this for initialization
	void Start ()
	{
		Timer = Instantiate(TimerPrefab);
		Timer.StartTimer();

		// note creator
		_noteCreator = Instantiate(NoteCreatorPrefab);

		// score listener
		_scoreListener.AddScoreBoard(GameObject.FindObjectOfType<ScoreBoard>());

		// music player
		_musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();

		// play/ pause button
		startButton = GameObject.Find("StartButton").GetComponent<Button>();
		startButton.GetComponentInChildren<Text>().text = "play";
		startButton.onClick.AddListener(PlayOrPause);

		// midi parser
		var midiManager = new MidiManager();
		var midiFile = midiManager.Parse();

		// load test midi file
		_noteCreator.LoadMidiFile(midiFile, 6f);

	}

	// start game (invoked by startButton)
	void PlayOrPause()
	{
		Timer.Trigger();
		_musicPlayer.PlayPause();
		if (Timer.Functioning)
		{
			startButton.GetComponentInChildren<Text>().text = "pause";
		}
		else
		{
			startButton.GetComponentInChildren<Text>().text = "play";
		}
	}

	void Update()
	{
		if (GetTime() > 6f && !_musicPlayer.Functioning)
		{
			_musicPlayer.Functioning = true;
			_musicPlayer.PlayPause();
		}
	}
}
