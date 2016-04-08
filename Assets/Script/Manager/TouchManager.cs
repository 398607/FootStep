using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		int count = Input.touchCount;
		// Debug.Log(count);
		if (count != 0)
		{
			Debug.Log(count);
			foreach (var touch in Input.touches)
			{
				if (touch.phase == TouchPhase.Began)
					Debug.Log(touch.position);
			}
		}
	}
}
