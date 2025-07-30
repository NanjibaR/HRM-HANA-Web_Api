using HrmApp.Api.HrmDTO;
using HrmApp.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using static System.Collections.Specialized.BitVector32;

namespace HrmApp.Api.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly HrmDbContext _context;

        public EmployeeController(HrmDbContext context)
        {
            _context = context;
        }


        //GET Operation

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<HrmDTO.EmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employees

                .Where(e => e.IdClient == 10001001)
                .Select(e => new EmployeeDTO
                {
                    IdClient = 10001001,
                    Id = e.Id,
                    EmployeeName = e.EmployeeName,
                    EmployeeNameBangla = e.EmployeeNameBangla,
                    FatherName = e.FatherName,
                    MotherName = e.MotherName,
                    BirthDate = e.BirthDate,
                    JoiningDate = e.JoiningDate,
                    ContactNo = e.ContactNo,
                    NationalIdentificationNumber = e.NationalIdentificationNumber,
                    Address = e.Address,
                    PresentAddress = e.PresentAddress,
                    IdGender = e.IdGender,
                    IdReligion = e.IdReligion,
                    IdDepartment = e.IdDepartment,
                    IdSection = e.IdSection,
                    IdDesignation = e.IdDesignation,
                    IdReportingManager = e.IdReportingManager,
                    IdJobType = e.IdJobType,
                    IdEmployeeType = e.IdEmployeeType,
                    IdMaritalStatus = e.IdMaritalStatus,
                    IdWeekOff = e.IdWeekOff,
                    CreatedBy = e.CreatedBy,
                    SetDate = e.SetDate,
                    IsActive = e.IsActive,
                    HasOvertime = e.HasOvertime ?? false,
                    HasAttendenceBonus = e.HasAttendenceBonus ?? false,


                    EmployeeDocuments = e.EmployeeDocuments.Select(doc => new DocummentDto
                    {
                        IdClient = doc.IdClient,
                        DocumentName = doc.DocumentName,
                        FileName = doc.FileName,
                        UploadDate = doc.UploadDate,
                        //UploadedFileExtention = extension,
                        //UploadedFile = uploadedBytes,
                        SetDate = DateTime.Now

                    }).ToList(),


                    EmployeeEducationInfos = e.EmployeeEducationInfos.Select(info => new EducationInfoDto
                    {
                        IdClient = info.IdClient,
                        InstituteName = info.InstituteName,
                        IdEducationLevel = info.IdEducationLevel,
                        IdEducationExamination = info.IdEducationExamination,
                        IdEducationResult = info.IdEducationResult,
                        ExamScale = info.ExamScale,
                        Marks = info.Marks,
                        Major = info.Major,
                        PassingYear = info.PassingYear,
                        IsForeignInstitute = info.IsForeignInstitute,
                        Duration = info.Duration,
                        Achievement = info.Achievement,
                        SetDate = DateTime.Now

                    }).ToList(),


                    EmployeeFamilyInfos = e.EmployeeFamilyInfos.Select(info => new EmployeefamilyInfoDto
                    {
                        IdClient = info.IdClient,
                        IdGender = info.IdGender,
                        IdRelationship = info.IdRelationship,
                        Name = info.Name,
                        ContactNo = info.ContactNo,
                        DateOfBirth = info.DateOfBirth,
                        CurrentAddress = info.CurrentAddress,
                        PermanentAddress = info.PermanentAddress,
                        SetDate = DateTime.Now

                    }).ToList(),



                    EmployeeProfessionalCertifications = e.EmployeeProfessionalCertifications.Select(info => new EmployeeProfessionalCertificationDto
                    {
                        IdClient = info.IdClient,
                        CertificationTitle = info.CertificationTitle,
                        CertificationInstitute = info.CertificationInstitute,
                        InstituteLocation = info.InstituteLocation,
                        FromDate = info.FromDate,
                        ToDate = info.ToDate,
                        SetDate = DateTime.Now

                    }).ToList(),





                })

                 .ToListAsync();
            return Ok(employees);
        }



        //GET by Id

        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult<HrmDTO.EmployeeDTO>> GetEmployeeById(int id)
        {
            var employee = await _context.Employees

                .Where(e => e.Id == id)
                .Select(e => new EmployeeDTO
                {
                    IdClient = 10001001,
                    Id = e.Id,
                    EmployeeName = e.EmployeeName,
                    EmployeeNameBangla = e.EmployeeNameBangla,
                    FatherName = e.FatherName,
                    MotherName = e.MotherName,
                    BirthDate = e.BirthDate,
                    JoiningDate = e.JoiningDate,
                    ContactNo = e.ContactNo,
                    NationalIdentificationNumber = e.NationalIdentificationNumber,
                    Address = e.Address,
                    PresentAddress = e.PresentAddress,
                    IdGender = e.IdGender,
                    IdReligion = e.IdReligion,
                    IdDepartment = e.IdDepartment,
                    IdSection = e.IdSection,
                    IdDesignation = e.IdDesignation,
                    IdReportingManager = e.IdReportingManager,
                    IdJobType = e.IdJobType,
                    IdEmployeeType = e.IdEmployeeType,
                    IdMaritalStatus = e.IdMaritalStatus,
                    IdWeekOff = e.IdWeekOff,
                    CreatedBy = e.CreatedBy,
                    SetDate = e.SetDate,
                    IsActive = e.IsActive,

                    EmployeeDocuments = e.EmployeeDocuments.Select(doc => new DocummentDto
                    {
                        IdClient = doc.IdClient,
                        DocumentName = doc.DocumentName,
                        FileName = doc.FileName,
                        UploadDate = doc.UploadDate,
                        //UploadedFileExtention = extension,
                        //UploadedFile = uploadedBytes,
                        SetDate = DateTime.Now

                    }).ToList(),




                    EmployeeEducationInfos = e.EmployeeEducationInfos.Select(info => new EducationInfoDto
                    {
                        IdClient = info.IdClient,
                        InstituteName = info.InstituteName,
                        IdEducationLevel = info.IdEducationLevel,
                        IdEducationExamination = info.IdEducationExamination,
                        IdEducationResult = info.IdEducationResult,
                        ExamScale = info.ExamScale,
                        Marks = info.Marks,
                        Major = info.Major,
                        PassingYear = info.PassingYear,
                        IsForeignInstitute = info.IsForeignInstitute,
                        Duration = info.Duration,
                        Achievement = info.Achievement,
                        SetDate = DateTime.Now

                    }).ToList(),



                    EmployeeFamilyInfos = e.EmployeeFamilyInfos.Select(info => new EmployeefamilyInfoDto
                    {
                        IdClient = info.IdClient,
                        IdGender = info.IdGender,
                        IdRelationship = info.IdRelationship,
                        Name = info.Name,
                        ContactNo = info.ContactNo,
                        DateOfBirth = info.DateOfBirth,
                        CurrentAddress = info.CurrentAddress,
                        PermanentAddress = info.PermanentAddress,
                        SetDate = DateTime.Now

                    }).ToList(),



                    EmployeeProfessionalCertifications = e.EmployeeProfessionalCertifications.Select(info => new EmployeeProfessionalCertificationDto
                    {
                        IdClient = info.IdClient,
                        CertificationTitle = info.CertificationTitle,
                        CertificationInstitute = info.CertificationInstitute,
                        InstituteLocation = info.InstituteLocation,
                        FromDate = info.FromDate,
                        ToDate = info.ToDate,
                        SetDate = DateTime.Now

                    }).ToList(),



                })
                .FirstOrDefaultAsync();

            if (employee == null)
                return NotFound("Sorry! Not Found.");

            return Ok(employee);

        }



        //POST Operation 

        [HttpPost]

        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDTO createDto)
        {
            var employee = new Employee
            {


                IdClient = 10001001,

                EmployeeName = createDto.EmployeeName,
                EmployeeNameBangla = createDto.EmployeeNameBangla,
                FatherName = createDto.FatherName,
                MotherName = createDto.MotherName,
                BirthDate = createDto.BirthDate,
                JoiningDate = createDto.JoiningDate,
                ContactNo = createDto.ContactNo,
                NationalIdentificationNumber = createDto.NationalIdentificationNumber,
                Address = createDto.Address,
                PresentAddress = createDto.PresentAddress,
                IdGender = createDto.IdGender,
                IdReligion = createDto.IdReligion,
                IdDepartment = createDto.IdDepartment,
                IdSection = createDto.IdSection,
                IdDesignation = createDto.IdDesignation,
                IdReportingManager = createDto.IdReportingManager,
                IdJobType = createDto.IdJobType,
                IdEmployeeType = createDto.IdEmployeeType,
                IdMaritalStatus = createDto.IdMaritalStatus,
                CreatedBy = createDto.CreatedBy,
                SetDate = DateTime.Now,
                IsActive = createDto.IsActive ?? true,




                EmployeeDocuments = createDto.EmployeeDocuments.Select(doc => new EmployeeDocument
                {
                    IdClient = doc.IdClient,
                    DocumentName = doc.DocumentName,
                    FileName = doc.FileName,
                    UploadDate = doc.UploadDate,
                    //UploadedFileExtention = extension,
                    //UploadedFile = uploadedBytes,
                    SetDate = DateTime.Now

                }).ToList(),





                EmployeeEducationInfos = createDto.EmployeeEducationInfos.Select(info => new EmployeeEducationInfo
                {
                    IdClient = info.IdClient,
                    InstituteName = info.InstituteName,
                    IdEducationLevel = info.IdEducationLevel,
                    IdEducationExamination = info.IdEducationExamination,
                    IdEducationResult = info.IdEducationResult,
                    ExamScale = info.ExamScale,
                    Marks = info.Marks,
                    Major = info.Major,
                    PassingYear = info.PassingYear,
                    IsForeignInstitute = info.IsForeignInstitute,
                    Duration = info.Duration,
                    Achievement = info.Achievement,
                    SetDate = DateTime.Now

                }).ToList(),




                EmployeeFamilyInfos = createDto.EmployeeFamilyInfos.Select(info => new EmployeeFamilyInfo
                {
                    IdClient = info.IdClient,
                    IdGender = info.IdGender,
                    IdRelationship = info.IdRelationship,
                    Name = info.Name,
                    ContactNo = info.ContactNo,
                    DateOfBirth = info.DateOfBirth,
                    CurrentAddress = info.CurrentAddress,
                    PermanentAddress = info.PermanentAddress,
                    SetDate = DateTime.Now


                }).ToList(),





                EmployeeProfessionalCertifications = createDto.EmployeeProfessionalCertifications.Select(info => new EmployeeProfessionalCertification
                {
                    IdClient = info.IdClient,
                    CertificationTitle = info.CertificationTitle,
                    CertificationInstitute = info.CertificationInstitute,
                    InstituteLocation = info.InstituteLocation,
                    FromDate = info.FromDate,
                    ToDate = info.ToDate,
                    SetDate = DateTime.Now


                }).ToList(),


            };


            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();


            return Ok(employee);
        }


        //PUT Operation

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO updateDto)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == updateDto.Id); // First employee Id will be like upodate Dto Id.
            if (employee == null)
            {
                return BadRequest("Employee not found! ");
            }

            employee.IdClient = 10001001;
            employee.IdGender = updateDto.IdGender;
            employee.EmployeeName = updateDto.EmployeeName;
            employee.Address = updateDto.Address;
            employee.PresentAddress = updateDto.PresentAddress;
            employee.FatherName = updateDto.FatherName;
            employee.MotherName = updateDto.MotherName;
            employee.JoiningDate = updateDto.JoiningDate;
            employee.ContactNo = updateDto.ContactNo;
            employee.NationalIdentificationNumber = updateDto.NationalIdentificationNumber;
            employee.IdReligion = updateDto.IdReligion;
            employee.IdDepartment = updateDto.IdDepartment;
            employee.IdSection = updateDto.IdSection;
            employee.IdReportingManager = updateDto.IdReportingManager;
            employee.IdJobType = updateDto.IdJobType;
            employee.IdEmployeeType = updateDto.IdEmployeeType;
            employee.IdMaritalStatus = updateDto.IdMaritalStatus;
            employee.IsActive = updateDto.IsActive;

            var result = await _context.SaveChangesAsync();


            return Ok(employee);

        }



        //Delete Operation

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteEmployee(int id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id && e.IdClient == 10001001);

            if (employee == null)
                return NotFound();

            employee.IsActive = false;

            await _context.SaveChangesAsync();

            return Ok(employee);
        }


    }




}

           
       
