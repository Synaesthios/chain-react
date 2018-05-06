using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPassFilterManager : MonoBehaviour {

    private float m_desiredAmount = 0;
    public AudioLowPassFilter LowPassFilter;

    private const float LerpSpeed = 1f;

    private List<float> ComboAmounts = new List<float>()
    {
        0,
        500,
        1000,
        1500,
        2000,
        2500,
        3200,
        4000,
        5000
    };

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        float currentFreq = LowPassFilter.cutoffFrequency;

        currentFreq = Mathf.Lerp(currentFreq, m_desiredAmount, LerpSpeed * Time.deltaTime);
        LowPassFilter.cutoffFrequency = currentFreq;

    }

    public void SetFromCombo(int index)
    {
        SetFilterAmount(ComboAmounts[index]);
    }

    public void SetFilterAmount(float amount)
    {
        m_desiredAmount = amount;
    }

    public void SetFilterAmountImmediate(float amount)
    {
        m_desiredAmount = 0;
        LowPassFilter.cutoffFrequency = 0;
    }
}
