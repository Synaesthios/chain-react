using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    struct StreakIncrease
    {
        public int streak;
        public StreakIncrease(int streak)
        {
            this.streak = streak;
        }
    }

    struct StreakEnded
    {
    }

    struct EnemyDied
    {
        public int scoreValue;
        public Vector3 position;
        public EnemyDied(int scoreValue, Vector3 position)
        {
            this.scoreValue = scoreValue;
            this.position = position;
        }
    }

    struct BossDied
    {
    }

    struct MusicChanged
    {
        public double m_dspStartTime;
        public float m_startTime;
        public float m_secondsBetweenBeats;
    }

    struct OnBeat
    {
    }

    struct PlayerDied
    {
    }
}
