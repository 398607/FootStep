using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour
{
	public float Time = 0f;
	
	public void StartTimer()
	{
		StartCoroutine("UpdateTime");
	}

	// Update is called once per frame
	private void Update ()
	{
	}

	private IEnumerator UpdateTime()
	{
		while (true)
		{
			Time += UnityEngine.Time.deltaTime;
			yield return null;
		}
	}
}
