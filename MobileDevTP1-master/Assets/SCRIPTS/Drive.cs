using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
   private float inputHorizontal;
   private float inputVertical;

   private string horizontalAxis= "Horizontal";
   private string verticalAxis = "Vertical";


   void update(){
       transform.Translate(0,0,1*Time.deltaTime*5f);

       inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
       inputVertical = SimpleInput.GetAxis(verticalAxis);

       transform.Rotate(0,inputHorizontal*5f,0f, Space.Self);

   }

}
