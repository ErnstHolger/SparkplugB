using System.Collections.Generic;
using SparkplugB;

namespace SparkplugB
{
    public static class Extension
    {
        public static DataSet ToDataSet(this List<double> values)
        {
            DataSet dataSet=new DataSet();
            dataSet.columns = new List<string> {"Value"};
            dataSet.num_of_columns = 1;
            Row row=new Row();
            row.elements=new List<DataSetValue>
            {
                new DataSetValue {double_value = 1}
            };
            dataSet.rows=new List<Row> {row};
            return dataSet;
        }
    }
}
