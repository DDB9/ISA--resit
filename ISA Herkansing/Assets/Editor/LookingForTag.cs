 using UnityEngine;
 using System.Collections;
 using UnityEditor;
 
 public class LookingForTag : MonoBehaviour {
 
     private static string SelectedTag = "Player";
 
     [MenuItem("Cheats/Select By Tag")]
     public static void SelectObjectsWithTag()
     {
         GameObject[] objects = GameObject.FindGameObjectsWithTag(SelectedTag);
         Selection.objects = objects;
     }
 }
