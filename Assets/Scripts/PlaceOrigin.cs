using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using UnityEngine.UI;
public class PlaceOrigin : MonoBehaviour
{

    public Toggle Tlock;


    public  bool isLock;


    public Vector3 offestPosition;
    public Vector3 offestRotation;

    public float offset = 0.01f;



        public void setPostion(string dir) {

         switch (dir) {
            case "w":
                offestPosition += new Vector3(0, 0, offset);
                break;
            case "a":
                offestPosition -= new Vector3(offset, 0, 0);
                break;
            case "s":
                offestPosition -= new Vector3(0, 0, offset);
                break;
            case "d":
                offestPosition += new Vector3(offset, 0, 0);
                break;
        }

        AnChor.position += offestPosition;
    }    



 
        /// <summary> A model to place when a raycast from a user touch hits a plane. </summary>
        public Transform AnChor;

        /// <summary> Updates this object. </summary>
        void Update()
        {



        isLock = Tlock.isOn;


           

        // If the player doesn't click the trigger button, we are done with this update.
        if (!NRInput.GetButtonDown(ControllerButton.TRIGGER))
            {
                return;
            }

            // Get controller laser origin.
            var handControllerAnchor = NRInput.DomainHand == ControllerHandEnum.Left ? ControllerAnchorEnum.LeftLaserAnchor : ControllerAnchorEnum.RightLaserAnchor;
            Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : handControllerAnchor);

            RaycastHit hitResult;
            if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10))
            {
                if (hitResult.collider.gameObject != null && hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>() != null)
                {

           
                var behaviour = hitResult.collider.gameObject.GetComponent<NRTrackableBehaviour>();
                    if (behaviour.Trackable.GetTrackableType() != TrackableType.TRACKABLE_PLANE)
                    {
                        return;
                    }

                if (!isLock) {
                    AnChor.position = hitResult.point;
                    isLock = true;
                    Tlock.isOn = isLock;
                }
                    // Instantiate Andy model at the hit point / compensate for the hit point rotation.
                   // Instantiate(AndyPlanePrefab, hitResult.point, Quaternion.identity, behaviour.transform);
                }
            }



        }
        
}
