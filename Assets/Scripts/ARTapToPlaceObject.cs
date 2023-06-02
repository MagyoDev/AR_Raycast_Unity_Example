using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))] //Gerenciador de elenco de Matriz
public class ARTapToPlaceObject : MonoBehaviour
{   
    public GameObject gameObjectToInstantiate; //Objeto colocado dentro da sala

    private GameObject spawnedObject; //Referência para o objeto criado
    private ARRaycastManager _arRaycastManager; //Projeção de raio
    private Vector2 touchPosition; //Vetor para a posição de toque para detectar onde o raycast tera que ser lançado

    static List<ARRaycastHit> hits = new List<ARRaycastHit>(); //Referências de acerto do raycast

    //Referência o raycast
    private void Awake() 
    {
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    //Entrada para toque
    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0) //Contagem de toque
        //Caso tenha toques > 0 retorna como verdadeiro caso não falso
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }

    void Update()
    {
        if(!TryGetTouchPosition(out Vector2 touchPosition)) //Gravar a matriz de toque
        return;

        if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon)) //Gravar Raycast
        {
            var hitPose = hits[0].pose;

            if(spawnedObject == null)
            {
                spawnedObject = Instantiate(gameObjectToInstantiate,hitPose.position, hitPose.rotation); //Objeto instaciado com posição e rotação
            }
            else 
            {
                spawnedObject.transform.position = hitPose.position; //Caso tenha um objeto e possível movê-lo 
            }
        }
    }
}
