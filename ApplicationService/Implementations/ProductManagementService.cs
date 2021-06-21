﻿using ApplicationService.DTOs;
using Data.Context;
using Data.Entitites;
using Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ApplicationService.Implementations
{
    public class ProductManagementService
    {
        public List<ProductDTO> Get(string query)
        {
            List<ProductDTO> productDto = new List<ProductDTO>();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                if (query == null)
                {
                    foreach (var item in unitOfWork.ProductRepository.Get())
                    {
                        productDto.Add(new ProductDTO
                        {
                            Id = item.Id,
                            ProductName = item.ProductName,
                            Description = item.Description,
                            Release = item.Release,
                            Price = item.Price,
                            Image = item.Image,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }                           
                        });
                    }
                }
                else
                {
                    foreach (var item in unitOfWork.ProductRepository.GetByQuery().Where(c => c.ProductName.Contains(query)).ToList())
                    {
                        productDto.Add(new ProductDTO
                        {
                            Id = item.Id,
                            ProductName = item.ProductName,
                            Description = item.Description,
                            Release = item.Release,
                            Price = item.Price,
                            Image = item.Image,
                            Category = new CategoryDTO
                            {
                                Id = item.Category.Id,
                                Title = item.Category.Title,
                                Description = item.Category.Description
                            }
                        });
                    }
                }
            }
            return productDto;
        }

        public ProductDTO GetById(long id)
        {
            ProductDTO productDto = new ProductDTO();

            using (UnitOfWork unitOfWork = new UnitOfWork())
            {
                Product product = unitOfWork.ProductRepository.GetByID(id);

                productDto = new ProductDTO
                {
                    Id = product.Id,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Release = product.Release,
                    Price = product.Price,
                    Image = product.Image,
                    Category = new CategoryDTO
                    {
                        Id = product.Category.Id,
                        Title = product.Category.Title,
                        Description = product.Category.Description
                    }
                };
            }
            return productDto;
        }

        public bool Save(ProductDTO productDto)
        {
            Product product = new Product
            {
                Id = productDto.Id,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Release = productDto.Release,
                Price = productDto.Price,
                Image = productDto.Image,
                CategoryId = productDto.Category.Id
            };

            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    if (productDto.Id == 0)
                    {
                        unitOfWork.ProductRepository.Insert(product);
                    }
                    else
                    {
                        unitOfWork.ProductRepository.Update(product);
                    }
                    unitOfWork.Save();
                }
                return true;
            }
            catch
            {
                System.Console.WriteLine(product);
                return false;
            }
        }



        public bool Delete(long id)
        {
            try
            {
                using (UnitOfWork unitOfWork = new UnitOfWork())
                {
                    Product product = unitOfWork.ProductRepository.GetByID(id);
                    unitOfWork.ProductRepository.Delete(product);
                    unitOfWork.Save();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}