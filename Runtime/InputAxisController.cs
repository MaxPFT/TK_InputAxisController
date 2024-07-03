using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Events;

public class InputAxisController : MonoBehaviour
{
    public List<InputActionReference_Float> m_floatInput = new List<InputActionReference_Float>();


    [System.Serializable]
    public class InputActionReference_Float
    {
        public string m_description = "Quelque chose attend un nom ...";
        public InputActionReference m_observe;
        public float m_value;
        public UnityEvent m_change;

        public void StartListening()
        {
            m_observe.action.Enable();
            m_observe.action.performed += (e) =>
            {
                m_value = e.ReadValue<float>();
                m_change.Invoke();
            };
            m_observe.action.canceled += (e) =>
            {
                m_value = e.ReadValue<float>();
                m_change.Invoke();
            };
        }
        
        public void StopListening()
        {
            m_observe.action.Disable();
            m_observe.action.performed -= (e) =>
            {
                m_value = e.ReadValue<float>();
                m_change.Invoke();
            };
            m_observe.action.canceled -= (e) =>
            {
                m_value = e.ReadValue<float>();
                m_change.Invoke();
            };
        }


    }
    private void Start()
    {
        foreach (var action in m_floatInput)
            action.StartListening();
    }
    private void OnDestroy()
    {
        foreach (var action in m_floatInput)
            action.StopListening();
    }

}
