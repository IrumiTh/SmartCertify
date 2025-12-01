using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartCertify.Domain.Entities;

[Table("UserActivityLog")]
public partial class UserActivityLog
{
    [Key]
    public int LogId { get; set; }

    public int? UserId { get; set; }

    [StringLength(200)]
    public string ActivityType { get; set; } = null!;

    public string? ActivityDescription { get; set; }

    public DateTime LogDate { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserActivityLogs")]
    public virtual UserProfile? User { get; set; }
}
