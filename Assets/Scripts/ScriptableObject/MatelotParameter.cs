using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MatelotParameter : ScriptableObject
{
    public float MaxEnergy; //L'énergie du matelot avant qu'il se fatigue
    public float Efficiency; //efficacité à réaliser une tâche 
    public float WalkSpeed; //Vitesse de marche
    public float EnergyRecoveryRate; //La vitesse à laquelle le matelot récupère son énergie
}
