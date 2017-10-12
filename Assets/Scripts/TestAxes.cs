using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TestAxes : MonoBehaviour {

    public Text theText;
    public Text theText2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        theText.text =
            "1 PS4 R1: " + Input.GetButton("1 PS4 R1") + "\n"
            + "1 PS4 L1: " + Input.GetButton("1 PS4 L1") + "\n"
            + "1 PS4 R2: " + Input.GetAxis("1 PS4 R2") + "\n"
            + "1 PS4 L2: " + Input.GetAxis("1 PS4 L2") + "\n"
            + "1 PS4 R3: " + Input.GetButton("1 PS4 R3") + "\n"
            + "1 PS4 L3: " + Input.GetButton("1 PS4 L3") + "\n\n"

            + "1 PS4 Left Analog Horizontal: " + Input.GetAxis("1 PS4 Left Analog Horizontal") + "\n"
            + "1 PS4 Left Analog Vertical: " + Input.GetAxis("1 PS4 Left Analog Vertical") + "\n"
            + "1 PS4 Right Analog Horizontal: " + Input.GetAxis("1 PS4 Right Analog Horizontal") + "\n"
            + "1 PS4 Right Analog Vertical: " + Input.GetAxis("1 PS4 Right Analog Vertical") + "\n\n"

            + "1 PS4 X: " + Input.GetButton("1 PS4 X") + "\n"
            + "1 PS4 O: " + Input.GetButton("1 PS4 O") + "\n"
            + "1 PS4 T: " + Input.GetButton("1 PS4 T") + "\n"
            + "1 PS4 S: " + Input.GetButton("1 PS4 S") + "\n\n"

            + "1 PS4 Dpad Horizontal: " + Input.GetAxis("1 PS4 Dpad Horizontal") + "\n"
            + "1 PS4 Dpad Vertical: " + Input.GetAxis("1 PS4 Dpad Vertical") + "\n\n"

            + "1 PS4 Share: " + Input.GetButton("1 PS4 Share") + "\n"
            + "1 PS4 Options: " + Input.GetButton("1 PS4 Options") + "\n"
            + "1 PS4 PSN: " + Input.GetButton("1 PS4 PSN") + "\n"
            + "1 PS4 Touch: " + Input.GetButton("1 PS4 Touch");

        theText2.text =
            "2 PS4 R1: " + Input.GetButton("2 PS4 R1") + "\n"
            + "2 PS4 L1: " + Input.GetButton("2 PS4 L1") + "\n"
            + "2 PS4 R2: " + Input.GetAxis("2 PS4 R2") + "\n"
            + "2 PS4 L2: " + Input.GetAxis("2 PS4 L2") + "\n"
            + "2 PS4 R3: " + Input.GetButton("2 PS4 R3") + "\n"
            + "2 PS4 L3: " + Input.GetButton("2 PS4 L3") + "\n\n"

            + "2 PS4 Left Analog Horizontal: " + Input.GetAxis("2 PS4 Left Analog Horizontal") + "\n"
            + "2 PS4 Left Analog Vertical: " + Input.GetAxis("2 PS4 Left Analog Vertical") + "\n"
            + "2 PS4 Right Analog Horizontal: " + Input.GetAxis("2 PS4 Right Analog Horizontal") + "\n"
            + "2 PS4 Right Analog Vertical: " + Input.GetAxis("2 PS4 Right Analog Vertical") + "\n\n"

            + "2 PS4 X: " + Input.GetButton("2 PS4 X") + "\n"
            + "2 PS4 O: " + Input.GetButton("2 PS4 O") + "\n"
            + "2 PS4 T: " + Input.GetButton("2 PS4 T") + "\n"
            + "2 PS4 S: " + Input.GetButton("2 PS4 S") + "\n\n"

            + "2 PS4 Dpad Horizontal: " + Input.GetAxis("2 PS4 Dpad Horizontal") + "\n"
            + "2 PS4 Dpad Vertical: " + Input.GetAxis("2 PS4 Dpad Vertical") + "\n\n"

            + "2 PS4 Share: " + Input.GetButton("2 PS4 Share") + "\n"
            + "2 PS4 Options: " + Input.GetButton("2 PS4 Options") + "\n"
            + "2 PS4 PSN: " + Input.GetButton("2 PS4 PSN") + "\n"
            + "2 PS4 Touch: " + Input.GetButton("2 PS4 Touch");
    }
}
