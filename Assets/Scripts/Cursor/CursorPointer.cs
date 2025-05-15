using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPointer : MonoBehaviour
{
    Camera MainCamera; //La caméra, qui va être utilisé pour cliquer sur des objets dans la scène
    public GameObject CurrentSelected { get; private set; } //Le premier objet sélectionné par l'utilisateur

    [Header("Key Codes")]
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0; //le bouton de base pour sélectionner est le click gauche

    [Header("Layers")]
    [SerializeField] LayerMask TaskLayerMask; //Le layer des tâches 
    [SerializeField] LayerMask MatelotLayerMask; //Le layer des matelots
  
    void Start()
    {
        MainCamera = GameManager.GM_Instance.MainCamera; //La caméra est récupérée grâce au Singleton
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(selectKey)) //Si le bouton de sélection est pressé 
        {
            if(CurrentSelected == null) //Si rien est sélectionné 
            {
                CurrentSelected = PickObject(TaskLayerMask | MatelotLayerMask); //on regarde si l'utilisateur à cliqué sur une tâche ou un matelot
            }
            else //Si quelque chose est déjà sélectionné, on regarde si on peut assigner un matelot à une tâche
            {
                Assign();
            }
        }

    }
  

    /// <summary>
    /// Fonction pour sélectionner un objet avec la souris par rapport au layer donné
    /// </summary>
    GameObject PickObject(LayerMask layerMask) 
    {
        
        Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {

             return hit.transform.gameObject;

        }
        return null;
        
    }
    /// <summary>
    /// Fonction pour assigner un matelot à une tâche
    /// </summary>
    void Assign()
    {
        Matelot matelot = CurrentSelected.GetComponent<Matelot>(); //On regarde si l'objet sélectionné est un matelot ou une tâche 
        Task task = CurrentSelected.GetComponent<Task>();
        if (matelot && matelot._MatelotStates != Matelot.States.TIRED) //Si c'est un matelot
        {
            GameObject TaskObject = PickObject(TaskLayerMask); //on regarde si l'utilisateur à sélectionné une tâche
            if(TaskObject == null) //Sinon on arrête la fonction et on réinitialise l'objet sélectionné actuellement 
            {
                CurrentSelected = null;
                return;
            }

            matelot.AddTask(TaskObject.GetComponent<Task>()); //si l'objet sélectionné est une tâche, on peut alors ajouter une tâche au matelot
            CurrentSelected = null; //et on réinitialise l'objet sélectionné
        }
        else //Si c'est une tâche
        {
            GameObject MatelotObject = PickObject(MatelotLayerMask); //On regarde si l'utilisateur à sélectionné un matelot
            if (MatelotObject == null || MatelotObject.GetComponent<Matelot>()._MatelotStates == Matelot.States.TIRED) //sinon ou si le matelot voulu est fatigué on réinitialise
            {
                CurrentSelected = null;
                return;
            }
            MatelotObject.GetComponent<Matelot>().AddTask(task); //si l'objet sélectionné est un matelot, on peut alors ajouter la tâche au matelot
            CurrentSelected = null; //et on réinitialise l'objet sélectionné
        }
    }

}
