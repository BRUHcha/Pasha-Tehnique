using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;


public class CodeStuff : MonoBehaviour
{
    [SerializeField] private TextMeshPro code;
    [SerializeField] private string password;
    [SerializeField] private AudioClip[] StuffSounds;

    private new AudioSource audio;
    private bool coroutinesWork = false;
    public bool Unlocked = false;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (coroutinesWork) 
            return;

        if (!Unlocked)
        {
            if (code.text.Length > 2)
            {
                if (code.text == password)
                    Unlocked = true;
                else
                    {
                        coroutinesWork = true;
                        StartCoroutine(DeletePass());
                    }
            }
        }
        else
        {
            code.color = Color.green;
            DeleteButtons<CodeButton>();
        }
    }

    public void numPressed(string num)
    {
        code.text = code.text + num;
    }

    private void DeleteButtons<T>() where T : CodeButton
    {
        T[] components = GetComponentsInChildren<T>();

        foreach (T component in components)
            Destroy(component);
    }

    IEnumerator DeletePass()
    {
        code.color = Color.red;
        audio.PlayOneShot(StuffSounds[0]);
        yield return new WaitForSeconds(1f);
        code.text = null;
        code.color = Color.white;
        coroutinesWork = false;
    }
}
