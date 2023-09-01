using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Malee;
// script reference by - Zee Vasilyev - https://www.youtube.com/watch?v=H2qMxWcO9dg&list=PLFY3TFPG0dkomvWyb2fcPeqDSSvIO0NYb&index=13
// script edited by Oliver lancashire
// sid 1901981

[CreateAssetMenu()]
public class AchievementDatabase : ScriptableObject {
    [Reorderable(sortable = false, paginate = false)]
    public AchievementsArray achievements;
    // create obj to create acheivement array
    [System.Serializable]
    public class AchievementsArray : ReorderableArray<Achievement> { }
}
