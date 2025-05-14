using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX //Une classe pour facilter la création d'instances dans la scène
{
    public GameObject VFXObject; //L'objet à instancier
    public GameObject CurrentVFX; //l'objet instancié
    public VFX(GameObject VfxObj)
    {
        VFXObject = VfxObj;
    }
    /// <summary>
    /// Instancier un Visuel en tant qu'enfant d'un objet
    /// </summary>
    public void InstanciateVFX(GameObject obj)
    {
        if(CurrentVFX == null)
        {
            CurrentVFX = Object.Instantiate(VFXObject,obj.transform);
        }
    }

    /// <summary>
    /// Détruire le visuel
    /// </summary>
    public void KillFX()
    {
        if (CurrentVFX == null)
        {
            return;
        }

        Object.Destroy(CurrentVFX);
    }





}
