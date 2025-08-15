// CardRegistrationModel.cs
using System;
using System.ComponentModel.DataAnnotations;

public class CardFeedbackModel
{
    // Thêm Id vì cột này không tự động tăng trong DB
    public string Id { get; set; }

    [Required(ErrorMessage = "Họ tên là bắt buộc")] // Thêm validation
    public string FullName { get; set; }

    public string? Address { get; set; }
    public string? Tel { get; set; }
    public string? Email { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    // Sửa: status trong DB là varchar(50)
    public string? Status { get; set; }

    public string? Title { get; set; }
}