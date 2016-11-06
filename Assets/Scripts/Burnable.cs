using UnityEngine;
using System.Collections;

public class Burnable : MonoBehaviour
{
	public int score = 1;

	public void Burn()
	{
		ScoreManager.Instance.AddScore(score);
		Destroy (gameObject);
	}
   
}
