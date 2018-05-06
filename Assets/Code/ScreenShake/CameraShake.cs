using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour {
	
	public float duration = 0.5f;
	public float speed = 1.0f;
	public float magnitude = 0.1f;

	public UnityStandardAssets.Utility.FollowTarget followTarget;

	private float timeSpent;

	void Start() {

	}
	// -------------------------------------------------------------------------
	public void PlayShake() {
		
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	// ------------------------------------------------------------------------
	
	// -------------------------------------------------------------------------
	IEnumerator Shake() {
		followTarget.enabled = false;
		float elapsed = 0.0f;
		float randomStart = Random.Range(-1000.0f, 1000.0f);
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + speed * percentComplete;
			
			// map noise to [-1, 1]
			float x = Util.Noise.GetNoise(alpha, 0.0f, 0.0f) * 2.0f - 1.0f;
			float z = Util.Noise.GetNoise(0.0f, alpha, 0.0f) * 2.0f - 1.0f;
			
			x *= magnitude * damper;
			z *= magnitude * damper;
			
			GetComponent<Camera>().transform.position = new Vector3(x, 0, z) + followTarget.GetUpdatedPosition();
				
			yield return null;
		}

		followTarget.enabled = true;
		
	}
}