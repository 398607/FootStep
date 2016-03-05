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

	// Use this for initialization
	void Start () {
		UpdateScoreListener = new UpdateScoreListener(this);
		ScoreText = GetComponent<Text>();
		UpdateScore(0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
