using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiquadFilter : MonoBehaviour
{
    string type;
    double a0,a1,a2,b1,b2;
    double Fc, Q, PeakGain;
    double z1, z2;
    

    public float process(float input){
        double output = input * a0 + z1;
        z1 = input * a1 + z2 - b1 * output;
        z2 = input * a2 - b2 * output;
        return (float)output;

        // double output = b0 * input + b1 * x1 + b2 * x2 - a1 * y1 - a2 * y2;

        // x2 = x1;
        // x1 = input;
        // y2 = y1;
        // y1 = output;

        // return output;
    }



    public void setBiquad(string type, double Fc, double Q, double PeakGainDB){
        this.type = type;
        this.Fc = Fc;
        this.Q = Q;
        z1 = z2 = 0.0;
        this.PeakGain = PeakGainDB;
        calcBiquad();
    }

    private void calcBiquad(){
        double norm;
        double V = Mathf.Pow(10, Mathf.Abs((float)PeakGain)/ 20.0f);
        double K = Mathf.Tan((float)(Mathf.PI * Fc));
        switch(this.type){
            case "bq_type_lowpass":
                norm = 1 / (1 + K / Q + K * K);
                a0 = K * K * norm;
                a1 = 2 * a0;
                a2 = a0;
                b1 = 2 * (K * K - 1) * norm;
                b2 = (1 - K / Q + K * K) * norm;
                break;
            case "bq_type_highpass":
                norm = 1 / (1 + K / Q + K * K);
                a0 = 1 * norm;
                a1 = -2 * a0;
                a2 = a0;
                b1 = 2 * (K * K - 1) * norm;
                b2 = (1 - K / Q + K * K) * norm;
                break;
            
            case "bq_type_bandpass":
                norm = 1 / (1 + K / Q + K * K);
                a0 = K / Q * norm;
                a1 = 0;
                a2 = -a0;
                b1 = 2 * (K * K - 1) * norm;
                b2 = (1 - K / Q + K * K) * norm;
                break;
            
            case "bq_type_notch":
                norm = 1 / (1 + K / Q + K * K);
                a0 = (1 + K * K) * norm;
                a1 = 2 * (K * K - 1) * norm;
                a2 = a0;
                b1 = a1;
                b2 = (1 - K / Q + K * K) * norm;
                break;
                
            case "bq_type_peak":
                if (PeakGain >= 0) {    // boost
                    norm = 1 / (1 + 1/Q * K + K * K);
                    a0 = (1 + V/Q * K + K * K) * norm;
                    a1 = 2 * (K * K - 1) * norm;
                    a2 = (1 - V/Q * K + K * K) * norm;
                    b1 = a1;
                    b2 = (1 - 1/Q * K + K * K) * norm;
                }
                else {    // cut
                    norm = 1 / (1 + V/Q * K + K * K);
                    a0 = (1 + 1/Q * K + K * K) * norm;
                    a1 = 2 * (K * K - 1) * norm;
                    a2 = (1 - 1/Q * K + K * K) * norm;
                    b1 = a1;
                    b2 = (1 - V/Q * K + K * K) * norm;
                }
                break;
            case "bq_type_lowshelf":
                if (PeakGain >= 0) {    // boost
                    norm = 1 / (1 + Mathf.Sqrt(2) * K + K * K);
                    a0 = (1 + Mathf.Sqrt(2*(float)V) * K + V * K * K) * norm;
                    a1 = 2 * (V * K * K - 1) * norm;
                    a2 = (1 - Mathf.Sqrt(2*(float)V) * K + V * K * K) * norm;
                    b1 = 2 * (K * K - 1) * norm;
                    b2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
                }
                else {    // cut
                    norm = 1 / (1 + Mathf.Sqrt(2*(float)V) * K + V * K * K);
                    a0 = (1 + Mathf.Sqrt(2) * K + K * K) * norm;
                    a1 = 2 * (K * K - 1) * norm;
                    a2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
                    b1 = 2 * (V * K * K - 1) * norm;
                    b2 = (1 - Mathf.Sqrt(2*(float)V) * K + V * K * K) * norm;
                }
                break;
            case "bq_type_highshelf":
                if (PeakGain >= 0) {    // boost
                    norm = 1 / (1 + Mathf.Sqrt(2) * K + K * K);
                    a0 = (V + Mathf.Sqrt(2*(float)V) * K + K * K) * norm;
                    a1 = 2 * (K * K - V) * norm;
                    a2 = (V - Mathf.Sqrt(2*(float)V) * K + K * K) * norm;
                    b1 = 2 * (K * K - 1) * norm;
                    b2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
                }
                else {    // cut
                    norm = 1 / (V + Mathf.Sqrt(2*(float)V) * K + K * K);
                    a0 = (1 + Mathf.Sqrt(2) * K + K * K) * norm;
                    a1 = 2 * (K * K - 1) * norm;
                    a2 = (1 - Mathf.Sqrt(2) * K + K * K) * norm;
                    b1 = 2 * (K * K - V) * norm;
                    b2 = (V - Mathf.Sqrt(2*(float)V) * K + K * K) * norm;
                }
                break;

        }
    }



}
