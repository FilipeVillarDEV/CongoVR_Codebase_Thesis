using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class RecordData : MonoBehaviour
{
    private int logProfilerFrameCnt, logProfilerFileCnt;
    // Start is called before the first frame update
    void Awake()
    {
        string src = Application.persistentDataPath + "/"+ Application.productName +"_Log_" + logProfilerFileCnt.ToString() + ".data";
        Profiler.logFile = src;
        Profiler.enableBinaryLog = true;
        Profiler.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        logProfilerFrameCnt++;
        if(logProfilerFrameCnt > 2000)
        {
            logProfilerFileCnt++;
            logProfilerFrameCnt = 0;
            string src = Application.persistentDataPath + "/" + Application.productName + "_Log_" + logProfilerFileCnt.ToString() + ".data";
            Profiler.logFile = src;
        }
    }
}
