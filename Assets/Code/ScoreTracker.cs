using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    Text m_streakText;

    [SerializeField]
    Text m_multiplierText;

    [SerializeField]
    private int m_scoreChangeVelocity = 5;

    [SerializeField]
    int[] m_streakAmountForMultiplierIncrease = { 4, 8, 12, 16, 24, 32, 40 };

    private int m_currentMultiplier;
    private long score;
    private double displayedScore;

	void Start ()
    {
        m_streakText.text = "";
        m_multiplierText.text = "";
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

            m_streakText.text = string.Format("Score: {0}", (int)displayedScore);
        }
    }

    private void OnEnemyDied(Events.EnemyDied evt)
    {
        score += evt.scoreValue * m_currentMultiplier;
    }

    private void OnStreakEnded(Events.StreakEnded evt)
    {
        m_currentMultiplier = 1;
        if(m_multiplierText != null)
        {
            m_multiplierText.text = string.Format("Multiplier: x{0}", m_currentMultiplier);
        }
        GameObject.FindObjectOfType<LowPassFilterManager>().SetFromCombo(0);
    }

    private void OnStreakIncrease(Events.StreakIncrease evt)
    {
        if (m_currentMultiplier >= m_streakAmountForMultiplierIncrease.Length)
            return;

        if (evt.streak >= m_streakAmountForMultiplierIncrease[m_currentMultiplier])
            m_currentMultiplier++;

        if(m_multiplierText != null)
            m_multiplierText.text = string.Format("Multiplier: x{0}", m_currentMultiplier);

        GameObject.FindObjectOfType<LowPassFilterManager>().SetFromCombo(m_currentMultiplier - 1);
    }
}
