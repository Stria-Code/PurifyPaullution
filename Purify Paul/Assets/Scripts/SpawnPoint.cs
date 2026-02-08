using UnityEngine;
using static SceneController;

public class SpawnPoint : MonoBehaviour
{
    public SpawnPointID id;
    public Vector3 cameraOffset = Vector3.zero; // position offset for camera bounds
    public Vector2 cameraBoundsSize = new Vector2(10f, 5f); // width and height for camera bounds
}
