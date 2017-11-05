﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDrink : MonoBehaviour, Grabbable {
    public Material yellow;
    public Material red;
    public Material white;


    private Rigidbody rigidbod;
    private bool held;
    private Transform originalParent;
    private bool highlighted;
    private Renderer rend;
    private Vector3 previousPos;
    private Vector3 currentPos;

	// Use this for initialization
	void Start () {
        currentPos = transform.position;
        originalParent = transform.parent;
        rigidbod = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void FixedUpdate()
    {
        previousPos = currentPos;
        currentPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Hand>().SetGrabbableObject(gameObject);
            GetHighlighted();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Hand>().RemoveGrabbableObject();
            GetDehighlighted();
        }
    }

    void GetHighlighted()
    {
        highlighted = true;

        // Debug
        rend.material = yellow;
    }

    void GetDehighlighted()
    {
        highlighted = false;

        // Debug
        rend.material = white;
    }

    public void GetGrabbed(Hand grabbingHand)
    {
        held = true;
        transform.SetParent(grabbingHand.transform);
        rigidbod.isKinematic = true;

        // Debug
        rend.material = red;
    }

    public void GetDropped(Hand droppingHand)
    {
        held = false;
        transform.SetParent(originalParent);
        rigidbod.isKinematic = false;

        Debug.Log(previousPos.y);
        Debug.Log(currentPos.y);

        Vector3 directionVector = currentPos - previousPos;
        float distance = Vector3.Distance(currentPos, previousPos);

        Debug.Log(directionVector.x + " " + directionVector.y + " " + directionVector.z);
        Debug.Log("Distance of " + distance);

        rigidbod.AddForce(directionVector * (distance*10000));
        
        // Debug


        if (highlighted)
        {
            rend.material = yellow;
        } else
        {
            rend.material = white;
        }

    }
}
