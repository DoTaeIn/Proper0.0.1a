using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyInfo))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the target script
        EnemyInfo enemyInfo = (EnemyInfo)target;

        // Draw the default enum popup
        enemyInfo.type = (Type)EditorGUILayout.EnumPopup("Type", enemyInfo.type);

        // Conditionally draw fields based on the selected enum value
        switch (enemyInfo.type)
        {
            case Type.enemy:
                enemyInfo.HP = EditorGUILayout.FloatField("HP", enemyInfo.HP);
                enemyInfo.Damage = EditorGUILayout.FloatField("Damage", enemyInfo.Damage);
                enemyInfo.CoolTime = EditorGUILayout.IntField("Cool Time", enemyInfo.CoolTime);
                enemyInfo.AttackRange = EditorGUILayout.FloatField("Attack Range", enemyInfo.AttackRange);
                enemyInfo.ChaseRange = EditorGUILayout.FloatField("Chase Range", enemyInfo.ChaseRange);
                break;

            case Type.trap:
                // Show fields relevant to traps (if any)
                // Example:
                enemyInfo.Damage = EditorGUILayout.FloatField("Damage", enemyInfo.Damage);
                break;

            case Type.weapon:
                // Show fields relevant to weapons (if any)
                // Example:
                enemyInfo.Damage = EditorGUILayout.FloatField("Damage", enemyInfo.Damage);
                enemyInfo.AttackRange = EditorGUILayout.FloatField("Attack Range", enemyInfo.AttackRange);
                break;
        }

        // Apply changes to the serialized object
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}