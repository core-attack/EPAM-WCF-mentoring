using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SimpleCalculatorService
{
    [ServiceContract]
    public interface ISimpleCalculator
    {
        [OperationContract]
        double Add(double a, double b);

        [OperationContract]
        double Mul(double a, double b);

        [OperationContract]
        double Sub(double a, double b);

        [OperationContract]
        double Div(double a, double b);
    }
}
