using UnityEngine;
using System.Collections;

public class trapFloorCollider : MonoBehaviour
{
    private GameObject trapFloor;
    //private bool goUp;
    Animator crumbleAnimation;

    // Use this for initialization
    void Start()
    {
        //goUp = false;
        trapFloor = GameObject.Find("TrapDoor");
        crumbleAnimation = trapFloor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (crumbleAnimation.GetBool("isOpen") && crumbleAnimation.GetBool("isCrumbled") && this.transform.position.y <= 0.26f)
        {
            this.transform.Translate(Vector3.up * 5f * Time.deltaTime);
        }
        else if (!crumbleAnimation.GetBool("isOpen") && crumbleAnimation.GetBool("isCrumbled") && this.transform.position.y >= -16){
			this.transform.Translate(-Vector3.up * 5f * Time.deltaTime);
		}
    }

    void OnTriggerEnter(Collider col)
    {
		//goUp = true;
        if (col.gameObject.CompareTag("Player") && trapFloor.GetComponent<Animator>().GetBool("isCrumbled"))
        {
            col.transform.parent = transform;
        }
    }

    void OnTriggerStay(Collider col)
    {
		//goUp = true;
        if (col.gameObject.CompareTag("Player") && trapFloor.GetComponent<Animator>().GetBool("isCrumbled"))
        {
            col.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider col)
    {
		//goUp = false;
        if (col.gameObject.CompareTag("Player"))
        {
            col.transform.parent = null;
        }
    }
}
