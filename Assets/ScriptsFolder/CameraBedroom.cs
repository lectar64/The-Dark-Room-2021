using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BedCameraUtilities// Nueva libreria que permite acceder a la clase 
{
    public class CameraBedroom : MonoBehaviour
    {
        Camera CameraBed;
        Ray Rcast;
        RaycastHit ColliderObject;
        Camera[] ArrayCamera;
        public static bool boleanCamera = true;
        public CameraBedroom(Camera cs)// Asigno mediante la clase ambas variables para no tener que hacerla estatica
        {
            CameraBed = cs;
        }
        void Start()
        {
            ArrayCamera = FindObjectsOfType<Camera>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void CameraBedAlgorthim()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (Physics.Raycast(Rcast, out ColliderObject, 5))
                {
                    Rcast.origin = CameraBed.transform.position;
                    Rcast.direction = CameraBed.transform.TransformDirection(Vector3.forward);
                    Rcast = CameraBed.ScreenPointToRay(Input.mousePosition);
                    if (ColliderObject.collider.CompareTag("Cer"))
                    {
                        Destroy(ColliderObject.collider.gameObject);  
                    }
                }
            }
            
        }
        public void ChangeCamera()
        {
            for (int i = 0; i < ArrayCamera.Length; i++)
            {
                if (ArrayCamera[i] != this.gameObject)
                {
                    ArrayCamera[i].enabled = !boleanCamera;
                }
            }
        }
    }
}
