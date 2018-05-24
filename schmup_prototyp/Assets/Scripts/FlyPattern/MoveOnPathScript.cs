using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnPathScript : MonoBehaviour {


    public EditorPathScript PathToFollow;

    public int CurrentWayPointID = 0;

    private float reachDistance = 1.0f;
    //public float rotationSpeed = 5.0f;
    public string pathName;
    private SpriteRenderer rendererComponent;

    [Header("Wie schnell soll er den Path abfliegen?")]
    public float speed;

    Vector3 last_postion;
    Vector3 current_position;

	// Use this for initialization
	void Start () {

        PathToFollow = GameObject.Find(pathName).GetComponent<EditorPathScript>();
        last_postion = transform.position;
		
	}

    void Awake(){
        rendererComponent = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(PathToFollow.path_objs[CurrentWayPointID].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, PathToFollow.path_objs[CurrentWayPointID].position, Time.deltaTime * speed);

        //var rotaion = Quaternion.LookRotation(PathToFollow.path_objs[CurrentWayPointID].position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotaion, Time.deltaTime * rotationSpeed);

        if(distance <= reachDistance)
        {
            CurrentWayPointID++;
        }

        if(CurrentWayPointID >= PathToFollow.path_objs.Count)
        {
            CurrentWayPointID = 0;
        }

        if (rendererComponent.IsVisibleFrom(Camera.main) == false)
            {
                Destroy(gameObject);
            }
		
	}
}
