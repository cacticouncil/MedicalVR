using UnityEngine;
using System.Collections;

public static class EventManager {
    public delegate void PauseDelegate();
    public delegate void UnpauseDelegate();
    public delegate void FreezeDelegate();
    public delegate void ThawDelegate();
    public delegate void VolumeDelegate(float volume);
    public delegate void TimeManipulation();
    public delegate void TimeFix();

    public static float EventTime = 1.0f;           //MovementTime in the world     0-1
    public static float PlayerTime = 1.0f;          //Player moverment in the world 0-1


    public static event PauseDelegate OnPause;
    public static event UnpauseDelegate OnUnpause;
    public static event FreezeDelegate OnFreeze;
    public static event ThawDelegate OnThaw;
    public static event VolumeDelegate OnVolumeChanged;
    public static event TimeManipulation TimeControl;
    public static event TimeFix ReturnTime;

    //SCORE/MULTIPLIER EMP
    //These delegate/events are called to change the score or end a kill streak
    public delegate void EnemyDeath(int scoreGain);
    public static event EnemyDeath OnEnemyDies;
    public delegate void SoulsGathered(int NumSouls);
    public static event SoulsGathered OnSoulGathered;
    public delegate void DamageTaken();
    public static event DamageTaken OnDamageTaken;

    static bool isPaused = false;
    static bool isFrozen = false;
    static bool isTime = false;

    public static void Pause() {
        if(OnPause != null) {
            OnPause();
        }
        isPaused = true;
    }

    public static void Unpause() {
        if(OnUnpause != null) {
            OnUnpause();
        }
        isPaused = false;
    }

    public static void Freeze()
    {
        if(OnFreeze != null)
        {
            OnFreeze();
        }
        isFrozen = true;
    }

    public static void Thaw()
    {
        if (OnThaw != null)
        {
            OnThaw();
        }
        isFrozen = false;
    }
    public static void TimeM()
    {
        if(TimeControl != null)
        {
            TimeControl();
        }
        isTime = true;
    }

    public static void ReturnT()
    {
        if(ReturnTime != null)
        {
            ReturnT();
        }
        isTime = false;
    }

    public static void VolumeChanged(float v) {
        if(OnVolumeChanged != null) {
            OnVolumeChanged(v);
        }
    }

    public static bool IsPaused() {
        return isPaused;
    }

    public static bool IsFrozen()
    {
        return isFrozen;
    }

    public static bool IsTime()
    {
        return isTime;
    }

    //SCORE/MULTIPLIER EMP
    public static void EnemyDied(int value)
    {
        if (OnEnemyDies != null)
        {
            OnEnemyDies(value);
        }
    }

    public static void GatherSouls(int NumSouls)
    {
        if (OnSoulGathered != null)
        {
            OnSoulGathered(NumSouls);
        }
    }

    public static void BreakStreak()
    {
        if (OnDamageTaken != null)
        {
            OnDamageTaken();
        }
    }
}
