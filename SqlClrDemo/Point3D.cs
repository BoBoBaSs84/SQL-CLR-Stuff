using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct Point3D : INullable
{
    // Properties
    public bool IsNull { get; private set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public override string ToString()
    {
        if (this.IsNull)
            return "NULL";
        else
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(X);
            builder.Append(",");
            builder.Append(Y);
            builder.Append(",");
            builder.Append(Z);
            return builder.ToString();
        }
    }

    public static Point3D Null
    {
        get
        {
            Point3D p = new Point3D();
            p.IsNull = true;
            return p;
        }
    }

    [SqlMethod(OnNullCall = false)]
    public static Point3D Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        Point3D p = new Point3D();
        string[] xyz = s.Value.Split(",".ToCharArray());
        p.X = double.Parse(xyz[0]);
        p.Y = double.Parse(xyz[1]);
        p.Z = double.Parse(xyz[2]);

        return p;
    }

    // Distance from origin to Point method.  
    [SqlMethod(OnNullCall = false)]
    public Double DistanceFromOrigin()
    {
        return DistanceFromXYZ(0.0, 0.0, 0.0);
    }

    // Distance from point to the specified point method.  
    [SqlMethod(OnNullCall = false)]
    public Double DistanceFromPoint(Point3D pFrom)
    {
        return DistanceFromXYZ(pFrom.X, pFrom.Y, pFrom.Z);
    }

    // Distance from point to the specified x, y and z values method.  
    [SqlMethod(OnNullCall = false)]
    public Double DistanceFromXYZ(double iX, double iY, double iZ)
    {
        return Math.Sqrt(Math.Pow(iX - X, 2.0) + Math.Pow(iY - Y, 2.0) + Math.Pow(iZ - Z, 2.0));
    }
}