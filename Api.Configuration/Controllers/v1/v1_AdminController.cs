using Api.Configuration.Repository;
using Api.Configuration.Service;
using Api.Master.Controllers;
using Entities.Api;
using Entities.Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Api.Configuration.Controllers
{
    [ApiController]
    public class AdminV1Controller : BaseController
    {
        public AdminRepository repository = new AdminRepository();

        public AdminV1Controller(IConfiguration _configuration)
        {        
            this.configuration = _configuration;
        }

        [HttpPost("api/v1/admin/createAccount")]
        public ActionResult<string> CreateAccount([FromBody] NewUserData newUser)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCreateAccountV1(repository);

                    if (!service.Exec(db, newUser))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/authenticate")]
        public ActionResult<string> Authenticate([FromBody] LoginInformation login)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminAuthenticateV1(repository);
                    var ua = new AuthenticatedUser();

                    if (!service.Exec(db, login, ref ua))
                        return BadRequest(service.Error);

                    return Ok(ua);
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/createCategory")]
        public ActionResult<string> CreateCategory([FromBody] NewCategoryData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCreateCategoryV1(repository);

                    if (!service.Exec(db, obj))
                        return BadRequest(service.Error);

                    return Ok(new { Id = service.IdCreated });
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/editCategory")]
        public ActionResult<string> EditCategory([FromBody] NewCategoryData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminEditCategoryV1(repository);

                    if (!service.Exec(db, obj))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/categories")]
        public ActionResult<string> Categories(int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCategoriesV1(repository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/category/{id}")]
        public ActionResult<string> Category(long id)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCategoryV1(repository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), id);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/createSubCategory")]
        public ActionResult<string> CreateSubCategory([FromBody] NewSubCategoryData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCreateSubCategoryV1(repository);

                    if (!service.Exec(db, obj))
                        return BadRequest(service.Error);

                    return Ok(new { Id = service.IdCreated });
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/subcategories")]
        public ActionResult<string> SubCategories(long categID, int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminSubCategoriesV1(repository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), categID, skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/editsubCategory")]
        public ActionResult<string> EditSubCategory([FromBody] NewSubCategoryData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminEditSubCategoryV1(repository);

                    if (!service.Exec(db, obj))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/subcategory/{id}")]
        public ActionResult<string> SubCategory(long id)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminSubCategoryV1(repository);
                    var resp = service.Exec(db, GetCurrentAuthenticatedUser(), id);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/createproduct")]
        public ActionResult<string> CreateProduct([FromBody] NewProductData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminCreateProductV1(repository);

                    if (!service.Exec(db, GetCurrentAuthenticatedUser(), obj))
                        return BadRequest(service.Error);

                    return Ok(new { Id = service.IdCreated });
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpPost("api/v1/admin/editProduct")]
        public ActionResult<string> EditProduct([FromBody] NewProductData obj)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminEditProductV1(repository);
                    
                    if (!service.Exec(db, GetCurrentAuthenticatedUser(), obj))
                        return BadRequest(service.Error);

                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/product/{id}")]
        public ActionResult<string> Product(long id)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminProductV1(repository);
                    var resp = service.Exec(db, id);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }

        [HttpGet("api/v1/admin/products")]
        public ActionResult<string> Products(long categID, long subcategID, int skip, int take)
        {
            try
            {
                using (var db = new SqlConnection(GetDBConnectionString()))
                {
                    var service = new AdminProductsV1(repository);
                    var resp = service.Exec(db, categID, subcategID, skip, take);

                    return Ok(JsonConvert.SerializeObject(resp));
                }
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ServiceError { DebugInfo = ex.ToString(), Message = _defaultError });
            }
        }
    }
}
