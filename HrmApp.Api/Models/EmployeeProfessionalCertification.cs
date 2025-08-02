using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HrmApp.Api.Models;

[PrimaryKey("IdClient", "Id")]
[Table("EmployeeProfessionalCertification")]
[Index("IdClient", "IdEmployee", "CertificationTitle", "FromDate", Name = "UNQ_EmployeeProfessionalCertification", IsUnique = true)]
public partial class EmployeeProfessionalCertification
{
    public int Id { get; set; }
    public int IdClient { get; set; }    
    
    public int IdEmployee { get; set; }
    public string CertificationTitle { get; set; } = null!;
    public string CertificationInstitute { get; set; } = null!;
    public string InstituteLocation { get; set; } = null!;
    public DateTime FromDate { get; set; }
    public DateTime? ToDate { get; set; }
    public DateTime? SetDate { get; set; }  
    public string? CreatedBy { get; set; }
    [ForeignKey("IdClient, IdEmployee")]
    [InverseProperty("EmployeeProfessionalCertifications")]
    public virtual Employee Employee { get; set; } = null!;
}
