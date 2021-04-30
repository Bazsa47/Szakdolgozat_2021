using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    [SerializeField]
    private CreateOrJoinRoomCanvas createOrJoinRoomCanvas;

    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas { get => createOrJoinRoomCanvas; }

    [SerializeField]
    private CurrentRoomCanvas currentRoomCanvas;

    public CurrentRoomCanvas CurrentRoomCanvas { get => currentRoomCanvas; }

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        CreateOrJoinRoomCanvas.FirstInitialize(this); 
        CurrentRoomCanvas.FirstInitialize(this);
    }

}
