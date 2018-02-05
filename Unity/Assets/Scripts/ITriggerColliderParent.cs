using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerColliderParent {

	void OnChildTriggerEnter (Collider other);
	void OnChildTriggerStay (Collider other);
	void OnChildTriggerExit (Collider other);
}

