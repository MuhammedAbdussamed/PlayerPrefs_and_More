using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    public InputActionAsset spaceKey;
    public Animator backgroundAnimator;
    public Animator textAnimator;
    private bool pressed;
    
    // Action Map
    private InputActionMap isPressedMap;

    // Actions
    private InputAction isPressed; 

    void Start()
    {
        spaceKey.Enable();

        isPressedMap = spaceKey.FindActionMap("interaction");
        isPressed = isPressedMap.FindAction("interact");
    }

    void Update()
    {
        Debug.Log(pressed);

        if (isPressed.triggered)
        {
            pressed = true;
        }

        if (pressed)
        {
            StartCoroutine(Close());
        }
    }

    IEnumerator Close()
    {
        backgroundAnimator.SetBool("isPressed",true);
        textAnimator.SetBool("isPressed",true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }
}
