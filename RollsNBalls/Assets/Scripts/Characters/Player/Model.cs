using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public int price;
    public string modelName;
    public string description;

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator <(Model A, Model B)
    {
        return A.price < B.price;
    }

    public static bool operator >(Model A, Model B)
    {
        return A.price > B.price;
    }

    public static bool operator <=(Model A, Model B)
    {
        return A.price <= B.price;
    }

    public static bool operator >=(Model A, Model B)
    {
        return A.price >= B.price;
    }

    public static bool operator ==(Model A, Model B)
    {
        return A.price == B.price && A.modelName == B.modelName && A.description == B.description;
    }

    public static bool operator !=(Model A, Model B)
    {
        return A.price != B.price || A.modelName != B.modelName || A.description != B.description;
    }
}
