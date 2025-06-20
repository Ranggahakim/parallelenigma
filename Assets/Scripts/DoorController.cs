using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject closedDoor;
    public GameObject openDoor;
    public void OpenDoor()
    {
        closedDoor.SetActive(false);
        openDoor.SetActive(true);
        Debug.Log("Door changed to open state.");
    }
}