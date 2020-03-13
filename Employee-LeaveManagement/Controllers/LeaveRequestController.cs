using AutoMapper;
using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Models;
using Employee_LeaveManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LeaveRequestController(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            ILeaveTypeRepository leaveTypeRepository,
            ILeaveAllocationRepository leaveAllocationRepository,
            IEmployeeRepository employeeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
            _userManager = userManager;
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;
            _employeeRepository = employeeRepository;
        }

        [Authorize(Roles = "Administrator")]
        // GET: LeaveRequest
        public ActionResult Index()
        {
            var leaveRequests = _leaveRequestRepository.FindAll();
            var leaveRequestsModel = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequests);
            var model = new AdminLeaveRequestViewModel
            {
                TotalRequests = leaveRequestsModel.Count,
                ApprovedRequests = leaveRequestsModel.Count(x => x.Approved == true),
                PendingRequests = leaveRequestsModel.Count(x => x.Approved == null),
                RejectedRequest = leaveRequestsModel.Count(x => x.Approved == false),
                LeaveRequests = leaveRequestsModel
            };
            return View(model);
        }
        public IActionResult MyLeave()
        {
            var leaveRequests = _leaveRequestRepository.FindAll().Where(x => x.RequestingEmployee.Id == _userManager.GetUserAsync(User).Result.Id);
            var leaveAllocations = _leaveAllocationRepository.GetLeaveAllocationsByEmployee(_userManager.GetUserAsync(User).Result.Id);

            var model = new EmployeeLeaveRequestViewModel
            {
                leaveRequests = leaveRequests,
                leaveAllocations = leaveAllocations

            };
            return View(model);
        }

        // GET: LeaveRequest/Details/5
        public ActionResult Details(Guid Id)
        {
            var request = _leaveRequestRepository.FindById(Id);
            var model = _mapper.Map<LeaveRequestViewModel>(request);


            return View(model);
        }

        [HttpPost]
        public IActionResult Details(LeaveRequestViewModel model)
        {

            var request = _mapper.Map<LeaveRequest>(model);
            request.RequestingEmployee = _employeeRepository.FindById(Guid.Parse(model.RequestingEmployeeId));
            request.ApprovedBy = _employeeRepository.FindById(Guid.Parse(_userManager.GetUserAsync(User).Result.Id));
            request.LeaveType = _leaveTypeRepository.FindById(model.LeaveTypeId);
            if (request.Approved == true)
            {
                var allocation = _leaveAllocationRepository.GetLeaveAllocationByEmployeeAndLeaveType(request.RequestingEmployee.Id, request.LeaveType.Id);
                allocation.NumberOfDays -= (int)(request.EndDate - request.StartDate).TotalDays;

                _leaveAllocationRepository.Update(allocation);
            }

            _leaveRequestRepository.Update(request);


            return RedirectToAction("Index");
        }

        // GET: LeaveRequest/Create
        public ActionResult Create()
        {
            var leaveTypes = _leaveTypeRepository.FindAll().ToList();
            var employees = _employeeRepository.FindAll().ToList();
            var leaveTypeItems = leaveTypes.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            var employeeItems = employees.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.Id
            });
            var model = new CreateLeaveRequestViewModel
            {
                LeaveTypes = leaveTypeItems,
                Employees = employeeItems

            };
            return View(model);
        }

        // POST: LeaveRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateLeaveRequestViewModel model)
        {

            try
            {
                var leaveTypes = _leaveTypeRepository.FindAll().ToList();
                var employees = _employeeRepository.FindAll().ToList();
                var startDate = Convert.ToDateTime(model.StartDate);
                var endDate = Convert.ToDateTime(model.EndDate);
                var leaveTypeItems = leaveTypes.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
                var employeeItems = employees.Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.Id
                });
                model.LeaveTypes = leaveTypeItems;
                model.Employees = employeeItems;
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (DateTime.Compare(startDate, endDate) > 1)
                {
                    ModelState.AddModelError("", "End Date Can not be earlier than Start Date");
                    return View(model);
                }


                var allocation =
                    _leaveAllocationRepository.GetLeaveAllocationByEmployeeAndLeaveType(model.EmployeeId, model.LeaveTypeId);

                var daysRequested = (int)(endDate - startDate).TotalDays;
                if (daysRequested > allocation.NumberOfDays)
                {
                    ModelState.AddModelError("", "You do not have enough days");
                    return View(model);
                }

                var leaveRequestModel = new LeaveRequestViewModel
                {
                    RequestingEmployeeId = model.EmployeeId,
                    StartDate = startDate,
                    EndDate = endDate,
                    Approved = null,
                    RequestDate = DateTime.Now,


                };
                var leaveRequest = new LeaveRequest
                {
                    Approved = null,
                    ApprovedBy = null,
                    EndDate = endDate,
                    LeaveType = _leaveTypeRepository.FindById(model.LeaveTypeId),
                    RequestDate = DateTime.Now,
                    RequestingEmployee = _employeeRepository.FindById(Guid.Parse(model.EmployeeId)),
                    StartDate = startDate
                };

                var isSuccess = _leaveRequestRepository.Create(leaveRequest);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong with submitting the record");
                    return View(model);
                }

                if (User.IsInRole("Administrator"))
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction("Index", "Home");


            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", "Something Went Wrong: " + exception.Message);
                return View(model);
            }
        }

        // GET: LeaveRequest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LeaveRequest/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveRequest/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View();
        }

        // POST: LeaveRequest/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public IActionResult CancelRequest(Guid requestId)
        {
            var leaveRequest = _leaveRequestRepository.FindById(requestId);
            var employee = _employeeRepository.FindById(Guid.Parse(_userManager.GetUserAsync(User).Result.Id));

            if (leaveRequest.Approved == true)
            {
                var leaveAllocation =
                    _leaveAllocationRepository.GetLeaveAllocationByEmployeeAndLeaveType(employee.Id,
                        leaveRequest.LeaveType.Id);
                leaveAllocation.NumberOfDays += (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;
                _leaveAllocationRepository.Update(leaveAllocation);
            }

            leaveRequest.IsDeleted = true;
            _leaveRequestRepository.Update(leaveRequest);
            return RedirectToAction("MyLeave");

        }
    }
}