using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{

    public float time;
    private AsyncOperation operation = new AsyncOperation();

    public void Start()
    {
        StartCoroutine(playgame());
    }
    IEnumerator LoadAsynchronously(int stateIndex)
    {

        //operation = SceneManager.LoadSceneAsync(stateIndex);
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            if (operation.progress == 0.9f)
            {
                yield return new WaitForSeconds(time);
                operation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    IEnumerator playgame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
  

}
