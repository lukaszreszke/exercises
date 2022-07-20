using System.ComponentModel.DataAnnotations;

namespace SimpleCalculator.Models;

public class CalcViewModel
{
    [Display(Name = "Salary")]
    public decimal Salary { get; set; }
    [Display(Name = "Period")] public string Period { get; set; } = "Monthly";
    
    [Display(Name = "Result")]
    public decimal Result { get; set; }
}