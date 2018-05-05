using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    Text m_text;

    [SerializeField]
    private int m_scoreChangeVelocity = 5;

    [SerializeField]
    int[] m_streakAmountForMultiplierIncrease = { 4, 8, 16, 32, 64, 128, 256 };

    private int m_currentMultiplier;
    private long score;
    private double displayedScore;

	void Start ()
    {
        m_text = GetComponent<Text>();
        m_text.text = "";
        m_currentMultiplier = 1;
        EventSystem.Subscribe<Events.EnemyDied>(OnEnemyDied);
        EventSystem.Subscribe<Events.StreakIncrease>(OnStreakIncrease);
        EventSystem.Subscribe<Events.StreakEnded>(OnStreakEnded);
	}

    private void Update()
    {
        if (displayedScore != score)
        {
            displayedScore += m_scoreChangeVelocity * Time.deltaTime;

            if (displayedScore > score)
                displayedScore = score;

            m_text.text = string.Format("Score: {0}\nMultiplier: x{1}", (int)displayedScore, m_currentMultiplier);
        }
    }

    private void OnEnemyDied(Events.EnemyDied evt)
    {
        score += evt.scoreValue * m_currentMultiplier;
    }

    private void OnStreakEnded(Events.StreakEnded evt)
    {
        m_currentMultiplier = 1;
    }

    private void OnStreakIncrease(Events.StreakIncrease evt)
    {
        if (m_currentMultiplier >= m_streakAmountForMultiplierIncrease.Length)
            return;

        if (evt.streak >= m_streakAmountForMultiplierIncrease[m_currentMultiplier])
            m_currentMultiplier++;
    }
}
