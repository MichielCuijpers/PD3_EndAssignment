﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeJump : MonoBehaviour {

    [SerializeField] private CharacterControllerBehaviour _characterControlScript;
    [SerializeField] private GameObject _player;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetAxis("AButton") != 0)
            {
                //set player to correct position
                CalculateSnappingPoint(gameObject.transform.forward, gameObject.transform.position);

                //disable components to avoid conflict
                _characterControlScript.IsHanging = true;

                //set y velocity to 0
                _characterControlScript.Velocity.y = 0;
            }
        }   
    }
    private void CalculateSnappingPoint(Vector3 forwardWall, Vector3 col)
    {
        #region offsetvalues
        //tweak offset values based on character model
        float yOffset = 1.4f;
        float xzOffset = 0.33f;
        #endregion offsetvalues
        if (forwardWall.x !=0)
        {
            //- (2 * forwardWall.x)
            _player.transform.position = new Vector3(col.x + (xzOffset * -transform.forward.x), col.y - yOffset, _player.transform.position.z );

            Debug.Log("X");
        }
        else if (forwardWall.z !=0)
        {
            //- (2 * forwardWall.z
            _player.transform.position = new Vector3(_player.transform.position.x , col.y - yOffset, col.z + (xzOffset * -transform.forward.z));
            
            Debug.Log("Z");
        }
        //set correct rotation
        Vector3 newRot = new Vector3(_player.transform.eulerAngles.x, transform.eulerAngles.y, _player.transform.eulerAngles.z);
        _player.transform.rotation = Quaternion.Euler(newRot);

        _characterControlScript.CurrentHangLocation = gameObject.transform.position; 

    }
}
