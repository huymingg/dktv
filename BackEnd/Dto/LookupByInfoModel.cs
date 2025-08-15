// File: Models/LookupByInfoModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class LookupByInfoModel
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public DateTime Dob { get; set; }
    [Required]
    public string Tel { get; set; }
}