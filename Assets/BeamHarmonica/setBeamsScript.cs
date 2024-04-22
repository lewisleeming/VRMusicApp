using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBeamsScript : MonoBehaviour
{
    [SerializeField] public HingeJoint ModDepth;
    [SerializeField] public HingeJoint FMFrequency;

    [SerializeField] public BeamScript Beam1;
    [SerializeField] public BeamScript Beam2;
    [SerializeField] public BeamScript Beam3;
    [SerializeField] public BeamScript Beam4;
    [SerializeField] public BeamScript Beam5;
    [SerializeField] public BeamScript Beam6;
    [SerializeField] public BeamScript Beam7;


    void Update(){
        Beam1.modDepth = ModDepth.angle;
        Beam1.fmFrequency = FMFrequency.angle;
        Beam2.modDepth = ModDepth.angle;
        Beam2.fmFrequency = FMFrequency.angle;
        Beam3.modDepth = ModDepth.angle;
        Beam3.fmFrequency = FMFrequency.angle;
        Beam4.modDepth = ModDepth.angle;
        Beam4.fmFrequency = FMFrequency.angle;
        Beam5.modDepth = ModDepth.angle;
        Beam5.fmFrequency = FMFrequency.angle;
        Beam6.modDepth = ModDepth.angle;
        Beam6.fmFrequency = FMFrequency.angle;
        Beam7.modDepth = ModDepth.angle;
        Beam7.fmFrequency = FMFrequency.angle;
    }
    // public void setBeamsScripts(){
    //     Beam1.modDepth = ModDepth.angle;
    //     Beam1.fmFrequency = FMFrequency.angle;
    //     Beam2.modDepth = ModDepth.angle;
    //     Beam2.fmFrequency = FMFrequency.angle;
    //     Beam3.modDepth = ModDepth.angle;
    //     Beam3.fmFrequency = FMFrequency.angle;
    //     Beam4.modDepth = ModDepth.angle;
    //     Beam4.fmFrequency = FMFrequency.angle;
    //     Beam5.modDepth = ModDepth.angle;
    //     Beam5.fmFrequency = FMFrequency.angle;
    //     Beam6.modDepth = ModDepth.angle;
    //     Beam6.fmFrequency = FMFrequency.angle;
    //     Beam7.modDepth = ModDepth.angle;
    //     Beam7.fmFrequency = FMFrequency.angle;
    // }

}
