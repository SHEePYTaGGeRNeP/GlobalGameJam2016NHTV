using UnityEngine;
using System.Collections;

public class DamageGiver : MonoBehaviour {

	public int damageAmount;
    public bool attacking = false;

    private Animator animator = null;

	// Use this for initialization
	void Start () {

        var tempAnimator = GetComponent<Collider>().transform.root.gameObject.GetComponent<Animator>();
        if (tempAnimator != null)
        {
            animator = tempAnimator;
        }
        else
        {
            attacking = true;
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (animator != null)
        {
            attacking = false;

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking"))
            {
                attacking = true;
            }
        }
	}
}
