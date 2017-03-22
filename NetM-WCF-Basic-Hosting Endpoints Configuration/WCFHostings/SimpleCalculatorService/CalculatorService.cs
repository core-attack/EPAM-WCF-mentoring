﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SimpleCalculatorService
{
    public class CalculatorService : ISimpleCalculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Mul(double a, double b)
        {
            return a * b;
        }

        public double Sub(double a, double b)
        {
            return a - b;
        }

        public double Div(double a, double b)
        {
            return a / b;
        }
    }
}
