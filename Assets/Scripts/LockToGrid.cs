using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class LockToGrid : MonoBehaviour
{
    public int tileSize = 1;
    public Vector3 tileOffset = Vector3.zero;


    void Update()
    {
        #if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            Vector3 currentPosition = transform.position;

            float snappedX = Mathf.Round(currentPosition.x / tileSize) * tileSize + tileOffset.x;
            float snappedZ = Mathf.Round(currentPosition.z / tileSize) * tileSize + tileOffset.z;
            float snappedY = Mathf.Round(currentPosition.y / tileSize) * tileSize + tileOffset.y;

            Vector3 snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
            transform.position = snappedPosition;
        }
        #endif
    }
}