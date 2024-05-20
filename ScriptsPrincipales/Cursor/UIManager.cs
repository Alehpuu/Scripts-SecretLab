using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Texture2D[] cursors;
    public static UIManager instance;

    private void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
        }
    }

public static void SetCursors(ObjectType objectType)
{
    if (instance == null)
        return;

    Debug.Log("Setting cursor for object type: " + objectType);

    Cursor.SetCursor(instance.cursors[(int)objectType], Vector2.zero, CursorMode.Auto);
}
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }
}
