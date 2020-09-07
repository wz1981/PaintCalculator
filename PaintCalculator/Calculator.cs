using Newtonsoft.Json;
using PaintCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintCalculator
{
    public class Calculator
    {
        private const int litrePerSqureMeter = 12;
        public string AreaAmountVoulmeInfo(string numbers)
        {
            ShapeOutput shapeOutput = new ShapeOutput();
            double width = 0;
            double length = 0;
            double height = 0;
           
            var delimiters = new List<char> { ',', '|','/' ,'?'};
            var splitNumbers = numbers
                .Split(delimiters.ToArray(),System.StringSplitOptions.RemoveEmptyEntries)
                .Select(double.Parse).ToList();
            if (splitNumbers.Count!=3) 
                return JsonConvert.SerializeObject(new ErrorHandler { Error="error", ErrorMessage="Invalid data"});
            var negativeNumbers = splitNumbers.Where(x => x < 0).ToList();
            if (negativeNumbers.Any())
                throw new Exception("Negatives not allowed:" + string.Join(",", negativeNumbers));
            width = splitNumbers[0];
            length = splitNumbers[1];
            height = splitNumbers[2];              
            shapeOutput.Area = Math.Round(width * length,2);
            shapeOutput.Amount = Math.Round( ((height * length) * 2 + (width * height) * 2)/12, 2);
            shapeOutput.Volume = Math.Round(width * height * length,2);
            return JsonConvert.SerializeObject(shapeOutput);
        }
        
    }
}
