using UnityEngine;
public class Timer
{
    public float StartValue;
    public float CurrentValue;

    public Timer(float UstartValue)
    {
        StartValue = UstartValue;
        CurrentValue = StartValue;
    }

    /// <summary>
    /// fonction pour rafra�chir le timer
    /// </summary>
    /// <param name="value">la valeur � enlever au timer</param>
    public void Remove(float value)
    {
        CurrentValue -= value;
    }

    /// <summary>
    /// Bool�en pour regarder si le timer est termin�
    /// </summary>
    /// <returns></returns>
    public bool Done()
    {
        if(CurrentValue <= 0)
        {
            CurrentValue = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

}
