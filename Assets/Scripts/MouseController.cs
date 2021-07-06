using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController
{
    Vector2 dragStartPosition;
    Vector2 launchVector;
    bool startedInCircle;
    GameObject tracer;

    GameObject atom;
    Rigidbody2D rb;
    CircleCollider2D coll;
    GameObject tracerPrefab;

    public MouseController(GameObject passsedAtom, GameObject tracerprefab)
    {
        atom = passsedAtom;
        rb = atom.GetComponent<Rigidbody2D>();
        coll = atom.GetComponent<CircleCollider2D>();
        tracerPrefab = tracerprefab;
    }

    public void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0) && InCircle())
        {
            startedInCircle = true;
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tracer = GameObject.Instantiate(tracerPrefab);
            tracer.SetActive(false);
            //Debug.Log(atom.GetComponent<Particle>().element.molecule.GetName());
            Debug.Log(atom.GetComponent<Particle>().element.molecule.GetRelativeCharge());
            atom.GetComponent<Particle>().playerInteracted = true;
        }
        if (Input.GetMouseButton(0) && startedInCircle)
        {
            Vector2 dragCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            launchVector = dragStartPosition - dragCurrentPosition;
            if (launchVector.magnitude > 0)
            {
                tracer.SetActive(true);
                tracer.transform.localPosition = (launchVector / -2f) + (Vector2) atom.transform.position ;
                tracer.transform.localPosition = new Vector3(tracer.transform.localPosition.x, tracer.transform.localPosition.y, 1f);
                tracer.transform.rotation = Quaternion.Euler(0, 0, (Mathf.Rad2Deg * Mathf.Atan(launchVector.x / launchVector.y))*-1f);
                tracer.transform.localScale = new Vector3(tracer.transform.localScale.x, launchVector.magnitude, 1f);
            }
        }
        if (Input.GetMouseButtonUp(0) && startedInCircle)
        {
            if (tracer.activeSelf)
            {
                atom.GetComponent<Particle>().gameController.moves++;
            }

            Vector2 finalVector = launchVector * launchVector.magnitude * 20f;
            //Debug.Log(finalVector.magnitude);
            if (finalVector.magnitude > 1000f)
            {
                finalVector = finalVector.normalized * (1000f);
            }
            rb.AddForce(finalVector);
            startedInCircle = false;
            GameObject.Destroy(tracer);
        }

    }

    bool InCircle()
    {
        return Vector2.Distance(atom.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 0.5f * atom.transform.localScale.x;
    }


}
