using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MatelotParameter : ScriptableObject
{
    public float MaxEnergy; //L'�nergie du matelot avant qu'il se fatigue
    public float Efficiency; //efficacit� � r�aliser une t�che 
    public float WalkSpeed; //Vitesse de marche
    public float EnergyRecoveryRate; //La vitesse � laquelle le matelot r�cup�re son �nergie
}
