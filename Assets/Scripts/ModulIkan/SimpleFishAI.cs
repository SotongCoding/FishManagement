using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFishAI : MonoBehaviour {
    // Start is called before the first frame update
    BoxCollider2D fishArea;
    Vector3 destination;
    float maxY, maxX;
    float minX, minY;
    public float speed;

    void Start () {
        fishArea = GetComponentInParent<BoxCollider2D> ();

        maxX = fishArea.bounds.max.x;
        minX = fishArea.bounds.min.x;

        maxY = fishArea.bounds.max.y;
        minY = fishArea.bounds.min.y;

        destination = getNewPos ();
    }

    // Update is called once per frame
    void Update () {
        Moving();
    }

    void Moving () {

        if (Vector2.Distance (this.transform.position, destination) <= 0.1f) {
            destination = getNewPos ();
        }
        transform.position = Vector2.MoveTowards (this.transform.position, destination, speed * Time.fixedDeltaTime);
        //TopDown
        Vector3 heading = destination - transform.position;
        float sudut = (Mathf.Atan2 (heading.y, heading.x)) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.Euler (0, 0, sudut - 90);
    }
    Vector2 getNewPos () {
        Vector2 newDest = new Vector2 (Random.Range (minX, maxX),
            Random.Range (minY, maxY));

        return newDest;
    }
}