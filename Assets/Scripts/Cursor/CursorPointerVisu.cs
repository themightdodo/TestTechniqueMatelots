using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CursorPointer))]
public class CursorPointerVisu : MonoBehaviour
{
    [Header("Visuel")]
    [SerializeField] GameObject SelectedObjectVisu; //L'indicateur d'objet sélectionné
    VFX SelectedObjectVFX;
    CursorPointer Pointer;//le script de sélection d'objets
    GameObject canvas;

    void Start()
    {
        SelectedObjectVFX = new VFX(SelectedObjectVisu);
        Pointer = GetComponent<CursorPointer>();
        canvas = GameManager.GM_Instance.canvas; //On récupère le canvas avec le singleton 
    }

    // Update is called once per frame
    void Update()
    {
        if(Pointer.CurrentSelected != null) //Si un objet est sélectionné le visuel est instancié et calqué sur la position de l'objet
        {
            SelectedObjectVFX.InstanciateVFX(canvas);
            SelectedObjectVFX.CurrentVFX.transform.position = Pointer.CurrentSelected.transform.position;
        }
        else
        {
            SelectedObjectVFX.KillFX();//sinon on s'assure que le visuel est bien détruit
        }
    }
}
