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

        //GET Operation

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<HrmDTO.EmployeeDTO>>> GetEmployees([FromQuery] int IdClient, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees

                .AsNoTracking()

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
                  



                    EmployeeDocuments = e.EmployeeDocuments.Select(doc => new DocummentDto
                    {
                        IdClient = doc.IdClient,
                        DocumentName = doc.DocumentName,
                        FileName = doc.FileName,
                        UploadDate = doc.UploadDate,
                        UploadedFileExtention = doc.UploadedFileExtention,
                        UploadedFile = doc.UploadedFile,
                      

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

                 .ToListAsync(cancellationToken);

            return Ok(employees);
        }




        //GET by Id

        [HttpGet("detail/{id:int}")]
        public async Task<ActionResult<HrmDTO.EmployeeDTO>> GetEmployeeById ([FromQuery] int IdCliuent, [FromQuery] int id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees

                .AsNoTracking()

                .Where(e => e.IdClient == 10001001 && e.Id == id)
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
                        UploadedFileExtention = doc.UploadedFileExtention,
                        UploadedFile = doc.UploadedFile,
                        SetDate = DateTime.Now,
                        UploadedFileBase = ConvertFileToBase64(doc.UploadedFile, doc.UploadedFileExtention),

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



        //POST Operation 

        [HttpPost]

        public async Task<ActionResult<Employee>> CreateEmployee([FromForm] EmployeeDTO createDto, CancellationToken cancellationToken)
        {

            //const long FileSize = 10 * 1024 * 1024;

            //// Validate EmployeeImage size

            //if (createDto.EmployeeImage != null && createDto.EmployeeImage.Length > FileSize)

            //{

            //    return BadRequest("Image size exceeded 10 MB!");

            //}

            //// Convert Employee image to byte[]

            //byte[]? employeeImageBytes = null;

            //if (createDto.EmployeeImage != null && createDto.EmployeeImage.Length > 0)

            //{

            //    using var ms = new MemoryStream();

            //    await createDto.EmployeeImage.CopyToAsync(ms);

            //    employeeImageBytes = ms.ToArray();

            //}

            //// Convert document files to byte[]

            //foreach (var doc in createDto.EmployeeDocuments)

            //{

            //    if (doc.UploadedFile != null)

            //    {

            //        if (doc.UploadedFile.Length > FileSize)

            //        {

            //            return BadRequest($"Document size exceeded 10 MB.");

            //        }

            //        using var ms = new MemoryStream();

            //        await doc.UploadedFile.CopyToAsync(ms);

            //        doc.UploadedFileExtention = Path.GetExtension(doc.UploadedFile.FileName);

            //        doc.FileName = Path.GetFileName(doc.UploadedFile.FileName);

            //        doc.UploadDate = DateTime.Now;

            //        doc.UploadedFileBase = ms.ToArray();

            //    }

            //}

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



                EmployeeDocuments = createDto.EmployeeDocuments.Select(doc => new EmployeeDocument
                {
                    IdClient = doc.IdClient,
                    DocumentName = doc.DocumentName,
                    FileName = doc.FileName,
                    UploadDate = doc.UploadDate,
                    UploadedFileExtention = doc.UploadedFileExtention,
                    UploadedFile = doc.UploadedFile,
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
            await _context.SaveChangesAsync(cancellationToken);


            return Ok(employee);
        }


        //PUT Operation

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromForm] EmployeeDTO updateDto, CancellationToken cancellationToken)

        {

            const long FileSize = 10 * 1024 * 1024;


            var employee = await _context.Employees
                .Include(e => e.EmployeeDocuments)

                .Include(e => e.EmployeeEducationInfos)

                .Include(e => e.EmployeeProfessionalCertifications)

                .Include(e => e.EmployeeFamilyInfos)
                .FirstOrDefaultAsync(e => e.IdClient == 10001001 && e.Id == updateDto.Id, cancellationToken); // First employee Id will be like upodate Dto Id.


            if (employee == null)
            {
                return BadRequest("Employee not found! ");
            }

            employee.IdClient = updateDto.IdClient;
            employee.Id = updateDto.Id;
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




            if (employee.EmployeeImage != null && employee.EmployeeImage.Length > 0)

            {

                if (employee.EmployeeImage.Length > FileSize)

                    throw new Exception("Employee image size cannot exceed 10 MB.");

                using var ms = new MemoryStream();

                await employee.EmployeeImage.CopyToAsync(ms, cancellationToken);

                employee.EmployeeImage = ms.ToArray();



                //Delete obsolete documents
                var deletedEmployeeDocumentList = employee.EmployeeDocuments
                 .Where(ed => ed.IdClient == updateDto.IdClient && !updateDto.EmployeeDocuments.Any(d => d.IdClient == ed.IdClient && d.Id == ed.Id))
                 .ToList();

                if (deletedEmployeeDocumentList.Any())
                {
                    _context.EmployeeDocuments.RemoveRange(deletedEmployeeDocumentList);
                }

                //up/insert new documents
                foreach (var item in updateDto.EmployeeDocuments)
                {
                    var existingEntry = employee.EmployeeDocuments.FirstOrDefault(ed => ed.IdClient == item.IdClient && ed.Id == item.Id);
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
                            IdEmployee = employee.Id,
                            DocumentName = item.DocumentName,
                            FileName = item.FileName,
                            UploadDate = item.UploadDate,
                            SetDate = DateTime.Now
                        };

                        employee.EmployeeDocuments.Add(newEmployeeDocument);
                    }
                }





                var result = await _context.SaveChangesAsync();


                return Ok(employee);

            }

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
   



        //private static string GetMimeType(string? extension)
        //{
        //    return extension?.ToLower() switch
        //    {
        //        ".jpg" or ".jpeg" => "image/jpeg",
        //        ".png" => "image/png",
        //        ".gif" => "image/gif",
        //        ".pdf" => "application/pdf",
        //        ".doc" => "application/msword",
        //        ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
        //        ".xls" => "application/vnd.ms-excel",
        //        ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //        ".txt" => "text/plain",
        //        _ => "application/octet-stream" // fallback
        //    };
        //}

        //private static string? ConvertImageToBase64(byte[]? image)
        //{
        //    if (image == null || image.Length == 0)
        //        return null;
        //    return image != null
        //        ? $"data:image/jpeg;base64,{Convert.ToBase64String(image)}"
        //        : null;
        //}


        //private static string? ConvertFileToBase64(byte[] fileBytes, string? fileExtension)
        //{
        //    if (fileBytes == null || string.IsNullOrEmpty(fileExtension))
        //        return null;

        //    var mimeType = GetMimeType(fileExtension);
        //    return $"data:{mimeType};base64,{Convert.ToBase64String(fileBytes)}";
        //}

    }




}

           
       
