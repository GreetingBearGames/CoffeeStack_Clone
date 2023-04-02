using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] public ValueController valueController;
    [SerializeField] Transform Player;
    [SerializeField] MovementController movementController;
    private static NodeManager _instance; 
    public List<GameObject> nodes;
    private Transform _lastNode; 
    public Transform lastNode
    {
        get {
            return _lastNode; }
        set { _lastNode = value; }
    }
    

    public static NodeManager Instance 
    {     
        get
        {
            if (_instance == null)
            {
                Debug.LogError("NodeManager Null");
            }
            return _instance;
        }
    }

    private void Awake() {
        _instance = this;
        lastNode = Player;
    }

    public void MovePlayerToMid(){
        movementController.isFinished = true;
    }
}
