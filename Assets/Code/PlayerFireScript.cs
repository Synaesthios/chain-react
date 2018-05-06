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
    BeatTrackerUI m_trackerUI;
    
    [SerializeField]
    float m_goodRatingSecondsBufferPercent = 0.5f;

    [SerializeField]
    float m_perfectRatingSecondsBufferPercent = 0.5f;

    [SerializeField]
    AudioSource m_audioSource;

    private const float c_delayBeforeMusicStarts = 2f;

    private double m_nextBeatTime;
    private double m_musicStartTime;
    private bool m_alreadyFiredForThisBeat;
    private bool m_initialized;
    private double m_previousBeatTime;
    private float m_previousHitDifference;

    public int Streak { get; private set; }

    private void Awake()
    {
        m_secondsBetweenBeats = 1 / (UniBpmAnalyzer.AnalyzeBpm(m_audioSource.clip)/ (60.0f));
        m_audioSource.Play();
        m_musicStartTime = AudioSettings.dspTime + c_delayBeforeMusicStarts;
        m_previousBeatTime = m_musicStartTime;
        m_audioSource.SetScheduledStartTime(m_musicStartTime);
        Shader.SetGlobalFloat("_MusicStartTime", Time.time + c_delayBeforeMusicStarts);
        m_goodRatingSecondsBufferPercent = m_secondsBetweenBeats * m_goodRatingSecondsBufferPercent;
        m_perfectRatingSecondsBufferPercent = m_secondsBetweenBeats * m_perfectRatingSecondsBufferPercent;
    }

    // Update is called once per frame
    void Update ()
    {
        if (AudioSettings.dspTime < m_musicStartTime)
        {
            return;
        }
        else if (!m_initialized)
        {
            m_initialized = true;
            m_musicStartTime += m_secondsBetweenBeats * 0.5f;
            m_previousBeatTime = m_musicStartTime;
            m_nextBeatTime = m_musicStartTime + m_secondsBetweenBeats;
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
                m_previousHitDifference = GetSecondsDifferenceFromBeat();
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