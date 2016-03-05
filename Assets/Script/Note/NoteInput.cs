using UnityEngine;
using System.Collections;

public class NoteInput
{
	public bool Clicked = false;
	public float ClickTime = 0f;

	public NoteInput(bool clicked, float clickTime)
	{
		Clicked = clicked;
		ClickTime = clickTime;
	}
}
