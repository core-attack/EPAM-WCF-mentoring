namespace CalculatorService
{
    using System.ServiceModel;
    using System.ServiceModel.Web;

    [ServiceContract]
    public interface ICalculatorService
    {
        [OperationContract]
        [WebGet]
        double Add(double a, double b);

        [OperationContract]
        [WebGet]
        double Diff(double a, double b);

        [OperationContract]
        [WebGet]
        double Mult(double a, double b);

        [OperationContract]
        [WebGet]
        double Div(double a, double b);

        [OperationContract]
        [WebGet]
        double PI();
    }
}
