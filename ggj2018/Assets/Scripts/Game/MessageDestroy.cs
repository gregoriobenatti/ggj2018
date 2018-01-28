using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageDestroy : MonoBehaviour {

    public LayerMask is_hitting_player_layer;
	public GameObject myself;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		if (Physics2D.OverlapCircle(this.transform.position, 0.2f, is_hitting_player_layer))
		{
			Debug.Log("Time to DESTROY!!");
			Destroy(myself);

		}
	}
}
