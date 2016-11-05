using UnityEngine;
using System.Collections;

public class Burnable : MonoBehaviour
{
	public void Burn()
	{
		Destroy (gameObject);
	}
   
}
