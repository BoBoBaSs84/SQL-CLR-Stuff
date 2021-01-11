using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct MaxVariance
{
    /// <summary>
    /// The MaxVariance aggregate calculate the difference between high and low values on any column.
    /// </summary>
    //  This is a place-holder field member
    private int m_LowValue;
    private int m_HighValue;
    public void Init()
    {
        // Put your code here
        m_LowValue = 999999999;
        m_HighValue = -999999999;
    }
    public void Accumulate(int value)
    {
        // Put your code here
        if ((value > m_HighValue))
        {
            m_HighValue = value;
        }

        if ((value < m_LowValue))
        {
            m_LowValue = value;
        }
    }
    public void Merge (MaxVariance Group)
    {
        // Put your code here
        if ((Group.GetHighValue() > m_HighValue))
        {
            m_HighValue = Group.GetHighValue();
        }

        if ((Group.GetLowValue() < m_LowValue))
        {
            m_LowValue = Group.GetLowValue();
        }
    }
    public int Terminate ()
    {
        // Put your code here
        return (m_HighValue - m_LowValue);
    }
    //  Helper methods
    private int GetLowValue()
    {
        return m_LowValue;
    }
    private int GetHighValue()
    {
        return m_HighValue;
    }
}
