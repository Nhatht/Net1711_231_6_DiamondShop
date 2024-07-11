using DiamondShopData.DAO;
using DiamondShopData.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondShopData;
using DiamondShopData.ViewModel.DiamondDTO;

namespace DiamondShopBusiness
{
    public class DiamondBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public DiamondBusiness()
        {
            _unitOfWork ??= new UnitOfWork();
        }   
        public async Task<IBusinessResult> GetAll(int pageNumber, int pageSize, string? query = null)
        {
            try
            {
                var diamonds = await _unitOfWork.DiamondRepository.GetAllAsync(pageNumber, pageSize, query);
                if(diamonds == null)
                {
                    return new BusinessResult(-4, "No diamond data");
                }
                else
                {
                    return new BusinessResult(1, "Get diamond list success", diamonds);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-1, ex.Message);
            }
        }
        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var diamond = await _unitOfWork.DiamondRepository.GetByIdAsync(id);
                if (diamond == null)
                {
                    return new BusinessResult(-4, "No diamond data by id");
                }
                else
                {
                    return new BusinessResult(1, "Get diamond success", diamond);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-1, ex.Message);
            }
        }
        // create insert method
        public async Task<IBusinessResult> Insert(DiamondDTO diamondDTO)
        {
            try
            {
                var diamond = new Diamond
                {
                    Id = diamondDTO.Id,
                    Name = diamondDTO.Name,
                    Origin = diamondDTO.Origin,
                    CaratWeight = diamondDTO.CaratWeight,
                    Color = diamondDTO.Color,
                    Clarity = diamondDTO.Clarity,
                    Cut = diamondDTO.Cut,
                    CertificateNumber = diamondDTO.CertificateNumber,
                    Price = diamondDTO.Price,
                    IsDeleted = diamondDTO.IsDeleted
                };

                var result = await _unitOfWork.DiamondRepository.CreateAsync(diamond);
                if (result == 1)
                {
                    return new BusinessResult(1, "Success", diamond);
                }
                else
                {
                    return new BusinessResult(-1, "Fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-1, ex.Message);
            }
        }


        public async Task<IBusinessResult> Update(int id, DiamondDTO diamondDto)
        {
            if (diamondDto == null)
            {
                return new BusinessResult(-1, "diamond cannot be null");
            }

            var diamond = await _unitOfWork.DiamondRepository.GetByIdAsync(id);
            if (diamond == null)
            {
                return new BusinessResult(-1, "No data found");
            }

            try
            {
                // Cập nhật các thuộc tính của diamond bằng các giá trị từ diamondDto
                diamond.Id = diamondDto.Id;
                diamond.Name = diamondDto.Name;
                diamond.Origin = diamondDto.Origin;
                diamond.CaratWeight = diamondDto.CaratWeight;
                diamond.Color = diamondDto.Color;
                diamond.Clarity = diamondDto.Clarity;
                diamond.Cut = diamondDto.Cut;
                diamond.CertificateNumber = diamondDto.CertificateNumber;
                diamond.Price = diamondDto.Price;
                diamond.IsDeleted = diamondDto.IsDeleted;

                await _unitOfWork.DiamondRepository.UpdateAsync(diamond);

                return new BusinessResult(1, "Success");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-1, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateIsDelete(int id, bool? isDelete)
        {
            var diamond = await _unitOfWork.DiamondRepository.GetByIdAsync(id);
            if (diamond == null)
            {
                return new BusinessResult(-1, "No data found");
            }

            try
            {
                // Nếu isDelete là true, đảo ngược giá trị của IsDeleted từ true thành false hoặc ngược lại
                if (isDelete.HasValue)
                {
                    if (isDelete.Value)
                    {
                        diamond.IsDeleted = !diamond.IsDeleted;
                    }
                    else
                    {
                        diamond.IsDeleted = isDelete.Value;
                    }
                }

                await _unitOfWork.DiamondRepository.UpdateAsync(diamond);

                return new BusinessResult(1, "Success");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-1, ex.Message);
            }
        }
        public async Task<IBusinessResult> Delete(int id)
        {
            var diamond = await _unitOfWork.DiamondRepository.GetByIdAsync(id);
            if (diamond == null)
            {
                return new BusinessResult(-1, "No data found");
            }

            try
            {
                await _unitOfWork.DiamondRepository.RemoveAsync(diamond); // Xóa đối tượng diamond

                return new BusinessResult(1, "Success");
            }
            catch (Exception ex)
            {
                return new BusinessResult(-1, ex.Message);
            }
        }




    }
}

