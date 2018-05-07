using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] m_music;

    [SerializeField]
    int[] m_musicBpm = { 110, 144, 140, 180, 117 };

    [SerializeField]
    AudioSource m_beatAudioSource;

    [SerializeField]
    int m_bpmCapBeforeHalf = 180;

    public static int CurrentBPM { get; private set;} 

    private AudioSource m_audioSource;
    private const float c_delayBeforeMusicStarts = 0.5f;

    private float m_secondsBetweenBeats = 1f;
    private double m_nextBeatTime;
    private double m_musicStartTime;
    private bool m_alreadyFiredForThisBeat;
    private bool m_initialized;
    private double m_previousBeatTime;
    private float m_previousHitDifference;
    private bool m_nextMusicScheduled;

    public void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_nextMusicScheduled = false;
    }

    private void SetupNextMusic()
    {
        CurrentBPM = m_musicBpm[LoadScene.gameSongIndex];
        m_secondsBetweenBeats = 1 / (CurrentBPM / 60.0f);
        m_musicStartTime = AudioSettings.dspTime + c_delayBeforeMusicStarts;
        m_previousBeatTime = m_musicStartTime;

        m_audioSource.Play();
        m_audioSource.SetScheduledStartTime(m_musicStartTime);
        m_beatAudioSource.Stop();
        m_beatAudioSource.Play();
        m_beatAudioSource.SetScheduledStartTime(m_musicStartTime);

        Shader.SetGlobalFloat("_MusicStartTime", Time.time + c_delayBeforeMusicStarts);

        EventSystem.Fire(new Events.MusicChanged
        {
            m_secondsBetweenBeats = m_secondsBetweenBeats,
            m_dspStartTime = m_musicStartTime,
            m_startTime = Time.time
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_audioSource.isPlaying && !m_nextMusicScheduled)
        {
            m_audioSource.clip = m_music[LoadScene.gameSongIndex];
            m_beatAudioSource.clip = m_audioSource.clip;
            SetupNextMusic();
            m_nextMusicScheduled = true;
        }
    }

    private void OnValidate()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_audioSource.loop = false;
        m_audioSource.playOnAwake = false;
    }
}
