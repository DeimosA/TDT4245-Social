using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStat
{
    USERBASE,
    CAPITAL,
    REPUTATION
}

[Serializable]
public struct StatPrerequisite
{
    public PlayerStat stat;
    public int targetValue;
    [Tooltip("True if player's stat must match or exceed, false is stat must be lesser than")]
    public bool statMustBeEqualOrGreater;
}

[Serializable]
public struct StatChange
{
    public PlayerStat stat;
    public int statChange;
}
