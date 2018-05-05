using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BeatRating
{
    Normal,
    Good,
    Perfect
}

static class BeatRatingExtensions
{
    public static Color GetColor(this BeatRating rating)
    {
        switch (rating)
        {
            case BeatRating.Normal:
                return Color.yellow;
            case BeatRating.Good:
                return Color.cyan;
            case BeatRating.Perfect:
                return Color.green;
        }

        return Color.white;
    }
}