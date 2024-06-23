using DiamondShopData;
using DiamondShopData.DAO;
using DiamondShopData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopBusiness
{
	public class CompanyBusiness
	{
		private readonly UnitOfWork _unitOfWork;
		public CompanyBusiness()
		{
			_unitOfWork ??= new UnitOfWork();
		}

		public async Task<IBusinessResult> GetAll()
		{
			try
			{
				var result = await _unitOfWork.CompanyRepository.GetAllAsync();
				if (result == null)
				{
					return new BusinessResult(-1, "No data");
				}
				else
				{
					return new BusinessResult(1, "Get list success", result);
				}
			}
			catch (Exception ex)
			{
				return new BusinessResult(-4, ex.Message);
			}
		}

		public async Task<IBusinessResult> GetById(int code)
		{
			try
			{
				#region Business rule
				#endregion
				var result = await _unitOfWork.CompanyRepository.GetByIdAsync(code);
				if (result == null)
				{
					return new BusinessResult(-1, "No data");
				}
				else
				{
					return new BusinessResult(1, "Get success", result);
				}
			}
			catch (Exception ex)
			{
				return new BusinessResult(-4, ex.Message);
			}
		}

		public async Task<IBusinessResult> Create(Company company)
		{
			try
			{
				var result = await _unitOfWork.CompanyRepository.CreateAsync(company);
				if (result == 0)
				{
					return new BusinessResult(-1, "Cannot create");
				}
				else
				{
					return new BusinessResult(1, "Create success", company);
				}
			}
			catch (Exception ex)
			{
				return new BusinessResult(-4, ex.Message);
			}
		}

		public async Task<IBusinessResult> Update(Company company)
		{
			_unitOfWork.CompanyRepository.UpdateAsync(company);
			return new BusinessResult(1, "Update success", 1);
		}
		public async Task<IBusinessResult> Remove(Company company)
		{
			try
			{
				var result = await _unitOfWork.CompanyRepository.RemoveAsync(company);
				if (result == false)
				{
					return new BusinessResult(-1, "Cannot remove");
				}
				else
				{
					return new BusinessResult(1, "Remove success", result);
				}
			}
			catch (Exception ex)
			{
				return new BusinessResult(-4, ex.Message);
			}
		}
	}
}