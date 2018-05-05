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

    readonly Color m_endRingColor = BeatRating.Perfect.GetColor();

    [SerializeField]
    float m_showFireEffectSeconds = 0.3f;
	
	// Update is called once per frame
	void Update ()
    {
        if(m_playerFireScript == null)
        {
            return;
        }

        m_ring.transform.localScale = Vector3.one *
            Mathf.Lerp(m_startRingScale, m_endRingScale, m_playerFireScript.ToBeatCenterPercentage);
        m_ring.color = Color.Lerp(m_startRingColor, m_endRingColor, m_playerFireScript.ToBeatCenterPercentage);
	}

    public void OnFire(BeatRating rating)
    {
        StartCoroutine(OnFireRoutine(rating));
    }

    private IEnumerator OnFireRoutine(BeatRating rating)
    {
        for (float elapsed = 0; elapsed < m_showFireEffectSeconds; elapsed += Time.deltaTime)
        {
            yield return null;
            m_inner.color = Color.Lerp(Color.white, rating.GetColor(),
                Mathf.PingPong(elapsed / m_showFireEffectSeconds * 2f, 1f));
        }

        m_inner.color = Color.white;
    }
}
