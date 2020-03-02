using AutoMapper;
using Employee_LeaveManagement.Contracts;
using Employee_LeaveManagement.Models;
using Employee_LeaveManagement.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Employee_LeaveManagement.Controllers
{
    [Authorize(Roles = "Administrator")]


    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeRepository _repository;
        private readonly IMapper _mapper;

        public LeaveTypesController(ILeaveTypeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var leaveTypes = _repository.FindAll().ToList();
            var model = _mapper.Map<List<LeaveType>, List<LeaveTypeViewModel>>(leaveTypes);
            return View(model);
        }

        // Get LeaveTypes/Edit/id
        public IActionResult Edit(Guid id)
        {

            var viewModel = _mapper.Map<LeaveTypeViewModel>(_repository.FindById(id));
            return View(viewModel);
        }
        //Post leavetypes/edit
        [HttpPut]
        public IActionResult Edit(LeaveTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var leaveType = _mapper.Map<LeaveType>(model);
            var isSuccess = _repository.Update(leaveType);
            if (isSuccess) return RedirectToAction("Index");

            ModelState.AddModelError("", "Something went Wrong");
            return View(model);
        }

        //Get LeaveTypes/Details/5
        public IActionResult Details(Guid id)
        {
            var viewModel = _mapper.Map<LeaveTypeViewModel>(_repository.FindById(id));
            return View(viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            var isSuccess = _repository.Delete(_repository.FindById(id));
            if (isSuccess) return RedirectToAction("Index");
            ModelState.AddModelError("", "Something went Wrong");
            return RedirectToAction("Index");

        }

        //Get :LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        //Post:LeaveTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var leaveType = _mapper.Map<LeaveType>(model);
            leaveType.DateCreated = DateTime.Now;
            var isSuccess = _repository.Create(leaveType);

            if (isSuccess) return RedirectToAction("Index");

            ModelState.AddModelError("", "Something went Wrong");
            return View(model);

        }
    }
}