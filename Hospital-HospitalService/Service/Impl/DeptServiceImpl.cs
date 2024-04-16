using Dapper;
using Hospital_HospitalService.Context;
using Hospital_HospitalService.DTO;
using Hospital_HospitalService.Entity;
using Hospital_HospitalService.Service.Interface;
using System.Data;

namespace Hospital_HospitalService.Service.Impl
{
    public class DeptServiceImpl(DapperContext context) : IDeptService
    {
        public object? CreateDept(DeptRequest deptRequest)
        {
            String query = "INSERT INTO Department (DeptName) VALUES ( @DeptName);SELECT SCOPE_IDENTITY();";
           var v= MapToEntity(deptRequest);
           return context.getConnection().Query(query,new { DeptName =v.DeptName}).FirstOrDefault();
        }

        public DepartmentEntity? getByDeptId(int id)
        {
            String query = "Select * from Department where DeptId=@Did";
            return context.getConnection().Query<DepartmentEntity>(query,new {Did=id}).FirstOrDefault();
        }

        public List<DepartmentEntity>? getByDeptName(string name)
        {
            String query = "Select * from Department where DeptName=@dname";
            return context.getConnection().Query<DepartmentEntity>(query, new { dname = name }).ToList();
        }

        private DepartmentEntity MapToEntity(DeptRequest request) => new DepartmentEntity { DeptName = request.DeptName };

    }
}
