using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{

	public float Score;
	public Text ScoreText;
	public UpdateScoreListener UpdateScoreListener = null;

	public void UpdateScore(float newScore)
	{
		Score = newScore;
		ScoreText.text = newScore.ToString(CultureInfo.CurrentCulture);
	}

	public ScoreBoard()
	{
		UpdateScoreListener = new UpdateScoreListener(this);
	}

	// Use this for initialization
	void Start () {
		ScoreText = GetComponent<Text>();
		UpdateScore(0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
