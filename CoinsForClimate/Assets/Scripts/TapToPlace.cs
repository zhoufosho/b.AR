using UnityEngine;

public class TapToPlace : MonoBehaviour
{
    bool placed = false;

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        // Start the game
        ClimateManager.BroadcastStart();

        placed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnSelect();
        }

        if (!placed)
        {
            // Do a raycast into the world that will only hit the Spatial Mapping mesh.
            var headPosition = Camera.main.transform.position;
            var gazeDirection = Camera.main.transform.forward;

            RaycastHit hitInfo;
            if (Physics.Raycast(headPosition, gazeDirection, out hitInfo,
                30.0f, SpatialMapping.PhysicsRaycastMask))
            {
                // Move this  object to where the raycast hit the
                // Spatial Mapping mesh.
                transform.position = hitInfo.point;
                transform.rotation.SetLookRotation(Vector3.up);

                // Rotate this object's parent object to face the user.
                //Quaternion toQuat = Camera.main.transform.localRotation;
                //toQuat.x = 0;
                //toQuat.z = 0;
                //transform.rotation = toQuat;
            }
        }
    }
}
