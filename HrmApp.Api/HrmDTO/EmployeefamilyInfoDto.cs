using System.ComponentModel.DataAnnotations;

namespace HrmApp.Api.HrmDTO
{
    public class EmployeefamilyInfoDto
    {
        public int IdClient { get; set; }

    
        public string Name { get; set; } = null!;

        public int IdGender { get; set; }

        public int IdRelationship { get; set; }

        public DateTime? DateOfBirth { get; set; }


        public string? ContactNo { get; set; }

    
        public string? CurrentAddress { get; set; }

       
        public string? PermanentAddress { get; set; }
        public DateTime? SetDate { get; set; }
    }

}

