using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM_Instance { get; private set; } //Setup du GameManager en tant que Singleton 
    public Camera MainCamera {get; private set;} //Pour rendre la camera accessible a tout les scripts 
    public GameObject canvas { get; private set; } // Pour rendre le canvas accessible à tout les scripts, pour instancier des visuels dans l'Unity UI World Space 

    void Awake()
    {
        GM_Instance = this; 
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); //On récupère la camera grâce au tag 
        canvas = GameObject.FindGameObjectWithTag("Canvas"); //pareil pour le canvas
    }


}
