using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoveAction))]
public class MoveActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MoveAction moveAction = (MoveAction) target;
        
        EditorGUILayout.PropertyField(serializedObject.FindProperty("m_moveActionType"));
        string propertyToShow = "";
        switch (moveAction.CurrentMoveActionType)
        {
            case MoveAction.MoveActionType.MOVE_TO_TRANSFORM : 
                propertyToShow = "m_moveToTransformConfig";
                break;
            case MoveAction.MoveActionType.MOVE_RANDOM:
                propertyToShow = "m_moveRandomConfig";
                break;
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyToShow));

        serializedObject.ApplyModifiedProperties();

    }

#if UNITY_EDITOR
    private void OnSceneGUI()
    {
        MoveAction moveAction = (MoveAction) target;
        Vector3 currentPosition = moveAction.transform.position;
        Handles.Label(currentPosition, moveAction.CurrentMoveActionType.ToString());
        switch (moveAction.CurrentMoveActionType)
        {
            case MoveAction.MoveActionType.MOVE_TO_TRANSFORM:
                Vector3 targetPosition = moveAction.CurrentMoveToTransformConfig.TargetPosition;
                Handles.DrawLine(currentPosition,targetPosition , 2f);
                EditorGUI.BeginChangeCheck();
                Vector3 newMovePosition = Handles.PositionHandle(targetPosition, Quaternion.identity);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(moveAction, "Chane Move Position");
                    moveAction.CurrentMoveToTransformConfig.TargetPosition = newMovePosition;
                    serializedObject.Update();
                }
                break;
            case MoveAction.MoveActionType.MOVE_RANDOM:
                Handles.color = Color.red;
                float lineThickness = 2f;
                
                Handles.DrawLine(currentPosition, currentPosition + (Vector3.up * moveAction.CurrentMoveRandomConfig.RandomDistance.y), lineThickness);
                Handles.DrawLine(currentPosition, currentPosition - (Vector3.up * moveAction.CurrentMoveRandomConfig.RandomDistance.y), lineThickness);
                
                Handles.DrawLine(currentPosition, currentPosition + (Vector3.forward * moveAction.CurrentMoveRandomConfig.RandomDistance.z), lineThickness);
                Handles.DrawLine(currentPosition, currentPosition - (Vector3.forward * moveAction.CurrentMoveRandomConfig.RandomDistance.z), lineThickness);
                
                Handles.DrawLine(currentPosition, currentPosition + (Vector3.right * moveAction.CurrentMoveRandomConfig.RandomDistance.x), lineThickness);
                Handles.DrawLine(currentPosition, currentPosition - (Vector3.right * moveAction.CurrentMoveRandomConfig.RandomDistance.x), lineThickness);
                
                break;
        }
    }
#endif
    
}
