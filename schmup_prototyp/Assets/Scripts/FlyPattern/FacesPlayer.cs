using UnityEngine;

public class FacesPlayer : MonoBehaviour {

    Transform player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player == null)
        {
            GameObject go = GameObject.Find("Player");

            if (go != null)
            {
                player = go.transform;
            }
        }

        Vector3 pos = player.transform.position;

        transform.position = new Vector3 (transform.position.x,pos.y,transform.position.z);

        //if (transform.position.x <= player.transform.position.x){

            //Quaternion rot = player.transform.rotation;
            
        //transform.rotation = new Quaternion (transform.rotation.w,transform.rotation.x,-180,transform.rotation.z);
        //}
    }
}
