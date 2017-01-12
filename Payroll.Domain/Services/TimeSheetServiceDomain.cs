using Payroll.Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Domain.Entities;
using Payroll.Domain.Interfaces.Repositories;

namespace Payroll.Domain.Services
{
    public class TimeSheetServiceDomain : ServiceDomainBase, ITimeSheetServiceDomain
    {
        private readonly ITimeSheetItemRepository _timeSheetItemRepository;
        private readonly ITimeSheetRepository _timeSheetRepository;

        public TimeSheetServiceDomain(ITimeSheetRepository timeSheetRepository, ITimeSheetItemRepository timeSheetItemRepository)
        {
            _timeSheetRepository = timeSheetRepository;
            _timeSheetItemRepository = timeSheetItemRepository;
        }

        public void CommitDay(List<TimeSheetItem> items)
        {
            try
            {
                StartTransaction();
                _timeSheetRepository.CommitDay(items);
                PersistTransaction();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public List<Employee> GetAllEmployees()
        {
            return _timeSheetItemRepository.GetAllEmployees();
        }

        public List<TimeSheetItem> GetByDate(DateTime date)
        {
            return _timeSheetItemRepository.GetByDate(date);
        }

        public List<Employee> GetEmployeesLeft(DateTime Date)
        {
            return _timeSheetItemRepository.GetEmployeesLeft(Date);
        }

        public void InsertSheetItem(TimeSheetItem item)
        {
            try
            {
                StartTransaction();
                _timeSheetItemRepository.Insert(item);
                PersistTransaction();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public void DeleteItem(int itemId)
        {
            try
            {
                StartTransaction();
                _timeSheetItemRepository.Remove(itemId);
                PersistTransaction();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public List<TimeSheetItem> GetAllItems()
        {
            return _timeSheetItemRepository.RecoverAll().ToList();
        }

        public List<TimeSheet> GetMonthsSheets(DateTime Date)
        {
            try
            {
                StartTransaction();
                var list = _timeSheetRepository.GetThisMonthsSheets(Date);
                PersistTransaction();
                return list;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);                
            }
        }

        public List<TimeSheetItem> GetByTimeSheetId(int id)
        {
            try
            {
                StartTransaction();
                var list = _timeSheetItemRepository.GetByTimeSheetId(id);
                PersistTransaction();
                return list;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public Employee GetEmployeeById(int ID)
        {
            try
            {
                StartTransaction();
                Employee emp = _timeSheetRepository.EmployeeByID(ID);
                PersistTransaction();
                return emp;
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
