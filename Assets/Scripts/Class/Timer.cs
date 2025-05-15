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
    /// fonction pour rafraîchir le timer
    /// </summary>
    /// <param name="value">la valeur à enlever au timer</param>
    public void Remove(float value)
    {
        CurrentValue -= value;
    }

    /// <summary>
    /// Booléen pour regarder si le timer est terminé
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
