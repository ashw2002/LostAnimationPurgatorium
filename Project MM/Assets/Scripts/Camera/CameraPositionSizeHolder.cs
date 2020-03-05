using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionSizeHolder : MonoBehaviour
{
    //The purpose for this little code's existence is so that when the player enters this gameobjects collider, there's something to set the cameras size to.
    public float size;
    public bool followingPlayer;
}
