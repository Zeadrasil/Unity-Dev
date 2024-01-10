using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraSwap : MonoBehaviour
{
    [SerializeField] private Camera boundCamera;
    [SerializeField] private bool deactivateSelfScripts;
    [SerializeField] private GameObject[] objectsToDeactivateScriptsOn;
    [SerializeField] private GameObject[] objectsToDisable;

    private Camera thisCamera;
    private bool switched = false;
    // Start is called before the first frame update
    void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && thisCamera != null && boundCamera != null && thisCamera.enabled && !boundCamera.enabled && Vector3.Distance(thisCamera.transform.position, boundCamera.transform.position) < 2)
        {
            boundCamera.enabled = true;
            var otherScripts = boundCamera.GetComponents<MonoBehaviour>();
            foreach(var script in otherScripts)
            {
                if(script is CameraSwap)
                {
                    ((CameraSwap)script).reActivate();
                    break;
                }
            }
            switched = true;
            if (deactivateSelfScripts)
            {
                var components = GetComponents<MonoBehaviour>();
                foreach (var component in components)
                {
                    if (component != this)
                    {
                        component.enabled = false;
                    }
                }
            }
            foreach(GameObject gameObject in objectsToDeactivateScriptsOn)
            {
                var components = gameObject.GetComponents<MonoBehaviour>();
                foreach(var component in components)
                {
                    component.enabled = false;
                }
            }
            foreach(GameObject gameObject in objectsToDisable)
            {
                MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
                var children = gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach (var component in children)
                {
                    component.enabled = false;
                }
                SkinnedMeshRenderer skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
                var skinChildren = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var component in skinChildren)
                {
                    component.enabled = false;
                }
            }
            GetComponent<AudioListener>().enabled = false;

        }
        else if(!Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.Space) && thisCamera.enabled && boundCamera.enabled && switched)
        {
            thisCamera.enabled = false;
            switched = false;
        }
    }
    public void reActivate()
    {
        if(deactivateSelfScripts)
        {
            var components = GetComponents<MonoBehaviour>();
            foreach( var component in components)
            {
                component.enabled = true;
            }
        }

        foreach (GameObject gameObject in objectsToDeactivateScriptsOn)
        {
            var components = gameObject.GetComponents<MonoBehaviour>();
            foreach (var component in components)
            {
                component.enabled = true;
            }
        }
        foreach (GameObject gameObject in objectsToDisable)
        {
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            var children = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var component in children)
            {
                component.enabled = true;
            }
            SkinnedMeshRenderer skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
            var skinChildren = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var component in skinChildren)
            {
                component.enabled = true;
            }
        }
        GetComponent<AudioListener>().enabled = true;
    }
}
