using System.Collections;
using System. Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHands : MonoBehaviour
{
    private ActionBasedController _actionBasedController;
    private float _speed = 5f;
    private Animator _vrHandsAnimator;
    private int _flexFloatID;
    private int _pinchFloatID;
    private float _gripCurrent, _gripTarget;
    private float _triggerCurrent, _triggerTarget;

    void Start(){
        _actionBasedController=GetComponentInParent<ActionBasedController>();
        _vrHandsAnimator = GetComponent <Animator>( );
        _flexFloatID = Animator.StringToHash( "Flex" );
        _pinchFloatID = Animator.StringToHash( "Pinch");
    }

    void Update(){
        HandsMovement();
    }

    void HandsMovement(){
        _gripTarget = _actionBasedController.selectAction.action.ReadValue<float>();
        _triggerTarget =_actionBasedController.activateAction.action.ReadValue<float>();
        if (_gripCurrent != _gripTarget){
            _gripCurrent = Mathf.MoveTowards(_gripCurrent,_gripTarget, Time.deltaTime *_speed);
        }
        if (_triggerCurrent!=_triggerTarget){

        }
        _triggerCurrent=Mathf.MoveTowards(_triggerCurrent, _triggerTarget, Time.deltaTime * Time.deltaTime *_speed);
        
        _vrHandsAnimator.SetFloat(_flexFloatID,_gripCurrent);
        _vrHandsAnimator.SetFloat(_pinchFloatID,_triggerCurrent);
    }

}

