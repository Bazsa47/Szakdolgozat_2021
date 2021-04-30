using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    private RoomsCanvases roomsCanvases;
    [SerializeField]
    private PlayerListingsMenu playerListingsMenu;

    [SerializeField]
    private LeaveRoomMenu leaveRoomMenu;
    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
        playerListingsMenu.Firstinitialize(canvases);
        leaveRoomMenu.FirstInitialize(canvases);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
