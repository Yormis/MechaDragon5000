using UnityEngine;
using System.Collections;

public class Burnable : MonoBehaviour
{
	public int score = 1;

	public GameObject explosionParticle;

	public void Burn()
	{
		ScoreManager.Instance.AddScore(score);

		if (explosionParticle != null)
		{
			Instantiate (explosionParticle, transform.position, Quaternion.identity);
		}

		Destroy (gameObject);
	}
   
}
