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
        public EnemyDied(int scoreValue)
        {
            this.scoreValue = scoreValue;
        }
    }

    struct BossDied
    {
    }
}
