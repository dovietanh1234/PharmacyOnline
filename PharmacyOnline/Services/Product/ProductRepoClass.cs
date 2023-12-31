﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PharmacyOnline.Common;
using PharmacyOnline.DTO.ProductDTO;
using PharmacyOnline.Entities;
using PharmacyOnline.Models.Candidate;
using PharmacyOnline.Models.ProductModel;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PharmacyOnline.Services.Product
{
    public class ProductRepoClass : IProductRepo
    {
        private readonly PharmacyOnlineContext _context;
        public static int PAGE_SIZE { get; set; } = 3;


        public ProductRepoClass(PharmacyOnlineContext context)
        {
            _context = context;
        }

        public async Task<object> AddProductTablet(ProductTablet model, string url)
        {
            try
            {
                var cate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == model.cateId);
                if(cate == null)
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = "category is not exist"
                    };
                }

                ProductDetail productDetailnew = new ProductDetail()
                {
                    ModelNumber = model.modelNumber,
                    Dies = model.dies,
                    MaxPressure = model.maxPressure,
                    MaxDiameterOfTablet = model.maxDiameterOfTablet,
                    MaxDepthOfFill = model.maxDepthOfFill,
                    ProductionCapacity = model.productionCapacity,
                    MachineSize = model.machineSize,
                    NetWeight = model.netWeight,
                };

               await _context.ProductDetails.AddAsync(productDetailnew);
               await _context.SaveChangesAsync();

                Entities.Product newProduct = new Entities.Product()
                {
                    CateId = 3,
                    ProductDetailId = productDetailnew.Id,
                    CreatedAt = DateTime.Now,
                    ProductName = model.productName,
                    Title = model.title,
                    Thumbnail = url
                };

                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return new ProductTabletDTO
                {
                      Id = newProduct.Id,
                      productName = newProduct.ProductName,
                      title = newProduct.Title,
                      thumbnail = newProduct.Thumbnail,
                      cateId = model.cateId,
                      productDetailId = productDetailnew.Id,
                      cateGet = new CategoryGet
                      {
                          CateName = cate.CateName
                      },
                    TabletP = new Tablet
                    {
                        modelNumber = model.modelNumber,
                        dies = model.dies,
                        maxPressure = model.maxPressure,
                        maxDiameterOfTablet = model.maxDiameterOfTablet,
                        maxDepthOfFill = model.maxDepthOfFill,
                        productionCapacity = model.productionCapacity,
                        machineSize = model.machineSize,
                        netWeight = model.netWeight,
                    }
                };


            }
            catch (Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }
        }


        public async Task<object> AddProductCapsule(ProductCapsule model, string url)
        {
            try
            {
                var cate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == model.cateId);
                if (cate == null)
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = $"not found the category"
                    };
                }


                ProductDetail productDetailnew = new ProductDetail()
                {
                    Output = model.Output,
                    CapsuleSize = model.CapsuleSize,
                    MachineDimension = model.MachineDimension,
                    ShippingWeight = model.ShippingWeight,
                };
                await _context.ProductDetails.AddAsync(productDetailnew);
                await _context.SaveChangesAsync();

                Entities.Product newProduct = new Entities.Product()
                {
                    CateId = 1,
                    ProductDetailId = productDetailnew.Id,
                    CreatedAt = DateTime.Now,
                    ProductName = model.productName,
                    Title = model.title,
                    Thumbnail = url
                };

                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();

                return new ProductCapsuleDTO
                {
                    Id = newProduct.Id,
                    productName = newProduct.ProductName,
                    title = newProduct.Title,
                    thumbnail = newProduct.Thumbnail,
                    cateId = model.cateId,
                    productDetailId = productDetailnew.Id,
                    cateGet = new CategoryGet
                    {
                        CateName = cate.CateName
                    },
                    CapsuleP = new capsule
                    {
                        Output = model.Output,
                        CapsuleSize = model.CapsuleSize,
                        MachineDimension = model.MachineDimension,
                        ShippingWeight = model.ShippingWeight,
                    }
                };
            }
            catch (Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }
        }

        public async Task<object> AddProductLiquid(ProductLiquid model, string url)
        {
            try
            {
                var cate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == model.cateId);
                if (cate == null)
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = $"not found the category"
                    };
                }

                ProductDetail productDetailnew = new ProductDetail()
                {
                    AirPressure = model.AirPressure,
                    AirVolume = model.AirVolume,
                    FillingSpeed = model.FillingSpeed,
                    FillingRange = model.FillingRange
                };
                await _context.ProductDetails.AddAsync(productDetailnew);
                await _context.SaveChangesAsync();

                Entities.Product newProduct = new Entities.Product()
                {
                    CateId = 2,
                    ProductDetailId = productDetailnew.Id,
                    CreatedAt = DateTime.Now,
                    ProductName = model.productName,
                    Title = model.title,
                    Thumbnail = url
                };


                await _context.Products.AddAsync(newProduct);
                await _context.SaveChangesAsync();



                return new ProductLiquidDTO
                {
                    Id = newProduct.Id,
                    productName = newProduct.ProductName,
                    title = newProduct.Title,
                    thumbnail = newProduct.Thumbnail,
                    cateId = model.cateId,
                    productDetailId = productDetailnew.Id,
                    cateGet = new CategoryGet
                    {
                        CateName = cate.CateName
                    },
                    LiquidP = new liquid
                    {
                        AirPressure = model.AirPressure,
                        AirVolume = model.AirVolume,
                        FillingSpeed = model.FillingSpeed,
                        FillingRange = model.FillingRange
                    }
                };

            }
            catch(Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }
        }



        public async Task<object> detailProduct(int idProduct)
        {
            try
            {

                var product = await _context.Products.Where(p => p.Id == idProduct).Include(p => p.Cate).FirstOrDefaultAsync();
                

                if (product == null)
                {
                    return new
                    {
                        status = 404,
                        message = "not found the product"
                    };
                }
                var productDetail = await _context.ProductDetails.FirstOrDefaultAsync( p => p.Id == product.ProductDetailId );

                if (productDetail == null)
                {
                    return new
                    {
                        status = 404,
                        message = "not found the product detail"
                    };
                }

                int result = product.CateId;

                switch(result)
                {
                    case 1: return new ProductCapsuleDTO
                    {
                        Id = product.Id,
                        productName = product.ProductName,
                        title = product.Title,
                        thumbnail = product.Thumbnail, 
                        cateId = product.CateId,
                        productDetailId = product.ProductDetailId,
                        cateGet = new CategoryGet { 
                            CateName = product.Cate.CateName
                        },
                        CapsuleP = new capsule
                        {
                            Output = productDetail.Output,
                            CapsuleSize = productDetail.CapsuleSize,
                            MachineDimension = productDetail.MachineDimension,
                            ShippingWeight = productDetail.ShippingWeight,
                        }

                    };
                    case 2: return new ProductLiquidDTO
                    {
                        Id = product.Id,
                        productName = product.ProductName,
                        title = product.Title,
                        thumbnail = product.Thumbnail,
                        cateId = product.CateId,
                        productDetailId = product.ProductDetailId,
                        cateGet = new CategoryGet
                        {
                            CateName = product.Cate.CateName
                        },
                        LiquidP = new liquid
                        {
                            AirPressure = productDetail.AirPressure,
                            AirVolume = productDetail.AirVolume,
                            FillingSpeed = productDetail.FillingSpeed,
                            FillingRange = productDetail.FillingRange
                        }
                        
                    };
                    case 3: return new ProductTabletDTO
                    {
                        Id = product.Id,
                        productName = product.ProductName,
                        title = product.Title,
                        thumbnail = product.Thumbnail,
                        cateId = product.CateId,
                        productDetailId = product.ProductDetailId,
                        cateGet = new CategoryGet
                        {
                            CateName = product.Cate.CateName
                        },
                        TabletP = new Tablet
                        {
                            modelNumber = productDetail.ModelNumber,
                            dies = productDetail.Dies,
                            maxPressure = productDetail.MaxPressure,
                            maxDiameterOfTablet = productDetail.MaxDiameterOfTablet,
                            maxDepthOfFill = productDetail.MaxDepthOfFill,
                            productionCapacity = productDetail.ProductionCapacity,
                            machineSize = productDetail.MachineSize,
                            netWeight = productDetail.NetWeight,
                        }
                    };

                    default:
                        return new
                        {
                            status = 404,
                            message = "category of products has not existed"
                        };
                }
               
            }
            catch (Exception ex)
            {
                return new result { status = 401, statusMessage = $"Error! has error occured {ex}" };
            }
        }

        public async Task<object> UpdateProductTablet(ProductTablet2 model, string url)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.id);

                if ( product == null )
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = $"not found Product"
                    };
                }
                // ví category product tablet == 3 -> nếu khác 3 thì sẽ sai ... case này nếu == 3 thì sẽ ko vào case if này
                if (product.CateId != 3) return new result
                {
                    status = 401,
                    statusMessage = $"not found Product tablet"
                };

                var cate = await _context.Categories.FirstOrDefaultAsync( c => c.Id == product.CateId);
                var productDt = await _context.ProductDetails.FirstOrDefaultAsync( pd => pd.Id == product.ProductDetailId );
                if ( cate == null || productDt == null) return new result
                {
                    status = 404,
                    statusMessage = $"not found Product & cate"
                };

                product.ProductName = model.productName==""?product.ProductName:model.productName;
                product.Title = model.title==""?product.Title:model.title;
                product.Thumbnail = model.thumbnail==null?product.Thumbnail:url;
                productDt.ModelNumber = model.modelNumber == ""? productDt.ModelNumber:model.modelNumber;
                productDt.Dies = model.dies == ""? productDt.Dies:model.dies;
                productDt.MaxPressure = model.maxPressure == ""?productDt.MaxPressure:model.maxPressure;
                productDt.MaxDiameterOfTablet = model.maxDiameterOfTablet==""?productDt.MaxDiameterOfTablet:model.maxDiameterOfTablet;
                productDt.MaxDepthOfFill = model.maxDepthOfFill == ""?productDt.MaxDepthOfFill:model.maxDepthOfFill;
                productDt.ProductionCapacity = model.productionCapacity==""?productDt.ProductionCapacity:model.productionCapacity;
                productDt.MachineSize = model.machineSize==""?productDt.MachineSize:model.machineSize;
                productDt.NetWeight = model.netWeight==""?productDt.NetWeight:model.netWeight;

                await _context.SaveChangesAsync();

                return new ProductTabletDTO()
                {
                    Id = product.Id,
                    productName = product.ProductName,
                    title = product.Title,
                    thumbnail = product.Thumbnail,
                    cateId = product.CateId,
                    productDetailId = product.ProductDetailId,
                    cateGet = new CategoryGet
                    {
                        CateName = cate.CateName
                    },
                    TabletP = new Tablet
                    {
                        modelNumber = productDt.ModelNumber,
                        dies = productDt.Dies,
                        maxPressure = productDt.MaxPressure,
                        maxDiameterOfTablet = productDt.MaxDiameterOfTablet,
                        maxDepthOfFill = productDt.MaxDepthOfFill,
                        productionCapacity = productDt.ProductionCapacity,
                        machineSize = productDt.MachineSize,
                        netWeight = productDt.NetWeight,
                    }
                };

            }
            catch (Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }
        }
        public async Task<object> UpdateProductCapsule(ProductCapsule2 model, string url)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.id);

                if (product == null)
                {
                    return new result
                    {
                        status = 401,
                        statusMessage = $"Not found product"
                    };
                }
                if (product.CateId != 1) return new result
                {
                    status = 401,
                    statusMessage = $"not found Product capsule"
                };

                var cate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CateId);
                var productDt = await _context.ProductDetails.FirstOrDefaultAsync(pd => pd.Id == product.ProductDetailId);
                if (cate == null || productDt == null) return new result
                {
                    status = 401,
                    statusMessage = $"not found Product & capsule"
                };

                product.ProductName = model.productName == "" ? product.ProductName : model.productName;
                product.Title = model.title == "" ? product.Title : model.title;
                product.Thumbnail = model.thumbnail == null ? product.Thumbnail : url;

                productDt.Output = model.Output == ""?productDt.Output:model.Output;
                productDt.CapsuleSize = model.CapsuleSize == ""?productDt.CapsuleSize:model.CapsuleSize;
                productDt.MachineDimension = model.MachineDimension == "" ? productDt.MachineDimension : model.MachineDimension;
                productDt.ShippingWeight = model.ShippingWeight == "" ? productDt.ShippingWeight : model.ShippingWeight;

                await _context.SaveChangesAsync();

                return new ProductCapsuleDTO
                {
                    Id = model.id,
                    productName = product.ProductName,
                    title = product.Title,
                    thumbnail = product.Thumbnail,
                    cateId = product.CateId,
                    productDetailId = product.ProductDetailId,
                    cateGet = new CategoryGet
                    {
                        CateName = cate.CateName
                    },
                    CapsuleP = new capsule
                    {
                        Output = productDt.Output,
                        CapsuleSize = productDt.CapsuleSize,
                        MachineDimension = productDt.MachineDimension,
                        ShippingWeight = productDt.ShippingWeight,
                    }
                };



            }
            catch (Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }
        }
        public async Task<object> UpdateProductLiquid(ProductLiquid2 model, string url)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == model.id);

                if (product == null)
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = $"product not found"
                    };
                }

                if (product.CateId != 2) return new result
                {
                    status = 401,
                    statusMessage = $"not found Liquid"
                };

                var cate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == product.CateId);
                var productDt = await _context.ProductDetails.FirstOrDefaultAsync(pd => pd.Id == product.ProductDetailId);
                if (cate == null || productDt == null) return new result
                {
                    status = 401,
                    statusMessage = $"data not found"
                };

                product.ProductName = model.productName == "" ? product.ProductName : model.productName;
                product.Title = model.title == "" ? product.Title : model.title;
                product.Thumbnail = model.thumbnail == null ? product.Thumbnail : url;

                productDt.AirPressure = model.AirPressure == ""?productDt.AirPressure:model.AirPressure;
                productDt.AirVolume = model.AirVolume==""?productDt.AirVolume:model.AirVolume;
                productDt.FillingSpeed = model.FillingSpeed == "" ? productDt.FillingSpeed : model.FillingSpeed;
                productDt.FillingRange = model.FillingRange == "" ? productDt.FillingRange : model.FillingRange;
                await _context.SaveChangesAsync();

                return new ProductLiquidDTO
                {
                    Id = model.id,
                    productName = product.ProductName,
                    title = product.Title,
                    thumbnail = product.Thumbnail,
                    cateId = product.CateId,
                    productDetailId = product.Id,
                    cateGet = new CategoryGet
                    {
                        CateName = cate.CateName
                    },
                    LiquidP = new liquid
                    {
                        AirPressure = productDt.AirPressure,
                        AirVolume = productDt.AirVolume,
                        FillingSpeed = productDt.FillingSpeed,
                        FillingRange = productDt.FillingRange
                    }
                };


            }
            catch(Exception ex)
            {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured {ex}"
                };
            }
        }

        public async Task<result> DeleteProduct(int ProductId)
        {
            try
            {
                var product = await _context.Products.FirstOrDefaultAsync( p => p.Id == ProductId);

                if ( product == null )
                {
                    return new result { status = 404, statusMessage = "not found product" };
                }

                var productDT = await _context.ProductDetails.FirstOrDefaultAsync(p => p.Id == product.ProductDetailId );
                if (productDT == null)
                {
                    return new result
                    {
                        status = 404,
                        statusMessage = "not found productDetail"
                    };
                }

                _context.Products.Remove(product);
                _context.ProductDetails.Remove(productDT);
                await _context.SaveChangesAsync();

                return new result
                {
                    status = 204,
                    statusMessage = "delete successfully"
                };


            }
            catch (Exception ex) {
                return new result
                {
                    status = 401,
                    statusMessage = $"Error! has error occured{ex}"
                };
            }
        }

        // huy xử lý cái này
        public async Task<List<getAll>> searchP(string search, int page, int pagesize = 10)
        {
            List<getAll> new_list = new List<getAll>();
            try
            {
                var products = _context.Products.AsQueryable().Where( p => (p.ProductName.Contains(search) || p.Title.Contains(search)) ).Include(p => p.Cate);

                var result = PaginationList<Entities.Product>.Create(products, page, pagesize > 3 ? pagesize : PAGE_SIZE);

                

                foreach (var product in result)
                {
                    if (product.IsAtive == true)
                    {
                        new_list.Add(new getAll()
                        {
                            Id = product.Id,
                            productName = product.ProductName,
                            title = product.Title,
                            thumbnail = product.Thumbnail,
                            cateGet = new CategoryGet
                            {
                                CateName = product.Cate.CateName
                            }
                        });
                    }
                }

                return new_list;


            }
            catch (Exception ex) {
                return new_list;
            }
        }
        

        public async Task<List<getAll>> sortSearchFilterPagin(string? isNewest, string? search, int? cate, string? sorting, int page, int pagesize = 10)
        {
             List<getAll> new_list = new List<getAll>();
            try
            {
                var products = _context.Products.AsQueryable();
                string isNew = "AVAILABLE";
                

                if (!string.IsNullOrEmpty(sorting))
                {
                    switch (sorting)
                    {
                        case "NAME_ASC": products = products.OrderBy(p => p.ProductName); break;
                        case "NAME_DESC": products = products.OrderByDescending(p => p.ProductName); break;
                        default: break;
                    }
                }

       
                if (!string.IsNullOrEmpty(isNewest))
                {
                    switch (isNewest)
                    {
                        case "NEW": 
                            products = products.Where( p => EF.Functions.DateDiffDay(p.CreatedAt, DateTime.Now) <=2 );
                            isNew = "NEW";
                            break;
                        case "AVAILABLE":
                            products = products.Where(p => EF.Functions.DateDiffDay(p.CreatedAt, DateTime.Now) > 2);
                            break; 
                        default: break;
                    }
                }

                if ( cate.HasValue )
                {
                    products = products.Where(p => p.CateId == cate);
                }


                if (!string.IsNullOrEmpty(search))
                {
                    products = products.Where(p => (p.ProductName.Contains(search) || p.Title.Contains(search)));
                }

                products = products.Include(p => p.Cate);

                var result = PaginationList<Entities.Product>.Create(products, page, pagesize > 3 ? pagesize : PAGE_SIZE);


                foreach (var product in result)
                {
                    
                    if (product.IsAtive == true)
                    {
                        if (product.CreatedAt != null)
                        {
                            DateTime createAt = (DateTime)(product.CreatedAt);
                            isNew = createAt.AddDays(2) > DateTime.Now ? "NEW" : "AVAILABLE";
                        }
                        new_list.Add(new getAll()
                            {
                                Id = product.Id,
                                productName = product.ProductName,
                                title = product.Title,
                                thumbnail = product.Thumbnail,
                                cateGet = new CategoryGet
                                {
                                    CateName = product.Cate.CateName
                                },
                                isNew = isNew
                            });

                    }
                }

                return new_list;


            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }


        public async Task<List<CategoryGet2>> getAllCategories()
        {
            List<CategoryGet2> listC = new List<CategoryGet2>();
            try
            {
                var cates = await _context.Categories.ToListAsync();

                foreach (var cate in cates)
                {
                    listC.Add(new CategoryGet2()
                    {
                        Id = cate.Id,
                        CateName = cate.CateName
                    });
                }
                return listC;

            }
            catch(Exception ex)
            {
                return listC;
            }
        }

    }
}
