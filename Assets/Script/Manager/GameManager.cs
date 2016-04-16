using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NAudio.Midi;
using UnityEngine.UI;
using NAudio.Midi;
using UnityEditor;

public class GameManager : MonoBehaviour
{
	// constant
	// when Timer->2.0s, the first Note appears at top of screen
	public float DelayBeforePlayMusic = NoteCreator.TimeBeforeCreate + 2f;

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
	// plotdisplay
	private PlotDisplay _plotDisplay;

	public List<Note> Notes = new List<Note>();

	public static PlotDisplay PlotDisplay
	{
		get { return Instance._plotDisplay; }
		set { }
	}

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
		Timer = Instantiate(Resources.Load<Timer>("Prefab/Usage/Timer"));
		Timer.StartTimer();

		// note creator
		_noteCreator = new NoteCreator();

		// plot manager
		_plotManager = new PlotManager();

		// score listener
		_scoreListener.AddScoreBoard(GameObject.FindObjectOfType<ScoreBoard>());

		// music player
		_musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
		_musicPlayer.LoadMusic("futurest!");

		// play/ pause button
		startButton = GameObject.Find("StartButton").GetComponent<Button>();
		startButton.GetComponentInChildren<Text>().text = "play";
		startButton.onClick.AddListener(OnStartButton);

		// plotDisplay
		_plotDisplay = GameObject.FindObjectOfType<PlotDisplay>();
		_plotDisplay.Init();
		_plotDisplay.Hide();
		_plotDisplay.gameObject.SetActive(false);

		// midi parser
		// var midiManager = new MidiManager();
		// var midiFile = midiManager.Parse();

		// load test midi file from Resource!
		LoadMidiFile("futurest!");
		// Debug.Log(file.ToString());

		// Line
		GameObject.Find("Line").transform.position = new Vector3(0, -4, 0);
		
		Debug.Log("GameManger Start() done");
	}

	public static void LoadMidiFile(string midiname)
	{
		var resource = Resources.Load("Midi/" + midiname) as TextAsset;
		if (resource == null)
		{
			Debug.Log("Midi File (*.bytes) cannot be read");
		}
		var file = new MidiFile(resource.bytes);
		Instance._noteCreator.LoadMidiFile(file, Instance.DelayBeforePlayMusic);
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
		// start music when asked
		if (GetTime() > DelayBeforePlayMusic && !_musicPlayer.Functioning)
		{
			_musicPlayer.Functioning = true;
			_musicPlayer.PlayPause();
		}

		// attached managers Update()
		_noteCreator.Update();
		_plotManager.Update();
	}
}
