using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Vuforia;
using System.Linq;
using System;

public class QRManagerBehavior : MonoBehaviour
{
    private List<GameObject> imagetargets;
    private TextMeshProUGUI[] textMeshProUGUIs;
    private UnityEngine.UI.Image image;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
       textMeshProUGUIs = this.transform.GetComponentsInChildren<TextMeshProUGUI>();
       //textMeshProUGUI.SetText("Target Count: {0}", targetCounter);
       imagetargets = new List<GameObject>();
        foreach(Transform child in this.transform)
        {
            //NOTE: Mit Tags nach ImageTrackern bzw. QR-Objekten suchen, entsprechenden Tag anlegen
            if(child.gameObject.activeSelf == true && child.GetComponent<DefaultObserverEventHandler>() != null)
            {
                imagetargets.Add(child.gameObject);
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        int targetcounter = 0;
        length = 0;

        //int trackcounter = 0;

        foreach(GameObject imageTracker in imagetargets)
        {
            DefaultObserverEventHandler defaultObserverEventHandler = imageTracker.GetComponent<DefaultObserverEventHandler>();
            if(defaultObserverEventHandler.trackingStatus == DefaultObserverEventHandler.TrackingStatus.Tracked)
            {
                SetPositionOfMarker(imageTracker);
                targetcounter++;
                if(targetcounter >= 2 && imagetargets[0].GetComponent<DefaultObserverEventHandler>().trackingStatus == DefaultObserverEventHandler.TrackingStatus.Tracked)
                {
                    //DrawLine(imageTrackers[0].transform.position, imageTrackers[1].transform.position, Color.red);
                    length = GetLengthFromOrigin(imagetargets);
                }
                else
                {
                    textMeshProUGUIs[1].SetText("Zu wenig / falsche Targets.");
                }
            }

        }

        /*
        if (imagetargets[0].GetComponent<DefaultObserverEventHandler>().trackingStatus == DefaultObserverEventHandler.TrackingStatus.Not_Tracked)
        {
            textMeshProUGUIs[1].SetText("Origin (QR1) wird nicht gefunden.");
        }*/
        ChangeTextOfTrackedImagesList(imagetargets);
        textMeshProUGUIs[0].SetText("Target Count: {0}" + Environment.NewLine + "Length: {1}", targetcounter, length);
    }



    //NOTE: UPDATE VON POSITION DES MARKERS IN RELATION ZU ERFASSTEN TARGETS
    private void SetPositionOfMarker(GameObject imagetarget)
    {
        image = imagetarget.GetComponentInChildren<UnityEngine.UI.Image>();
        image.transform.position = imagetarget.transform.position;
        image.transform.rotation = imagetarget.transform.rotation * Quaternion.Euler(90, 0, 0);
    }

    //NOTE: UPDATE LIST OF ALL TRACKED IMAGETARGETS
    private void ChangeTextOfTrackedImagesList(List<GameObject> alltargets)
    {
        String trackedtargetnames = "";
        foreach (GameObject target in alltargets)
        {
            DefaultObserverEventHandler defaultObserverEventHandler = target.GetComponent<DefaultObserverEventHandler>();
            if (defaultObserverEventHandler.trackingStatus == DefaultObserverEventHandler.TrackingStatus.Tracked)
            {
                trackedtargetnames = trackedtargetnames + " " + target.name;
            }
        }
        textMeshProUGUIs[2].SetText("Tracked Targets:" + Environment.NewLine + trackedtargetnames);
    }

    //NOTE: LÄNGE VON ORIGIN ZU ALLEN WEITEREN TARGETS
    private float GetLengthFromOrigin(List<GameObject> alltargets)
    {
        float length = 0;
        GameObject widesttarget = null;
        int i = 0;
        int trackcounter = 0;

        foreach(GameObject target in alltargets)
        {
            if (length < (alltargets[0].transform.position - alltargets[i].transform.position).magnitude && !alltargets[i].transform.position.Equals(Vector3.zero))
            {
                length = (alltargets[0].transform.position - alltargets[i].transform.position).magnitude;
                widesttarget = target;               
            }

            foreach (GameObject targets in imagetargets)
            {
                if (imagetargets[trackcounter].GetComponent<DefaultObserverEventHandler>().trackingStatus == DefaultObserverEventHandler.TrackingStatus.Not_Tracked)
                {
                    imagetargets[trackcounter].transform.position = Vector3.zero;
                    imagetargets[trackcounter].transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                trackcounter++;
            }
            trackcounter = 0;
            i++;
        }


        textMeshProUGUIs[1].SetText("Längste Strecke zu -> " + widesttarget.name);

        Debug.Log("QR Origin ist verbunden mit " + widesttarget.name);
              
        return length;
    }

    /*NOTE: LÄNGE ZWISCHEN ALLEN TARGETS
    private float GetLengthFromEveryone(List<GameObject> alltargets)
    {
        float length = 0;
        GameObject firstwidesttarget = null;
        GameObject secondwidesttarget = null;
        int firsttargetcomparisson = 0;
        int secondtargetcomparisson = 0;

        foreach (GameObject target1 in alltargets)
        {
            foreach (GameObject target2 in alltargets)
            {
                if (length < (alltargets[firsttargetcomparisson].transform.position - alltargets[secondtargetcomparisson].transform.position).magnitude)
                {
                    length = (alltargets[firsttargetcomparisson].transform.position - alltargets[secondtargetcomparisson].transform.position).magnitude;
                    firstwidesttarget = target1;
                    secondwidesttarget = target2;
                }
                secondtargetcomparisson++;
            }
            secondtargetcomparisson = 0;
            firsttargetcomparisson++;
        }
        textMeshProUGUIs[1].SetText("Längste Strecke zw.: " + firstwidesttarget.name + " -> " + secondwidesttarget.name);
        //Debug.Log(firstwidesttarget.name + " ist verbunden mit " + secondwidesttarget.name);
        return length;
    }
    */

    /*
    private void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.015f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = color;
        lr.endColor = color;
        lr.startWidth = 0.002f;
        lr.endWidth = 0.002f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }*/

    [SerializeField]
    private int targetCounter;
}
