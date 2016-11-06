using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
	private static ScoreManager _instance = null;
	public static ScoreManager Instance { get { return _instance; } }

	int score = 0;
	public static int hiScore = 0;

	public TextMesh textMesh;

	void Start()
	{
		_instance = this;
	}

	public void AddScore(int addScore)
	{
		score += addScore;
		textMesh.text = score + "p";

		if (score > hiScore)
		{
			hiScore = score;
		}
	}
}
