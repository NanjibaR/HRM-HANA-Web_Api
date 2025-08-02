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
        public async Task<ActionResult<IEnumerable<HrmDTO.EmployeeDTO>>> GetEmployees([FromQuery] int idClient, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .Where(e => e.IdClient == idClient)
                .Select(e => new EmployeeDTO
                {

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
                    EmployeeImage = e.EmployeeImage,
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
                    IsActive = e.IsActive ?? true,
                    HasOvertime = e.HasOvertime ?? false,
                    HasAttendenceBonus = e.HasAttendenceBonus ?? false,
                    //  EmployeeImageBase = ConvertImageToBase (e.EmployeeImage),



                    EmployeeDocuments = e.EmployeeDocuments.Select(doc => new DocummentDto
                    {
                        IdClient = e.IdClient,
                        DocumentName = doc.DocumentName,
                        FileName = doc.FileName,
                        UploadDate = doc.UploadDate,
                        UploadedFileExtention = doc.UploadedFileExtention,
                        UploadedFile = doc.UploadedFile,
                        // UploadedFileBase = ConvertFileToBase (doc.UploadedFile, doc.UploadedFileExtention),

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
                        IdClient = e.IdClient,
                        CertificationTitle = info.CertificationTitle,
                        CertificationInstitute = info.CertificationInstitute,
                        InstituteLocation = info.InstituteLocation,
                        FromDate = info.FromDate,
                        ToDate = info.ToDate,
                        SetDate = DateTime.Now

                    }).ToList(),

                })
                .ToListAsync(cancellationToken);

            return Ok(employees);


        }






        //GET by Id

        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult<HrmDTO.EmployeeDTO>> GetEmployeeById([FromQuery] int idClient, [FromQuery] int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Where(e => e.IdClient == idClient && e.Id == id)
                .Select(e => new EmployeeDTO

                {
                    IdClient = e.IdClient,
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
                    // EmployeeImageBase = ConvertImageToBase (e.EmployeeImage),



                    EmployeeDocuments = e.EmployeeDocuments.Select(doc => new DocummentDto
                    {
                        IdClient = doc.IdClient,
                        DocumentName = doc.DocumentName,
                        FileName = doc.FileName,
                        UploadDate = doc.UploadDate,
                        UploadedFileExtention = doc.UploadedFileExtention,
                        UploadedFile = doc.UploadedFile,
                        SetDate = DateTime.Now,
                        //UploadedFileBase = ConvertFileToBase (doc.UploadedFile, doc.UploadedFileExtention)

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
                .FirstOrDefaultAsync(cancellationToken);

            if (employee == null)
                return NotFound("Sorry! Not Found.");

            return Ok(employee);

        }




        // ConvertFileToByteArrayAsync method 


        private async Task<byte[]?> ConvertFileToByteArrayAsync(IFormFile? file, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return null;

            const long maxFileSize = 10 * 1024 * 1024;

            if (file.Length > maxFileSize)
                throw new Exception("File size cannot exceed 10 MB.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);
            return memoryStream.ToArray();
        }





        //POST Operation 

        [HttpPost]

        public async Task<ActionResult<Employee>> CreateEmployee([FromForm] EmployeeDTO createDto, CancellationToken cancellationToken)
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



                EmployeeImage = await ConvertFileToByteArrayAsync(createDto.ProfileFile, cancellationToken),

                EmployeeDocuments = new List<EmployeeDocument>(),
                //EmployeeDocuments = createDto.EmployeeDocuments.Select(doc => new EmployeeDocument
                //{
                //    IdClient = doc.IdClient,
                //    DocumentName = doc.DocumentName,
                //    FileName = doc.FileName,
                //    UploadDate = doc.UploadDate,
                //    UploadedFileExtention = doc.UploadedFileExtention,
                //    UploadedFile = doc.UploadedFile,
                //    SetDate = DateTime.Now

                //}).ToList(),

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

            foreach (var doc in createDto.EmployeeDocuments)
            {
                var extension = Path.GetExtension(doc.UpFile?.FileName);
                employee.EmployeeDocuments.Add(new EmployeeDocument
                {
                    IdClient = doc.IdClient,
                    DocumentName = doc.DocumentName,
                    FileName = doc.FileName,
                    UploadDate = doc.UploadDate,
                    UploadedFileExtention = extension,
                    UploadedFile = await ConvertFileToByteArrayAsync(doc.UpFile, cancellationToken),
                    SetDate = DateTime.Now
                });
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(cancellationToken);


            return Ok(employee);
        }






        //PUT Operation

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeDTO updateDto, CancellationToken cancellationToken)

        {
            if (updateDto == null)

                throw new Exception($"data not found: {nameof(updateDto)}");

            var idClient = updateDto.IdClient;
            var id = updateDto.Id;

            var existingEmployee = await _context.Employees
                .Include(e => e.EmployeeDocuments)
                .Include(e => e.EmployeeEducationInfos)
                .Include(e => e.EmployeeFamilyInfos)
                .Include(e => e.EmployeeProfessionalCertifications)
                .AsSplitQuery()
                .FirstOrDefaultAsync(e => e.IdClient == idClient && e.Id == id);

            var newImage = await ConvertFileToByteArrayAsync(updateDto.ProfileFile, cancellationToken);
            if (existingEmployee == null)

                return BadRequest("Not DFounds!");


            existingEmployee.IdClient = updateDto.IdClient;
            existingEmployee.Id = updateDto.Id;
            existingEmployee.IdGender = updateDto.IdGender;
            existingEmployee.EmployeeName = updateDto.EmployeeName ?? existingEmployee.EmployeeName;
            existingEmployee.Address = updateDto.Address;
            existingEmployee.PresentAddress = updateDto.PresentAddress;
            existingEmployee.FatherName = updateDto.FatherName ?? existingEmployee.FatherName;
            existingEmployee.MotherName = updateDto.MotherName ?? existingEmployee.MotherName;
            if (newImage != null)
            {
                existingEmployee.EmployeeImage = newImage;
            }
            existingEmployee.JoiningDate = updateDto.JoiningDate;
            existingEmployee.ContactNo = updateDto.ContactNo;
            existingEmployee.NationalIdentificationNumber = updateDto.NationalIdentificationNumber;
            existingEmployee.IdReligion = updateDto.IdReligion ?? existingEmployee.IdReligion;
            existingEmployee.IdGender = updateDto.IdGender ?? existingEmployee.IdGender;
            existingEmployee.IdReportingManager = updateDto.IdReportingManager ?? existingEmployee.IdReportingManager;
            existingEmployee.IdJobType = updateDto.IdJobType ?? existingEmployee.IdJobType;
            existingEmployee.IdEmployeeType = updateDto.IdEmployeeType ?? existingEmployee.IdEmployeeType;
            existingEmployee.IdWeekOff = updateDto.IdWeekOff ?? existingEmployee.IdWeekOff;
            existingEmployee.IdMaritalStatus = updateDto.IdMaritalStatus ?? existingEmployee.IdMaritalStatus;
            existingEmployee.IdDepartment = updateDto.IdDepartment;
            existingEmployee.IdSection = updateDto.IdSection;
            existingEmployee.BirthDate = updateDto.BirthDate ?? existingEmployee.BirthDate;
            existingEmployee.JoiningDate = updateDto.JoiningDate ?? existingEmployee.JoiningDate;
            existingEmployee.Address = updateDto.Address ?? existingEmployee.Address;
            existingEmployee.PresentAddress = updateDto.PresentAddress ?? existingEmployee.PresentAddress;
            existingEmployee.NationalIdentificationNumber = updateDto.NationalIdentificationNumber ?? existingEmployee.NationalIdentificationNumber;
            existingEmployee.ContactNo = updateDto.ContactNo ?? existingEmployee.ContactNo;
            existingEmployee.IsActive = updateDto.IsActive ?? existingEmployee.IsActive;
            existingEmployee.SetDate = DateTime.Now;



            //Delete EmployeeDocumentList

            var deletedEmployeeDocumentList = existingEmployee.EmployeeDocuments
                 .Where(ed => ed.IdClient == updateDto.IdClient && !updateDto.EmployeeDocuments.Any(d => d.IdClient == ed.IdClient && d.Id == ed.Id))
                 .ToList();
            if (deletedEmployeeDocumentList.Any())
            {
                _context.EmployeeDocuments.RemoveRange(deletedEmployeeDocumentList);
            }


            //Delete  EmployeeEducationInfoList

            var deletedEmployeeEducationInfoList = existingEmployee.EmployeeEducationInfos
                .Where(e => e.IdClient == updateDto.IdClient && !updateDto.EmployeeEducationInfos.Any(edu => edu.IdClient == edu.IdClient && edu.Id == edu.Id))
                .ToList();

            if (deletedEmployeeEducationInfoList.Any())
            {
                _context.EmployeeEducationInfos.RemoveRange(deletedEmployeeEducationInfoList);
            }



            //Delete  EmployeefamilyInfoList

            var deletedEmployeefamilyInfoList = existingEmployee.EmployeeFamilyInfos
                .Where(e => e.IdClient == updateDto.IdClient && !updateDto.EmployeeFamilyInfos.Any(ef => ef.IdClient == ef.IdClient && ef.Id == ef.Id))
                .ToList();

            if (deletedEmployeefamilyInfoList.Any())
            {
                _context.EmployeeFamilyInfos.RemoveRange(deletedEmployeefamilyInfoList);
            }


            //Delete  EmployeeProfessionalCertificationList

            var deletedEmployeeProfessionalCertificationList = existingEmployee.EmployeeProfessionalCertifications
                .Where(e => e.IdClient == updateDto.IdClient && !updateDto.EmployeeProfessionalCertifications.Any(ep => ep.IdClient == ep.IdClient && ep.Id == ep.Id))
                .ToList();

            if (deletedEmployeeProfessionalCertificationList.Any())
            {
                _context.EmployeeProfessionalCertifications.RemoveRange(deletedEmployeeProfessionalCertificationList);
            }



            //up/insert new documents
            foreach (var item in updateDto.EmployeeDocuments)
            {
                var existingEntry = existingEmployee.EmployeeDocuments.FirstOrDefault(ed => ed.IdClient == item.IdClient && ed.Id == item.Id);
                if (existingEntry != null)
                {
                    existingEntry.DocumentName = item.DocumentName;
                    existingEntry.FileName = item.FileName;
                    existingEntry.UploadDate = item.UploadDate;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeDocument = new EmployeeDocument()
                    {
                        IdClient = item.IdClient,
                        IdEmployee = existingEmployee.Id,
                        DocumentName = item.DocumentName,
                        FileName = item.FileName,
                        UploadDate = item.UploadDate,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeDocuments.Add(newEmployeeDocument);
                }
            }



            //up/insert new EmployeeEducationInfos
            foreach (var info in updateDto.EmployeeEducationInfos)
            {
                var existingEntry = existingEmployee.EmployeeEducationInfos.FirstOrDefault(ed => ed.IdClient == info.IdClient && ed.Id == info.Id);
                if (existingEntry != null)
                {
                    existingEntry.InstituteName = info.InstituteName;
                    existingEntry.IdEducationLevel = info.IdEducationLevel;
                    existingEntry.IdEducationExamination = info.IdEducationExamination;
                    existingEntry.IdEducationResult = info.IdEducationResult;
                    existingEntry.Major = info.Major;
                    existingEntry.PassingYear = info.PassingYear;
                    existingEntry.IsForeignInstitute = info.IsForeignInstitute;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeEducationInfo = new EmployeeEducationInfo()
                    {
                        IdClient = info.IdClient,
                        IdEmployee = existingEmployee.Id,
                        InstituteName = info.InstituteName,
                        IdEducationLevel = info.IdEducationLevel,
                        IdEducationExamination = info.IdEducationExamination,
                        IdEducationResult = info.IdEducationResult,
                        Major = info.Major,
                        PassingYear = info.PassingYear,
                        IsForeignInstitute = info.IsForeignInstitute,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeEducationInfos.Add(newEmployeeEducationInfo);
                }
            }


            //up/insert new EmployeefamilyInfos

            foreach (var info in updateDto.EmployeeFamilyInfos)
            {
                var existingEntry = existingEmployee.EmployeeFamilyInfos.FirstOrDefault(ef => ef.IdClient == info.IdClient && ef.Id == info.Id);
                if (existingEntry != null)
                {
                    existingEntry.Name = info.Name;
                    existingEntry.IdGender = info.IdGender;
                    existingEntry.IdRelationship = info.IdRelationship;
                    existingEntry.ContactNo = info.ContactNo;
                    existingEntry.CurrentAddress = info.CurrentAddress;
                    existingEntry.PermanentAddress = info.PermanentAddress;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeFamilyInfo = new EmployeeFamilyInfo()
                    {
                        IdClient = info.IdClient,
                        IdEmployee = existingEmployee.Id,
                        IdGender = info.IdGender,
                        IdRelationship = info.IdRelationship,
                        ContactNo = info.ContactNo,
                        CurrentAddress = info.CurrentAddress,
                        PermanentAddress = info.PermanentAddress,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeFamilyInfos.Add(newEmployeeFamilyInfo);
                }
            }


            //up/insert new EmployeeProfessionalCertification

            foreach (var info in updateDto.EmployeeProfessionalCertifications)
            {
                var existingEntry = existingEmployee.EmployeeProfessionalCertifications.FirstOrDefault(ep => ep.IdClient == info.IdClient && ep.Id == info.Id);
                if (existingEntry != null)
                {
                    existingEntry.CertificationTitle = info.CertificationTitle;
                    existingEntry.CertificationInstitute = info.CertificationInstitute;
                    existingEntry.InstituteLocation = info.InstituteLocation;
                    existingEntry.FromDate = info.FromDate;
                    existingEntry.ToDate = info.ToDate;
                    existingEntry.SetDate = DateTime.Now;
                }
                else
                {
                    var newEmployeeProfessionalCertificationInfo = new EmployeeProfessionalCertification()
                    {
                        IdClient = info.IdClient,
                        IdEmployee = existingEmployee.Id,
                        CertificationTitle = info.CertificationTitle,
                        CertificationInstitute = info.CertificationInstitute,
                        InstituteLocation = info.InstituteLocation,
                        FromDate = info.FromDate,
                        ToDate = info.ToDate,
                        SetDate = DateTime.Now
                    };

                    existingEmployee.EmployeeProfessionalCertifications.Add(newEmployeeProfessionalCertificationInfo);
                }
            }

            var result = await _context.SaveChangesAsync();
            return Ok(existingEmployee);

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



