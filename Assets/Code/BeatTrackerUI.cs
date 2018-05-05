using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeatTrackerUI : MonoBehaviour
{
    [SerializeField]
    PlayerFireScript m_playerFireScript;

    [SerializeField]
    Image m_ring;

    [SerializeField]
    Image m_inner;

    [SerializeField]
    float m_startRingScale = 3f;

    [SerializeField]
    float m_endRingScale = 1f;

    [SerializeField]
    Color m_startRingColor = Color.clear;

    [SerializeField]
    Color m_endRingColor = Color.green;
       
	
	// Update is called once per frame
	void Update ()
    {
        m_ring.transform.localScale = Vector3.one *
            Mathf.Lerp(m_startRingScale, m_endRingScale, m_playerFireScript.ToBeatCenterPercentage);
        m_ring.color = Color.Lerp(m_startRingColor, m_endRingColor, m_playerFireScript.ToBeatCenterPercentage);
	}

    public void OnFire()
    {
        StartCoroutine(OnFireRoutine());
    }

    private IEnumerator OnFireRoutine()
    {
        for (float elapsed = 0; elapsed < 0.3f; elapsed += Time.deltaTime)
        {
            yield return null;
            m_inner.color = Color.Lerp(Color.white, Color.green,
                Mathf.PingPong(elapsed, elapsed * 0.5f));
        }

        m_inner.color = Color.white;
    }
}
