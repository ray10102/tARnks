﻿using UnityEngine;
//  [RequireComponent(typeof(LineRenderer))]
 public class LineRendererArrow : MonoBehaviour
 {
     [Tooltip("The percent of the line that is consumed by the arrowhead")]
     [Range(0, 1)]
     public float PercentHead = 0.4f;
    //  public Vector3 ArrowOrigin;
    // public GameObject barrel;
    public Vector3 ArrowOrigin;
     public Vector3 ArrowTarget;

     //public Vector3 direction = Vector3.zero;
     private LineRenderer cachedLineRenderer;
     void Start()
     {
         ArrowOrigin = GameObject.Find("barrel").transform.position;
         ArrowTarget = ArrowOrigin * 3;
         UpdateArrow();
     }
     private void OnValidate()
     {
         UpdateArrow();
     }
    //  [ContextMenu("UpdateArrow")]
     void UpdateArrow()
     {
         float AdaptiveSize = (float)(PercentHead / Vector3.Distance(ArrowOrigin, ArrowTarget));

         if (cachedLineRenderer == null)
             cachedLineRenderer = GetComponent<LineRenderer>();


     cachedLineRenderer.widthCurve = new AnimationCurve(
         new Keyframe(0, 0.4f)
         , new Keyframe(0.999f - AdaptiveSize, 0.4f)  // neck of arrow
         , new Keyframe(1 - AdaptiveSize, 1f)  // max width of arrow head
         , new Keyframe(1, 0f));  // tip of arrow

     cachedLineRenderer.SetPositions(new Vector3[] {
               ArrowOrigin
               , Vector3.Lerp(ArrowOrigin, ArrowTarget, 0.999f - AdaptiveSize)
               , Vector3.Lerp(ArrowOrigin, ArrowTarget, 1 - AdaptiveSize)
               , ArrowTarget });
     }
 }