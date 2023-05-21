using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagementSystem.Dtos.StudentDto;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interface;
using LibraryManagementSystem.Services.Interface;

namespace LibraryManagementSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateStudentDto createStudentDto)
        {
            var student = new Student()
            {
                FirstName = createStudentDto.FirstName,
                LastName = createStudentDto.LastName,
                PhoneNumber = createStudentDto.PhoneNumber,
                Email = createStudentDto.Email,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedUserId = createStudentDto.CreatedUserId,
                ModifiedUserId = createStudentDto.CreatedUserId,
                ImageUrl = createStudentDto.ImageUrl,
                Grade = createStudentDto.Grade
            };
            await _unitOfWork.CreateAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _studentRepository.GetByAsync(x=>x.Id==id);
            await _unitOfWork.DeleteAsync(student);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UpdateStudentDto updateStudentDto)
        {
            var student = await _studentRepository.GetByAsync(x=>x.Id==updateStudentDto.Id);
            student.FirstName = updateStudentDto.FirstName;
            student.LastName = updateStudentDto.LastName;
            student.PhoneNumber = updateStudentDto.PhoneNumber;
            student.Email = updateStudentDto.Email;
            student.ModifiedDate = DateTime.UtcNow;
            student.ModifiedUserId = updateStudentDto.ModifiedUserId;
            student.Grade = updateStudentDto.Grade;
            if(!string.IsNullOrEmpty(updateStudentDto.ImageUrl)) 
            {
                student.ImageUrl = updateStudentDto.ImageUrl;
            }
            await _unitOfWork.UpdateAsync(student);
            await _unitOfWork.SaveAsync();
        }
    }
}