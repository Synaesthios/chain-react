using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFireScript : MonoBehaviour {

    [SerializeField]
    PlayerBullet m_bulletPrefab;

    [SerializeField]
    BeatTrackerUI m_trackerUI;
    
    [SerializeField]
    float m_goodRatingSecondsBufferPercent = 0.5f;

    [SerializeField]
    float m_perfectRatingSecondsBufferPercent = 0.5f;

    private float m_secondsBetweenBeats;
    private double m_nextBeatTime;
    private double m_musicStartTime;
    private bool m_alreadyFiredForThisBeat;
    private bool m_initialized;
    private double m_previousBeatTime;

    public int Streak { get; private set; }

    private void Start()
    {
        EventSystem.Subscribe<Events.MusicChanged>(OnMusicChanged);
    }

    private void OnMusicChanged(Events.MusicChanged evt)
    {
        m_secondsBetweenBeats = evt.m_secondsBetweenBeats;
        m_musicStartTime = evt.m_dspStartTime;
        m_musicStartTime += m_secondsBetweenBeats * 0.5f; // Apply offset
        m_previousBeatTime = m_musicStartTime;
        m_nextBeatTime = m_musicStartTime + m_secondsBetweenBeats;
        m_goodRatingSecondsBufferPercent = m_secondsBetweenBeats * m_goodRatingSecondsBufferPercent;
        m_perfectRatingSecondsBufferPercent = m_secondsBetweenBeats * m_perfectRatingSecondsBufferPercent;
        m_initialized = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (AudioSettings.dspTime < m_musicStartTime || !m_initialized)
        {
            return;
        }

        while (AudioSettings.dspTime >= m_nextBeatTime)
        {
            m_alreadyFiredForThisBeat = false;
            m_previousBeatTime = m_nextBeatTime;
            m_nextBeatTime = m_previousBeatTime + m_secondsBetweenBeats;
        }

        if (!m_alreadyFiredForThisBeat && Input.GetMouseButtonDown(0))
        {
            if (GetFireRating() == BeatRating.Perfect)
            {
                Streak++;
                EventSystem.Fire(new Events.StreakIncrease(Streak));
            }
            else
            {
                Streak = 0;
                EventSystem.Fire(new Events.StreakEnded());
            }

            m_alreadyFiredForThisBeat = true;
            m_trackerUI.OnFire(GetFireRating());
            var bullet = Instantiate(m_bulletPrefab, transform.position + transform.forward * 2f, transform.rotation);
            bullet.Init(GetFireRating());
        }

        Shader.SetGlobalFloat("_BeatPercentage", ToBeatCenterPercentage);
	}

    private float GetSecondsDifferenceFromBeat()
    {
        return Mathf.Abs((m_secondsBetweenBeats * 0.5f) - (float)(AudioSettings.dspTime - m_previousBeatTime));
    }

    private BeatRating GetFireRating()
    {
        // Use half the beat time as beat hit point and calculate difference from
        // that midpoint as rating calculation. 
        var secondsDifferenceFromBeat = GetSecondsDifferenceFromBeat();
        if (secondsDifferenceFromBeat < (m_perfectRatingSecondsBufferPercent * 0.5f))
            return BeatRating.Perfect;
        else if (secondsDifferenceFromBeat < (m_goodRatingSecondsBufferPercent * 0.5f))
            return BeatRating.Good;
        else
            return BeatRating.Normal;
    }

    public float ToBeatCenterPercentage
    {
        get
        {
            return 1f - (GetSecondsDifferenceFromBeat() / (m_secondsBetweenBeats * 0.5f));
        }
    }
    
    /*
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 400, 200),
            string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
            m_previousHitDifference,
            GetSecondsDifferenceFromBeat(),
            GetFireRating().ToString(),
            m_alreadyFiredForThisBeat,
            true
            ));
    }
    */

    private void OnValidate()
    {
        if (m_secondsBetweenBeats < m_goodRatingSecondsBufferPercent)
            m_secondsBetweenBeats = m_goodRatingSecondsBufferPercent;
        if (m_perfectRatingSecondsBufferPercent > m_goodRatingSecondsBufferPercent)
            m_perfectRatingSecondsBufferPercent = m_goodRatingSecondsBufferPercent;
    }
}