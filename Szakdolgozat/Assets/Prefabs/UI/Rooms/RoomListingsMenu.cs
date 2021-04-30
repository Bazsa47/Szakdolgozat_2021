using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListing roomListing;

    [SerializeField]
    private Transform content;

    private List<RoomListing> listings = new List<RoomListing>();
    private RoomsCanvases roomsCanvases;
    public override void OnJoinedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
        content.DestroyChildren();
        listings.Clear();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        roomsCanvases = canvases;
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList){
            if (info.RemovedFromList || info.MaxPlayers == info.PlayerCount)
                DestroyRoomListing(info);
            else{
                int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1) {
                    RoomListing listing = Instantiate(roomListing, content);
                    if (listing != null)
                    {
                        listing.SetRoomInfo(info);    
                        listings.Add(listing);
                    }
                }else
                {
                    listings[index].SetRoomInfo(info);
                }
               
            }         
        }
    }

    public void DestroyRoomListing(RoomInfo info)
    {
        int index = listings.FindIndex(x => x.RoomInfo.Name == info.Name);
        if (index != -1){
            Destroy(listings[index].gameObject);
            listings.RemoveAt(index);
        }
    }
}
