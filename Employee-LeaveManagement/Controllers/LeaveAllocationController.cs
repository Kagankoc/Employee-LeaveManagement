using AutoMapper;
using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Models;
using Employee_LeaveManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LeaveAllocationController : Controller
    {
        private readonly ILeaveAllocationRepository _repository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        int _numberUpdated = 0;

        public LeaveAllocationController(ILeaveAllocationRepository repository,
            ILeaveTypeRepository leaveTypeRepository,
            IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
            _userManager = userManager;
        }
        // GET: LeaveAllocation
        public ActionResult Index(int numberUpdated)
        {
            var leaveTypes = _leaveTypeRepository.FindAll().ToList();
            var mappedLeaveTypes = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leaveTypes);
            var model = new CreateLeaveAllocationViewModel
            {
                LeaveTypes = mappedLeaveTypes,
                NumberUpdated = numberUpdated
            };
            return View(model);
        }

        // GET: LeaveAllocation/Details/5
        public ActionResult Details(string id)
        {

            var employee = _mapper.Map<EmployeeViewModel>(_userManager.FindByIdAsync(id).Result);
            var allocations = _repository.GetLeaveAllocationsByEmployee(id);
            var model = new ViewAllocationsViewModel
            {
                Employee = employee,
                LeaveAllocations = _mapper.Map<List<LeaveAllocationViewModel>>(allocations)
            };
            return View(model);
        }

        // GET: LeaveAllocation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LeaveAllocation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Edit/5
        public ActionResult Edit(Guid id)
        {
            var leaveAllocation = _repository.FindById(id);
            var model = _mapper.Map<EditLeaveAllocationViewModel>(leaveAllocation);
            return View(model);
        }

        // POST: LeaveAllocation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditLeaveAllocationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var allocation = _mapper.Map<LeaveAllocation>(model);
                allocation.Employee = _repository.GetEmployeeById(allocation.EmployeeId);
                allocation.LeaveType = _repository.GetLeaveTypeById(allocation.LeaveTypeId);
                var Success = _repository.Update(allocation);
                if (!Success)
                {
                    ModelState.AddModelError("", "Error While Saving");
                    return View(model);
                }

                return RedirectToAction(nameof(Details), new { id = model.EmployeeId });
            }
            catch
            {
                return View();
            }
        }

        // GET: LeaveAllocation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LeaveAllocation/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        public IActionResult SetLeave(Guid Id)
        {
            var leaveType = _leaveTypeRepository.FindById(Id);
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;


            foreach (var employee in employees)
            {
                if (_repository.CheckAllocation(Id, employee.Id))
                {
                    continue;
                }


                var leaveAllocation = new LeaveAllocation
                {
                    DateCreated = DateTime.Now,

                    EmployeeId = employee.Id,
                    LeaveTypeId = Id,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = DateTime.Now.Year

                };

                _repository.Create(leaveAllocation);
                _numberUpdated++;
            }

            return RedirectToAction("Index", new { numberUpdated = _numberUpdated });
        }

        public IActionResult ListEmployees()
        {
            var employees = _userManager.GetUsersInRoleAsync("Employee").Result;
            var model = _mapper.Map<List<EmployeeViewModel>>(employees);
            return View(model);
        }
    }
}