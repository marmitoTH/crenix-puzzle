using UnityEngine;

public class Nugget : MonoBehaviour
{
	public TMPro.TMP_Text speech;

	public void Speak(string text)
	{
		speech.text = text;
	}
}
