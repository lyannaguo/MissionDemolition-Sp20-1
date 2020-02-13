using UnityEngine;
using System.Collections;
public class FollowCam : MonoBehaviour {
static public GameObject POI; 

[Header("Set in Inspector")]
public float easing = 0.05f;
public Vector2 minXY = Vector2.zero;

[Header("Set Dynamically")]
public float camZ; 

void Awake() {
    camZ = this.transform.position.z;
    }

    void FixedUpdate () {
        Vector3 destination;
        if (POI == null ) {
            destination = Vector3.zero; 
            }else {
            // Get the position of the poi
            destination = POI.transform.position;
            // If poi is a Projectile, check to see if it's at rest
            if (POI.tag == "Projectile" ) {
                if ( POI.GetComponent<Rigidbody>().IsSleeping() ) { 
                    // return to default view
                    POI = null ;
                    // in the next update
                    return ;
                }
            }
        }
        destination.x = Mathf.Max( minXY.x, destination.x ); 
        destination.y = Mathf.Max( minXY.y, destination.y );
        // Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        transform.position = destination; 
        Camera.main.orthographicSize = destination.y + 10; 
        }
}