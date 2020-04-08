using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using WebGigHub.Core;
using WebGigHub.Core.Dtos;
using WebGigHub.Core.Models;
using WebGigHub.Persistence;

namespace WebGigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        public AttendancesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpPost]
        public async Task<IHttpActionResult> Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var attendance = await _unitOfWork.AttendanceRepository.GetAttendance(userId, dto.GigId);
            
            if (attendance != null)
                return BadRequest("The attendance already exists.");

            attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            
            _unitOfWork.AttendanceRepository.Add(attendance);
            await _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = await _unitOfWork.AttendanceRepository.GetAttendance(userId, id);

            if (attendance == null)
                return NotFound();

            _unitOfWork.AttendanceRepository.Remove(attendance);
            await _unitOfWork.Complete();

            return Ok(id);
        }
    }
}