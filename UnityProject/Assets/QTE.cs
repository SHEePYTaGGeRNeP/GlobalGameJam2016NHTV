using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QTE : MonoBehaviour
{
    public GameObject trigger1;
    public GameObject trigger2;

    public enum XboxButtons
    {
        A,
        B,
        X,
        Y,
        R1,
        R2,
        L1,
        L2
    }

    public XboxButtons currentButton;
    public Transform buttonUITransform;
    public List<GameObject> buttons;
    public float activeTime = 1f; // qte active time in seconds
    public bool activate = false;
    public Camera camera;

    private float timer = 0f;

	// Use this for initialization
	void Start () 
    {
        buttons.ForEach(x => x.SetActive(false));
    }
	
	// Update is called once per frame
	void Update ()
    {
        var buttonName = currentButton.ToString();
        var button = buttons.FindLast(x => x.name.Contains(buttonName));
        var pos = camera.WorldToScreenPoint(buttonUITransform.position);

        if (activate)
        {
            if (!button.activeSelf)
            {
                button.SetActive(true);
            }

            timer += Time.deltaTime;

            // QTE still active
            if (timer < activeTime)
            {

            }
            // disable qte
            else
            {
                activate = false;
                timer = 0f;
            }
        }
        else
        {
            if (button.activeSelf)
            {
                button.SetActive(false);
            }
        }
    }

    public void Activate()
    {
        if (!activate)
        {
            activate = true;
        }
    }
}
