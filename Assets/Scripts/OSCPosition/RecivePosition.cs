using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecivePosition : MonoBehaviour
{
    OSC osc;

    public string Objname;

    public bool islocalposition , isOringin;

    Vector3 recivedPosition;

    public Transform anchor;

    // Start is called before the first frame update
    void Start()
    {
        osc= FindObjectOfType<OSC>().GetComponent<OSC>();
        osc.SetAddressHandler( "/"+Objname , OnReceivePos );

        anchor=GameObject.FindGameObjectWithTag("Anchor").transform;

    }


    void OnReceivePos(OscMessage message){

        float x = message.GetFloat(0);
        float y = message.GetFloat(1);
		float z = message.GetFloat(2);
        float rx = message.GetFloat(3);
        float ry = message.GetFloat(4);
		float rz = message.GetFloat(5);

  
		recivedPosition = new Vector3(x,y,z);
      

    


        transform.rotation=Quaternion.Euler(rx,ry,rz);

    }

    // Update is called once per frame
    void Update()
    {


        if(isOringin){

                transform.position=recivedPosition-anchor.position;

        }
        else{

                if(!islocalposition){
                    transform.position=recivedPosition;

                }else{

                    transform.localPosition=recivedPosition;
                }




        }
        
    }
}
