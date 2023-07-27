using Colyseus.Schema;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveController : MonoBehaviour
{
    internal void OnChange(List<DataChange> changes)
    {
        var position = transform.position;
        var velocity = new Vector3();

        foreach (var dataChange in changes)
        {
            switch (dataChange.Field)
            {
                case "x":
                    position.x = (float)dataChange.Value;
                    break;
                case "y":
                    position.z = (float)dataChange.Value;
                    break;
                case "vx":
                    velocity.x = (float)dataChange.Value;
                    break;
                case "vy":
                    velocity.z = (float)dataChange.Value;
                    break;
                default:
                    Debug.LogWarning("Не обрабатывается изменение поля: " + dataChange.Field);
                    break;
            }
        }

        transform.position = position;
    }
}

//using Colyseus.Schema;
//using System.Collections.Generic;
//using UnityEngine;

//public class EnemyMoveController : MonoBehaviour
//{
//    internal void OnChange(List<DataChange> changes)
//    {
//        var position = transform.position;

//        foreach (var dataChange in changes)
//        {
//            switch (dataChange.Field)
//            {
//                case "x":
//                    position.x = (float)dataChange.Value;
//                    break;
//                case "y":
//                    position.z = (float)dataChange.Value;
//                    break;
//                default:
//                    Debug.LogWarning("Не обрабатывается изменение поля: " + dataChange.Field);
//                    break;
//            }
//        }

//        transform.position = position;
//    }
//}
