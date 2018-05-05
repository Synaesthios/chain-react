using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireScript : MonoBehaviour {

    [SerializeField]
    float m_secondsBetweenBeats = 1f;

    [SerializeField]
    PlayerBullet m_bulletPrefab;

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

	// Update is called once per frame
	void Update ()
    {
        m_elapsedSecondsSinceLastBeat += Time.deltaTime;
        if (m_elapsedSecondsSinceLastBeat > m_secondsBetweenBeats)
            m_elapsedSecondsSinceLastBeat = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            var bullet = Instantiate(m_bulletPrefab, transform.position, Quaternion.identity);
            bullet.Init(GetFireRating());
        }
	}

    private float GetSecondsDifferenceFromBeat()
    {
        return m_elapsedSecondsSinceLastBeat > m_goodRatingSecondsBuffer ?
            m_secondsBetweenBeats - m_elapsedSecondsSinceLastBeat :
            m_elapsedSecondsSinceLastBeat;
    }

    private BeatFireRating GetFireRating()
    {
        var secondsDifferenceFromBeat = GetSecondsDifferenceFromBeat();
        if (secondsDifferenceFromBeat < m_perfectRatingSecondsBuffer)
            return BeatFireRating.Perfect;
        else if (secondsDifferenceFromBeat < m_goodRatingSecondsBuffer)
            return BeatFireRating.Good;
        else
            return BeatFireRating.Normal;
    }
    /*
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 400, 200),
            string.Format("{0}\n{1}\n{2}",
            m_elapsedSecondsSinceLastBeat,
            GetSecondsDifferenceFromBeat(),
            GetFireRating().ToString()));
    }
    */
}