// CardRegistrationModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class CardRegistrationModel
{
    // Thêm Id vì cột này không tự động tăng trong DB
    public string Id { get; set; }

    public string? CardType { get; set; }
    public string? ReaderType { get; set; }
    public string? RegisterType { get; set; }

    [Required(ErrorMessage = "Họ tên là bắt buộc")] // Thêm validation
    public string FullName { get; set; }

    public DateTime? Dob { get; set; }
    public string? Cccd { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Tel { get; set; }
    public string? Job { get; set; }
    public string? Photo { get; set; }

    // Sửa: status trong DB là varchar(50)
    public string? Status { get; set; }

    public string? CardNo { get; set; }
    public string? Representative { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    [Required]
    public string CreatedBy { get; set; }

    public DateTime? Deadline { get; set; }
    public string? Note { get; set; }
    public string? Office { get; set; }

    // Sửa: gender trong DB là varchar(50)
    public string? Gender { get; set; }

    public DateTime? ExpiredDate { get; set; }
    public string? ReceiveType { get; set; }
    public int PrintCount { get; set; }
    public bool IsPayment { get; set; }
    public int? SentEmail { get; set; }
    public int Level { get; set; }
    public string? PaymentType { get; set; }
    public string? Nation { get; set; }
    public string? RegistrationCode { get; set; }
}