using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFireScript : MonoBehaviour {

    [SerializeField]
    float m_secondsBetweenBeats = 1f;

    [SerializeField]
    PlayerBullet m_bulletPrefab;

    [SerializeField]
    Image m_beatRingImage;


    [SerializeField]
    Image m_beatRingInnerImage;
    
    public enum BeatFireRating
    {
        Normal,
        Good,
        Perfect
    }

    [SerializeField]
    float m_goodRatingSecondsBuffer = 0.3f;

    [SerializeField]
    float m_perfectRatingSecondsBuffer = 0.2f;

    private float m_elapsedSecondsSinceLastBeat;
    private bool m_alreadyFiredForThisBeat;

	// Update is called once per frame
	void Update ()
    {
        m_elapsedSecondsSinceLastBeat += Time.deltaTime;
        if (m_elapsedSecondsSinceLastBeat > m_secondsBetweenBeats)
        {
            m_alreadyFiredForThisBeat = false;
            m_elapsedSecondsSinceLastBeat = 0;
        }

        UpdateBeatRing();

        if (!m_alreadyFiredForThisBeat && Input.GetMouseButtonDown(0))
        {
            m_alreadyFiredForThisBeat = true;
            var bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
            bullet.Init(GetFireRating());
        }

	}

    private float GetSecondsDifferenceFromBeat()
    {
        return Mathf.Abs((m_secondsBetweenBeats * 0.5f) - m_elapsedSecondsSinceLastBeat);
    }

    private BeatFireRating GetFireRating()
    {
        // Use half the beat time as beat hit point and calculate difference from
        // that midpoint as rating calculation. 
        var secondsDifferenceFromBeat = GetSecondsDifferenceFromBeat();
        if (secondsDifferenceFromBeat < (m_perfectRatingSecondsBuffer * 0.5f))
            return BeatFireRating.Perfect;
        else if (secondsDifferenceFromBeat < (m_goodRatingSecondsBuffer * 0.5f))
            return BeatFireRating.Good;
        else
            return BeatFireRating.Normal;
    }

    private void UpdateBeatRing()
    {

    }

    private IEnumerator ShowInnerBeatRingFireROutine()
    {
        yield return null;
    }

    public float ToBeatCenterPercentage
    {
        get
        {
            return 1f - (GetSecondsDifferenceFromBeat() / (m_secondsBetweenBeats * 0.5f));
        }
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 400, 200),
            string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
            m_elapsedSecondsSinceLastBeat,
            GetSecondsDifferenceFromBeat(),
            GetFireRating().ToString(),
            m_alreadyFiredForThisBeat,
            true
            ));
    }

    private void OnValidate()
    {
        if (m_secondsBetweenBeats < m_goodRatingSecondsBuffer)
            m_secondsBetweenBeats = m_goodRatingSecondsBuffer;
        if (m_perfectRatingSecondsBuffer > m_goodRatingSecondsBuffer)
            m_perfectRatingSecondsBuffer = m_goodRatingSecondsBuffer;
    }
}