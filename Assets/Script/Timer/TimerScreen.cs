using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;

public class TimerScreen : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		StartCoroutine("UpdateTime");
	}

	private IEnumerator UpdateTime()
	{
		while (true)
		{
			gameObject.GetComponentInParent<Text>().text = GameManager.GetTime().ToString(CultureInfo.CurrentCulture);
			yield return null;
		}
	}
}
