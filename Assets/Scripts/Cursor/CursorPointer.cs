using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorPointer : MonoBehaviour
{
    Camera MainCamera; //La cam�ra, qui va �tre utilis� pour cliquer sur des objets dans la sc�ne
    public GameObject CurrentSelected { get; private set; } //Le premier objet s�lectionn� par l'utilisateur

    [Header("Key Codes")]
    [SerializeField] private KeyCode selectKey = KeyCode.Mouse0; //le bouton de base pour s�lectionner est le click gauche

    [Header("Layers")]
    [SerializeField] LayerMask TaskLayerMask; //Le layer des t�ches 
    [SerializeField] LayerMask MatelotLayerMask; //Le layer des matelots
  
    void Start()
    {
        MainCamera = GameManager.GM_Instance.MainCamera; //La cam�ra est r�cup�r�e gr�ce au Singleton
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(selectKey)) //Si le bouton de s�lection est press� 
        {
            if(CurrentSelected == null) //Si rien est s�lectionn� 
            {
                CurrentSelected = PickObject(TaskLayerMask | MatelotLayerMask); //on regarde si l'utilisateur � cliqu� sur une t�che ou un matelot
            }
            else //Si quelque chose est d�j� s�lectionn�, on regarde si on peut assigner un matelot � une t�che
            {
                Assign();
            }
        }

    }
  

    /// <summary>
    /// Fonction pour s�lectionner un objet avec la souris par rapport au layer donn�
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
    /// Fonction pour assigner un matelot � une t�che
    /// </summary>
    void Assign()
    {
        Matelot matelot = CurrentSelected.GetComponent<Matelot>(); //On regarde si l'objet s�lectionn� est un matelot ou une t�che 
        Task task = CurrentSelected.GetComponent<Task>();
        if (matelot && matelot._MatelotStates != Matelot.States.TIRED) //Si c'est un matelot
        {
            GameObject TaskObject = PickObject(TaskLayerMask); //on regarde si l'utilisateur � s�lectionn� une t�che
            if(TaskObject == null) //Sinon on arr�te la fonction et on r�initialise l'objet s�lectionn� actuellement 
            {
                CurrentSelected = null;
                return;
            }

            matelot.AddTask(TaskObject.GetComponent<Task>()); //si l'objet s�lectionn� est une t�che, on peut alors ajouter une t�che au matelot
            CurrentSelected = null; //et on r�initialise l'objet s�lectionn�
        }
        else //Si c'est une t�che
        {
            GameObject MatelotObject = PickObject(MatelotLayerMask); //On regarde si l'utilisateur � s�lectionn� un matelot
            if (MatelotObject == null || MatelotObject.GetComponent<Matelot>()._MatelotStates == Matelot.States.TIRED) //sinon ou si le matelot voulu est fatigu� on r�initialise
            {
                CurrentSelected = null;
                return;
            }
            MatelotObject.GetComponent<Matelot>().AddTask(task); //si l'objet s�lectionn� est un matelot, on peut alors ajouter la t�che au matelot
            CurrentSelected = null; //et on r�initialise l'objet s�lectionn�
        }
    }

}
