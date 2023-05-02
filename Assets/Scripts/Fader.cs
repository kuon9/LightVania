using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
   
   // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());    
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }


    IEnumerator FadeIn()
	{
		float time = 1f;

		while(time > 0f) // as long as time is greater than 0 then we execute the function.
		{ 
			time -= Time.deltaTime; // Time.deltaTime = one single frame.
			float a = curve.Evaluate(time);
			img.color = new Color( 0f, 0f, 0f, a); // color,color,color, alpha.
			yield return 0; // wait till next frame
		}
		// load a scene.
	}

    IEnumerator FadeOut(string scene)
	{
		float time = 0f;

		while(time <  1f) // as long as time is greater than 0 then we execute the function.
		{ 
			time += Time.deltaTime; // Time.deltaTime = one single frame.
			float a = curve.Evaluate(time);
			img.color = new Color( 0f, 0f, 0f, a); // color,color,color, alpha.
			yield return 0; // wait till next frame
		}
		SceneManager.LoadScene(scene);
	}
}
