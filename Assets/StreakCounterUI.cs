using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class StreakCounterUI : MonoBehaviour
{
    private Text m_text;

    [SerializeField]
    float m_pulseTextTime = 0.5f;

    [SerializeField]
    float m_pulseTextScaleExtraSize = 0.2f;

    private void Awake()
    {
        m_text = GetComponent<Text>();
        m_text.text = "";
        EventSystem.Subscribe<Events.StreakIncrease>(OnStreakIncrease);
        EventSystem.Subscribe<Events.StreakEnded>(OnStreakEnded);
    }

    private void OnStreakIncrease(Events.StreakIncrease evt)
    {
        if(this == null)
        {
            return;
        }
        StartCoroutine(OnStreakIncreaseRoutine(evt.streak));
    }

    private void OnStreakEnded(Events.StreakEnded evt)
    {
        m_text.text = "";
    }

    private IEnumerator OnStreakIncreaseRoutine(int streak)
    {
        m_text.text = string.Format("Streak: {0}", streak);
        for (float elapsed = 0; elapsed < m_pulseTextTime; elapsed += Time.deltaTime)
        {
            yield return null;
            m_text.transform.localScale = Vector3.one *
                (1f + (Mathf.PingPong(elapsed / m_pulseTextTime * 2f, 1f) * m_pulseTextScaleExtraSize));
        }

        m_text.transform.localScale = Vector3.one;
    }
}
