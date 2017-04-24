using UnityEngine;
using System.Collections;

public class Ani : MonoBehaviour {
	public Animator anim;
	public CatsMovement cat;
	
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		cat = this.GetComponentInParent<CatsMovement>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 catPos = cat.cat.transform.position;
		if (cat.victim != null){
			catPos = cat.cat.transform.position;
			Vector3 victimPos = cat.victim.transform.position;
			Vector3 attack = victimPos - catPos;
			Vector3 movement_vector = attack;
			
		
			if ((catPos.x != 351 || catPos.z != 374) && cat.victim != null){
				anim.SetBool("iswalking", true);
				anim.SetFloat("input_x", movement_vector.x);
				anim.SetFloat("input_y", movement_vector.z);
			} else {
				anim.SetBool("iswalking", false);
				//Debug.Log("iswalking set to false");
			}
		} else if ((cat.victim == null) && (catPos.x == 351 || catPos.z == 374)){
			anim.SetBool("iswalking", false);
		}
	}
}
