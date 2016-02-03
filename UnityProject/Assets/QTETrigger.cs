using UnityEngine;
using System.Collections;

public class QTETrigger : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;
    public bool triggeredByPlayer;

    private int idTriggered = -1;

	// Use this for initialization
	void Start () 
    {
        triggeredByPlayer = false;	
	}

    void OnTriggerEnter(Collider other)
    {
        var id = other.gameObject.GetInstanceID();
        
        if (id == playerOne.GetInstanceID() || id == playerTwo.GetInstanceID())
        {
            triggeredByPlayer = true;
            idTriggered = id;
        }
    }

    void OnTriggerExit(Collider other)
    {
        var id = other.gameObject.GetInstanceID();

        if (id == idTriggered)
        {
            triggeredByPlayer = false;
        }
    }
}
