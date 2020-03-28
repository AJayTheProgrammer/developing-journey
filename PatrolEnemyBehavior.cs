using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyBehavior : MonoBehaviour {

    float health = 100;
    UnityEngine.AI.NavMeshAgent nav;
    Transform player;
    public GameObject[] waypoints = new GameObject[6];
    int currentWaypoint = 0;
    public GameObject shot;
    float shotTime = 0;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If the player is close enough to the enemy, the enemy will move towards the player,
        // If the player and the enemy are close enough, the enemy will 'shoot' the player causing him to lose health.
        if (Vector3.Distance(player.position, this.transform.position) <= 10f)
        {
            nav.SetDestination(player.position);
            if (Vector3.Distance(player.position, this.transform.position) <= 2f && Time.time >= shotTime)
                shoot();
        }
        else
        {
            // Then, if the player and enemy are too far apart the enemy will continue to patrol the area using an array list of waypoints.
            nav.SetDestination(waypoints[currentWaypoint].transform.position);
            if (Vector3.Distance(waypoints[currentWaypoint].transform.position, this.transform.position) <= 2f)
            {
                if (currentWaypoint == 5)
                    currentWaypoint = 0;
                else
                    currentWaypoint++;
            }
        }
    }


    // This method creates a GameObject, then the object is shot at a specific velocity.
    // There is also a delay of 2 floats so that the enemy can not constantly shoot the game object.

    void shoot()
    {
        GameObject clone;
        clone = Instantiate(shot, this.transform.position + transform.forward * 2 + transform.up, this.transform.rotation) as GameObject;
        clone.GetComponent<Rigidbody>().velocity = this.transform.forward * 30;
        Destroy(clone, 5.0f);
        shotTime = Time.time + 2f;
    }


    // This unique method checks to see if an object has a collided with another object that contains a different collider.
    // It also checks to see if the object has a certain tag to know whether or not to decrease health.
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Shot")
        {
            float amount = float.Parse(col.gameObject.name.Substring(0, 2)); //gets the damage number 
            health += -amount;
            Destroy(col.gameObject);
           if (health <= 0)
               this.gameObject.SetActive(false);
        }
        else if (col.gameObject.tag == "friend")
        {
           health += -10f;
           Destroy(col.gameObject);
            if (health <= 0)
                this.gameObject.SetActive(false);
        }
    }
}
