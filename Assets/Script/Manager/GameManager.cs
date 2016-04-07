using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NAudio.Midi;
using UnityEngine.UI;
using NAudio.Midi;

public class GameManager : MonoBehaviour
{
	// inspector
	public Timer TimerPrefab;
	public Note NotePrefab;

	[HideInInspector]
	public Timer Timer;
	public static GameManager Instance = null;

	private readonly ScoreListener _scoreListener = null;
	private NoteCreator _noteCreator = null;
	private PlotManager _plotManager = null;
	private MusicPlayer _musicPlayer = null;

	// GUI
	private Button startButton = null;
	private bool startButtonActive = true;

	public List<Note> Notes = new List<Note>(); 

	public static float GetTime()
	{
		// lazy find?
		if (Instance.Timer == null)
		{
			return 0f;
		}
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
		Instance = this;

		_scoreListener = new ScoreListener();
	}

	// Use this for initialization
	void Start ()
	{
		Timer = Instantiate(TimerPrefab);
		Timer.StartTimer();

		// note creator
		_noteCreator = new NoteCreator();

		// plot manager
		_plotManager = new PlotManager();

		// score listener
		_scoreListener.AddScoreBoard(GameObject.FindObjectOfType<ScoreBoard>());

		// music player
		_musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();

		// play/ pause button
		startButton = GameObject.Find("StartButton").GetComponent<Button>();
		startButton.GetComponentInChildren<Text>().text = "play";
		startButton.onClick.AddListener(OnStartButton);

		// midi parser
		// var midiManager = new MidiManager();
		// var midiFile = midiManager.Parse();

		// load test midi file
		var file = new MidiFile("futurest!.mid");
		_noteCreator.LoadMidiFile(file, 6f);
		// Debug.Log(file.ToString());
		
		Debug.Log("GameManger Start() done");

	}

	private void OnStartButton()
	{
		if (startButtonActive)
			Trigger();
	}

	public static void TriggerStartButtonActive()
	{
		Instance.startButtonActive = !Instance.startButtonActive;
	}

	// start game/ pause game
	public static void Trigger()
	{
		Instance.Timer.Trigger();
		Instance._musicPlayer.PlayPause();
		Instance.startButton.GetComponentInChildren<Text>().text = Playing() ? "pause" : "play";
	}

	void Update()
	{
		// start music
		if (GetTime() > 6f && !_musicPlayer.Functioning)
		{
			_musicPlayer.Functioning = true;
			_musicPlayer.PlayPause();
		}

		// attached managers Update()
		_noteCreator.Update();
		_plotManager.Update();
	}
}
