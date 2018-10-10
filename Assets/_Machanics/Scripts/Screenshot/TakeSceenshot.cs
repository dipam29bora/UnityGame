using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeSceenshot : MonoBehaviour {

    public string FolderPath;
    public float ShutterWaitTime = 3;
    float m_temptime;

    private Coroutine m_Capture;

    void Start()
    {
        m_temptime = ShutterWaitTime;
    }

	void Update () {
		if(Input.GetMouseButtonDown(0))
        {
            if(m_Capture == null)
                m_Capture = StartCoroutine(Capture());
        }

        if(m_Capture!=null)
        {
            print("time : " + (m_temptime -= Time.deltaTime).ToString("0"));

            if(m_temptime <= 0)
            {
                m_temptime = ShutterWaitTime;
                m_Capture = null;
            }
        }
	}

    IEnumerator Capture()
    {
        yield return new WaitForSeconds(ShutterWaitTime);
        ScreenCapture.CaptureScreenshot(FolderPath + "/Screen.png");
    }
}
