using Dapper;
using Hospital_HospitalService.Context;
using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Entity;
using Hospital_HospitalService.Service.Interface;
using System.Net.Http;

namespace Hospital_HospitalService.Service.Impl
{
    public class DoctorServiceImpl(IHttpClientFactory httpClientFactory,DapperContext context) : IDoctorService
    {
        public object? CreateDoctor(DoctorRequest request)
        {
            UserObject obj =  request.DeptId>0? getUserByEmailId(request.Email):throw new Exception("Department Id Should be greater than 0");
            if (!obj.role.Equals("DOCTOR",StringComparison.OrdinalIgnoreCase)) 
                throw new Exception("Invalid User Role");
            DoctorEntity e= MapToEntity(request, obj);
            
            String query = "INSERT INTO Doctor (DId,DoctorName,DoctorEmail, DeptId, Specialization, Qualifications) VALUES (@DId,@DoctorName,@DoctorEmail, @DeptId, @Specialization, @Qualifications);";
          return  context.getConnection().Execute(query, e);


        }

        private DoctorEntity MapToEntity(DoctorRequest request, UserObject userObj)
        {
            return new DoctorEntity
            {
                DId= userObj.userID,
                DoctorName=userObj.firstName,
                DoctorEmail=userObj.email,
                DeptId=request.DeptId,
                Specialization=request.Specialization,
                Qualifications=request.Qualifications
            };
        }
        public DoctorEntity? GetDoctorById(int doctorId)
        {
            string query = "SELECT * FROM Doctor WHERE DId = @DoctorId;";
            return context.getConnection().QueryFirstOrDefault<DoctorEntity>(query, new { DoctorId = doctorId });
        }

        public List<DoctorEntity>? GetAllDoctors()
        {
            string query = "SELECT * FROM Doctor;";
            return context.getConnection().Query<DoctorEntity>(query).ToList();
        }

        public object? UpdateDoctor(int doctorId, DoctorRequest request)
        {
            DoctorEntity existingDoctor = GetDoctorById(doctorId) as DoctorEntity   ;
            if (existingDoctor == null)
                return null; // Doctor not found

            existingDoctor.DeptId = request.DeptId;
            existingDoctor.Specialization = request.Specialization;
            existingDoctor.Qualifications = request.Qualifications;

            string query = "UPDATE Doctor SET DeptId = @DeptId, Specialization = @Specialization, Qualifications = @Qualifications WHERE DId = @DoctorId;";
            return context.getConnection().Execute(query, new { DoctorId = doctorId, DeptId = request.DeptId, Specialization = request.Specialization, Qualifications = request.Qualifications });
        }

        public int DeleteDoctor(int doctorId)
        {
            string query = "DELETE FROM Doctor WHERE DId = @DoctorId;";
            return context.getConnection().Execute(query, new { DoctorId = doctorId });
        }
        public UserObject getUserByEmailId(String Email)
        {
            var httpclient = httpClientFactory.CreateClient("userByEmail");
            var responce = httpclient.GetAsync($"getbyemail?email={Email}").Result;
            if (responce.IsSuccessStatusCode)
            {
                return responce.Content.ReadFromJsonAsync<UserObject>().Result;
            }
            throw new Exception("UserNotFound Create User FIRST OE TRY DIFFERENT EMAIL ID");
        }
        public List<DoctorEntity>? GetDoctorsBySpecialization(string specialization)
        {
            string query = "SELECT * FROM Doctor WHERE Specialization = @Specialization;";
            return context.getConnection().Query<DoctorEntity>(query, new { Specialization = specialization }).ToList();
        }
    }
}
